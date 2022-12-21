using System.Data;

public class Aoc2022_Day21 : BaseDay
{
    private Dictionary<string, long> MonkeyVsNumber = new();
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
            if (long.TryParse(value, out long longValue))
                MonkeyVsNumber.Add(key, longValue);
        }
    }

    private void ParseExpressions()
    {
        foreach (var line in Input)
        {
            var key = line.Split(": ")[0];
            var value = line.Split(": ")[1];
            if (!long.TryParse(value, out _))
            {
                var expression = value.Split(" ");
                Calculations.Add(key, (expression[0], expression[1], expression[2]));
            }
        }
    }

    private void ParseUnresolved()
    {

    }

    // private long EvaluateExpression(string expression)
    // {
    //     DataTable dt = new DataTable();
    //     var v = dt.Compute(expression, "");
    //     return long.Parse(v.ToString());
    // }

    private long EvaluateExpression(long term1, long term2, string opera) => opera switch
    {
        "*" => term1 * term2,
        "+" => term1 + term2,
        "-" => term1 - term2,
        "/" => term1 / term2,
        _ => throw new InvalidOperationException()
    };

    public override void RunA()
    {
        while (Calculations.ContainsKey("root"))
        {
            foreach (var line in Calculations)
            {
                if (MonkeyVsNumber.TryGetValue(line.Value.Item1, out long item1) &&
                    MonkeyVsNumber.TryGetValue(line.Value.Item3, out long item3))
                {
                    MonkeyVsNumber.Add(line.Key, EvaluateExpression(item1, item3, line.Value.Item2));
                    Calculations.Remove(line.Key);
                }
            }
        }
        System.Console.WriteLine($"A: root yells {MonkeyVsNumber["root"]}");
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
