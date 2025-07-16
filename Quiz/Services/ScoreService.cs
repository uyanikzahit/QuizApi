namespace QuizApi.Services
{
    public class ScoreService
    {
        private static readonly Dictionary<string, int> _scores = new();

        // Skoru artır
        public void IncreaseScore(string username)
        {
            if (_scores.ContainsKey(username))
                _scores[username]++;
            else
                _scores[username] = 1;
        }

        // Skoru getir
        public int GetScore(string username)
        {
            return _scores.TryGetValue(username, out int score) ? score : 0;
        }
    }
}
