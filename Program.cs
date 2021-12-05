using System.Net;
using Microsoft.Extensions.Configuration;

// Used code from https://github.com/viceroypenguin/adventofcode/blob/master/Program.cs
public static class Program
{
    public static async Task Main(string[] args)
    {
        // if date is in args, else today
        var today = new DateTime(2021, 12, 4);

        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        var configuration = builder.Build();
        var sessionId = configuration["sessionId"];

        if (string.IsNullOrWhiteSpace(sessionId))
            throw new ArgumentNullException(nameof(sessionId), "Please provide an AoC session id in the configuration file.");

        await DownloadInputFile(today, sessionId);

        var csFile = $"{today.Year}/Day{today.Day:00}.cs";
        if (!File.Exists(csFile))
        {
            File.WriteAllText(csFile, GetDayTemplate(today));
        }
    }

    private static async Task DownloadInputFile(DateTime dateToDownload, string sessionId)
    {
        var baseAddress = new Uri("https://adventofcode.com");
        var cookieContainer = new CookieContainer();
        cookieContainer.Add(baseAddress, new Cookie("session", sessionId));

        var HttpClient = new HttpClient(
            new HttpClientHandler
            {
                CookieContainer = cookieContainer,
                AutomaticDecompression = DecompressionMethods.All,
            })
        {
            BaseAddress = baseAddress,
        };

        var inputFile = $@"{dateToDownload.Year}\day{dateToDownload.Day:00}.input.txt";
        if (!Directory.Exists(dateToDownload.Year.ToString()))
            Directory.CreateDirectory(dateToDownload.Year.ToString());
        if (!File.Exists(inputFile))
        {
            var response = await HttpClient.GetAsync($"{dateToDownload.Year}/day/{dateToDownload.Day}/input");
            response.EnsureSuccessStatusCode();
            File.WriteAllText(inputFile, await response.Content.ReadAsStringAsync());
        }
    }

    private static string GetDayTemplate(DateTime dateTime)
    {
        var test = $@"public class Day{dateTime.Day:00} : BaseDay
{{
    public Day{dateTime.Day:00}(string inputFileName) : base(inputFileName)
    {{

    }}
    public void RunA()
    {{
    
    }}

    public void RunB()
    {{
        
    }}
}}";
        return test;
    }
}

