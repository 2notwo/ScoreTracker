# Score Tracker

A C# console app for tracking arcade game scores: players, games,
leaderboards and personal bests, saved to a JSON file.

## Features
- Add players, games and scores, with input validation
- Top 10 leaderboard per game (default) and personal bests (LINQ)
- Data saved and loaded as JSON
- Unit tests with xUnit

## How to run
    dotnet run --project ScoreTracker

## How to test
    dotnet test

## Next steps
This is the first version of ArcadeBoard, which turns it into a
Web API with a database and live leaderboards.
