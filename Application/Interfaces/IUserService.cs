using System.Threading.Tasks;
using Domain.DTOs;
using Domain.Entities;  

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<User> RegisterAsync(RegisterDto registerDto);
        Task<User> AuthenticateAsync(LoginDto loginDto);
        Task<User> UpdateAvatarAsync(Guid userId, string avatarRelativePath);


        /// <summary>
        /// Обновляет данные аккаунта пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="updateDto">DTO с новыми данными аккаунта.</param>
        /// <returns>Обновленный объект пользователя.</returns>
        Task<User> UpdateAccountAsync(System.Guid userId, UpdateUserAccountDto updateDto);
    }
}
