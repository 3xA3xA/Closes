using Application.Interfaces;
using Infrastructure.Data;
using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _dbContext;

        public UserService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> RegisterAsync(RegisterDto registerDto)
        {
            // Проверяем, существует ли уже пользователь с таким email
            if (await _dbContext.Users.AnyAsync(u => u.Email == registerDto.Email))
                throw new Exception("Пользователь с таким email уже существует.");

            // Хеширование пароля. Здесь используется библиотека BCrypt.Net-Next, установите через NuGet пакет BCrypt.Net-Next
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

            var user = new User
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                PasswordHash = hashedPassword
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> AuthenticateAsync(LoginDto loginDto)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
                throw new Exception("Неверные учетные данные.");
            return user;
        }

        public async Task<User> UpdateAccountAsync(Guid userId, UpdateUserAccountDto updateDto)
        {
            // Находим пользователя по его идентификатору
            var user = await _dbContext.Users.FindAsync(userId);
            if (user == null)
                throw new Exception("Пользователь не найден.");

            // Обновление email, если пришёл новый и он отличается от текущего
            if (!string.IsNullOrWhiteSpace(updateDto.Email) && updateDto.Email != user.Email)
            {
                // Проверка уникальности email
                bool exists = await _dbContext.Users.AnyAsync(u => u.Email == updateDto.Email && u.Id != userId);
                if (exists)
                    throw new Exception("Пользователь с таким email уже существует.");

                user.Email = updateDto.Email;
            }

            // Обновление имени, если пришло новое значение
            if (!string.IsNullOrWhiteSpace(updateDto.Name) && updateDto.Name != user.Name)
            {
                user.Name = updateDto.Name;
            }

            // Обновление пароля, если пришёл новый пароль
            if (!string.IsNullOrEmpty(updateDto.NewPassword))
            {
                // Необходимо, чтобы пришёл и старый пароль для проверки
                if (string.IsNullOrEmpty(updateDto.OldPassword))
                    throw new Exception("Старый пароль обязателен для смены пароля.");

                // Проверка корректности старого пароля
                if (!BCrypt.Net.BCrypt.Verify(updateDto.OldPassword, user.PasswordHash))
                    throw new Exception("Неверный старый пароль.");

                // Если новый пароль отличается, обновляем его
                // (так как новый пароль в любом случае хешируется по новой, дополнительная проверка не обязательна)
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(updateDto.NewPassword);
            }

            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateAvatarAsync(Guid userId, string avatarRelativePath)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user == null)
                throw new Exception("Пользователь не найден.");

            user.AvatarUrl = avatarRelativePath;
            await _dbContext.SaveChangesAsync();
            return user;
        }
    }
}
