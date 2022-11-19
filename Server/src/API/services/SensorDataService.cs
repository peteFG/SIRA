using Context.DAL;
using Type = Context.DAL.Type;

namespace API.services;

public class SensorDataService
{
    public static readonly SensorDataService Instance = new SensorDataService();
    private List<SensorData> _allSensorDataPoints = new List<SensorData>();


    public List<SensorDataCoord> GetNotableHeightDifferences(List<SensorData> sensorData)
    {
        List<SensorDataCoord> coordListHeight = new List<SensorDataCoord>();
        List<SensorDataCoord> coordListSpeed = new List<SensorDataCoord>();
        for (int i = 0; i < _allSensorDataPoints.Count; i += 2)
        {
            var currentDataPoint = _allSensorDataPoints[i];
            var nextDataPoint = _allSensorDataPoints[i + 1];
            var result1Altitude = float.TryParse(currentDataPoint.Altitude, out var parsedValueAltitude1);
            var result2Altitude = float.TryParse(nextDataPoint.Altitude, out var parsedValueAltitude2);
            if (result1Altitude && result2Altitude)
            {
                var absoluteAltitudeDifference = Math.Abs(parsedValueAltitude1 - parsedValueAltitude2);
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

            var result1Speed = float.TryParse(currentDataPoint.Speed, out var parsedValueSpeed1);
            var result2Speed = float.TryParse(nextDataPoint.Speed, out var parsedValueSpeed2);
            if (result1Speed && result2Speed)
            {
                var speedDifference = parsedValueSpeed1 - parsedValueSpeed2;
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
        }

        var returnList = new List<SensorDataCoord>();
        returnList.AddRange(coordListHeight);
        returnList.AddRange(coordListSpeed);
        return returnList;

        //TODO
        // check for speed and altitude 
        // push to array when altitude differs by 1
        // push to speed Array when altitude differs by 1

        // create 3rd array for "Überholabstand" that returns a list with
        // all LEFT values grouped by 5 (0-5 cm, 5-10cm.....)
        // and the amount that values appears
        // 0-5 appears x times

        // return value of that method can be SensorDataAnalyse with properties
        // HeightCoord of Type SensorDataCoord
        // SpeedCoord of Type SensorDataCoord
        // OvertakingDistance of Type (to define, example: string that contains 0-5cm and amount prop)

        // The call in the controller returns a list<SensorDataAnalyse> the same as this method
    }

    public void SetAllSensorDataPoints(List<SensorData> sensorData)
    {
        _allSensorDataPoints = sensorData;
    }
}