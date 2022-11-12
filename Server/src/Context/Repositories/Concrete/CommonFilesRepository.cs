using Context.DAL;
using Context.Settings;

namespace Context.Repositories.Concrete;

public class CommonFilesRepository : MongoRepository<CommonFile>, ICommonFilesRepository
{
    public CommonFilesRepository(MongoDBContext Context) : base(Context)
    {
    }
}