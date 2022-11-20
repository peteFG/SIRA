using CsvHelper.Configuration.Attributes;

namespace Unittests.Seeding.HelperObjects;

public class DangerZoneMixin
{
    [Name("Strassenname")] public string Strassenname { get; set; }
    [Name("Nummer")] public string Nummer { get; set; }
    [Name("X")] public string XCoord { get; set; }
    [Name("Y")] public string YCoord { get; set; }
}