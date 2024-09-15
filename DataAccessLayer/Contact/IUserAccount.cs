using DataAccessLayer.DTOs;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataAccessLayer.DTOs.ServiceResponse;

namespace DataAccessLayer.Contact
{
    public interface IUserAccount
    {
        Task<GeneralResponse> CreateAccount(UserDTO userDTO);
        Task<LoginResponse> LoginAccount(LoginDTO loginDTO);

        Task<ServiceResponse.GeneralResponse> CreateRole(string roleName);
        Task<ServiceResponse.GeneralResponse> AssignRoleToUser(string userEmail, string roleName);
        Task<ServiceResponse.GeneralResponseData<List<string>>> GetUserRoles(string userEmail);

        Task<ServiceResponse.GeneralResponseData<List<UserWithRolesDTO>>> GetAllUsersWithRoles();

    }
}
