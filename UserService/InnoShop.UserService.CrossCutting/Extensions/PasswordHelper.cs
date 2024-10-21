using System.Security.Cryptography;
using System.Text;

namespace InnoShop.UserService.CrossCutting.Extensions;

public class PasswordHelper
{
    public static string HashPassword(string password)
    {
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var hashedBytes = SHA256.HashData(passwordBytes);

        return Convert.ToBase64String(hashedBytes);
    }

    public static string GeneratePasswordRecoveryCode()
    {
        using var randomNumberGenerator = RandomNumberGenerator.Create();
        var bytes = new byte[4];
        randomNumberGenerator.GetBytes(bytes);

        var value = BitConverter.ToInt32(bytes, 0);
        value = Math.Abs(value);
        value = (value % 90000000) + 10000000;

        return value.ToString("D8");
    }
}