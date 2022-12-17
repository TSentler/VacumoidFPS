namespace YaVk
{
    public class PlayerInfoLeaderboard
    {
        public string Name { get; }
        public int Score { get; }

        public PlayerInfoLeaderboard(string name, int score)
        {
            Name = name;
            Score = score;
        }
    }
}