using MoreLinq;

public class Aoc2022_Day08 : BaseDay
{
    public Aoc2022_Day08(string inputFileName) : base(inputFileName)
    {
        _inputArray = Input.Select(x => x.Select(y => int.Parse(y.ToString())).ToArray()).ToArray();
    }

    private readonly int[][] _inputArray;

    public override void RunA()
    {
        var nrOfVisibleTrees = 2 * (_inputArray.Length - 1) + 2 * (_inputArray[0].Length - 1);
        for (var i = 1; i < _inputArray.Length - 1; i++)
            for (var j = 1; j < _inputArray[0].Length - 1; j++)
            {
                var currentTree = _inputArray[i][j];
                var right = CheckRight(i, j, _inputArray);
                var left = CheckLeft(i, j, _inputArray);
                var top = CheckUp(i, j, _inputArray);
                var down = CheckDown(i, j, _inputArray);
                if (right || left || top || down)
                    nrOfVisibleTrees++;
            }

        System.Console.WriteLine($"Trees visible: {nrOfVisibleTrees}");
    }

    private bool CheckRight(int row, int col, int[][] treeArray)
    {
        var currentTree = treeArray[row][col];
        var enumerable = GetRow(row, treeArray).Skip(col + 1).ToList();
        return enumerable.All(t => t < currentTree);
    }

    private bool CheckLeft(int row, int col, int[][] treeArray)
    {
        var currentTree = treeArray[row][col];
        var enumerable = GetRow(row, treeArray).Take(col).ToList();
        return enumerable.All(t => t < currentTree);
    }

    private bool CheckUp(int row, int col, int[][] treeArray)
    {
        var currentTree = treeArray[row][col];
        var enumerable = GetColumn(col, treeArray).Take(row).ToList();
        return enumerable.All(t => t < currentTree);
    }

    private bool CheckDown(int row, int col, int[][] treeArray)
    {
        var currentTree = treeArray[row][col];
        var enumerable = GetColumn(col, treeArray).Skip(row + 1).ToList();
        return enumerable.All(t => t < currentTree);
    }

    private int[] GetRow(int rowNr, int[][] array)
    {
        return array[rowNr];
    }

    private int[] GetColumn(int colNr, int[][] array)
    {
        return array.Select(x => x[colNr]).ToArray();
    }

    public override void RunB()
    {
        List<int> scenicScores = new();

        for (var i = 1; i < _inputArray.Length - 1; i++)
            for (var j = 1; j < _inputArray[0].Length - 1; j++)
            {
                scenicScores.Add(GetScenicScore(i, j, _inputArray));
            }

        var maxScore = scenicScores.Max();
        System.Console.WriteLine($"Max scenic score: {maxScore}");
    }

    private int GetScenicScore(int row, int col, int[][] treeArray)
    {
        var right = GetNrOfTreesRight(row, col, treeArray);
        var left = GetNrOfTreesLeft(row, col, treeArray);
        var up = GetNrOfTreesUp(row, col, treeArray);
        var down = GetNrOfTreesDown(row, col, treeArray);
        return right * left * up * down;
    }

    private int GetNrOfTreesRight(int row, int col, int[][] treeArray)
    {
        var currentTree = treeArray[row][col];
        var enumerable = GetRow(row, treeArray).Skip(col + 1).TakeUntil(t => t >= currentTree);
        return enumerable.Count();
    }

    private int GetNrOfTreesLeft(int row, int col, int[][] treeArray)
    {
        var currentTree = treeArray[row][col];
        var reverse = GetRow(row, treeArray).Take(col).Reverse();
        var enumerable = reverse.TakeUntil(t => t >= currentTree);
        return enumerable.Count();
    }

    private int GetNrOfTreesUp(int row, int col, int[][] treeArray)
    {
        var currentTree = treeArray[row][col];
        var enumerable = GetColumn(col, treeArray).Take(row).Reverse().TakeUntil(t => t >= currentTree);
        return enumerable.Count();
    }

    private int GetNrOfTreesDown(int row, int col, int[][] treeArray)
    {
        var currentTree = treeArray[row][col];
        var enumerable = GetColumn(col, treeArray).Skip(row + 1).TakeUntil(t => t >= currentTree);
        return enumerable.Count();
    }
}