using Business_Logic_Layer.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Business_Logic_Layer.DTOS.AuthDTO;

namespace Business_Logic_Layer.Interface
{
   public interface IAuthService
    {

        Task<AuthResponseDTO> RegisterAsync(RegisterDTO registerDto);
        Task<AuthResponseDTO> LoginAsync(LoginDTO loginDto);
        Task<bool> ChangePasswordAsync(string userId, string oldPassword, string newPassword);
        Task<bool> ResetPasswordAsync(string email);
        Task<UserDTO> GetUserProfileAsync(string userId);
        Task<bool> UpdateUserProfileAsync(string userId, object profileUpdates);
    }
}

