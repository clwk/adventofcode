public class Day02
{
    public enum Direction
    {
        forward,
        up,
        down
    }

    public (int forward, int up) FromDirection(Direction direction, int stepSize) =>
    direction switch
    {
        Direction.forward => (stepSize, 0),
        Direction.up => (0, -stepSize),
        Direction.down => (0, stepSize),
        _ => (0, 0)
    };

    public void RunA()
    {
        var input = File.ReadAllLines(@".\day02.input.txt").ToList();

        (int forward, int up) position = (0, 0);

        foreach (var line in input)
        {
            var directionStepsize = line.Split(' ');
            var relativePosition = FromDirection(
                Enum.Parse<Direction>(directionStepsize[0]),
                int.Parse(directionStepsize[1]));

            position = (position.forward + relativePosition.forward,
                        position.up + relativePosition.up);
        }

        System.Console.WriteLine($"Horizontal {position.forward} Depth {position.up}");

        System.Console.WriteLine($"Result {position.forward * position.up}");
    }

    public (int forward, int up, int aim) FromDirectionB(Direction direction, int stepSize, int aim) =>
        direction switch
        {
            Direction.forward => (stepSize, aim * stepSize, 0),
            Direction.up => (0, 0, -stepSize),
            Direction.down => (0, 0, stepSize),
            _ => (0, 0, 0)
        };

    public void RunB()
    {
        var input = File.ReadAllLines(@".\day02.input.txt").ToList();

        (int forward, int up, int aim) position = (0, 0, 0);

        foreach (var line in input)
        {
            var directionStepsize = line.Split(' ');
            var relativePosition = FromDirectionB(
                Enum.Parse<Direction>(directionStepsize[0]),
                int.Parse(directionStepsize[1]),
                position.aim);

            position = (position.forward + relativePosition.forward,
                        position.up + relativePosition.up,
                        position.aim + relativePosition.aim);
        }

        System.Console.WriteLine($"Horizontal {position.forward} Depth {position.up}");

        System.Console.WriteLine($"Result {position.forward * position.up}");
    }
}