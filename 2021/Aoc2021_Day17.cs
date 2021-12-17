public class Aoc2021_Day17 : BaseDay
{
    public Aoc2021_Day17(string inputFileName) : base(inputFileName)
    { }

    // target area: x=102..157, y=-146..-90
    (int x, int y) TargetMin => (102, -146);
    (int x, int y) TargetMax => (157, -90);
    int MaxStep => 400;

    public override void RunA()
    {
        int maxY = 0;
        for (int x = 1; x <= TargetMax.x; x++)
        {
            for (int y = TargetMin.y; y <= 1000; y++)
            {
                var currentMaxY = FindMaxYPos((x, y));
                if (currentMaxY > maxY)
                {
                    maxY = currentMaxY;
                }
            }
        }

        System.Console.WriteLine($"Max y position {maxY}");
    }

    private int FindMaxYPos((int x, int y) startSpeed)
    {
        (int x, int y) speed = startSpeed;
        int step = 0;
        int maxY = 0;

        (int x, int y) position = (0, 0);
        while (!(position.x <= TargetMax.x && position.x >= TargetMin.x &&
                position.y <= TargetMax.y && position.y >= TargetMin.y) &&
                step < MaxStep)
        {
            position.x = position.x + speed.x;
            position.y = position.y + speed.y;
            if (position.y > maxY) maxY = position.y;
            speed = GetNextSpeed(speed);
            step++;
        }
        return step >= MaxStep ? -int.MaxValue : maxY;
    }

    private (int x, int y) GetNextSpeed((int x, int y) currentSpeed)
    {
        (int x, int y) nextSpeed = (0, 0);
        if (currentSpeed.x != 0)
            nextSpeed.x = currentSpeed.x > 0 ? currentSpeed.x - 1 : currentSpeed.x + 1;

        nextSpeed.y = currentSpeed.y - 1;
        return nextSpeed;
    }

    public override void RunB()
    {

    }
}