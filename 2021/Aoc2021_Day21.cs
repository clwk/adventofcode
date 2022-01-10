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
    private Dictionary<long, int> Player2LastPoses { get; set; } = new();
    private Dictionary<long, int> Player1Scores { get; set; } = new();
    private Dictionary<long, int> Player2Scores { get; set; } = new();
    private long MaxGameIndex { get; set; } = 0;
    private HashSet<long> FinishedGames { get; set; } = new();

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
        MaxGameIndex = 1;
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

        while (FinishedGames.Count < MaxGameIndex)
        {
            var tempMaxGameIndex = MaxGameIndex;

            for (long gameIdx = 1; gameIdx <= MaxGameIndex; gameIdx++)
            {
                // Fortsätt spela endast om ingen kommit upp i 21
                if (Player1Scores[gameIdx] < playToScore && Player2Scores[gameIdx] < playToScore)
                {
                    var clonedIdx = gameIdx;
                    for (int dice1 = 1; dice1 <= 3; dice1++)
                    {

                        if (!(dice1 == 1))
                        {
                            tempMaxGameIndex++;
                            CloneGame(tempMaxGameIndex, clonedIdx);
                        }
                        for (int dice2 = 1; dice2 <= 3; dice2++)
                        {
                            if (!(dice1 == 1 && dice2 == 1))
                            {
                                tempMaxGameIndex++;
                                CloneGame(tempMaxGameIndex, clonedIdx);
                            }
                            for (int dice3 = 1; dice3 <= 3; dice3++)
                            {
                                // var tempIdx = gameIdx;
                                // var clonedIdx = gameIdx;
                                // add new gameindex
                                if (!(dice1 == 1 && dice2 == 1 && dice3 == 1))
                                {
                                    tempMaxGameIndex++;
                                    CloneGame(tempMaxGameIndex, clonedIdx);
                                }

                                var newPos = (Player1LastPoses[tempMaxGameIndex] + dice1 + dice2 + dice3) % 10;
                                var newScore = GetScore(newPos);

                                Player1LastPoses[tempMaxGameIndex] = newPos;
                                Player1Scores[tempMaxGameIndex] += GetScore(newPos);
                            }
                        }
                    }
                }
                else
                {
                    FinishedGames.Add(gameIdx);
                }
            }
            MaxGameIndex = tempMaxGameIndex;
            System.Console.WriteLine($"Max game index {MaxGameIndex}");

            for (long gameIdx = 1; gameIdx <= MaxGameIndex; gameIdx++)
            {
                // Fortsätt spela endast om ingen kommit upp i playToScore
                if (Player1Scores[gameIdx] < playToScore && Player2Scores[gameIdx] < playToScore)
                {
                    var clonedIdx = gameIdx;
                    for (int dice1 = 1; dice1 <= 3; dice1++)
                    {
                        if (!(dice1 == 1))
                        {
                            tempMaxGameIndex++;
                            CloneGame(tempMaxGameIndex, clonedIdx);
                        }
                        for (int dice2 = 1; dice2 <= 3; dice2++)
                        {
                            if (!(dice1 == 1 && dice2 == 1))
                            {
                                tempMaxGameIndex++;
                                CloneGame(tempMaxGameIndex, clonedIdx);
                            }
                            for (int dice3 = 1; dice3 <= 3; dice3++)
                            {
                                if (!(dice1 == 1 && dice2 == 1 && dice3 == 1))
                                {
                                    tempMaxGameIndex++;
                                    CloneGame(tempMaxGameIndex, clonedIdx);
                                }

                                var newPos = (Player2LastPoses[tempMaxGameIndex] + dice1 + dice2 + dice3) % 10;
                                var newScore = GetScore(newPos);

                                Player2LastPoses[tempMaxGameIndex] = newPos;
                                Player2Scores[tempMaxGameIndex] += GetScore(newPos);
                            }
                        }
                    }
                }
                else
                {
                    FinishedGames.Add(gameIdx);
                }
            }
            MaxGameIndex = tempMaxGameIndex;

            Console.WriteLine($"Games finished: {FinishedGames.Count}/{MaxGameIndex}");
        }
        var player1wins = Player1Scores.Count(x => x.Value >= playToScore);
        var player2wins = Player2Scores.Count(x => x.Value >= playToScore);
        var player1max = Player1Scores.Max(x => x.Value);
        var playerwmax = Player2Scores.Max(x => x.Value);
        Console.WriteLine($"Player 1: {player1wins} Player 2: {player2wins} nr games: {gamesLeft}/{MaxGameIndex}");

    }

    private void CloneGame(long tempMaxGameIndex, long clonedIdx)
    {
        Player1Scores.Add(tempMaxGameIndex, Player1Scores[clonedIdx]);
        Player2Scores.Add(tempMaxGameIndex, Player2Scores[clonedIdx]);
        Player1LastPoses.Add(tempMaxGameIndex, Player1LastPoses[clonedIdx]);
        Player2LastPoses.Add(tempMaxGameIndex, Player2LastPoses[clonedIdx]);
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