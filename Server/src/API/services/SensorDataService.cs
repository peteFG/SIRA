﻿using System.Collections;
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

    public ArrayList GetNotableSpeedDifferences()
    {
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
        }

        coordListSpeed = RemoveOutliers(coordListSpeed);
        returnList.AddRange(coordListSpeed);

        var overTakeList = CategoriseOvertakes(overtakeDistances);
        returnList.AddRange(overTakeList);
        return returnList;
    }


    private static void CheckForDifferences(SensorData currentDataPoint, string currentValue, string nextValue,
        List<SensorDataCoord> coordList, Type type)
    {
        bool currentValueValid = double.TryParse(currentValue, NumberStyles.Any,
            CultureInfo.InvariantCulture, out var parsedCurrentValue);
        bool nextValueValid = double.TryParse(nextValue, NumberStyles.Any,
            CultureInfo.InvariantCulture, out var parsedNextValue);

        if (!currentValueValid || !nextValueValid) return;

        var valueDifference = type switch
        {
            Type.Speed => Math.Round((decimal) (parsedCurrentValue - parsedNextValue), 2),
            _ => decimal.Zero
        };
        if (type == Type.Speed && Math.Abs(valueDifference) > 3)
        {
            coordList.Add(new SensorDataCoord
            {
                XCoord = currentDataPoint.XCoord,
                YCoord = currentDataPoint.YCoord,
                Difference = valueDifference,
                Type = type
            });
        }
    }

    private static List<SensorDataCoord> RemoveOutliers(List<SensorDataCoord> coordList)
    {
        decimal coordDifferenceSum = coordList.Sum(coord => coord.Difference);

        var averageDifference = coordDifferenceSum / coordList.Count;
        coordList = coordList.OrderBy(coord => Math.Abs(averageDifference - coord.Difference)).TakeWhile(coord =>
            Math.Abs(averageDifference) - Math.Abs(coord.Difference) <= averageDifference * new decimal(1.5)).ToList();
        //Reworked
        //coordList = coordList.OrderBy(coord => Math.Abs(averageDifference - coord.Difference)).Take(20).ToList();


        return coordList;
    }

    public ArrayList GetOvertakes()
    {
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

            CheckForOvertakes(currentDataPoint, nextDataPoint, overtakeDistances);
        }

        var overTakeList = CategoriseOvertakes(overtakeDistances);
        returnList.AddRange(overTakeList);
        return returnList;
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
        if (overtakeDistances.IsNullOrEmpty())
        {
            return new List<OvertakingDistance>();
        }

        for (int rangeFrom = 0; rangeFrom < overtakeDistances.Max(); rangeFrom += 50)
        {
            var rangeTo = rangeFrom + 50;
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

    public List<DateTime> GetMinimumAndMaximumDate()
    {
        if (_allSensorDataPoints.IsNullOrEmpty())
        {
            return new List<DateTime>();
        }

        var sortedPoints = _allSensorDataPoints.Where(x => DateTime.Parse(x.Date) > new DateTime(2000, 1, 1)).ToList();
        sortedPoints.Sort((x, y) => DateTime.Compare(DateTime.Parse(x.Date), DateTime.Parse(y.Date)));
        return new List<DateTime>
        {
            DateTime.Parse(sortedPoints[sortedPoints.Count - 1].Date),
            DateTime.Parse(sortedPoints[0].Date)
        };
    }
}