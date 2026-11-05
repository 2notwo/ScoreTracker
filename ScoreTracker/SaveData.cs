namespace ScoreTracker;

public class SaveData
{
    public List<Player> Players { get; set; } = new();
    public List<Game> Games { get; set; } = new();
    public List<Score> Scores { get; set; } = new();
}