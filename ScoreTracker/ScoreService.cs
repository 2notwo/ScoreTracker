namespace ScoreTracker;

public class ScoreService
{
    public List<Player> Players { get; } = new();
    public List<Game> Games { get; } = new();
    public List<Score> Scores { get; } = new();

    public Player AddPlayer(string name)
    {
        var player = new Player { Id = Players.Count + 1, Name = name };
        Players.Add(player);
        return player;
    }

    public Game AddGame(string name)
    {
        var game = new Game { Id = Games.Count + 1, Name = name };
        Games.Add(game);
        return game;
    }

    public Score AddScore(int playerId, int gameId, int points)
    {
        if (!Players.Any(p => p.Id == playerId))
        {
            throw new ArgumentException("Player ID does not exist.");
        }
        
        if (!Games.Any(g => g.Id == gameId))
        {
            throw new ArgumentException("Game ID does not exist.");
        }
        var score = new Score { PlayerId = playerId, GameId = gameId, Points = points};
        
        Scores.Add(score);
        return score;
    }

    public List<Score> GetTopScores(int gameId, int count = 10)
    {
        if (!Games.Any(g => g.Id == gameId))
        {
            throw new ArgumentException("Game ID does not exist.");
        }

        return Scores
            .Where(s => s.GameId == gameId)
            .OrderByDescending(s => s.Points)
            .Take(count)
            .ToList();
    }

    public Score? GetPersonalBest(int playerId)
    {
        if (!Players.Any(p=> p.Id == playerId ))
        {
            throw new ArgumentException("Player ID does not exist.");
        }
        
        return Scores
            .Where(s => s.PlayerId == playerId)
            .OrderByDescending(s => s.Points)
            .FirstOrDefault();
    }
}