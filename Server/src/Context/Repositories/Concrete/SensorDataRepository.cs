using Context.DAL;
using Context.Settings;

namespace Context.Repositories.Concrete;

public class SensorDataRepository : MongoRepository<SensorData>, ISensorDataRepository
{
    public SensorDataRepository(MongoDBContext Context) : base(Context)
    {
    }
}