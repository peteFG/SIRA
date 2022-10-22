using Context.DAL;
using Context.UnitOfWork;

namespace Context
{
    public class MonitoringFacade
    {
        public MongoDBUnitOfWork MongoDB { get; private set; }


        private MonitoringFacade()
        {
            MongoDB = new MongoDBUnitOfWork();
        }

        private static MonitoringFacade _instance;

        public static MonitoringFacade Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MonitoringFacade();
                }

                return _instance;
            }
        }

        public async Task Init()
        {
            // DataAcquistion.Start();
        }
    }
}