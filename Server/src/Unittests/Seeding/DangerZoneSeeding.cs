using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Context.DAL;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.IdentityModel.Tokens;
using NUnit.Framework;
using Unittests.Seeding.HelperObjects;

namespace Unittests.Seeding;

public class DangerZoneSeeding : BaseUnitTests
{
    [Test]
    public async Task CreateFistDangerZone()
    {
        DangerZone zone = new DangerZone();

        zone.XCoord = "47.0715349";
        zone.YCoord = "15.4366008";
        zone.ToolTipText = "Badgasse 4";

        DangerZone returnval = await MongoUoW.DangerZones.InsertOneAsync(zone);

        Assert.NotNull(returnval);
    }

    [Test]
    public async Task AddDangerZonesFromDirectory()
    {
        var currentDir = Directory.GetCurrentDirectory() + @"..\..\..\..\..\Utilities\data\";
        var files = Directory.GetFiles(currentDir);
        var fullPath = Path.GetFullPath(files.FirstOrDefault(x => x.ToLower().Contains(
            "Gefahrenstellen_Graz.csv".ToLower())));

        Assert.True(Path.GetExtension(fullPath).Equals(".csv"));
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            Delimiter = ";",
            MissingFieldFound = null
        };

        var zones = new List<DangerZone>();
        using (var reader = new StreamReader(fullPath))
        using (var csv = new CsvReader(reader, config))
        {
            var records = csv.GetRecords<DangerZoneMixin>().ToList();
            if (records.IsNullOrEmpty()) throw new Exception("records are empty");
            zones.AddRange(records.Select(record => new DangerZone
            {
                ToolTipText = record.Strassenname + " " + record.Nummer,
                XCoord = record.XCoord,
                YCoord = record.YCoord
            }));
        }

        Assert.False(zones.IsNullOrEmpty());
        await MongoUoW.DangerZones.InsertManyAsync(zones);
    }
}