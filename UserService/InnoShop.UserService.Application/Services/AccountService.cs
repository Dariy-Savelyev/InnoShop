using AutoMapper;
using InnoShop.UserService.Application.Models;
using InnoShop.UserService.Application.ServiceInterfaces;
using InnoShop.UserService.Domain.Models;
using InnoShop.UserService.Domain.RepositoryInterfaces;

namespace InnoShop.UserService.Application.Services;

public class AccountService(IUserRepository userRepository, IMapper mapper) : IAccountService
{
    public async Task RegistrationAsync(RegistrationModel model)
    {
        var user = mapper.Map<User>(model);

        await userRepository.AddAsync(user);
    }

    public Task<string> LoginAsync(LoginModel model)
    {
        throw new NotImplementedException();
    }

    /*public async Task<string> LoginAsync(LoginModel model)
    {
        /*var email = model.Email.Trim();
        var user = await userManager.FindByEmailAsync(email) ?? await userManager.FindByNameAsync(email);

        if (user == null)
        {
            throw ExceptionHelper.GetArgumentException(nameof(LoginModel), "Incorrect credentials");
        }

        var signInResult = await signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: true);
        if (signInResult.Succeeded)
        {
            return await tokenComponent.RefreshTokenAsync(user);
        }

        if (signInResult.IsLockedOut)
        {
            throw ExceptionHelper.GetForbiddenException("Your account has been locked for 20 minutes");
        }

        throw ExceptionHelper.GetArgumentException(nameof(LoginModel), "Incorrect credentials");
    }*/
}