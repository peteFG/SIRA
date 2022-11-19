namespace Context.DAL;

public class SensorDataAnalysis
{
    // return value of that method can be SensorDataAnalyse with properties
    // HeightCoord of Type SensorDataCoord
    // SpeedCoord of Type SensorDataCoord
    // OvertakingDistance of Type (to define, example: string that contains 0-5cm and amount prop)

    public SensorDataCoord HeightCoord { get; set; }
    public SensorDataCoord SpeedCoord { get; set; }
    public string OvertakingDistance { get; set; }
}