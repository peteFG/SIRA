﻿using Context.Repositories.Concrete;
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
            var builder = new ConfigurationBuilder().SetBasePath(Constants.CurrentFolder);
            if (File.Exists(Constants.CurrentFolder + "\\appsettings.local.json"))
                builder.AddJsonFile("appsettings.local.json");
            else
                builder.AddJsonFile("appsettings.json");

            MongoDBSettings settings = builder.Build().GetSection("MongoDbSettings").Get<MongoDBSettings>();
            MongoDBContext context = new MongoDBContext(settings);
            Context = context;
        }

        public ISampleRepository Samples => new SampleRepository(Context);
        public IInfoRepository Infos => new InfoRepository(Context);
        public ISubmittedDataRepository SubmittedDataPoints => new SubmittedDataRepository(Context);
        public ISensorDataRepository SensorDataPoints => new SensorDataRepository(Context);
        public IDangerZoneRepository DangerZones => new DangerZoneRepository(Context);
        public ICommonFilesRepository CommonFiles => new CommonFilesRepository(Context);
    }
}