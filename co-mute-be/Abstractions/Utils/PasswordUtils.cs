using System.Text;

namespace co_mute_be.Abstractions.Utils
{
    public class PasswordUtils
    {
        public static string HashPassword(string password)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(password)); // Super secret hash + salt algo here...
        }

        public static string UnHashPassword(string passwordHash)
        {
            // Reverse super secret hash + salt algo here...
            var data = Convert.FromBase64String(passwordHash);
            return Encoding.UTF8.GetString(data);
        }
    }
}
