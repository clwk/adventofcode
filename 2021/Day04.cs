public class Day04 : BaseDay
{
    public Day04(string inputFileName) : base(inputFileName)
    { }

    public override void RunA()
    {
        var bingoNumbers = Input[0].Split(',').Select(x => int.Parse(x)).ToList();

        var bingoBoardsInput = Input.Skip(2);
        int skipN = 0;
        int skipSize = 6;
        var bingoBoards = new List<BingoBoard>();

        while (skipN * skipSize < bingoBoardsInput.Count())
        {
            var oneBoard = bingoBoardsInput.Skip(skipN * skipSize).Take(5);
            var oneBoardCleanup = oneBoard.Select(x => x.Trim(' '));
            skipN++;
            bingoBoards.Add(new BingoBoard(oneBoardCleanup));
        }

        int score = PlayBingo(bingoNumbers, bingoBoards);

        System.Console.WriteLine($"Total score {score}");
    }

    private int PlayBingo(List<int> bingoNumbers, List<BingoBoard> bingoBoards)
    {
        int score = 0;
        for (int bingoNr = 1; bingoNr < bingoNumbers.Count(); bingoNr++)
        {
            foreach (var board in bingoBoards)
            {
                var numbersDrawn = bingoNumbers.Take(bingoNr);
                var boardExceptMarked = board.Board.Select(x => x.Except(numbersDrawn));
                var isBingo = boardExceptMarked.Any(x => x.Count() == 0);
                if (isBingo)
                {
                    return CalculateScore(boardExceptMarked, numbersDrawn.Last());
                }
            }
        }

        return score;
    }

    private int CalculateScore(IEnumerable<IEnumerable<int>> boardExceptMarked, int last)
    {
        // Calculate using 5 first lines only, since the rest are same but vertical
        var unmarkedSum = boardExceptMarked.Take(5).Select(x => x.Sum()).Sum();
        return unmarkedSum * last;
    }

    public override void RunB()
    {

    }
}

public class BingoBoard
{
    public List<List<int>> Board { get; } = new List<List<int>>();
    public BingoBoard(IEnumerable<string> inputBoard)
    {
        var originalBoard = inputBoard.Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(str => int.Parse(str)).ToList()).ToList();
        Board.AddRange(originalBoard);

        // Add vertical lines to make comparison with drawn numbers easier
        var boardSize = Board.Count();
        for (int i = 0; i < boardSize; i++)
        {
            Board.Add(Board.Take(5).Select(x => x[i]).ToList());
        }
    }
}