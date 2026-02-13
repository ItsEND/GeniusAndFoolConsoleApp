public class Question
{
    public Question(string quest, string answer)
    {
        Quest = quest;
        Answer = answer;
    }

    public string Quest { get;private set; }


    public string Answer { get;private set; }
}
