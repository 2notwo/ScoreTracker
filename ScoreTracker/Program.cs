using ScoreTracker;
using System.ComponentModel;

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
    }

}

void AddPlayer() => Console.WriteLine("TODO: add player ");
void AddGame() => Console.WriteLine("TODO: add game");
void AddScore() => Console.WriteLine("TODO: add score");
void ShowLeaderboard() => Console.WriteLine("TODO: leaderboard");
void ShowPersonalBest() => Console.WriteLine("TODO: personal best");
