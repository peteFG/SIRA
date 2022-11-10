using Context.DAL;
using Context.Settings;

namespace Context.Repositories.Concrete;

public class DangerZoneRepository : MongoRepository<DangerZone>, IDangerZoneRepository
{
    public DangerZoneRepository(MongoDBContext Context) : base(Context)
    {
    }
}