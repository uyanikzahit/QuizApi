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
        private readonly ScoreService _scoreService;

        public QuizController()
        {
            _questionService = new QuestionService();
            _scoreService = new ScoreService(); // skor servisi örneği
        }

        // Tüm soruları getir
        [HttpGet("questions")]
        public ActionResult<List<Question>> GetAllQuestions()
        {
            var questions = _questionService.GetAllQuestions();
            return Ok(questions);
        }

        // Rastgele bir soru getir
        [HttpGet("question/random")]
        public ActionResult<Question> GetRandomQuestion()
        {
            var question = _questionService.GetRandomQuestion();
            return Ok(question);
        }

        // Cevabı kontrol et, süreyi kontrol et, skoru güncelle
        [HttpPost("answer")]
        public ActionResult<object> CheckAnswer([FromBody] AnswerRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!request.SentAt.HasValue)
            {
                return BadRequest(new { message = "Cevap gönderim zamanı belirtilmelidir." });
            }

            var now = DateTime.UtcNow;
            var timeDifference = now - request.SentAt.Value;

            if (timeDifference.TotalSeconds > 60)
            {
                return BadRequest(new { message = "Süre doldu. 1 dakika içinde cevap vermelisiniz." });
            }

            var question = _questionService.GetAllQuestions().FirstOrDefault(q => q.Id == request.QuestionId);
            if (question == null)
            {
                return NotFound(new { message = "Belirtilen soru bulunamadı." });
            }

            bool isCorrect = _questionService.CheckAnswer(request.QuestionId, request.SelectedOption);

            int userScore = 0;

            if (isCorrect && !string.IsNullOrEmpty(request.Username))
            {
                _scoreService.IncreaseScore(request.Username);
                userScore = _scoreService.GetScore(request.Username);
            }

            return Ok(new
            {
                correct = isCorrect,
                score = userScore
            });
        }
    }
}
