public class Aoc2021_Day15 : BaseDay
{
    // Graph<(int, int)> graph = new Graph<(int, int)>(false, true);
    (int rows, int cols) Dimensions;
    Graph<int> Graph = new Graph<int>(false, true);
    Node<int>[,] AllNodes;// = new Node<int>[10, 10];
    int[,] InputData;// = new int[10, 10];

    public Aoc2021_Day15(string inputFileName) : base(inputFileName)
    {
        Dimensions.rows = Input.Count;
        Dimensions.cols = Input[0].Count();

        AllNodes = new Node<int>[Dimensions.rows, Dimensions.cols];
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
                AllNodes[i, j] = Graph.AddNode(InputData[i, j]);
            }
        }
    }

    public override void RunA()
    {
        List<Edge<int>> path = Graph.GetShortestPathDijkstra(AllNodes[0, 0], AllNodes[Dimensions.rows - 1, Dimensions.cols - 1]);
        path.ForEach(e => Console.WriteLine(e));
        var lowestRisk = path.Sum(x => x.Weight);
        System.Console.WriteLine($"Lowest risk is {lowestRisk}");
        // SolveExampleGraph();
    }

    private static void SolveExampleGraph()
    {
        Graph<int> graph = new Graph<int>(true, true);
        Node<int> n1 = graph.AddNode(1);
        Node<int> n2 = graph.AddNode(2);
        Node<int> n3 = graph.AddNode(3);
        Node<int> n4 = graph.AddNode(4);
        Node<int> n5 = graph.AddNode(5);
        Node<int> n6 = graph.AddNode(6);
        Node<int> n7 = graph.AddNode(7);
        Node<int> n8 = graph.AddNode(8);
        graph.AddEdge(n1, n2, 9);
        graph.AddEdge(n1, n3, 5);
        graph.AddEdge(n2, n1, 3);
        graph.AddEdge(n2, n4, 18);
        graph.AddEdge(n3, n4, 12);
        graph.AddEdge(n4, n2, 2);
        graph.AddEdge(n4, n8, 8);
        graph.AddEdge(n5, n4, 9);
        graph.AddEdge(n5, n6, 2);
        graph.AddEdge(n5, n7, 5);
        graph.AddEdge(n5, n8, 3);
        graph.AddEdge(n6, n7, 1);
        graph.AddEdge(n7, n5, 4);
        graph.AddEdge(n7, n8, 6);
        graph.AddEdge(n8, n5, 3);
        List<Edge<int>> path = graph.GetShortestPathDijkstra(n1, n5);
        path.ForEach(e => Console.WriteLine(e));
    }

    public override void RunB()
    {

    }
}

