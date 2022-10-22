using Context.DAL;

namespace Context.Repositories.Concrete;

public interface ISampleRepository : IMongoRepository<Sample>
{
}