List<Question> questions = new List<Question>
{
    new Question ("Сколько будет два плюс два умноженное на два", 6),
    new Question ("Бревно нужно распилить на 10 частей, сколько надо сделать распилов", 9),
    new Question ("На двух руках 10 пальцев. Сколько пальцев на 5 руках", 25),
    new Question ("Укол делают каждые полчаса, сколько нужно минут для трех уколов", 60),
    new Question ("Пять свечей горело, две потухли. Сколько свечей осталось?", 2),
};

var fileStorage = new GameResultsStorage();
fileStorage.EnsureFileExists();

var userName = GetUserName();

Random rand = new Random();

do
{
    var roundQuestions = ShuffleQuestions(questions, rand);

    var correctAnswersCount = PlayGame(roundQuestions);
    var diagnose = GetDiagnose(correctAnswersCount, roundQuestions.Count);

    fileStorage.AppendResult(new GameResult(userName, diagnose, correctAnswersCount));

    Console.WriteLine($"К сожалению для Вас, {userName}, игра закончилась и ваш диагноз: {diagnose}");
    Console.WriteLine("Предлагаю ознакомиться с таблицей рекордов!");
    Console.WriteLine();

    fileStorage.ShowResult();

}
while (AskYesNo("Ведите ДА, если хотите начать заново, иначе введите НЕТ: "));

static bool AskYesNo(string inputString)// название метода подразумевает что ответ будет да или нет, а что будет если пользователь  введет абракадабра? Это ведь не нет))))
{
    Console.WriteLine(inputString);
    while (true)
    {
        string input = Console.ReadLine()!;

        if (input.ToUpper() == "ДА")
            return true;

        else if (input.ToUpper() == "НЕТ")
            return false;

        Console.WriteLine("Ничего не понятно, введи ДА или НЕТ.");

    }
}

static string GetDiagnose(int correctAnswersCount, int totalQuestionsCount)
{
    double percentage = (double)correctAnswersCount / totalQuestionsCount * 100;
    switch (percentage)
    {
        case >= 90:
            return "Гений";
        case >= 70:
            return "Талант";
        case >= 50:
            return "Нормальный";
        case >= 30:
            return "Дурак";
        case >= 10:
            return "Кретин";
        case >= 0:
            return "Идиот";
        default:
            throw new ArgumentOutOfRangeException(nameof(correctAnswersCount));
    }
}

static int PlayGame(List<Question> questions)
{
    int correctAnswersCount = 0;
    for (int i = 0; i < questions.Count; i++)
    {
        string question = $"Вопрос {i + 1}) {questions[i].Text}";
        int userAnswer = ReadInt(question);

        if (userAnswer == questions[i].Answer)
            correctAnswersCount++;

    }
    return correctAnswersCount;
}

static int ReadInt(string question)
{
    while (true)
    {
        Console.WriteLine(question);
        string input = Console.ReadLine()!;

        if (int.TryParse(input, out int value))
            return value;

        Console.WriteLine("Введите число!");
    }
}

static string GetUserName()
{
    Console.WriteLine("Приветствую в игре Гений и Дурак, перед началом предлагаю представиться!");
    Console.Write("Введите имя: ");

    string userName = (Console.ReadLine() ?? "").Trim();
    if (string.IsNullOrEmpty(userName))
    {
        userName = "Игрок";
    }

    return userName;
}

static List<Question> ShuffleQuestions(List<Question> questions, Random rand)
    => questions.OrderBy(_ => rand.Next()).ToList();


