using BCrypt.Net;

namespace MDW_Back_ops.Helpers
{
    public static class PasswordHasher
    {
        public static string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);

        public static bool VerifyPassword(string password, string hash) => BCrypt.Net.BCrypt.Verify(password, hash);
    }
}
