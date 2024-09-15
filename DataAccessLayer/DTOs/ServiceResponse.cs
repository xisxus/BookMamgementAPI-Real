using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class ServiceResponse
    {
        public record GeneralResponse(bool Flag, string Message);
        public record GeneralResponseSingle(bool Flag, string Message , object? Data = null);
        public record GeneralResponseData<T>(bool Flag, string Message, T? Data = default);
        public record LoginResponse(bool Flag, string Token, string Message);
        public record UserSession(string? Id, string? Name, string? Email, string? Role);



        
    }
}
