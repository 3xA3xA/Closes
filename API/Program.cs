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
                        .WithOrigins("http://localhost:5173") // ����� ������ React-����������
                        .AllowAnyHeader()
                        .AllowAnyMethod(); // ��������� ��� ������ (GET, POST, OPTIONS � �.�.)
                });
            });

            // ���������� ������ ����������� � ���� ������
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // ����������� �������� ����������
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IGroupService, GroupService>();
            builder.Services.AddScoped<IWishlistService, WishlistService>();


            // ������������ �����������
            builder.Services.AddControllers();

            // ����������� Swagger ��� ������������ API
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Closes API",
                    Version = "v1",
                    Description = "������������ � API ���������� ���-����������."
                });
            });

            // ����������� JWT-��������������
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    // �������� ����������� ������ �� ������������
                    var jwtKey = builder.Configuration["Jwt:Key"];
                    if (string.IsNullOrWhiteSpace(jwtKey))
                    {
                        throw new Exception("JWT Key �� ����� � ������������. ��������� appsettings.json.");
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
                        ClockSkew = TimeSpan.Zero  // ����� ���������, ����� ����� �� ���� ��������������� ������� "����������"
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

            // ����������� ���������� �������������� � �����������
            app.UseGlobalExceptionMiddleware();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
