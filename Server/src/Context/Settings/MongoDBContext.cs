using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using MongoDB.Entities;
using Serilog;
using Utilities;

namespace Context.Settings
{
    public class MongoDBContext
    {
        ILogger log = Logger.ContextLog<MongoDBContext>();

        public IMongoDatabase DataBase { get; set; }
        public GridFSBucket bucket { get; set; }

        public Boolean IsConnected => DataBase != null;

        public MongoDBContext(MongoDBSettings settings)
        {
            log.Debug("Connecting to Database");


            // DataBase = new MongoClient(clientsettings).GetDatabase(settings.DatabaseName);

            Task tks = Connect(settings);
            tks.Wait();
        }

        public async Task Connect(MongoDBSettings settings)
        {
            MongoClientSettings clientsettings = new MongoClientSettings();
            clientsettings.Server = new MongoServerAddress(settings.Server, settings.Port);


            if (!String.IsNullOrEmpty(settings.Username) && !String.IsNullOrEmpty(settings.Password))
            {
                clientsettings.Credential =
                    MongoCredential.CreateCredential("admin", settings.Username, settings.Password);
            }


            await DB.InitAsync(settings.DatabaseName, clientsettings);

            DataBase = DB.Database(settings.DatabaseName);

            bucket = new GridFSBucket(DataBase);
            if (DataBase != null)
            {
                log.Information("Successfully connected to Mongo DB " + settings.Server + ":" + settings.Port);
            }
            else
            {
                log.Fatal("Could not connect to Mongo DB " + settings.Server + ":" + settings.Port);
            }
        }
    }
}