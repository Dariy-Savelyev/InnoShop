using AutoMapper;
using InnoShop.UserService.Application.Models;
using InnoShop.UserService.Application.ServiceInterfaces;
using InnoShop.UserService.CrossCutting.Extensions;
using InnoShop.UserService.Domain.Models;
using InnoShop.UserService.Domain.RepositoryInterfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InnoShop.UserService.Application.Services;

public class AccountService(IUserRepository userRepository, IMapper mapper, IConfiguration configuration) : IAccountService
{
    public async Task<IEnumerable<GetAllUserModel>> GetAllUsersAsync()
    {
        var usersDb = await userRepository.GetAllAsync();

        var users = mapper.Map<IEnumerable<GetAllUserModel>>(usersDb);

        return users;
    }

    public async Task RegisterAsync(UserRegistrationModel model)
    {
        var user = mapper.Map<User>(model);

        await userRepository.AddAsync(user);
    }

    public async Task<string> LoginAsync(UserLoginModel model)
    {
        var user = await userRepository.GetUserByEmailAsync(model.Email);
        var passwordHash = PasswordHasher.HashPassword(model.Password);

        if (user == null || passwordHash != user.PasswordHash)
        {
            ExceptionHelper.ThrowUnauthorizedException("Incorrect credentials.");
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]!);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.Name, user!.Id),
                new(ClaimTypes.Role, user.Role)
            }),
            Expires = DateTime.UtcNow.AddDays(double.Parse(configuration["Jwt:TokenExpirationDays"]!)),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = configuration["Jwt:Issuer"],
            Audience = configuration["Jwt:Audience"]
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public async Task EditUserAsync(UserModificationModel model)
    {
        var userDb = await userRepository.GetByIdAsync(model.Id);

        if (userDb == null)
        {
            throw ExceptionHelper.GetNotFoundException("User not found.");
        }

        userDb.UserName = model.UserName;

        userDb.Email = model.Email;

        await userRepository.ModifyAsync(userDb);
    }

    public async Task DeleteUserAsync(UserDeletionModel model)
    {
        var userDb = await userRepository.GetByIdAsync(model.Id);

        if (userDb == null)
        {
            throw ExceptionHelper.GetNotFoundException("User not found.");
        }

        await userRepository.DeleteAsync(userDb);
    }
}