namespace QuizApi.Models
{
    public class AnswerRequest
    {
        public int QuestionId { get; set; } // Cevaplanan soru ID'si
        public string SelectedOption { get; set; } = null!;    // Kullanıcının seçtiği şık
        public string? Username { get; set; }          // Kullanıcı adı (opsiyonel skor takibi için)

        public DateTime? SentAt { get; set; }          // Nullable yaparak opsiyonel yaptık

    }
}
