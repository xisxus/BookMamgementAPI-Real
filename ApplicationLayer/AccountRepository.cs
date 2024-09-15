using DataAccessLayer.Contact;
using DataAccessLayer.DTOs;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using static DataAccessLayer.DTOs.ServiceResponse;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class AccountRepository : IUserAccount
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly IConfiguration config;

    public AccountRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
        this.config = config;
    }

    public async Task<ServiceResponse.GeneralResponse> AssignRoleToUser(string userEmail, string roleName)
    {
        var user = await userManager.FindByEmailAsync(userEmail);
        if (user == null) return new GeneralResponse(false, "User not found");

        if (!await roleManager.RoleExistsAsync(roleName)) return new GeneralResponse(false, "Role does not exist");

        var result = await userManager.AddToRoleAsync(user, roleName);
        if (!result.Succeeded) return new GeneralResponse(false, "Error assigning role");

        return new GeneralResponse(true, "Role assigned to user successfully");
    }

    public async Task<ServiceResponse.GeneralResponse> CreateAccount(UserDTO userDTO)
    {
        if (userDTO == null) return new GeneralResponse(false, "Model is empty");
        var newUser = new ApplicationUser()
        {
            FullName = userDTO.Name,
            Email = userDTO.Email,
            PasswordHash = userDTO.Password,
            UserName = userDTO.Email
        };
        var user = await userManager.FindByNameAsync(newUser.Email);

        if (user is not null) return new GeneralResponse(false, "User Already Registered");
        var createuser = await userManager.CreateAsync(newUser!, userDTO.Password);
        if (!createuser.Succeeded) return new GeneralResponse(false, "Error Occured");
        var checkAdmin = await roleManager.FindByNameAsync("Admin");
        if (checkAdmin is null)
        {
            await roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
            await userManager.AddToRoleAsync(newUser, "Admin");
            return new GeneralResponse(true, "Account created");
        }
        else
        {
            var checkUser = await roleManager.FindByNameAsync("User");
            if (checkUser is null)

                await roleManager.CreateAsync(new IdentityRole() { Name = "User" });
            await userManager.AddToRoleAsync(newUser, "User");
            return new GeneralResponse(true, "Account created");

        }
    }

    public async Task<ServiceResponse.GeneralResponse> CreateRole(string roleName)
    {
        if (string.IsNullOrEmpty(roleName)) return new GeneralResponse(false, "Role name is required");

        if (await roleManager.RoleExistsAsync(roleName)) return new GeneralResponse(false, "Role already exists");

        var roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
        if (!roleResult.Succeeded) return new GeneralResponse(false, "Error creating role");

        return new GeneralResponse(true, "Role created successfully");
    }

    public async Task<ServiceResponse.GeneralResponseData<List<UserWithRolesDTO>>> GetAllUsersWithRoles()
    {
        var users = userManager.Users.ToList();
        var userRolesList = new List<UserWithRolesDTO>();

        foreach (var user in users)
        {
            var roles = await userManager.GetRolesAsync(user);
            userRolesList.Add(new UserWithRolesDTO
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Roles = roles.ToList()
            });
        }

        if (!userRolesList.Any())
            return new GeneralResponseData<List<UserWithRolesDTO>>(false, "No users found", null);

        return new GeneralResponseData<List<UserWithRolesDTO>>(true, "Users retrieved successfully", userRolesList);
    }

    public async Task<ServiceResponse.GeneralResponseData<List<string>>> GetUserRoles(string userEmail)
    {
        var user = await userManager.FindByEmailAsync(userEmail);
        if (user == null) return new GeneralResponseData<List<string>>(false, "User not found", null);

        var roles = await userManager.GetRolesAsync(user);
        if (!roles.Any()) return new GeneralResponseData<List<string>>(false, "No roles assigned to the user", null);

        return new GeneralResponseData<List<string>>(true, "Roles retrieved successfully", roles.ToList());
    }

    public async Task<ServiceResponse.LoginResponse> LoginAccount(LoginDTO loginDTO)
    {
        if (loginDTO == null) return new LoginResponse(false, null!, "Login container is empty");
        var getUser = await userManager.FindByEmailAsync(loginDTO.Email);

        if (getUser is null) return new LoginResponse(false, null!, "User not Found");
        bool checkPassword = await userManager.CheckPasswordAsync(getUser, loginDTO.Password);

        if (!checkPassword) return new LoginResponse(false, null!, "Invalid username/password");
        var getUserRole = await userManager.GetRolesAsync(getUser);
        var userSession = new UserSession(getUser.Id, getUser.FullName, getUser.Email, getUserRole.First());

        string token = GenerateJWTToken(userSession);
        return new LoginResponse(true, token, "User login Successfull");
    }

    private string GenerateJWTToken(UserSession user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var userClaims = new[]
        {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Name,user.Name),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Role,user.Role),
            };
        var token = new JwtSecurityToken(
            issuer: config["JWT:Issuer"],
            audience: config["JWT:Audience"],
            claims: userClaims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}