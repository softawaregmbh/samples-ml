using System;
using System.Collections.Generic;
using System.Text;

namespace MLNetTableSoccer.TestConsole
{

    public class Tournament
    {
        public string id { get; set; }
        public string type { get; set; }
        public string _id { get; set; }
        public string name { get; set; }
        public DateTime created { get; set; }
        public object[] groups { get; set; }
        public Player[] players { get; set; }
        public Team[] teams { get; set; }
        public Round[] rounds { get; set; }
        public object[] ko { get; set; }
        public int mode { get; set; }
        public int numRounds { get; set; }
        public Options options { get; set; }
        public int nameType { get; set; }
        public string version { get; set; }
        public bool started { get; set; }
        public long lastTransactionTimestamp { get; set; }
        public int lastTransaction { get; set; }
    }

    public class Options
    {
        public string id { get; set; }
        public string type { get; set; }
        public string _id { get; set; }
        public int numPoints { get; set; }
        public int numSets { get; set; }
        public bool twoAhead { get; set; }
        public bool fastInput { get; set; }
        public int pointsWin { get; set; }
        public int pointsDraw { get; set; }
        public bool fairShuffle { get; set; }
        public Discipline[] disciplines { get; set; }
        public Table[] tables { get; set; }
        public int tablesPerPlay { get; set; }
        public bool hasDisciplines { get; set; }
        public int maxLostGames { get; set; }
        public bool draw { get; set; }
        public bool byeRating { get; set; }
        public Tablemeta[] tableMeta { get; set; }
        public bool multiTableTournament { get; set; }
        public bool useCloseGameRating { get; set; }
        public int closeGameDifference { get; set; }
        public int closeGamePointsWin { get; set; }
        public int closeGamePointsLoose { get; set; }
    }

    public class Discipline1
    {
        public string id { get; set; }
        public string type { get; set; }
        public string _id { get; set; }
        public Set[] sets { get; set; }
        public bool team1Confirmed { get; set; }
        public bool team2Confirmed { get; set; }
        public string playId { get; set; }
    }

    public class Discipline
    {
        public string id { get; set; }
        public string type { get; set; }
        public string _id { get; set; }
        public int numPoints { get; set; }
        public int numSets { get; set; }
        public bool twoAhead { get; set; }
        public bool fastInput { get; set; }
    }

    public class Table
    {
        public string id { get; set; }
        public string type { get; set; }
        public string _id { get; set; }
        public string name { get; set; }
        public bool deactivated { get; set; }
    }

    public class Tablemeta
    {
        public string id { get; set; }
        public string type { get; set; }
        public string _id { get; set; }
        public string header { get; set; }
        public string property { get; set; }
        public bool visible { get; set; }
        public bool ignoreSort { get; set; }
        public bool hidden { get; set; }
        public bool reverse { get; set; }
        public string renderer { get; set; }
        public bool listed { get; set; }
    }

    public class Player
    {
        public string id { get; set; }
        public string type { get; set; }
        public string _id { get; set; }
        public Stats stats { get; set; }
        public Meta meta { get; set; }
        public string _name { get; set; }
        public int weight { get; set; }
        public int startIndex { get; set; }
        public bool removed { get; set; }
        public bool deactivated { get; set; }
    }

    public class Stats
    {
        public string id { get; set; }
        public string type { get; set; }
        public string _id { get; set; }
        public int games { get; set; }
        public int points { get; set; }
        public int won { get; set; }
        public int lost { get; set; }
        public int draws { get; set; }
        public int sets_won { get; set; }
        public int sets_lost { get; set; }
        public int sets_diff { get; set; }
        public int dis_won { get; set; }
        public int dis_lost { get; set; }
        public int dis_draw { get; set; }
        public int dis_diff { get; set; }
        public int goals { get; set; }
        public int goals_in { get; set; }
        public int w_l { get; set; }
        public int goal_diff { get; set; }
        public Opponents opponents { get; set; }
        public int lives { get; set; }
        public int bh1 { get; set; }
        public int bh2 { get; set; }
        public int sb { get; set; }
        public int points_per_game { get; set; }
        public int corrected_points_per_game { get; set; }
        public int place { get; set; }
        public int result_losts { get; set; }
        public int result_wins { get; set; }
        public int placeChangeIndicator { get; set; }
        public bool hidden { get; set; }
    }

    public class Opponents
    {
    }
    
    public class Meta
    {
        public string id { get; set; }
        public string type { get; set; }
        public string _id { get; set; }
        public bool _addedLater { get; set; }
        public int addedInRound { get; set; }
        public bool hadBye { get; set; }
        public int tableIndex { get; set; }
        public bool hasCorrectedValue { get; set; }
        public int baseGames { get; set; }
        public int basePoints { get; set; }
    }

    public class Team
    {
        public string id { get; set; }
        public string type { get; set; }
        public string _id { get; set; }
        public Stats stats { get; set; }
        public Meta meta { get; set; }
        public int startIndex { get; set; }
        public Player[] players { get; set; }
        public bool removed { get; set; }
        public bool deactivated { get; set; }
    }

    public class Round
    {
        public string id { get; set; }
        public string type { get; set; }
        public string _id { get; set; }
        public string name { get; set; }
        public Play[] plays { get; set; }
    }

    public class Play
    {
        public string id { get; set; }
        public string type { get; set; }
        public string _id { get; set; }
        public bool valid { get; set; }
        public Team team1 { get; set; }
        public Team team2 { get; set; }
        public Discipline1[] disciplines { get; set; }
        public long timeStart { get; set; }
        public long timeEnd { get; set; }
        public bool deactivated { get; set; }
        public bool team1bye { get; set; }
        public bool team2bye { get; set; }
        public bool skipped { get; set; }
        public string roundId { get; set; }
        public Table[] tables { get; set; }
    }

    public class Set
    {
        public string id { get; set; }
        public string type { get; set; }
        public string _id { get; set; }
        public int team1 { get; set; }
        public int team2 { get; set; }
    }
}
