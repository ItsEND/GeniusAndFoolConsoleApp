public class Question
{
    public string Text { get; private set; }
    public int Answer { get; private set; }

    public Question(string question, int answer)
    {
        Text = question;
        Answer = answer;
    }
}