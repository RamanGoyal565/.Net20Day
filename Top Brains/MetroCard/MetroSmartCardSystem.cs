using System;
using System.Collections.Generic;
using System.Linq;
// Do not modify
class TravelSummary {
    public long lastEntryStation;
    public long lastExitStation;
    public long lastEntryTime;
    public long lastExitTime;
    public double totalFarePaid;
    public int totalTrips;
    public double averageFarePerTrip;
}
// Do not modify
class Commuter {
    public int cardNumber;
    public String commuterName;
    public String commuterType; // "SENIOR", "ADULT", "STUDENT", "CHILD"
    public TravelSummary travelSummary;
}
// Do not modify
public class Station {
    public int stationId;
    public String stationName;
    public int zone; // 1, 2, or 3 (different fare zones)
    public double latitude;
    public double longitude;
}
// Do not modify
interface MetroOperations {
    void issueCard(int cardNumber, String commuterName, String commuterType);
    bool tapIn(int cardNumber, int stationId, long epochTime);
    bool tapOut(int cardNumber, int stationId, long epochTime);
    Commuter getCommuterInfo(int cardNumber);
    List<Double> fareHistory(int cardNumber);
    Dictionary<String, Double> getZoneWiseRevenue(long startTime, long endTime);
    List<String> getFrequentRoute(int cardNumber);
    double getDailyPassSavings(int cardNumber, long date);
}
class Journey
{
    public int EntryStation;
    public long EntryTime;
    public Journey(int station, long time)
    {
        EntryStation = station;
        EntryTime = time;
    }
}
class TripRecord
{
    public int EntryStation;
    public int ExitStation;
    public long EntryTime;
    public long ExitTime;
    public double Fare;
    public string Route;
    public string ZonePair;
    public long Day;
}
class MetroCardManager : MetroOperations
{
    private Dictionary<int, Station> stations = new Dictionary<int, Station>();
    private Dictionary<int, Commuter> commuters = new Dictionary<int, Commuter>();
    private Dictionary<int, Journey> activeJourneys = new Dictionary<int, Journey>();
    private Dictionary<int, List<double>> fareHistories = new Dictionary<int, List<double>>();
    private Dictionary<int, List<TripRecord>> tripHistory = new Dictionary<int, List<TripRecord>>();
    private Dictionary<int, Dictionary<long, double>> dailyFare = new Dictionary<int, Dictionary<long, double>>();
    private double baseFare;
    private double perKmRate;
    private double maxDailyCap;
    public MetroCardManager(List<Station> stations, double baseFare, double perKmRate, double maxDailyCap)
    {
        foreach (var s in stations)
            this.stations[s.stationId] = s;
        this.baseFare=baseFare;
        this.maxDailyCap=maxDailyCap;
        this.perKmRate=perKmRate;    
    }
    private double ApplyDiscount(double fare, string type)
    {
        switch (type)
        {
            case "SENIOR":
                return fare * 0.5;
            case "STUDENT":
                return fare * 0.75;
            case "CHILD":
                return fare * 0.25;
            default:
                return fare;
        }
    }
    private long GetDay(long epoch)
    {
        DateTime dt = DateTimeOffset.FromUnixTimeMilliseconds(epoch).UtcDateTime;
        return dt.Year * 10000L + dt.Month * 100 + dt.Day;
    }
    private double CalculateDistance(Station s1,Station s2)
    {
        double lat1 = Math.PI * s1.latitude / 180.0;
        double lon1 = Math.PI * s1.longitude / 180.0;
        double lat2 = Math.PI * s2.latitude / 180.0;
        double lon2 = Math.PI * s2.longitude / 180.0;
        double dlat = lat2 - lat1;
        double dlon = lon2 - lon1;
        double a = Math.Pow(Math.Sin(dlat / 2), 2) + Math.Cos(lat1) * Math.Cos(lat2) * Math.Pow(Math.Sin(dlon / 2), 2);
        double c = 2 * Math.Asin(Math.Sqrt(a));
        return 6371 * c;
    }
    public void issueCard(int cardNumber, String commuterName, String commuterType)
    {
        if (commuters.ContainsKey(cardNumber))
            return;
        commuters[cardNumber] = new Commuter
        {
            cardNumber = cardNumber,
            commuterName = commuterName,
            commuterType = commuterType,
            travelSummary = new TravelSummary()
        };
        fareHistories[cardNumber] = new List<double>();
        tripHistory[cardNumber] = new List<TripRecord>();
        dailyFare[cardNumber] = new Dictionary<long, double>();
    }
    public bool tapIn(int cardNumber, int stationId, long epochTime)
    {
        if (!commuters.ContainsKey(cardNumber))
            return false;
        if (!stations.ContainsKey(stationId))
            return false;
        if (activeJourneys.ContainsKey(cardNumber))
            return false;
        activeJourneys[cardNumber] = new Journey(stationId, epochTime);
        var ts = commuters[cardNumber].travelSummary;
        ts.lastEntryStation = stationId;
        ts.lastEntryTime = epochTime;
        return true;
    }
    public bool tapOut(int cardNumber, int stationId, long epochTime)
    {
        if (!commuters.ContainsKey(cardNumber))
            return false;
        if (!stations.ContainsKey(stationId))
            return false;
        if (!activeJourneys.ContainsKey(cardNumber))
            return false;
        Journey journey = activeJourneys[cardNumber];
        if (journey.EntryStation == stationId)
            return false;
        if (epochTime <= journey.EntryTime)
            return false;
        Station entry = stations[journey.EntryStation];
        Station exit = stations[stationId];
        double distance = CalculateDistance(entry, exit);
        double duration = (epochTime - journey.EntryTime) / (1000.0 * 60.0);
        double fare;
        if (duration > 120)
            fare = baseFare * 3;
        else
            fare = baseFare + distance * perKmRate;
        fare = ApplyDiscount(fare, commuters[cardNumber].commuterType);
        long day = GetDay(journey.EntryTime);
        if (!dailyFare[cardNumber].ContainsKey(day))
            dailyFare[cardNumber][day] = 0;
        double paidToday = dailyFare[cardNumber][day];
        if (paidToday >= maxDailyCap)
        {
            fare = 0;
        }
        else if (paidToday + fare > maxDailyCap)
        {
            fare = maxDailyCap - paidToday;
        }
        dailyFare[cardNumber][day] += fare;
        TravelSummary summary = commuters[cardNumber].travelSummary;
        summary.lastExitStation = stationId;
        summary.lastExitTime = epochTime;
        summary.totalFarePaid += fare;
        summary.totalTrips++;
        summary.averageFarePerTrip =summary.totalFarePaid / summary.totalTrips;
        fareHistories[cardNumber].Add(fare);
        TripRecord record = new TripRecord
        {
            EntryStation = entry.stationId,
            ExitStation = exit.stationId,
            EntryTime = journey.EntryTime,
            ExitTime = epochTime,
            Fare = fare,
            Route =entry.stationName +" to " +exit.stationName,
            ZonePair ="Zone" + entry.zone + "-Zone" + exit.zone,
            Day = day
        };
        tripHistory[cardNumber].Add(record);
        activeJourneys.Remove(cardNumber);
        return true;
    }
    public Commuter getCommuterInfo(int cardNumber)
    {
        if (!commuters.ContainsKey(cardNumber))
            return null;
        return commuters[cardNumber];
    }
    public List<double> fareHistory(int cardNumber)
    {
        if (!fareHistories.ContainsKey(cardNumber))
            return new List<double>();
        return fareHistories[cardNumber]
            .TakeLast(5)
            .OrderByDescending(x => x)
            .ToList();
    }
    public Dictionary<string, double> getZoneWiseRevenue(long startTime, long endTime)
    {
        Dictionary<string, double> revenue = new Dictionary<string, double>();
        foreach (var trips in tripHistory.Values)
        {
            foreach (var trip in trips)
            {
                if (trip.ExitTime >= startTime && trip.ExitTime <= endTime)
                {
                    if (!revenue.ContainsKey(trip.ZonePair))
                        revenue[trip.ZonePair] = 0;
                    revenue[trip.ZonePair] += trip.Fare;
                }
            }
        }
        return revenue.OrderByDescending(x => x.Value)
                .ToDictionary(x => x.Key, x => x.Value);
    }
    public List<string> getFrequentRoute(int cardNumber)
    {
        if (!tripHistory.ContainsKey(cardNumber))
            return new List<string>();
        Dictionary<string, int> routes = new Dictionary<string, int>();
        foreach (TripRecord trip in tripHistory[cardNumber])
        {
            if (!routes.ContainsKey(trip.Route))
                routes[trip.Route] = 0;
            routes[trip.Route]++;
        }
        return routes
            .OrderByDescending(x => x.Value)
            .ThenBy(x => x.Key).Take(3)
            .Select(x => x.Key).ToList();
    }
    public double getDailyPassSavings(int cardNumber, long date)
    {
        if (!dailyFare.ContainsKey(cardNumber))
            return 0;
        if (!dailyFare[cardNumber].ContainsKey(date))
            return 0;
        double spent = dailyFare[cardNumber][date];
        double passCost = maxDailyCap * 0.8;
        return Math.Max(0, spent - passCost);
    }
}