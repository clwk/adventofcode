using System.Diagnostics;

public class Aoc2021_Day21 : BaseDay
{
    private int Player1Start { get; }
    private int Player2Start { get; }
    private int Player1LastPos { get; set; }
    private int Player2LastPos { get; set; }
    private int Player1Score { get; set; } = 0;
    private int Player2Score { get; set; } = 0;
    private int LastDice { get; set; }
    private int DiceRolls { get; set; } = 0;
    // Part B
    private long Player1Wins { get; set; } = 0;
    private long Player2Wins { get; set; } = 0;
    private Dictionary<long, int> Player1LastPoses { get; set; } = new();
    private Dictionary<long, int> Player2LastPoses { get; set; } = new();// (int, bool)[3];
    private Dictionary<long, int> Player1Scores { get; set; } = new();// { new KeyValuePair(1, 0) };
    private Dictionary<long, int> Player2Scores { get; set; } = new();
    private long maxGameIndex { get; set; } = 0;

    public Aoc2021_Day21(string inputFileName) : base(inputFileName)
    {
        Player1Start = int.Parse(Input[0].Split(": ")[1]);
        Player2Start = int.Parse(Input[1].Split(": ")[1]);
        Player1LastPos = Player1Start;
        Player2LastPos = Player2Start;
        Player1LastPoses.Add(1, Player1LastPos);
        Player2LastPoses.Add(1, Player2LastPos);
        Player1Scores.Add(1, 0);
        Player2Scores.Add(1, 0);
        maxGameIndex = 1;
    }

    public override void RunA()
    {
        while (Player1Score < 1000 && Player2Score < 1000)
        {
            Player1LastPos = GetDiceRoll(Player1LastPos);
            Player1Score += GetScore(Player1LastPos);
            if (Player1Score < 1000)
            {
                Player2LastPos = GetDiceRoll(Player2LastPos);
                Player2Score += GetScore(Player2LastPos);
            }
            Debug.WriteLine(Player1Score);
            Debug.WriteLine(Player2Score);
        }
        var result = Math.Min(Player1Score, Player2Score) * LastDice;

        System.Console.WriteLine($"Result is {result}");
    }

    public override void RunB()
    {
        // Hur para ihop omgångar från spelare1/2 ?
        int playToScore = 21;
        long gamesLeft = 1;

        while (gamesLeft > 0)
        {
            var tempMaxGameIndex = maxGameIndex;

            // index som identifierar ett spel ?
            for (long gameIdx = 1; gameIdx <= maxGameIndex; gameIdx++)
            {
                // Fortsätt spela endast om ingen kommit upp i 21
                if (Player1Scores[gameIdx] < playToScore && Player2Scores[gameIdx] < playToScore)
                    for (int dice = 1; dice <= 3; dice++)
                    {
                        var tempIdx = gameIdx;
                        var clonedIdx = tempIdx;
                        // add new gameindex
                        if (dice != 1)
                        {
                            tempMaxGameIndex++;
                            tempIdx = tempMaxGameIndex;
                            Player2Scores.Add(tempIdx, Player2Scores[clonedIdx]);
                            Player1Scores.Add(tempIdx, Player1Scores[clonedIdx]);
                            Player2LastPoses.Add(tempIdx, Player2LastPoses[clonedIdx]);
                            Player1LastPoses.Add(tempIdx, Player1LastPoses[clonedIdx]);
                        }

                        var newPos = (Player1LastPoses[tempIdx] + dice) % 10;
                        var newScore = GetScore(newPos);

                        Player1LastPoses[tempIdx] = newPos;
                        Player1Scores[tempIdx] += GetScore(newPos);
                    }
            }
            maxGameIndex = tempMaxGameIndex;

            for (long gameIdx = 1; gameIdx <= maxGameIndex; gameIdx++)
            {
                // Fortsätt spela endast om ingen kommit upp i playToScore
                if (Player1Scores[gameIdx] < playToScore && Player2Scores[gameIdx] < playToScore)
                    for (int dice = 1; dice <= 3; dice++)
                    {
                        var tempIdx = gameIdx;
                        var clonedIdx = tempIdx;
                        // add new gameindex
                        if (dice != 1)
                        {
                            tempMaxGameIndex++;
                            tempIdx = tempMaxGameIndex;
                            Player1Scores.Add(tempIdx, Player1Scores[clonedIdx]);
                            Player2Scores.Add(tempIdx, Player2Scores[clonedIdx]);
                            Player1LastPoses.Add(tempIdx, Player1LastPoses[clonedIdx]);
                            Player2LastPoses.Add(tempIdx, Player2LastPoses[clonedIdx]);
                        }

                        var newPos = (Player2LastPoses[tempIdx] + dice) % 10;
                        var newScore = GetScore(newPos);

                        Player2LastPoses[tempIdx] = newPos;
                        Player2Scores[tempIdx] += GetScore(newPos);
                    }
            }
            maxGameIndex = tempMaxGameIndex;

            var player1wins = Player1Scores.Count(x => x.Value >= playToScore);
            var player2wins = Player2Scores.Count(x => x.Value >= playToScore);
            var player1max = Player1Scores.Max(x => x.Value);
            var playerwmax = Player2Scores.Max(x => x.Value);
            gamesLeft = maxGameIndex - player1wins - player2wins;
            Debug.WriteLine($"Player 1: {player1wins} Player 2: {player2wins} nr games: {gamesLeft}/{maxGameIndex}");
        }

    }

    public int GetDiceRoll(int lastPos)
    {
        var diceResult = (Enumerable.Range(LastDice + 1, 3).Sum() + lastPos) % 10;
        LastDice += 3;
        DiceRolls++;
        return diceResult;
    }

    public int GetScore(int position)
    {
        if (position == 0)
            return 10;
        else
            return position;
    }


}