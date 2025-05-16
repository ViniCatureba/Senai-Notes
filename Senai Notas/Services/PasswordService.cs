using System.Security.Cryptography;
using System.Text;

namespace Senai_Notas.Services
{
    public class PasswordService
    {

        /// Gera um hash SHA256 para a senha informada.
       
        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        /// <summary>
        /// Compara uma senha em texto puro com um hash armazenado.
        /// </summary>
        public bool VerifyPassword(string password, string hashedPassword)
        {
            var hashOfInput = HashPassword(password);
            return hashOfInput == hashedPassword;
        }
    }
}
