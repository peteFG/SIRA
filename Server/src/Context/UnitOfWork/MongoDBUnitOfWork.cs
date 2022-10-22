using Context.Repositories.Concrete;
using Context.Settings;
using Microsoft.Extensions.Configuration;
using Utilities;

namespace Context.UnitOfWork
{
    public class MongoDBUnitOfWork
    {
        public MongoDBContext Context { get; private set; }

        public MongoDBUnitOfWork()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Constants.CurrentFolder)
                .AddJsonFile("appsettings.json");

            MongoDBSettings settings = builder.Build().GetSection("MongoDbSettings").Get<MongoDBSettings>();
            MongoDBContext context = new MongoDBContext(settings);
            Context = context;
        }

        public ISampleRepository Samples => new SampleRepository(Context);
    }
}