using System.Data;

public class Aoc2022_Day21 : BaseDay
{
    private Dictionary<string, int> MonkeyVsNumber = new();
    private Dictionary<string, (string, string, string)> Calculations = new();

    private HashSet<string> Unresolved = new();

    public Aoc2022_Day21(string inputFileName) : base(inputFileName)
    {
        ParseNumbersOnly();
        ParseExpressions();
    }

    private void ParseNumbersOnly()
    {
        foreach (var line in Input)
        {
            var key = line.Split(": ")[0];
            var value = line.Split(": ")[1];
            if (int.TryParse(value, out int intValue))
                MonkeyVsNumber.Add(key, intValue);
        }
    }

    private void ParseExpressions()
    {
        foreach (var line in Input)
        {
            var key = line.Split(": ")[0];
            var value = line.Split(": ")[1];
            if (!int.TryParse(value, out _))
            {
                var expression = value.Split(" ");
                Calculations.Add(key, (expression[0], expression[1], expression[2]));
            }
        }
    }

    private void ParseUnresolved()
    {

    }

    private int EvaluateExpression(string expression)
    {
        DataTable dt = new DataTable();
        var v = dt.Compute(expression, "");
        return int.Parse(v.ToString());
    }

    public override void RunA()
    {
        while (Calculations.ContainsKey("root"))
        {
            foreach (var line in Calculations)
            {
                if (MonkeyVsNumber.TryGetValue(line.Value.Item1, out int item1) &&
                    MonkeyVsNumber.TryGetValue(line.Value.Item3, out int item3))
                {
                    MonkeyVsNumber.Add(line.Key, EvaluateExpression(item1 + line.Value.Item2 + item3));
                    Calculations.Remove(line.Key);
                }
            }
        }
    }

    public override void RunB()
    {

    }


    public enum Operation
    {
        Plus,
        Minus,
        Multiplication,
        Division
    }
}
