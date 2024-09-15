
using DataAccessLayer.Contact;
using DataAccessLayer.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IUserAccount userAccount) : ControllerBase
    {
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserDTO userDTO)
        {
            var response = await userAccount.CreateAccount(userDTO);
            return Ok(response);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var response = await userAccount.LoginAccount(loginDTO);
            return Ok(response);
        }

        // GET: api/UserAccount/UsersWithRoles
        [HttpGet("UsersWithRoles")]
        public async Task<IActionResult> GetAllUsersWithRoles()
        {
            var response = await userAccount.GetAllUsersWithRoles();
            if (!response.Flag)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }

        // POST: api/UserAccount/CreateRole
        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole([FromBody] string roleName)
        {
            var response = await userAccount.CreateRole(roleName);
            if (!response.Flag)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Message);
        }

        // POST: api/UserAccount/AssignRoleToUser
        [HttpPost("AssignRoleToUser")]
        public async Task<IActionResult> AssignRoleToUser([FromBody] AssignRoleDTO assignRoleDTO)
        {
            var response = await userAccount.AssignRoleToUser(assignRoleDTO.UserEmail, assignRoleDTO.RoleName);
            if (!response.Flag)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Message);
        }

        // GET: api/UserAccount/GetUserRoles?userEmail={userEmail}
        [HttpGet("GetUserRoles")]
        public async Task<IActionResult> GetUserRoles([FromQuery] string userEmail)
        {
            var response = await userAccount.GetUserRoles(userEmail);
            if (!response.Flag)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }
    }
}
