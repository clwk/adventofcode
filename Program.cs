using System.Net;
using System.Reflection;
using Microsoft.Extensions.Configuration;

// Used code from https://github.com/viceroypenguin/adventofcode/blob/master/Program.cs
public static class Program
{
    public static string ClassName { get; set; }

    static Program()
    {
        var today = DateTime.Now;
        ClassName = $"Aoc{today.Year}_Day{today.Day:00}";
    }

    public static async Task Main(string[] args)
    {
        // TODO: if date is in args, else today
        var today = DateTime.Now;

        System.Console.WriteLine($"Running day {today.Day:00} year {today.Year}");
        ClassName = $"Aoc{today.Year}_Day{today.Day:00}";

        string sessionId = BuildAndGetConfiguration();
        var inputFileName = await DownloadInputFile(today, sessionId);

        CreateTodaysCsFileNeeded(today);
        RunTodaysInstanceMethods(today, inputFileName);
    }

    private static void RunTodaysInstanceMethods(DateTime today, string inputFileName)
    {
        var todaysClass = Assembly.GetExecutingAssembly().GetTypes()
                          .FirstOrDefault(t => t.BaseType == typeof(BaseDay) && t.Name == ClassName);

        if (todaysClass != null)
        {
            var GetDay = Activator.CreateInstance(todaysClass, new object[1] { $"{inputFileName}" }) as BaseDay;
            var methods = todaysClass.GetMethods();
            System.Console.WriteLine("Running part A");
            GetDay?.RunA();
            System.Console.WriteLine("Running part B");
            GetDay?.RunB();
        }
        else
        {
            System.Console.WriteLine($"Class file for Day {today.Day:00} not found, please rerun. ");
        }
    }

    private static string BuildAndGetConfiguration()
    {
        var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        var configuration = builder.Build();
        var sessionId = configuration["sessionId"];

        if (string.IsNullOrWhiteSpace(sessionId))
            throw new ArgumentNullException(nameof(sessionId), "Please provide an AoC session id in the configuration file.");
        return sessionId;
    }

    private static bool CreateTodaysCsFileNeeded(DateTime today)
    {
        var csFile = $"{today.Year}/{ClassName}.cs";
        if (!File.Exists(csFile))
        {
            File.WriteAllText(csFile, GetDayTemplate(today));
            return true;
        }
        return false;
    }

    private static async Task<string> DownloadInputFile(DateTime dateToDownload, string sessionId)
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

        var inputFile = $@"{dateToDownload.Year}\Aoc{dateToDownload.Year}_Day{dateToDownload.Day:00}.input.txt";
        if (!Directory.Exists(dateToDownload.Year.ToString()))
            Directory.CreateDirectory(dateToDownload.Year.ToString());
        if (!File.Exists(inputFile))
        {
            var response = await HttpClient.GetAsync($"{dateToDownload.Year}/day/{dateToDownload.Day}/input");
            response.EnsureSuccessStatusCode();
            File.WriteAllText(inputFile, await response.Content.ReadAsStringAsync());
        }
        return inputFile;
    }

    private static string GetDayTemplate(DateTime dateTime)
    {
        var className = $"Aoc{dateTime.Year}_Day{dateTime.Day:00}";

        var test = $@"public class {className} : BaseDay
{{
    public {className}(string inputFileName) : base(inputFileName)
    {{  }}

    public override void RunA()
    {{
    
    }}

    public override void RunB()
    {{
        
    }}
}}";
        return test;
    }
}