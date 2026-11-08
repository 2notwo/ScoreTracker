namespace ScoreTracker;
using System.Text.Json;

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
            .ThenBy(s => s.Date)
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

    public void Save(string path)
    {
        var data = new SaveData { Players = Players, Games = Games, Scores = Scores };
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(data, options);
        File.WriteAllText(path, json);
    }

    public void Load(string path)
    {
        if (!File.Exists(path))
        {
            return;
        }

        var json = File.ReadAllText(path);
        var data = JsonSerializer.Deserialize<SaveData>(json);

        if (data is null)
        {
            return;
        }
        
        Players.Clear();
        Games.Clear();
        Scores.Clear();

        Players.AddRange(data.Players);
        Games.AddRange(data.Games);
        Scores.AddRange(data.Scores);
    }
}