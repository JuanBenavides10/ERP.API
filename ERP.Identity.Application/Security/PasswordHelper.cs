namespace ERP.Identity.Application.Security
{
    public class PasswordHelper
    {
        public static void create_password_hash(string password, out byte[] hash, out byte[] salt)
        {
            using (var hmac = new System.Security.Cryptography.Rfc2898DeriveBytes(password, 16, 10000))
            {
                salt = hmac.Salt;
                hash = hmac.GetBytes(32);
            }
        }

        public static bool verify_password(string password, byte[] hash, byte[] salt)
        {
            using (var hmac = new System.Security.Cryptography.Rfc2898DeriveBytes(password, salt, 10000))
            {
                var computedHash = hmac.GetBytes(32);
                return computedHash.SequenceEqual(hash);
            }
        }
    }
}
