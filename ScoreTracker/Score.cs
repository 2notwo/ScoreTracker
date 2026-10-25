namespace ScoreTracker;

public class Score
{
    public int PlayerId { get; set; }
    public int GameId { get; set; }

    private int points;
    public int Points
    {
        get { return points;}
        set {
            if (value < 0)
            {
                throw new ArgumentException("Points can't be negative");
            }

            points = value;

        }
    }

    public DateTime Date { get; set; } = DateTime.Now;
}