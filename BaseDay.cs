public abstract class BaseDay
{
    public string InputFileName { get; }
    public List<string> Input => File.ReadAllLines(InputFileName).ToList();

    protected BaseDay(string inputFileName)
    {
        InputFileName = inputFileName;
    }

    public virtual void RunA() {

    }

    
    public virtual void RunB() {
        
    }
}