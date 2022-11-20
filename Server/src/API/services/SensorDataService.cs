using System.Collections;
using System.Globalization;
using Context.DAL;
using Microsoft.IdentityModel.Tokens;
using Type = Context.DAL.Type;

namespace API.services;

public class SensorDataService
{
    public static readonly SensorDataService Instance = new();
    private List<SensorData> _allSensorDataPoints = new();

    public void SetAllSensorDataPoints(List<SensorData> sensorData)
    {
        _allSensorDataPoints = sensorData;
    }

    public ArrayList GetNotableDifferencesAndOvertakes()
    {
        List<SensorDataCoord> coordListHeight = new List<SensorDataCoord>();
        List<SensorDataCoord> coordListSpeed = new List<SensorDataCoord>();
        List<int> overtakeDistances = new List<int>();
        var returnList = new ArrayList();

        if (_allSensorDataPoints.IsNullOrEmpty())
        {
            return returnList;
        }

        for (int i = 0; i < _allSensorDataPoints.Count - 1; i += 2)
        {
            var currentDataPoint = _allSensorDataPoints[i];
            var nextDataPoint = _allSensorDataPoints[i + 1];

            CheckForHeightDifferences(currentDataPoint, nextDataPoint, coordListHeight);
            CheckForSpeedDifferences(currentDataPoint, nextDataPoint, coordListSpeed);
            CheckForOvertakes(currentDataPoint, nextDataPoint, overtakeDistances);
        }

        // For the mvp we take the first 20 elements of the lists
        // afterwards, server side pagiation is needed

        returnList.AddRange(coordListHeight.Take(20).ToList());
        returnList.AddRange(coordListSpeed.Take(20).ToList());

        var overTakeList = CategoriseOvertakes(overtakeDistances);
        returnList.AddRange(overTakeList);
        return returnList;
    }

    private static void CheckForHeightDifferences(SensorData currentDataPoint, SensorData nextDataPoint,
        List<SensorDataCoord> coordListHeight)
    {
        bool validCurrentAltitude = double.TryParse(currentDataPoint.Altitude, NumberStyles.Any,
            CultureInfo.InvariantCulture, out var parsedValueCurrentAltitude);
        bool validNextAltitude = double.TryParse(nextDataPoint.Altitude, NumberStyles.Any,
            CultureInfo.InvariantCulture, out var parsedValueNextAltitude);

        if (!validCurrentAltitude || !validNextAltitude) return;

        var absoluteAltitudeDifference =
            Math.Abs(Math.Round((decimal) (parsedValueCurrentAltitude - parsedValueNextAltitude), 2));
        if (absoluteAltitudeDifference > 1)
        {
            coordListHeight.Add(new SensorDataCoord
            {
                XCoord = currentDataPoint.XCoord,
                YCoord = currentDataPoint.YCoord,
                Difference = absoluteAltitudeDifference,
                Type = Type.Altitude
            });
        }
    }

    private static void CheckForSpeedDifferences(SensorData currentDataPoint, SensorData nextDataPoint,
        List<SensorDataCoord> coordListSpeed)
    {
        bool validCurrentSpeed = float.TryParse(currentDataPoint.Speed, NumberStyles.Any,
            CultureInfo.InvariantCulture, out var parsedValueCurrentSpeed);
        bool validNextSpeed = float.TryParse(nextDataPoint.Speed, NumberStyles.Any,
            CultureInfo.InvariantCulture, out var parsedValueNextSpeed);

        if (!validCurrentSpeed || !validNextSpeed) return;
        var speedDifference = Math.Round((decimal) (parsedValueCurrentSpeed - parsedValueNextSpeed), 2);
        if (Math.Abs(speedDifference) > 1)
        {
            coordListSpeed.Add(new SensorDataCoord
            {
                XCoord = currentDataPoint.XCoord,
                YCoord = currentDataPoint.YCoord,
                Difference = speedDifference,
                Type = Type.Speed
            });
        }
    }

    private static void CheckForOvertakes(SensorData currentDataPoint, SensorData nextDataPoint,
        List<int> overtakeDistances)
    {
        var validDistanceLeftCurrent =
            int.TryParse(currentDataPoint.DistanceLeft, out var parsedValueCurrentDistance);
        var validDistanceLeftNext = int.TryParse(nextDataPoint.DistanceLeft, out var parsedValueNextDistance);
        if (validDistanceLeftCurrent)
        {
            overtakeDistances.Add(parsedValueCurrentDistance);
        }

        if (validDistanceLeftNext)
        {
            overtakeDistances.Add(parsedValueNextDistance);
        }
    }

    private static List<OvertakingDistance> CategoriseOvertakes(List<int> overtakeDistances)
    {
        var overTakeList = new List<OvertakingDistance>();
        for (int rangeFrom = 0; rangeFrom < overtakeDistances.Max(); rangeFrom += 5)
        {
            var rangeTo = rangeFrom + 5;
            foreach (var overTakeDistance in overtakeDistances)
            {
                if (overTakeDistance <= rangeFrom || overTakeDistance >= rangeTo) continue;
                if (overTakeList.Exists(x => x.RangeFrom.Equals(rangeFrom) && x.RangeTo.Equals(rangeTo)))
                {
                    var overTakeEntry = overTakeList.Find(
                        x => x.RangeFrom.Equals(rangeFrom) && x.RangeTo.Equals(rangeTo));
                    overTakeEntry.Amount += 1;
                }
                else
                {
                    overTakeList.Add(new OvertakingDistance
                    {
                        RangeFrom = rangeFrom,
                        RangeTo = rangeTo,
                        Amount = 1,
                        Range = rangeFrom + "-" + rangeTo
                    });
                }
            }
        }

        return overTakeList;
    }
}