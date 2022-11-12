using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Context;
using Context.DAL;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;
using NUnit.Framework;

namespace Unittests.Seeding;

public class CommonFileSeeding : BaseUnitTests
{
    private GridFSBucket bucket = MonitoringFacade.Instance.MongoDB.Context.bucket;

    [Test]
    public async Task UploadAccidentReport()
    {
        var currentDir = Directory.GetCurrentDirectory() + @"..\..\..\..\..\Utilities\data\";
        var files = Directory.GetFiles(currentDir);
        var fullPathFileName = files.FirstOrDefault(x => x.ToLower().Contains(
            "europ"));
        var fileName = Path.GetFileName(fullPathFileName);
        var fileStream = File.Open(fullPathFileName, FileMode.Open);
        var options = new GridFSUploadOptions
        {
            Metadata = new BsonDocument { { "FileName", fileName }, { "Type", "application/pdf" } }
        };
        using var stream = await bucket.OpenUploadStreamAsync(fileName, options);
        fileStream.CopyTo(stream);
        await stream.CloseAsync();
        CommonFile commonFile = new CommonFile
        {
            Name = fileName,
            FileObjectId = stream.Id
        };

        CommonFile returnval = await MongoUoW.CommonFiles.InsertOneAsync(commonFile);
        Assert.NotNull(returnval);
    }
}