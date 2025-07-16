// Models/LoginRequest.cs
using System.ComponentModel.DataAnnotations;

namespace QuizApi.Models
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Kullanıcı adı zorunludur.")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Parola zorunludur.")]
        public string Password { get; set; } = null!;
    }
}
