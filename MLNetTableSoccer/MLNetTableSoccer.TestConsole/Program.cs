using Newtonsoft.Json;
using System;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using CsvHelper;
using System.Globalization;

namespace MLNetTableSoccer.TestConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string inputData = @"D:\softaware\samples-ml\data\softaware.ktool";
            string exportFile = @"D:\softaware\samples-ml\data\tablesoccer-export.csv";


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
                               GoalDifference = roundResult.team1-roundResult.team2,
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
