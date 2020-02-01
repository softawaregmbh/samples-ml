using Newtonsoft.Json;
using System;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using CsvHelper;
using System.Globalization;
using MLNetTableSoccerML.Model.GoalDifference;

namespace MLNetTableSoccer.TestConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //await GenerateCsvFromKickertoolData(
            //    @"D:\softaware\samples-ml\data\softaware.ktool",
            //    @"D:\softaware\samples-ml\data\tablesoccer-export.csv");

            TestGoalDifferencePrediction();
        }

        private static void TestGoalDifferencePrediction()
        {
            var game = new ModelInput()
            {
                Team1GoalKeeper = "Philipp",
                Team1Striker = "Michael",
                Team2GoalKeeper = "Roman",
                Team2Striker = "Markus",
                Weekday = "Monday",
                Hour = 12
            };

            ModelOutput predictionResult = ConsumeModel.Predict(game);

            Console.WriteLine("========== Goal Difference Prediction ==========");
            Console.WriteLine($"{game.Team1GoalKeeper} + {game.Team1Striker} vs. {game.Team2GoalKeeper} + {game.Team2Striker}");
            Console.WriteLine($"Model Result: {predictionResult.Score}");

            int roundedDifference = (int)Math.Round(predictionResult.Score);
            int goalsTeam1;
            int goalsTeam2;

            if (predictionResult.Score > 0)
            {
                // Team 1 wins
                goalsTeam1 = 5;
                goalsTeam2 = 5 - roundedDifference;
            }
            else
            {
                // Team 2 wins
                goalsTeam1 = 5 + roundedDifference;
                goalsTeam2 = 5;
            }

            Console.WriteLine($"Expected Goals: {goalsTeam1} : {goalsTeam2}");
        }

        private static async Task GenerateCsvFromKickertoolData(string inputData, string exportFile)
        {
            
            string json = await File.ReadAllTextAsync(inputData);
            var tournament = JsonConvert.DeserializeObject<Tournament>(json);

            var results = (from p in tournament.rounds.SelectMany(r => r.plays)
                           where p.valid
                           let team1 = tournament.teams.Single(t => t.id == p.team1.id)
                           let team2 = tournament.teams.Single(t => t.id == p.team2.id)
                           let roundResult = p.disciplines.First().sets.First()
                           let timestamp = DateTimeOffset.FromUnixTimeMilliseconds(p.timeStart).ToLocalTime()
                           select new
                           {
                               Hour = timestamp.Hour,
                               Weekday = timestamp.DayOfWeek,
                               Team1GoalKeeper = GetPlayerName(tournament, team1, 0),
                               Team1Striker = GetPlayerName(tournament, team1, 1),
                               Team2GoalKeeper = GetPlayerName(tournament, team2, 0),
                               Team2Striker = GetPlayerName(tournament, team2, 1),
                               GoalsTeam1 = roundResult.team1,
                               GoalsTeam2 = roundResult.team2,
                               GoalDifference = roundResult.team1 - roundResult.team2,
                               Result = roundResult.team1 > roundResult.team2 ? "1" : "2"
                           }).ToList();

            using (var writer = new StreamWriter(exportFile))
            using (var csv = new CsvWriter(writer, new CultureInfo("en-US")))
            {
                await csv.WriteRecordsAsync(results);
            }
        }

        private static string GetPlayerName(Tournament tournament, Team team, int playerIndex)
        {
            return tournament.players.Single(pl => pl.id == team.players[playerIndex].id)._name;
        }
    }
}
