using System.Collections;
using System.Globalization;
using Context.DAL;
using Context.DAL.SensorOperations;
using Microsoft.IdentityModel.Tokens;
using Type = Context.DAL.SensorOperations.Type;

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

            CheckForDifferences(currentDataPoint, currentDataPoint.Speed, nextDataPoint.Speed, coordListSpeed,
                Type.Speed);
            CheckForDifferences(currentDataPoint, currentDataPoint.Altitude, nextDataPoint.Altitude, coordListHeight,
                Type.Altitude);
            CheckForOvertakes(currentDataPoint, nextDataPoint, overtakeDistances);
        }

        returnList.AddRange(coordListHeight);
        returnList.AddRange(coordListSpeed);

        var overTakeList = CategoriseOvertakes(overtakeDistances);
        returnList.AddRange(overTakeList);
        return returnList;
    }

    private static void CheckForDifferences(SensorData currentDataPoint, string currentValue, string nextValue,
        List<SensorDataCoord> coordListHeight, Type type)
    {
        bool currentValueValid = double.TryParse(currentValue, NumberStyles.Any,
            CultureInfo.InvariantCulture, out var parsedCurrentValue);
        bool nextValueValid = double.TryParse(nextValue, NumberStyles.Any,
            CultureInfo.InvariantCulture, out var parsedNextValue);

        if (!currentValueValid || !nextValueValid) return;

        var valueDifference = type switch
        {
            Type.Altitude => Math.Abs(Math.Round((decimal)(parsedCurrentValue - parsedNextValue), 2)),
            Type.Speed => Math.Round((decimal)(parsedCurrentValue - parsedNextValue), 2),
            _ => decimal.Zero
        };
        if (Math.Abs(valueDifference) > 1)
        {
            coordListHeight.Add(new SensorDataCoord
            {
                XCoord = currentDataPoint.XCoord,
                YCoord = currentDataPoint.YCoord,
                Difference = valueDifference,
                Type = type
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