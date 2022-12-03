public class Aoc2022_Day02 : BaseDay
{
    public Aoc2022_Day02(string inputFileName) : base(inputFileName)
    {
    }

    private readonly Dictionary<char, Move> _shapeBySymbol = new(){
        {'A', Move.Rock},
        {'B', Move.Paper},
        {'C', Move.Scissors},
        {'X', Move.Rock},
        {'Y', Move.Paper},
        {'Z', Move.Scissors},
    };

    private readonly Dictionary<char, OutCome> _outcomeBySymbol = new(){
        {'X', OutCome.Lost},
        {'Y', OutCome.Draw},
        {'Z', OutCome.Win},
    };

    public override void RunA()
    {
        var moves = Input.Select(x => (_shapeBySymbol[x[0]], _shapeBySymbol[x[2]]))
                         .ToList();

        var points = moves.Select(x => GetScoreByMoves(x.Item1, x.Item2)).ToList();
        var pointsSum = (points.Sum(x => x.Item1), points.Sum(y => y.Item2));
        Console.WriteLine($"Your sum is {pointsSum.Item2}");
    }

    public override void RunB()
    {
        var moves = Input.Select(x => (_shapeBySymbol[x[0]],
            GetMoveByOutcome(_shapeBySymbol[x[0]],
                             _outcomeBySymbol[x[2]])))
                         .ToList();

        var points = moves.Select(x => GetScoreByMoves(x.Item1, x.Item2)).ToList();
        var pointsSum = (points.Sum(x => x.Item1), points.Sum(y => y.Item2));
        Console.WriteLine($"Your sum is {pointsSum.Item2}");
    }

    private int GetScoreForPlayer(OutCome outCome, Move move) => ((int)outCome + (int)move);

    private (int, int) GetScoreByMoves(Move player1, Move player2) => (player1, player2) switch
    {
        (Move.Rock, Move.Rock) => (GetScoreForPlayer(OutCome.Draw, Move.Rock), GetScoreForPlayer(OutCome.Draw, Move.Rock)),
        (Move.Rock, Move.Paper) => (GetScoreForPlayer(OutCome.Lost, Move.Rock), GetScoreForPlayer(OutCome.Win, Move.Paper)),
        (Move.Rock, Move.Scissors) => (GetScoreForPlayer(OutCome.Win, Move.Rock), GetScoreForPlayer(OutCome.Lost, Move.Scissors)),
        (Move.Paper, Move.Rock) => (GetScoreForPlayer(OutCome.Win, Move.Paper), GetScoreForPlayer(OutCome.Lost, Move.Rock)),
        (Move.Paper, Move.Paper) => (GetScoreForPlayer(OutCome.Draw, Move.Paper), GetScoreForPlayer(OutCome.Draw, Move.Paper)),
        (Move.Paper, Move.Scissors) => (GetScoreForPlayer(OutCome.Lost, Move.Paper), GetScoreForPlayer(OutCome.Win, Move.Scissors)),
        (Move.Scissors, Move.Scissors) => (GetScoreForPlayer(OutCome.Draw, Move.Scissors), GetScoreForPlayer(OutCome.Draw, Move.Scissors)),
        (Move.Scissors, Move.Rock) => (GetScoreForPlayer(OutCome.Lost, Move.Scissors), GetScoreForPlayer(OutCome.Win, Move.Rock)),
        (Move.Scissors, Move.Paper) => (GetScoreForPlayer(OutCome.Win, Move.Scissors), GetScoreForPlayer(OutCome.Lost, Move.Paper)),
        (_, _) => throw new ArgumentException()
    };

    private Move GetMoveByOutcome(Move player1, OutCome outCome) => (player1, outCome) switch
    {
        (Move.Rock, OutCome.Draw) => Move.Rock,
        (Move.Rock, OutCome.Lost) => Move.Scissors,
        (Move.Rock, OutCome.Win) => Move.Paper,
        (Move.Paper, OutCome.Draw) => Move.Paper,
        (Move.Paper, OutCome.Lost) => Move.Rock,
        (Move.Paper, OutCome.Win) => Move.Scissors,
        (Move.Scissors, OutCome.Draw) => Move.Scissors,
        (Move.Scissors, OutCome.Lost) => Move.Paper,
        (Move.Scissors, OutCome.Win) => Move.Rock,
        (_, _) => throw new ArgumentException()
    };
}

internal enum OutCome
{
    Lost = 0,
    Draw = 3,
    Win = 6
}

internal enum Move
{
    Rock = 1,
    Paper = 2,
    Scissors = 3
}