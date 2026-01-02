using Business_Logic_Layer.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Business_Logic_Layer.DTOS.AuthDTO;

namespace Business_Logic_Layer.Interface
{
   public interface IUserService
    {
        Task<AuthResponseDTO> RegisterAsync(RegisterDTO registerDto);
        Task<AuthResponseDTO> LoginAsync(LoginDTO loginDto);
        Task<UserDTO> GetUserByIdAsync(string userId);
        Task<bool> UpdateUserAsync(string userId, UpdatUserDTO updateDto);
        Task<bool> ChangePasswordAsync(string userId, ChangePasswordDTO changePasswordDto);
        Task<object> GetUserProfileAsync(string userId);
        Task<List<UserDTO>> GetAllUsersAsync();
        Task<bool> DeleteUserAsync(string userId);
    }
}

