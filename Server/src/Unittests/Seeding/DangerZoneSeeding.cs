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

        zone.XCoord = "47.0715349";
        zone.YCoord = "15.4366008";
        zone.ToolTipText = "Badgasse 4";

        DangerZone returnval = await MongoUoW.DangerZones.InsertOneAsync(zone);

        Assert.NotNull(returnval);
    }
}