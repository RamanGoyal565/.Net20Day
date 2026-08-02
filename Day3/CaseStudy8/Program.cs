using System;
using System.Collections.Generic;
using System.Linq;

namespace CaseStudy8
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Team lions = new Team("Lions", BuildPlayers("Lions"));
            Team tigers = new Team("Tigers", BuildPlayers("Tigers"));
            Team eagles = new Team("Eagles", BuildPlayers("Eagles"));

            List<Fixture> fixtures = new List<Fixture>
            {
                new Fixture(new DateOnly(2026, 8, 10), lions, tigers, "Stadium A"),
                new Fixture(new DateOnly(2026, 8, 12), lions, eagles, "Stadium B"),
                new Fixture(new DateOnly(2026, 8, 14), tigers, eagles, "Stadium C")
            };

            Tournament tournament = new Tournament("Champions Cup", new List<Team> { lions, tigers, eagles }, fixtures);

            Console.WriteLine("Case Study 8");
            Console.WriteLine("Total number of teams participating: " + tournament.GetTotalTeams());
            PrintFixtures("Fixture of Lions", tournament.GetFixturesByTeam("Lions"));
            PrintPlayers("Players of Tigers", tournament.GetPlayersByTeam("Tigers"));
        }

        private static List<Player> BuildPlayers(string teamName)
        {
            List<Player> players = new List<Player>();
            for (int i = 1; i <= 13; i++)
            {
                players.Add(new Player(teamName + "-P" + i, teamName + " Player " + i));
            }

            return players;
        }

        private static void PrintFixtures(string title, IEnumerable<Fixture> fixtures)
        {
            Console.WriteLine(title);
            foreach (Fixture fixture in fixtures)
            {
                Console.WriteLine("- " + fixture.Date.ToString("yyyy-MM-dd") + " | " + fixture.TeamA.Name + " vs " + fixture.TeamB.Name + " | " + fixture.Venue);
            }
        }

        private static void PrintPlayers(string title, IEnumerable<Player> players)
        {
            Console.WriteLine(title);
            foreach (Player player in players)
            {
                Console.WriteLine("- " + player.Name);
            }
        }
    }

    public class Player
    {
        public Player(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; private set; }
        public string Name { get; private set; }
    }

    public class Team
    {
        public Team(string name, List<Player> players)
        {
            Name = name;
            Players = players;
        }

        public string Name { get; private set; }
        public List<Player> Players { get; private set; }
    }

    public class Fixture
    {
        public Fixture(DateOnly date, Team teamA, Team teamB, string venue)
        {
            Date = date;
            TeamA = teamA;
            TeamB = teamB;
            Venue = venue;
        }

        public DateOnly Date { get; private set; }
        public Team TeamA { get; private set; }
        public Team TeamB { get; private set; }
        public string Venue { get; private set; }
    }

    public class Tournament
    {
        private readonly List<Team> _teams;
        private readonly List<Fixture> _fixtures;

        public Tournament(string name, List<Team> teams, List<Fixture> fixtures)
        {
            Name = name;
            _teams = teams;
            _fixtures = fixtures;
        }

        public string Name { get; private set; }

        public int GetTotalTeams()
        {
            return _teams.Count;
        }

        public List<Fixture> GetFixturesByTeam(string teamName)
        {
            return _fixtures.Where(delegate(Fixture fixture)
            {
                return fixture.TeamA.Name.Equals(teamName, StringComparison.OrdinalIgnoreCase)
                    || fixture.TeamB.Name.Equals(teamName, StringComparison.OrdinalIgnoreCase);
            }).ToList();
        }

        public List<Player> GetPlayersByTeam(string teamName)
        {
            return _teams.Where(delegate(Team team) { return team.Name.Equals(teamName, StringComparison.OrdinalIgnoreCase); })
                .SelectMany(delegate(Team team) { return team.Players; })
                .ToList();
        }
    }
}