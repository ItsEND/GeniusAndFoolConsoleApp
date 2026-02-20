public class GameResult
{
    public string UserName { get; set; }
    public string Diagnosis { get; set; }
    public int CorrectAnswersCount { get; set; }
    public GameResult(string userName, string diagnosis, int correctAnswersCount)
    {
        UserName = userName;
        Diagnosis = diagnosis;
        CorrectAnswersCount = correctAnswersCount;
    }

}
