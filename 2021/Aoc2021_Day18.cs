using Sprache;

public class Aoc2021_Day18 : BaseDay
{
    public Aoc2021_Day18(string inputFileName) : base(inputFileName)
    { }

    public override void RunA()
    {
        var toExplode = Grammar.MetaPair.Parse("[[[[[9,8],1],2],3],4]");

        PairOrVal result = ExplodeOrSplit(toExplode);

        List<PairOrVal> snailfishNumbers = new();
        foreach (var expr in Input)
        {
            var parsed = Grammar.MetaPair.Parse(expr);
            snailfishNumbers.Add(parsed);
            System.Console.WriteLine(parsed.ToString());
        }
    }

    private PairOrVal ExplodeOrSplit(PairOrVal toExplode)
    {
        PairOrVal tempPair;
        if (toExplode.NestingLevel > 4)
            tempPair = Explode(toExplode);
        throw new NotImplementedException();
    }

    private PairOrVal Explode(PairOrVal pairTree)
    {
        while (pairTree.NestingLevel > 1)
        {
            pairTree = pairTree.Pair.n1.NestingLevel > pairTree.Pair.n2.NestingLevel ? pairTree.Pair.n1 : pairTree.Pair.n2;
        }

        // explode left
        var pairForRight = pairTree;
        // No return neeeded?
        var tempPair = ExplodeLeft(pairTree);
        ExplodeRight(pairTree);
        // pairTree = new PairOrVal(0);
        pairTree.Pair = (null, null);
        pairTree.Value = 0;

        // return needed?
        return pairTree;
    }

    private void ExplodeRight(PairOrVal pair)
    {
        // TODO
        var pairForRight = pair;
        // pair is to the left
        if (pairForRight.IsLeftPair.HasValue && pairForRight.IsLeftPair.Value)
        {
            pairForRight.Parent.Pair.n2.Value += pairForRight.Pair.n2.Value;
        }
        // pairForRight is to the right
        else
        {
            while (pairForRight.Parent != null)
            {
                if (pairForRight.Parent.IsLeftPair.HasValue && pairForRight.Parent.IsLeftPair.Value)
                {
                    // correct? sure it's a value?
                    pairForRight.Parent.Pair.n2.Value += pairForRight.Pair.n2.Value;
                }
                else
                {
                    pairForRight = pairForRight.Parent;
                }
            }
        }
    }

    private static PairOrVal ExplodeLeft(PairOrVal pair)
    {
        var pairForLeft = pair;
        // pair is to the right
        if (pairForLeft.IsLeftPair.HasValue && !pairForLeft.IsLeftPair.Value)
        {
            pairForLeft.Parent.Pair.n1.Value += pairForLeft.Pair.n1.Value;
        }
        // pairForLeft is to the left
        else
        {
            while (pairForLeft.Parent != null)
            {
                if (pairForLeft.Parent.IsLeftPair.HasValue && !pairForLeft.Parent.IsLeftPair.Value)
                {
                    // correct? sure it's a value?
                    pairForLeft.Parent.Pair.n1.Value += pairForLeft.Pair.n1.Value;
                }
                else
                {
                    pairForLeft = pairForLeft.Parent;
                }
            }
            // if (pair.Parent)
        }

        return pairForLeft;
    }

    public override void RunB()
    {

    }
}

public class PairOrVal
{
    public bool IsDefined => Value != null || Pair.n1 != null || Pair.n2 != null;
    public bool IsValue => Value.HasValue;
    public bool? IsLeftPair { get; set; }
    public (PairOrVal n1, PairOrVal n2) Pair { get; set; }
    public int? Value { get; set; }
    public PairOrVal Parent { get; set; }

    public int NestingLevel
    {
        get
        {
            if (IsValue)
                return 0;
            else
            {
                var nestingBelow = new List<int?>() { Pair.n1?.NestingLevel, Pair.n2?.NestingLevel };
                return (nestingBelow.Max(x => x.GetValueOrDefault()) + 1);
            }
        }
    }

    public PairOrVal((PairOrVal n1, PairOrVal n2) pair)
    {
        Pair = pair;
        Pair.n1.Parent = this;
        Pair.n1.IsLeftPair = true;
        Pair.n2.Parent = this;
        Pair.n2.IsLeftPair = false;
    }

    public PairOrVal(PairOrVal n)
    {
        if (!n.IsValue)
        {
            Pair = n.Pair;
            Pair.n1.Parent = this;
            Pair.n1.IsLeftPair = true;
            Pair.n2.Parent = this;
            Pair.n2.IsLeftPair = false;
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
