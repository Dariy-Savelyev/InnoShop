namespace InnoShop.UserService.CrossCutting.Constants;

public static class UserRoles
{
    public const string Administrator = nameof(Administrator);
    public const string User = nameof(User);
    public const string Bot = nameof(Bot);

    public static readonly IReadOnlyCollection<string> UserRoleList =
    [
        Administrator,
        User,
        Bot,
    ];
}