using Microsoft.AspNetCore.Mvc;
using QuizApi.Models;
using QuizApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizController : ControllerBase
    {
        private readonly QuestionService _questionService;

        public QuizController()
        {
            _questionService = new QuestionService();
        }

        //Tüm soruları getir
        [HttpGet("questions")]
        public ActionResult<List<Question>> GetAllQuestions()
        {
            var questions = _questionService.GetAllQuestions();
            return Ok(questions);
        }

        //Rastgele bir soru getirir
        [HttpGet("question/random")]
        public ActionResult<Question> GetRandomQuestion()
        {
            var question = _questionService.GetRandomQuestion();
            return Ok(question);
        }

        //Cevabı kontrol et, süreyi kontrol et, skoru güncelle
        [HttpPost("answer")]
        public ActionResult<object> CheckAnswer([FromBody] AnswerRequest request)
        {
            // Model doğrulaması
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // SentAt kontrolü: gönderim zamanı zorunlu, opsiyonel değil
            if (!request.SentAt.HasValue)
            {
                return BadRequest(new { message = "Cevap gönderim zamanı belirtilmelidir." });
            }

            var now = DateTime.UtcNow;
            var timeDifference = now - request.SentAt.Value;

            // 60 saniyeden uzun süreli cevaplar reddedilir
            if (timeDifference.TotalSeconds > 60)
            {
                return BadRequest(new { message = "Süre doldu. 1 dakika içinde cevap vermelisiniz." });
            }

            // Soru var mı kontrolü
            var question = _questionService.GetAllQuestions().FirstOrDefault(q => q.Id == request.QuestionId);
                                          

            if (question == null)
            {
                return NotFound(new { message = "Belirtilen soru bulunamadı." });
            }

            // Cevap doğru mu?
            bool isCorrect = _questionService.CheckAnswer(request.QuestionId, request.SelectedOption);

            // Skor güncelle (kullanıcı adı varsa)
            if (isCorrect && !string.IsNullOrEmpty(request.Username))
            {
            }

            // Sonucu ve skoru dön
            return Ok(new
            {
                correct = isCorrect,
            });
        }
    }
}
