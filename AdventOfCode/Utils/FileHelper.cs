namespace AdventOfCode.Utils;

public static class FileHelper
{
    // The year of the Advent of Code puzzles.
    private const int Year = 2025;

    private static readonly HttpClient httpClient = new();

    public static string GetInputFilePath(int puzzleId)
    {
        var assemblyLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location);
        // We're currently in [ProjectPath]\bin\[Debug|Release]\netN.0
        var solutionDirectory = Path.Combine(assemblyLocation!, @"..\..\..\..\");
        var solutionName = new DirectoryInfo(solutionDirectory).Name;

        var inputDirectory = Path.GetFullPath(Path.Combine(solutionDirectory, $"..\\{solutionName}Input"));
        if (!Directory.Exists(inputDirectory))
        {
            Directory.CreateDirectory(inputDirectory);
        }

        var filePath = Path.Combine(inputDirectory, $"{puzzleId:D2}.txt");
        if (!File.Exists(filePath))
        {
            var url = $"https://adventofcode.com/{Year}/day/{puzzleId}/input";
            Console.WriteLine($"{filePath} doesn't exist, downloading from {url}");
            var content = FetchContentFromUrl(url).Result;
            File.WriteAllText(filePath, content);
        }
        return filePath;
    }

    private static async Task<string> FetchContentFromUrl(string url)
    {
        var sessionCookie = Environment.GetEnvironmentVariable("AOC_SESSION", EnvironmentVariableTarget.User);
        if (string.IsNullOrEmpty(sessionCookie))
        {
            throw new InvalidOperationException("Session cookie is not set in the environment variables.");
        }

        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("Cookie", $"session={sessionCookie}");
        var response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}
