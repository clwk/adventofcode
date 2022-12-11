public class Aoc2022_Day11 : BaseDay
{
    public Aoc2022_Day11(string inputFileName) : base(inputFileName)
    { }

    public override void RunA()
    {

    }

    public override void RunB()
    {

    }
}

public class Monkey
{
    private List<int> items = new();

    public Monkey(List<int> startItems, OperationType type, int operand, int testDivisor, int trueMonkey, int falseMonkey)
    {
        items = startItems;
        OperationType = type;
        Operand = operand;
        TestDivisor = testDivisor;
        TrueMonkey = trueMonkey;
        FalseMonkey = falseMonkey;
    }
    IReadOnlyList<int> Items { get => items;  }
    public OperationType OperationType { get; }
    public int Operand { get; }
    public int TestDivisor { get; }
    public int TrueMonkey { get; }
    public int FalseMonkey { get; }

    public void AddItem(int item)
    {
        items.Add(item);
    }

    public void RemoveItem(int item)
    {
        items.Remove(item);
    }
}

public enum OperationType
{
    Plus,
    Times
}