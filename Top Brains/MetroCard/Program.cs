public class Program
{
    static string[] SplitKeepingQuotes(string input)
    {
        List<string> result = new List<string>();
        bool inQuote = false;
        string current = "";
        foreach (char c in input)
        {
            if (c == '"')
            {
                inQuote = !inQuote;
                continue;
            }
            if (c == ' ' && !inQuote)
            {
                if (current.Length > 0)
                {
                    result.Add(current);
                    current = "";
                }
            }
            else
            {
                current += c;
            }
        }
        if (current.Length > 0)
            result.Add(current);
        return result.ToArray();
    }
    public static void Main(string[] args)
    {
        string[] first = Console.ReadLine().Split();
        int numberOfRequests = int.Parse(first[0]);
        double baseFare = double.Parse(first[1]);
        double perKmRate = double.Parse(first[2]);
        double maxDailyCap = double.Parse(first[3]);
        int stationCount = int.Parse(Console.ReadLine());
        List<Station> stations = new List<Station>();
        for (int i = 0; i < stationCount; i++)
        {
            string[] s = Console.ReadLine().Split();
            stations.Add(new Station
            {
                stationId = int.Parse(s[0]),
                stationName = s[1],
                zone = int.Parse(s[2]),
                latitude = double.Parse(s[3]),
                longitude = double.Parse(s[4])
            });
        }
        MetroCardManager manager =new MetroCardManager(stations,baseFare,perKmRate,maxDailyCap);
        for (int i = 0; i < numberOfRequests; i++)
        {
            Console.WriteLine("Enter request:"+i+1);
            string line = Console.ReadLine();
            string[] cmd = SplitKeepingQuotes(line);
            switch (cmd[0])
            {
                case "issueCard":
                {
                    int card = int.Parse(cmd[1]);
                    string name = cmd[2];
                    string type = cmd[3];
                    manager.issueCard(card, name, type);
                    break;
                }
                case "tapIn":
                {
                    Console.WriteLine(manager.tapIn(int.Parse(cmd[1]),int.Parse(cmd[2]),long.Parse(cmd[3])).ToString().ToLower());
                    break;
                }
                case "tapOut":
                {
                    Console.WriteLine(manager.tapOut(int.Parse(cmd[1]),int.Parse(cmd[2]),long.Parse(cmd[3])).ToString().ToLower());
                    break;
                }
                case "commuterInfo":
                {
                    Commuter c =manager.getCommuterInfo(int.Parse(cmd[1]));
                    if (c != null)
                    {
                        TravelSummary t = c.travelSummary;
                        Console.WriteLine($"{c.cardNumber} {c.commuterName} {c.commuterType} {t.lastEntryStation} {t.lastExitStation} {t.lastEntryTime} {t.lastExitTime} {t.totalFarePaid:F2} {t.totalTrips} {t.averageFarePerTrip:F2}");
                    }
                    break;
                }
                case "fareHistory":
                {
                    List<double> fares =manager.fareHistory(int.Parse(cmd[1]));
                    foreach (double fare in fares)
                        Console.WriteLine(fare.ToString("F2"));
                    break;
                }
                case "zoneRevenue":
                {
                    var revenue =manager.getZoneWiseRevenue(long.Parse(cmd[1]),long.Parse(cmd[2]));
                    foreach (var item in revenue)
                    {
                        Console.WriteLine($"{item.Key}:{item.Value:F2}");
                    }
                    break;
                }
                case "frequentRoute":
                {
                    List<string> routes =manager.getFrequentRoute(int.Parse(cmd[1]));
                    foreach (string r in routes)
                        Console.WriteLine(r);
                    break;
                }
                case "dailySavings":
                {
                    Console.WriteLine(manager.getDailyPassSavings(int.Parse(cmd[1]),long.Parse(cmd[2])).ToString("F2"));
                    break;
                }
            }
        }
    }
}