using ScoreTracker;

namespace ScoreTracker.Tests;

public class ScoreTests
{
    [Fact]
    public void Points_cannot_be_negative()
    {
        var score = new Score();

        Assert.Throws<ArgumentException>(() => score.Points = -10);
    }

    [Fact]
    public void Points_can_be_zero()
    {
        var score = new Score();

        score.Points = 0;

        Assert.Equal(0, score.Points);
    }

    [Fact]
    public void Player_ToString_shows_id_and_name()
    {
        var player = new Player { Id = 1, Name = "NoTwo"};

        string result = player.ToString();

        Assert.Equal("1: NoTwo", result);
    }
}