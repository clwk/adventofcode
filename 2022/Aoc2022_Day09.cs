public class Aoc2022_Day09 : BaseDay
{
    public Aoc2022_Day09(string inputFileName) : base(inputFileName)
    {
        _headPos = (0, 0);
        _tailPos = (0, 0);
    }

    private Dictionary<char, (int row, int col)> MoveByDirection = new()
    {
        {'U', (-1, 0)},
        {'D', (1, 0)},
        {'R', (0, 1)},
        {'L', (0, -1)}
    };

    private (int row, int col) _headPos;
    private (int row, int col) _lastHeadPos;
    private (int row, int col) _tailPos;
    private (int row, int col) _lastTailPos;
    private HashSet<(int row, int col)> _tailPositions = new();

    public override void RunA()
    {
        // Next line
        // Repeat by line
        // Move head
        // Check tail and move
        foreach (var line in Input)
        {
            for (int i = 0; i < int.Parse(line[2].ToString()); i++)
            {
                _lastHeadPos = _headPos;
                _lastTailPos = _tailPos;
                MoveHead(MoveByDirection[line[0]]);
                MoveTail(GetTailRelativeMove());
                _tailPositions.Add(_tailPos);
                PrintMap();
            }
        }
        System.Console.WriteLine($"Min coord: {_tailPositions.MinBy(t => t.row)}. Max {_tailPositions.MaxBy(t => t.col)}");
        System.Console.WriteLine($"Number of tail positions: {_tailPositions.Count()}");
    }

    private void PrintMap()
    {
        // var rows = Enumerable.Range(Math.Min(_headPos.row, )))
        // var test = Math.mi
        for (int row = -5; row <= 10; row++)
        {
            for (int col = -10; col <= 0; col++)
            {
                if (_headPos == _tailPos && _headPos == (row, col))
                    System.Console.Write("X");
                else if (col == 0 && row == 0)
                    System.Console.Write("s");
                else if (_headPos.col == col && _headPos.row == row)
                    System.Console.Write("H");
                else if (_tailPos.col == col && _tailPos.row == row)
                    System.Console.Write("T");
                else
                    System.Console.Write(".");
            }
            System.Console.WriteLine(" ");
        }
    }

    private void MoveHead((int row, int col) move)
    {
        _headPos = (_headPos.row + move.row, _headPos.col + move.col);
    }

    private void MoveTail((int row, int col) tailRelativeMove)
    {
        _tailPos = (_tailPos.row + tailRelativeMove.row, _tailPos.col + tailRelativeMove.col);
    }

    private (int row, int col) GetTailRelativeMove()
    {
        var tailRelativeMove = (0, 0);
        var rowDiff = _headPos.Item1 - _tailPos.Item1;
        var colDiff = _headPos.Item2 - _tailPos.Item2;
        // 6 outer positions
        if (Math.Abs(rowDiff) == 2)
        {
            tailRelativeMove.Item1 = rowDiff / 2;
            tailRelativeMove.Item2 = colDiff;
            return tailRelativeMove;
        }
        // 6 outer positions
        if (Math.Abs(colDiff) == 2)
        {
            tailRelativeMove.Item2 = colDiff / 2;
            tailRelativeMove.Item1 = rowDiff;
            return tailRelativeMove;
        }
        return (0, 0);
    }

    public override void RunB()
    {

    }
}