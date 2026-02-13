List<Question> questions = new List<Question>
{
    new Question ("Сколько будет два плюс два умноженное на два", "6"),
    new Question ("Бревно нужно распилить на 10 частей, сколько надо сделать распилов", "9"),
    new Question ("На двух руках 10 пальцев. Сколько пальцев на 5 руках", "25"),
    new Question ("Укол делают каждые полчаса, сколько нужно минут для трех уколов", "60"),
    new Question ("Пять свечей горело, две потухли. Сколько свечей осталось?", "2"),
};


Random rand = new Random();
questions = questions.OrderBy(x => rand.Next()).ToList();

int countsRightAnswers = StartGame(questions);
Console.WriteLine($"Ваш диагноз: {ShowDiagnose(countsRightAnswers)}");

static string ShowDiagnose(int countsRightAnswers)
{
    switch (countsRightAnswers)
    {
        case 1:
            return "Кретин";
        case 2:
            return "Дурак";
        case 3:
            return "Нормальный";
        case 4:
            return "Талант";
        case 5:
            return "Гений";
        default:
            return "Идиот";
    }
}

static int StartGame(List<Question> questions)
{
    int countsRightAnswers = 0;
    for (int i = 0; i < questions.Count; i++)
    {
        Console.WriteLine($"Вопрос {i + 1}) {questions[i].Quest}");
        string userAnswer = Console.ReadLine()!;
        if (userAnswer == questions[i].Answer)
        {
            countsRightAnswers++;
        }
    }

    return countsRightAnswers;
}