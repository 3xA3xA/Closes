using API.Middlewares;
using Application.Interfaces;
using Application.Services;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("ReactPolicy", policyBuilder =>
                {
                    policyBuilder
                        .WithOrigins("http://localhost:5173") // домен вашего React-приложения
                        .AllowAnyHeader()
                        .AllowAnyMethod(); // разрешаем все методы (GET, POST, OPTIONS и т.д.)
                });
            });

            // Подключаем строку подключения к базе данных
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Регистрация сервисов приложения
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IGroupService, GroupService>();
            builder.Services.AddScoped<IWishlistService, WishlistService>();


            // Регистрируем контроллеры
            builder.Services.AddControllers();

            // Регистрация Swagger для документации API
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Closes API",
                    Version = "v1",
                    Description = "Документация к API мобильного веб-приложения."
                });
            });

            // Настраиваем JWT-аутентификацию
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    // Получаем необходимые данные из конфигурации
                    var jwtKey = builder.Configuration["Jwt:Key"];
                    if (string.IsNullOrWhiteSpace(jwtKey))
                    {
                        throw new Exception("JWT Key не задан в конфигурации. Проверьте appsettings.json.");
                    }

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                        ClockSkew = TimeSpan.Zero  // Можно настроить, чтобы токен не имел дополнительного времени "отклонения"
                    };
                });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Closes API v1");
                });
            }

            app.UseCors("ReactPolicy");

            app.UseHttpsRedirection();

            // Обязательно подключаем аутентификацию и авторизацию
            app.UseGlobalExceptionMiddleware();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
