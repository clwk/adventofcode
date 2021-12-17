public class Aoc2021_Day15 : BaseDay
{
    Graph<(int, int)> Graph = new Graph<(int, int)>(true, true);
    (int rows, int cols) Dimensions;
    Node<(int, int)>[,] AllNodes;
    int[,] InputData;

    public Aoc2021_Day15(string inputFileName) : base(inputFileName)
    {
        Dimensions.rows = Input.Count;
        Dimensions.cols = Input[0].Count();

        AllNodes = new Node<(int, int)>[Dimensions.rows, Dimensions.cols];
        InputData = new int[Dimensions.rows, Dimensions.cols];
        InputDataToNodes();
        InputDataToEdges();
    }

    private void InputDataToEdges()
    {
        for (int i = 0; i < Dimensions.rows; i++)
        {
            for (int j = 0; j < Dimensions.cols; j++)
            {
                if (i < Dimensions.rows - 1)
                    Graph.AddEdge(AllNodes[i, j], AllNodes[i + 1, j], InputData[i + 1, j]);
                if (j < Dimensions.cols - 1)
                    Graph.AddEdge(AllNodes[i, j], AllNodes[i, j + 1], InputData[i, j + 1]);
            }

        }
    }

    private void InputDataToNodes()
    {
        for (int i = 0; i < Dimensions.rows; i++)
        {
            for (int j = 0; j < Dimensions.cols; j++)
            {
                InputData[i, j] = int.Parse(Input[i][j].ToString());
                AllNodes[i, j] = Graph.AddNode((i, j));
            }
        }
    }

    public override void RunA()
    {
        List<Edge<(int, int)>> path = Graph.GetShortestPathDijkstra(AllNodes[0, 0], AllNodes[Dimensions.rows - 1, Dimensions.cols - 1]);
        path.ForEach(e => Console.WriteLine(e));
        var lowestRisk = path.Sum(x => x.Weight);
        System.Console.WriteLine($"Lowest risk is {lowestRisk}");
        foreach (var edge in path)
        {
            if (edge.To.Data.Item2 < edge.From.Data.Item2)
                System.Console.WriteLine($"strange {edge}");
            if (edge.To.Data.Item1 < edge.From.Data.Item1)
                System.Console.WriteLine($"strange {edge}");
        }
    }

    public override void RunB()
    {

    }
}

