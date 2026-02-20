using System.Text.Json;

public class GameResultsStorage
{
    private string path = Path.Combine(AppContext.BaseDirectory, "result.json");

    public void EnsureFileExists()
    {
        if (!File.Exists(path))
        {
            using var _ = File.Create(path);
        }
    }
    public void AppendResult(GameResult gamerResult)
    {
        var results = ReadAllResults();
        results.Add(gamerResult);
        var options = new JsonSerializerOptions { WriteIndented = true };

        var json = JsonSerializer.Serialize(results, options);
        File.WriteAllText(path, json);

    }
    public List<GameResult> ReadAllResults()
    {
        if (!File.Exists(path))
            return new List<GameResult>();

        var json = File.ReadAllText(path);

        if (string.IsNullOrWhiteSpace(json))
            return new List<GameResult>();

        return JsonSerializer.Deserialize<List<GameResult>>(json) ?? new List<GameResult>();
    }

    public void ShowResult()
    {
        var results = ReadAllResults().OrderByDescending(x => x.CorrectAnswersCount).ToList();

        PrintTable(results, headerName: "ФИО", headerDiagnosis: "Диагноз", headerCount: "Кол-во правильных");
    }

    private static void PrintTable(List<GameResult> results, string headerName, string headerDiagnosis, string headerCount)
    {
        const int NameW = 20;
        const int DiagnosisW = 11;
        const int CountW = 20;

        string FormatRow(string name, string diagnosis, string count) =>
            $"|| {name,-NameW} || {diagnosis,-DiagnosisW} || {count,-CountW} ||";

        var header = FormatRow(headerName, headerDiagnosis, headerCount);
        var separator = new string('-', header.Length);

        Console.WriteLine(header);
        Console.WriteLine(separator);

        foreach (var r in results)
        {
            Console.WriteLine(FormatRow(r.UserName, r.Diagnosis, r.CorrectAnswersCount.ToString()));
            Console.WriteLine(separator);
        }
    }
}
