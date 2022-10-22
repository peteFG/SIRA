using System.Threading.Tasks;
using NUnit.Framework;

namespace Unittests
{
    public class MongoDBTest : BaseUnitTests
    {
        [Test]
        public async Task TestConnect()
        {
            Assert.IsTrue(MongoContext.IsConnected);
        }
    }
}