using ScoreTracker;

var service = new ScoreService();

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
    var player = service.AddPlayer(name);
    Console.WriteLine($"Added {player}");
}

void AddGame()
{
    string name = ReadText("Game name: ");
    var game = service.AddGame(name);
    Console.WriteLine($"Added {game}");
}

void AddScore()
{
    bool canContinue = true;

    if (service.Players.Count == 0)
    {
        Console.WriteLine("There are no players, add a player first.");
        canContinue = false;
    }

    if (service.Games.Count == 0)
    {
        Console.WriteLine("There are no games, add a game first.");
        canContinue = false;
    }

    if (!canContinue)
        return;

    foreach (var player in service.Players)
    {
        Console.WriteLine(player);
    }

    int playerId = ReadInt("Choose a player ID: ");
    if (!service.Players.Any(p => p.Id == playerId))
    {
        Console.WriteLine("Player ID does not exist.");
        return;
    }
    

    foreach (var game in service.Games)
    {
        Console.WriteLine(game);
    }

    int gameId = ReadInt("Choose a game ID: ");
    if (!service.Games.Any(g => g.Id == gameId))
    {
        Console.WriteLine("Game ID does not exist.");
        return;
    }
    int points = ReadInt("How many points: ");

    try
    {
        service.AddScore(playerId, gameId, points);

        Player player = service.Players.First(p => p.Id == playerId);
        Game game = service.Games.First(g => g.Id == gameId);

        Console.WriteLine($"Added {points} points for {player.Name} on {game.Name}");

    }
    catch (ArgumentException ex)
    {
        Console.WriteLine(ex.Message);
    }
}

void ShowLeaderboard()
{
    foreach (var g in service.Games)
        Console.WriteLine(g);

    int gameId = ReadInt("Game ID: ");
    
    try
    {
        var top = service.GetTopScores(gameId);
        var game = service.Games.First(g => g.Id == gameId);
        
        if (top.Count == 0)
        {
            Console.WriteLine($"No scores for {game.Name} yet.");
            return;
        }

        Console.WriteLine($"--- {game.Name}: top 10 ---");
        int rank = 1;
        foreach (var s in top)
        {
            var player = service.Players.First(p => p.Id == s.PlayerId);
            Console.WriteLine($"{rank}. {player.Name} - {s.Points}");
            rank++;
        }
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine(ex.Message);
    }
}

void ShowPersonalBest()
{
    foreach (var p in service.Players)
        Console.WriteLine(p);

    int playerId = ReadInt("Player ID: ");
    
    try
    {
        var best = service.GetPersonalBest(playerId);
        var player = service.Players.First(p => p.Id == playerId);
        
        if (best == null)
        {
            Console.WriteLine($"Player {player.Name} has no scores.");
            return;
        }
        
        var game = service.Games.First(g => g.Id == best.GameId);
        
        Console.WriteLine($"{player.Name}'s best is {best.Points} on {game.Name}");
        Console.WriteLine("Best per Game: ");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine(ex.Message);
    }
    
    Console.WriteLine("Best Per Game: ");
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
