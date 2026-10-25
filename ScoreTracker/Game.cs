namespace ScoreTracker;

public class Game
{
    public int Id { get; set; }
    public string Name { get; set; } = ""; //avoid null

    public override string ToString() => $"{Id}: {Name}";
}