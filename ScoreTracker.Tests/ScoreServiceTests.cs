namespace ScoreTracker.Tests;

public class ScoreServiceTests
{
    [Fact]
    public void GetTopScores_returns_highest_first()
    {
        var service = new ScoreService();
        var player = service.AddPlayer("Jakub");
        var game = service.AddGame("Deadlock");
        service.AddScore(player.Id, game.Id, 100);
        service.AddScore(player.Id, game.Id, 900);
        service.AddScore(player.Id, game.Id, 500);

        var top = service.GetTopScores(game.Id);

        Assert.Equal(900, top[0].Points);
        Assert.Equal(500, top[1].Points);
        Assert.Equal(100, top[2].Points);
    }

    [Fact]
    public void GetTopScores_returns_at_most_10_scores()
    {
        var service = new ScoreService();
        var player = service.AddPlayer("Jakub");
        var game = service.AddGame("Deadlock");

        for (int i = 1; i <= 12; i++)
        {
            service.AddScore(player.Id, game.Id, i * 100);
        }

        var top = service.GetTopScores(game.Id);

        Assert.Equal(10, top.Count);
        Assert.Equal(300, top.Last().Points);
    }

    [Fact]
    public void GetTopScores_only_includes_scores_for_requested_game()
    {
        var service = new ScoreService();
        var player = service.AddPlayer("Jakub");
        var game = service.AddGame("Deadlock");
        var game2 = service.AddGame("League of Legends");

        service.AddScore(player.Id, game.Id, 100);
        service.AddScore(player.Id, game2.Id, 200);

        var top = service.GetTopScores(game.Id);

        var only = Assert.Single(top);
        Assert.Equal(100, only.Points);
    }

    [Fact]
    public void GetTopScores_returns_empty_list_when_no_scores()
    {
        var service = new ScoreService();
        var game = service.AddGame("Deadlock");

        var top = service.GetTopScores(game.Id);

        Assert.Empty(top);
    }
    
    [Fact]
    public void GetTopScores_throws_when_gameID_does_not_exist()
    {
        var service = new ScoreService();

        Assert.Throws<ArgumentException>(() => service.GetTopScores(1));
    }

    [Fact]
    public void GetPersonalBest_returns_highest_score_not_latest()
    {
        var service = new ScoreService();
        var player = service.AddPlayer("Jakub");
        var game = service.AddGame("Deadlock");
        
        service.AddScore(player.Id, game.Id, 200);
        service.AddScore(player.Id, game.Id, 100);

        var pb = service.GetPersonalBest(player.Id);

        Assert.NotNull(pb);
        Assert.Equal(200, pb.Points);
    }

    [Fact]
    public void GetPersonalBest_returns_null_for_no_scores()
    {
        var service = new ScoreService();
        var player = service.AddPlayer("Jakub");

        var pb = service.GetPersonalBest(player.Id);
        
        Assert.Null(pb);
    }

    [Fact]
    public void GetPersonalBest_throws_for_a_playerID_that_does_not_exist()
    {
        var service = new ScoreService();

        Assert.Throws<ArgumentException>(() => service.GetPersonalBest(1));
    }

    [Fact]
    public void AddScore_throws_for_a_playerID_that_does_not_exist()
    {
        var service = new ScoreService();
        var game = service.AddGame("Deadlock");
        
        Assert.Throws<ArgumentException>(() => service.AddScore(1, game.Id, 100));
    }

    [Fact]
    public void AddScore_throws_for_a_gameID_that_does_not_exist()
    {
        var service = new ScoreService();
        var player = service.AddPlayer("Jakub");

        Assert.Throws<ArgumentException>(() => service.AddScore(player.Id, 5, 100));
    }

    [Fact]
    public void Saved_data_loads_back()
    {
        string path = Path.GetTempFileName();
        var original = new ScoreService();
        var player = original.AddPlayer("Jakub");
        var game = original.AddGame("Deadlock");
        original.AddScore(player.Id, game.Id, 4500);

        original.Save(path);
        var loaded = new ScoreService();
        loaded.Load(path);

        Assert.Single(loaded.Players);
        Assert.Equal(4500, loaded.Scores[0].Points);
        File.Delete(path);
    }
    
}