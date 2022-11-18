using System.Threading.Tasks;
using Context.DAL;
using NUnit.Framework;

namespace Unittests.Seeding;

public class DangerZoneSeeding : BaseUnitTests
{
    [Test]
    public async Task CreateFistDangerZone()
    {
        DangerZone zone = new DangerZone();

        zone.XCoord = "47.1159973";
        zone.YCoord = "15.4301136";
        zone.ToolTipText = "Popelkaring 54";

        DangerZone returnval = await MongoUoW.DangerZones.InsertOneAsync(zone);

        Assert.NotNull(returnval);
    }
}