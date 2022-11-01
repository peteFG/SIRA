using Context.DAL;
using Context.Settings;

namespace Context.Repositories.Concrete;

public class SubmittedDataRepository : MongoRepository<SubmittedData>, ISubmittedDataRepository
{
    public SubmittedDataRepository(MongoDBContext Context) : base(Context)
    {
    }
}