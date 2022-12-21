using System.Data;

public class Aoc2022_Day21 : BaseDay
{
    private readonly Dictionary<string, long> _monkeyVsNumber = new();
    private readonly Dictionary<string, (string, string, string)> _calculations = new();

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
                _monkeyVsNumber.Add(key, longValue);
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
                _calculations.Add(key, (expression[0], expression[1], expression[2]));
            }
        }
    }

    private static long EvaluateExpression(long term1, long term2, string opera) => opera switch
    {
        "*" => term1 * term2,
        "+" => term1 + term2,
        "-" => term1 - term2,
        "/" => term1 / term2,
        _ => throw new InvalidOperationException()
    };

    public override void RunA()
    {
        while (_calculations.ContainsKey("root"))
        {
            foreach (var line in _calculations)
            {
                EvaluateLineIfPossible(line);
            }
        }
        System.Console.WriteLine($"A: root yells {_monkeyVsNumber["root"]}");
    }

    private void EvaluateLineIfPossible(KeyValuePair<string, (string, string, string)> line)
    {
        if (_monkeyVsNumber.TryGetValue(line.Value.Item1, out long item1) &&
            _monkeyVsNumber.TryGetValue(line.Value.Item3, out long item3))
        {
            _monkeyVsNumber.Add(line.Key, EvaluateExpression(item1, item3, line.Value.Item2));
            _calculations.Remove(line.Key);
        }
    }

    public override void RunB()
    {

    }
}
