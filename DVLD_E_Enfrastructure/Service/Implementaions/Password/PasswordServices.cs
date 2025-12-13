using DVLD_Application.Services.Interfaces.Password;
using System;
using System.Security.Cryptography;
using System.Text;

namespace DVLD_E_Enfrastructure.Service.Implementaions.Password
{
    public class PasswordServices : IPasswordService
    {
        private const int SaltSize = 16; 
        private const int KeySize = 32;  
        private const int Iterations = 100_000;

        public string HashPassword(string password)
        {
            if (password is null) throw new ArgumentNullException(nameof(password));

            var salt = new byte[SaltSize];
            RandomNumberGenerator.Fill(salt);

            using var deriveBytes = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            var key = deriveBytes.GetBytes(KeySize);

            var iterationsPart = Iterations.ToString();
            var saltPart = Convert.ToBase64String(salt);
            var keyPart = Convert.ToBase64String(key);

            return string.Create(iterationsPart.Length + 1 + saltPart.Length + 1 + keyPart.Length, (iterationsPart, saltPart, keyPart),
                (span, state) =>
                {
                    var (it, s, k) = state;
                    var idx = 0;
                    it.AsSpan().CopyTo(span[idx..]); idx += it.Length;
                    span[idx++] = '.';
                    s.AsSpan().CopyTo(span[idx..]); idx += s.Length;
                    span[idx++] = '.';
                    k.AsSpan().CopyTo(span[idx..]);
                });
        }

        public bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            if (hashedPassword is null) throw new ArgumentNullException(nameof(hashedPassword));
            if (providedPassword is null) throw new ArgumentNullException(nameof(providedPassword));

            var parts = hashedPassword.Split('.', 3);
            if (parts.Length != 3) return false;

            if (!int.TryParse(parts[0], out var iterations)) return false;

            byte[] salt;
            byte[] key;
            try
            {
                salt = Convert.FromBase64String(parts[1]);
                key = Convert.FromBase64String(parts[2]);
            }
            catch (FormatException)
            {
                return false;
            }
            using var deriveBytes = new Rfc2898DeriveBytes(providedPassword, salt, iterations, HashAlgorithmName.SHA256);
            var derivedKey = deriveBytes.GetBytes(key.Length);

            return CryptographicOperations.FixedTimeEquals(derivedKey, key);
        }
    }
}
