using Sprache;

public class Aoc2021_Day18 : BaseDay
{
    public Aoc2021_Day18(string inputFileName) : base(inputFileName)
    { }

    public override void RunA()
    {
        List<PairOrVal> snailfishNumbers = new();
        foreach (var expr in Input)
        {
            var parsed = Grammar.MetaPair.Parse(expr);
            snailfishNumbers.Add(parsed);
            System.Console.WriteLine(parsed.ToString());
        }
    }

    public override void RunB()
    {

    }
}

public class PairOrVal
{
    public bool IsDefined => Value != null || Pair.n1 != null || Pair.n2 != null;
    public bool IsValue => Value.HasValue;
    public (PairOrVal n1, PairOrVal n2) Pair { get; set; }
    public int? Value { get; set; }
    public (PairOrVal, PairOrVal) AddInner()
    {
        Pair = (new PairOrVal(), new PairOrVal());
        return Pair;
    }
    public PairOrVal()
    {

    }
    public PairOrVal((PairOrVal n1, PairOrVal n2) pair)
    {
        Pair = pair;
    }

    public PairOrVal(PairOrVal n)
    {
        if (!n.IsValue)
        {
            Pair = n.Pair;
        }
        else
        {
            Value = n.Value;
        }
    }

    public PairOrVal(int value)
    {
        Value = value;
    }

    public override string ToString()
    {
        if (IsValue)
        {
            return Value.ToString();
        }
        else
        {
            return $"[{Pair.n1},{Pair.n2}]";
        }
    }

}

static class Grammar
{
    private static readonly Parser<char> LBracket = Parse.Char('[');
    private static readonly Parser<char> RBracket = Parse.Char(']');
    private static readonly Parser<char> Comma = Parse.Char(',');
    
    public static readonly Parser<PairOrVal> Pair =
        from lbr in LBracket
        from ldigit in Parse.Digit.Once().Text()
        from sep in Comma
        from rdigit in Parse.Digit.Once().Text()
        from rbr in RBracket
        select new PairOrVal((new PairOrVal(int.Parse(ldigit)), new PairOrVal(int.Parse(rdigit))));

    public static readonly Parser<PairOrVal> Value =
        from digit in Parse.Digit.Once().Text()
        select new PairOrVal(int.Parse(digit));

    public static readonly Parser<PairOrVal> OuterPair =
        from lbr in LBracket
        from lexpr in Pair.Or(Value)
        from sep in Comma
        from rexpr in Pair.Or(Value)
        from rbr in RBracket
        select new PairOrVal((new PairOrVal(lexpr), new PairOrVal(rexpr)));

    public static readonly Parser<PairOrVal> MetaPair =
        from lbr in LBracket
        from lexpr in MetaPair.Or(OuterPair).Or(Pair).Or(Value)
        from sep in Comma
        from rexpr in MetaPair.Or(OuterPair).Or(Pair).Or(Value)
        from rbr in RBracket
        select new PairOrVal((new PairOrVal(lexpr), new PairOrVal(rexpr)));
}
