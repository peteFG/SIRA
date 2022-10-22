using Context.DAL;
using Context.Settings;

namespace Context.Repositories.Concrete;

public class SampleRepository : MongoRepository<Sample>, ISampleRepository
{
    public SampleRepository(MongoDBContext Context) : base(Context)
    {
    }
}