using QuizApi.Models;

namespace QuizApi.Services
{
    public class QuestionService
    {
        private List<Question> _questions;

        public QuestionService()
        {
            _questions = new List<Question>
            {
                new Question
                {
                    Id = 1,
                    QuestionText = "Türkiye'nin başkenti neresidir?",
                    Options = new List<string> { "Ankara", "İstanbul", "İzmir", "Bursa" },
                    CorrectAnswer = "Ankara"
                },
                new Question
                {
                    Id = 2,
                    QuestionText = "Aşağıdaki hayvanlardan hangisi memeli değildir?",
                    Options = new List<string> { "Balina", "Yarasa", "Penguen", "Köpek" },
                    CorrectAnswer = "Penguen"
                },
                new Question
                {
                    Id = 3,
                    QuestionText = "1 kilometre kaç metredir?",
                    Options = new List<string> { "10", "100", "1000", "10000" },
                    CorrectAnswer = "1000"
                },
                new Question
                {
                    Id = 4,
                    QuestionText = ".NET Core'da Dependency Injection (Bağımlılık Enjeksiyonu) ne işe yarar?",
                    Options = new List<string> { "Bellek yönetimi", "Veritabanı işlemleri", "Nesnelerin bağımlılıklarının yönetilmesi", "Thread oluşturma" },
                    CorrectAnswer = "Nesnelerin bağımlılıklarının yönetilmesi"
                },
                new Question
                {
                    Id = 5,
                    QuestionText = "HTTP protokolünde 404 hata kodu ne anlama gelir?",
                    Options = new List<string> { "Sunucu hatası", "İstek başarılı", "İstenen kaynak bulunamadı", "Yönlendirme yapıldı" },
                    CorrectAnswer = "İstenen kaynak bulunamadı"
                },
                new Question
                {
                    Id = 6,
                    QuestionText = "C# dilinde async ve await anahtar kelimeleri ne için kullanılır?",
                    Options = new List<string> { "Eşzamanlı işlem yapmak için", "Hata yakalamak için", "Asenkron programlama yapmak için", "Veri tipini belirtmek için" },
                    CorrectAnswer = "Asenkron programlama yapmak için"
                },
                new Question
                {
                    Id = 7,
                    QuestionText = "RESTful API tasarımında 'PUT' HTTP metodu genellikle ne için kullanılır?",
                    Options = new List<string> { "Veri oluşturmak için", "Veri silmek için", "Veriyi tamamen güncellemek için", "Veri almak için" },
                    CorrectAnswer = "Veriyi tamamen güncellemek için"
                }
            };
        }

        public List<Question> GetAllQuestions() => _questions;

        public Question GetRandomQuestion()
        {
            var random = new Random();
            int index = random.Next(_questions.Count);
            return _questions[index];
        }

        public bool CheckAnswer(int questionId, string selectedOption)
        {
            var question = _questions.FirstOrDefault(q => q.Id == questionId);
            if (question == null) return false;
            return question.CorrectAnswer.Equals(selectedOption, StringComparison.OrdinalIgnoreCase);
        }
    }
}
