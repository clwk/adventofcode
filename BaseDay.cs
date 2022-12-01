public abstract class BaseDay
{
    private string InputFileName { get; }
    protected List<string> Input => File.ReadAllLines(InputFileName).ToList();
    protected string InputAsString => File.ReadAllText(InputFileName);

    protected BaseDay(string inputFileName)
    {
        InputFileName = inputFileName;
    }

    public virtual void RunA() {

    }

    
    public virtual void RunB() {
        
    }
}