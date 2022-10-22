using System.Threading.Tasks;
using Context.Settings;
using Context.UnitOfWork;
using NUnit.Framework;
using Serilog;
using Utilities;

namespace Unittests
{
    public class BaseUnitTests
    {
        protected ILogger log = Logger.ContextLog<BaseUnitTests>();
        protected MongoDBUnitOfWork MongoUoW = null;
        protected MongoDBContext MongoContext = null;

        [OneTimeSetUp]
        public async Task Setup()
        {
            Logger.InitLogger();

            MongoUoW = new MongoDBUnitOfWork();
            MongoContext = MongoUoW.Context;
        }

        [Test]
        public void MyFirstLog()
        {
            log.Information("My first try");
            Assert.IsTrue(true);
        }
    }
}