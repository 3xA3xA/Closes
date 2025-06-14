using System.Threading.Tasks;
using Domain.DTOs;
using Domain.Entities;  

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<User> RegisterAsync(RegisterDto registerDto);
        Task<User> AuthenticateAsync(LoginDto loginDto);
    }
}
