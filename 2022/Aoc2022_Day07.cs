public class Aoc2022_Day07 : BaseDay
{
    public Aoc2022_Day07(string inputFileName) : base(inputFileName)
    { }

    public override void RunA()
    {
        FsNode rootNode = new FsNode("root", NodeType.Directory, 0);
        var currentNode = rootNode;

        // var lines = Input.GetRange(2, 11);

        var skipNr = 2;

        var lines2 = Input.Skip(skipNr).TakeWhile(x => x[0] != '$').ToList();
        rootNode.AddNewChildContents("adsf", lines2);
        skipNr += lines2.Count();

        var commandString = Input[skipNr];
        var commandStrings = Input.Skip(skipNr).TakeWhile(l => l.Split(' ')[1] == "cd");

        foreach (var cdCommand in commandStrings)
        {
            var commandParts = cdCommand.Split(' ');
            if (commandParts[2] == "..")
                currentNode = currentNode.ParentNode;
            else
                currentNode = currentNode.ChildNodes.Single(n => n.Name == commandParts[2]);
        }

        var dirName = commandString.Split(' ')[2];
        currentNode = currentNode.ChildNodes.Single(n => n.Name == dirName);

        // ParseLine(line);
    }

    private void ParseLine(string line)
    {

        throw new NotImplementedException();
    }

    // private void AddNewChildContents(string name, List<string> nodeListing, FsNode parentNode)
    // {
    //     FsNode newNode = new FsNode(name, NodeType.Directory, parentNode);

    //     foreach (var line in nodeListing)
    //     {
    //         var nodeName = line.Split(' ')[1];

    //         if (line[0] == 'd')
    //             newNode.AddChildNode(new FsNode(nodeName, NodeType.Directory, newNode));
    //         if (char.IsDigit(line[0]))
    //             newNode.AddChildNode(new FsNode(nodeName, NodeType.File, newNode));
    //     }

    // }



    public override void RunB()
    {

    }
}

// public record File(int size, string name);
// public record Command(string argument) : Object;



public enum LineType
{
    Command,
    Directory,
    File
}


public class FsNode
{
    public string Name { get; }
    public NodeType NodeType { get; }
    public int Size { get; set; } = 0;
    public FsNode ParentNode { get; }

    public List<FsNode> ChildNodes => _childNodes;

    private List<FsNode> _childNodes = new();
    public FsNode(string name, NodeType nodeType, int size = 0, FsNode parentNode = null)
    {
        Name = name;
        NodeType = nodeType;
        Size = size;
        ParentNode = parentNode;
    }

    public void AddNewChildContents(string name, List<string> nodeListing)
    {
        // FsNode newNode = new FsNode(name, NodeType.Directory, parentNode);

        foreach (var line in nodeListing)
        {
            var nodeName = line.Split(' ')[1];
            var dirOrSize = line.Split(' ')[0];

            if (line[0] == 'd')
                AddChildNode(new FsNode(nodeName, NodeType.Directory, 0, this));
            if (char.IsDigit(line[0]))
                AddChildNode(new FsNode(nodeName, NodeType.File, int.Parse(dirOrSize), this));
        }

    }

    public void AddChildNode(FsNode node)
    {

        _childNodes.Add(node);
    }
}

public enum NodeType
{
    Directory,
    File
}