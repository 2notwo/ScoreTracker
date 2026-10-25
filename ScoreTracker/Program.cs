using ScoreTracker;

Player jakub = new Player{Id = 1, Name="Jakub"};
Game pacman = new Game{Id=1, Name="Pac-Man"};
Score score = new Score { PlayerId = jakub.Id, GameId = pacman.Id, Points = 4500 };

Console.WriteLine(jakub);
Console.WriteLine(pacman);
Console.WriteLine($"{score.Points} points on {score.Date:d}");