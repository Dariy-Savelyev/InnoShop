using AutoMapper;
using InnoShop.UserService.Application.Models;
using InnoShop.UserService.Application.ServiceInterfaces;
using InnoShop.UserService.CrossCutting.Extensions;
using InnoShop.UserService.Domain.Models;
using InnoShop.UserService.Domain.RepositoryInterfaces;

namespace InnoShop.UserService.Application.Services;

public class AccountService(IUserRepository userRepository, IMapper mapper) : IAccountService
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

    public async Task<bool> LoginAsync(UserLoginModel model)
    {
        var user = await userRepository.GetUserByEmailAsync(model.Email);

        var passwordHash = PasswordHasher.HashPassword(model.Password);

        return user != null && passwordHash == user.PasswordHash;
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

    /*
     public async Task<RefreshTokenModel> LoginAsync(LoginModel model)
       {
       var user = await userManager.FindByEmailAsync(model.Email) ?? await FindByEmailAsync(model.Email);
       var signInResult = await signInManager.CheckPasswordSignInAsync(user!, model.Password, lockoutOnFailure: true);
       if (signInResult.Succeeded)
       {
       return await tokenComponent.RefreshTokenAsync(user!);
       }
       if (signInResult.IsLockedOut)
       {
       throw ExceptionHelper.GetForbiddenException("Your account has been locked for 20 minutes");
       }
       throw ExceptionHelper.GetArgumentException(nameof(LoginModel), "Incorrect credentials");
       }
       private async Task<User?> FindByEmailAsync(string email)
       {
       return await userManager.Users.IgnoreQueryFilters()
       .Where(x => x.NormalizedEmail == userManager.NormalizeEmail(email))
       .SingleOrDefaultAsync();
       }
       public async Task<RefreshTokenModel> LoginAsync(LoginModel model)
       {
       var email = model.Email.Trim();
       var user = await userManager.FindByEmailAsync(email) ?? await userManager.FindByNameAsync(email);
       if (user == null)
       {
       throw ExceptionHelper.GetArgumentException(nameof(LoginModel), "Incorrect credentials");
       }
       var signInResult = await signInManager.CheckPasswordSignInAsync(user!, model.Password, lockoutOnFailure: true);
       if (signInResult.Succeeded)
       {
       return await tokenComponent.RefreshTokenAsync(user!);
       }
       if (signInResult.IsLockedOut)
       {
       throw ExceptionHelper.GetForbiddenException("Your account has been locked for 20 minutes");
       }
       throw ExceptionHelper.GetArgumentException(nameof(LoginModel), "Incorrect credentials");
       }
     */
}