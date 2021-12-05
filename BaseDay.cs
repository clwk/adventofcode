using System.Net;

public abstract class BaseDay
{
    protected string InputFileName { get; }
    public List<string> Input => File.ReadAllLines(InputFileName).ToList();

    public BaseDay(string inputFileName)
    {
        InputFileName = inputFileName;
    }
}