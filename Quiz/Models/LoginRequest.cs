namespace QuizApi.Models
{
    /// <summary>
    /// Kullanıcı giriş bilgilerini içeren model.
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// Kullanıcı adı (örnek: admin)
        /// </summary>
        public string Username { get; set; } = null!;

        /// <summary>
        /// Parola (örnek: admin123)
        /// </summary>
        public string Password { get; set; } = null!;
    }
}
