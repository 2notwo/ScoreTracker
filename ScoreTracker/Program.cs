using ScoreTracker;

var players = new List<Player>();
var games = new List<Game>();
var scores = new List<Score>();

bool running = true;
while (running)
{
    Console.WriteLine();
    Console.WriteLine("=== Score Tracker ===");
    Console.WriteLine("1. Add player");
    Console.WriteLine("2. Add game");
    Console.WriteLine("3. Add score");
    Console.WriteLine("4. Show leaderboard");
    Console.WriteLine("5. Show player's best");
    Console.WriteLine("6. Quit");
    Console.Write("Choose: ");

    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1": AddPlayer(); break;
        case "2": AddGame(); break;
        case "3": AddScore(); break;
        case "4": ShowLeaderboard(); break;
        case "5": ShowPersonalBest(); break;
        case "6": running = false; break;
        default: Console.WriteLine("Not a valid option."); break;
    }

}

void AddPlayer()
{
    string name = ReadText("Player name: ");
    Player player = new Player { Id = players.Count + 1, Name = name };
    players.Add(player);
    Console.WriteLine($"Added {player}");
}

void AddGame()
{
    string name = ReadText("Game name: ");
    Game game = new Game { Id = games.Count + 1, Name = name };
    games.Add(game);
    Console.WriteLine($"Added {game}");
}

void AddScore()
{

    bool canContinue = true;

    if (players.Count == 0)
    {
        Console.WriteLine("There are no players, add a player first.");
        canContinue = false;
    }

    if (games.Count == 0)
    {
        Console.WriteLine("There are no games, add a game first.");
        canContinue = false;
    }

    if (!canContinue)
        return;

    foreach (var player in players)
    {
        Console.WriteLine(player);
    }

    int playerId = ReadInt("Choose a player ID: ");
    if (!players.Any(p => p.Id == playerId))
    {
        Console.WriteLine("Player ID doesn't exist");
        return;
    }

    foreach (var game in games)
    {
        Console.WriteLine(game);
    }

    int gameId = ReadInt("Choose a game ID: ");
    if (!games.Any(g => g.Id == gameId))
    {
        Console.WriteLine("Game ID doesn't exist");
        return;
    }

    int points = ReadInt("How many points: ");

    try
    {
        Score score = new Score { PlayerId = playerId, GameId = gameId, Points = points };
        scores.Add(score);

        Player player = players.First(p => p.Id == playerId);
        Game game = games.First(g => g.Id == gameId);

        Console.WriteLine($"Added {points} points for {player.Name} on {game.Name}");

    }
    catch (ArgumentException ex)
    {
        Console.WriteLine(ex.Message);
    }


}

void ShowLeaderboard()
{
    foreach (var g in games)
        Console.WriteLine(g);

    int gameId = ReadInt("Game ID: ");
    var game = games.FirstOrDefault(g => g.Id == gameId);
    if (game == null)
    {
        Console.WriteLine("No game with that ID.");
        return;
    }

    var top10 = scores
        .Where(s => s.GameId == gameId)
        .OrderByDescending(s => s.Points)
        .Take(10)
        .ToList();

    if (top10.Count == 0)
    {
        Console.WriteLine($"No scores for {game.Name} yet.");
        return;
    }

    Console.WriteLine($"--- {game.Name}: top 10 ---");
    int rank = 1;
    foreach (var s in top10)
    {
        var player = players.First(p => p.Id == s.PlayerId);
        Console.WriteLine($"{rank}. {player.Name} - {s.Points}");
        rank++;
    }
}

void ShowPersonalBest()
{
    foreach(var p in players)
        Console.WriteLine(p);

    int playerId = ReadInt("Player ID: ");
    var player = players.FirstOrDefault(p => p.Id == playerId);
    if (player == null)
    {
        Console.WriteLine("No player with that ID.");
        return;
    }

    var pScores = scores
        .Where(s => s.PlayerId == playerId).ToList();

    if (!pScores.Any())
    {
        Console.WriteLine($"Player {player.Name} has no scores.");
        return;
    }

    var best = pScores
        .OrderByDescending(s => s.Points)
        .First();
    var game = games.First(g => g.Id == best.GameId);
    
    Console.WriteLine($"{player.Name}'s best is {best.Points} on {game.Name}");
}

int ReadInt(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        if (int.TryParse(Console.ReadLine(), out int number))
            return number;

        Console.WriteLine("Please enter a whole number.");
    }
}

string ReadText(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        string? text = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(text))
            return text.Trim();

        Console.WriteLine("This can't be empty.");
    }
}
