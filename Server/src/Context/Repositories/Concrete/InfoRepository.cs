using Context.DAL;
using Context.Settings;

namespace Context.Repositories.Concrete;

public class InfoRepository : MongoRepository<Info>, IInfoRepository
{
    public InfoRepository(MongoDBContext Context) : base(Context)
    {
    }
}