public class Aoc2022_Day08 : BaseDay
{
    public Aoc2022_Day08(string inputFileName) : base(inputFileName)
    {
        _inputArray = Input.Select(x => x.Select(y => int.Parse(y.ToString())).ToArray()).ToArray();
    }

    private int[][] _inputArray;

    public override void RunA()
    {
        // 4 directions to consider, stop when found
        // var _inputArray = Input.Select(x => x.Select(y => int.Parse(y.ToString())).ToArray()).ToArray();

        int nrOfVisibleTrees = 2 * (_inputArray.Length - 1) + 2 * (_inputArray[0].Length - 1);
        for (int i = 1; i < _inputArray.Length - 1; i++)
            for (int j = 1; j < _inputArray[0].Length - 1; j++)
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
        var testScre = GetScenicScore(1, 2, _inputArray);
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
        var enumerable = GetRow(row, treeArray).Skip(col + 1).TakeWhile(t => t < currentTree);
        return enumerable.Count();
    }

    private int GetNrOfTreesLeft(int row, int col, int[][] treeArray)
    {
        var currentTree = treeArray[row][col];
        var reverse = GetRow(row, treeArray).Take(col).Reverse();
        var enumerable = reverse.TakeWhile(t => t < currentTree);
        // var enumerable = reverse.take(t => t < currentTree);
        return enumerable.Count()+1;
    }

    private int GetNrOfTreesUp(int row, int col, int[][] treeArray)
    {
        var currentTree = treeArray[row][col];
        var enumerable = GetColumn(col, treeArray).Take(row).Reverse().TakeWhile(t => t < currentTree);
        return enumerable.Count();
    }

    private int GetNrOfTreesDown(int row, int col, int[][] treeArray)
    {
        var currentTree = treeArray[row][col];
        var enumerable = GetColumn(col, treeArray).Skip(row + 1).TakeWhile(t => t < currentTree);
        return enumerable.Count();
    }
}