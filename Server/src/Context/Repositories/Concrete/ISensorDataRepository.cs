using Context.DAL;

namespace Context.Repositories.Concrete;

public interface ISensorDataRepository : IMongoRepository<SensorData>
{
}