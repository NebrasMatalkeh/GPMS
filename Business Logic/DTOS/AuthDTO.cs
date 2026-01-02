using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.DTOS
{
    public class AuthDTO
    {
        public class LoginDTO
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class RegisterDTO
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string Role { get; set; }
            public float? GPA { get; set; }
            public string Skills { get; set; }
            public string Interests { get; set; }
            public string ResearchInterests { get; set; }
            public string Department { get; set; }
        }

        public class AuthResponseDTO
        {
            public string Token { get; set; }
            public string UserId { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Role { get; set; }
            public DateTime ExpiresAt { get; set; }
        }
        public class ChangePasswordDTO
        {
            public string OldPassword { get; set; }
            public string ConfirmPassword { get; set; }
            public string NewPassword { get; set; }
        }
        public class UpdatUserDTO
        {
            public string Name { get; set; }
            public string Email { get; set; }
            

        }
        
            
        
    }
}
