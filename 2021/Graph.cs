using Priority_Queue;
// From "C# Data Structures and Algorithms" by Marcin Jamro
public class Graph<T>
{
    public Graph(bool isDirected, bool isWeighted)
    {
        _isDirected = isDirected;
        _isWeighted = isWeighted;
    }
    private bool _isDirected = false;
    private bool _isWeighted = false;
    public List<Node<T>> Nodes { get; set; } = new List<Node<T>>();
    public List<Edge<T>> GetEdges()
    {
        List<Edge<T>> edges = new List<Edge<T>>();
        foreach (Node<T> from in Nodes)
        {
            for (int i = 0; i < from.Neighbors.Count; i++)
            {
                Edge<T> edge = new Edge<T>()
                {
                    From = from,
                    To = from.Neighbors[i],
                    Weight = i < from.Weights.Count
                ? from.Weights[i] : 0
                };
                edges.Add(edge);
            }
        }
        return edges;
    }
    public Node<T> AddNode(T value)
    {
        Node<T> node = new Node<T>() { Data = value };
        Nodes.Add(node);
        UpdateIndices();
        return node;
    }

    public void RemoveNode(Node<T> nodeToRemove)
    {
        Nodes.Remove(nodeToRemove);
        UpdateIndices();
        foreach (Node<T> node in Nodes)
        {
            RemoveEdge(node, nodeToRemove);
        }
    }

    private void UpdateIndices()
    {
        int i = 0;
        Nodes.ForEach(n => n.Index = i++);
    }

    public void AddEdge(Node<T> from, Node<T> to, int weight = 0)
    {
        from.Neighbors.Add(to);
        if (_isWeighted)
        {
            from.Weights.Add(weight);
        }
        if (!_isDirected)
        {
            to.Neighbors.Add(from);
            if (_isWeighted)
            {
                to.Weights.Add(weight);
            }
        }
    }

    public void RemoveEdge(Node<T> from, Node<T> to)
    {
        int index = from.Neighbors.FindIndex(n => n == to);
        if (index >= 0)
        {
            from.Neighbors.RemoveAt(index);
            if (_isWeighted)
            {
                from.Weights.RemoveAt(index);
            }
        }
    }
    public Edge<T> this[int from, int to]
    {
        get
        {
            Node<T> nodeFrom = Nodes[from];
            Node<T> nodeTo = Nodes[to];
            int i = nodeFrom.Neighbors.IndexOf(nodeTo);
            if (i >= 0)
            {
                Edge<T> edge = new Edge<T>()
                {
                    From = nodeFrom,
                    To = nodeTo,
                    Weight = i < nodeFrom.Weights.Count
                ? nodeFrom.Weights[i] : 0
                };
                return edge;
            }
            return null;
        }
    }

    public List<Edge<T>> GetShortestPathDijkstra(Node<T> source, Node<T> target)
    {
        int[] previous = new int[Nodes.Count];
        Fill(previous, -1);
        int[] distances = new int[Nodes.Count];
        Fill(distances, int.MaxValue); 
        distances[source.Index] = 0;
        // Can be replaced with PriorityQueue ?
        // https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.priorityqueue-2
        SimplePriorityQueue<Node<T>> nodes =
        new SimplePriorityQueue<Node<T>>();
        for (int i = 0; i < Nodes.Count; i++)
        {
            nodes.Enqueue(Nodes[i], distances[i]);
        }
        while (nodes.Count != 0)
        {
            Node<T> node = nodes.Dequeue();
            for (int i = 0; i < node.Neighbors.Count; i++)
            {
                Node<T> neighbor = node.Neighbors[i];
                int weight = i < node.Weights.Count
                ? node.Weights[i] : 0;
                int weightTotal = distances[node.Index] + weight;
                if (distances[neighbor.Index] > weightTotal)
                {
                    distances[neighbor.Index] = weightTotal;
                    previous[neighbor.Index] = node.Index;
                    nodes.UpdatePriority(neighbor,
                    distances[neighbor.Index]);
                }
            }
        }
        List<int> indices = new List<int>();
        int index = target.Index;
        while (index >= 0)
        {
            indices.Add(index);
            index = previous[index];
        }
        indices.Reverse();
        List<Edge<T>> result = new List<Edge<T>>();
        for (int i = 0; i < indices.Count - 1; i++)
        {
            Edge<T> edge = this[indices[i], indices[i + 1]];
            result.Add(edge);
        }
        return result;
    }

    private void Fill<Q>(Q[] array, Q value)
    {
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = value;
        }
    }
}

public class Node<T>
{
    public int Index { get; set; }
    public T Data { get; set; }
    public List<Node<T>> Neighbors { get; set; } = new List<Node<T>>();
    public List<int> Weights { get; set; } = new List<int>();

    public override string ToString()
    {
        return $"Node with index {Index}: {Data}, neighbors: { Neighbors.Count}";
    }
}

public class Edge<T>
{
    public Node<T> From { get; set; }
    public Node<T> To { get; set; }
    public int Weight { get; set; }

    public override string ToString()
    {
        return $"Edge: {From.Data} -> {To.Data}, weight: { Weight}";
    }
}
