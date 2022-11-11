using System.ComponentModel.DataAnnotations;
using Context;
using Context.DAL;
using Context.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommonFilesController : ControllerBase
    {
        MongoDBUnitOfWork mongo = MonitoringFacade.Instance.MongoDB;
        private GridFSBucket bucket = MonitoringFacade.Instance.MongoDB.Context.bucket;

        /// <summary>
        /// Gets all existing files.
        /// If no files are found a 404 is raised.
        /// </summary>
        [HttpGet("ListCommonFiles")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Sample))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<CommonFile>>> ListCommonFiles()
        {
            List<CommonFile> files = mongo.CommonFiles.FilterBy(x => true).ToList();

            if (files != null)
            {
                return files;
            }

            return NotFound();
        }


        /// <summary>
        /// Gets a file by their id.
        /// Raises a 404 if file is not found.
        /// </summary>
        /// <param name="fileId"></param>
        [HttpGet("GetCommonFile")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommonFile))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CommonFile>> GetCommonFile([Required] [FromQuery] string fileId)
        {
            var file = await mongo.CommonFiles.FindByIdAsync(fileId);

            if (file != null)
            {
                return file;
            }

            return NotFound();
        }


        /// <summary>
        /// Upload a common file. Only pdfs are allowed.
        /// If the file is empty, or files other than pdfs are uploaded a 400 is raised.
        /// If the method is successful a 200 is raised.
        /// </summary>
        /// <param name="file"></param>
        [HttpPost("AddCommonFile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CommonFile>> AddCommonFile(IFormFile file)
        {
            if (file == null)
            {
                return BadRequest("Your uploaded file was empty");
            }

            var type = file.ContentType;
            if (!"pdf".Equals(type.Split('/').Last()))
            {
                return BadRequest("Only pdfs are allowed for upload");
            }

            var options = new GridFSUploadOptions
            {
                Metadata = new BsonDocument { { "FileName", file.FileName }, { "Type", type } }
            };

            using var stream = await bucket.OpenUploadStreamAsync(file.FileName, options);
            file.CopyTo(stream);
            await stream.CloseAsync();
            var commonFile = new CommonFile
            {
                FileObjectId = stream.Id,
                Name = file.FileName
            };
            return await mongo.CommonFiles.InsertOneAsync(commonFile);
        }

        /// <summary>
        /// Download a file that is associated with a CommonFiles Object via a searchTerm.
        /// If the file or CommonFile object can not be found a 404 is raised.
        /// If the method is successful a 200 is raised.
        /// </summary>
        /// <param name="searchTerm"></param>
        [HttpGet("DownloadCommonFile/{searchTerm}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<object> DownloadCommonFile([FromRoute] string searchTerm)
        {
            var commonFile = mongo.CommonFiles.FilterBy(x => x.Name.ToLower().Contains(searchTerm.ToLower()))
                .FirstOrDefault();
            if (commonFile == null)
            {
                return NotFound("File containing the search term \"" + searchTerm + "\" could not be found");
            }

            var filter = Builders<GridFSFileInfo<ObjectId>>.Filter.Eq(x => x.Id, commonFile.FileObjectId);
            var searchResult = await bucket.FindAsync(filter);
            var fileEntry = searchResult.FirstOrDefault();
            if (fileEntry == null)
            {
                return NotFound("File containing the search term \"" + searchTerm + "\" could not be found");
            }

            var downloadAsBytesAsync = await bucket.DownloadAsBytesAsync(fileEntry.Id);
            return new FileContentResult(downloadAsBytesAsync, "application/pdf")
            {
                FileDownloadName = fileEntry.Filename
            };
        }

        /// <summary>
        /// Deletes a certain publication and the corresponding file if available.
        /// Only Admins can execute this action. Otherwise a 401 is raised.
        /// If the publication can not be found a 404 is raised.
        /// </summary>
        /// <param name="fileId"></param>
        [HttpDelete("DeleteCommonFile/{fileId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(void))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeletePublication([Required] [FromRoute] string fileId)
        {
            var file = await mongo.CommonFiles.FindByIdAsync(fileId);
            if (file == null)
            {
                return NotFound("File could not be found");
            }

            if (file.FileObjectId != null && !file.FileObjectId.Equals(ObjectId.Empty))
            {
                await bucket.DeleteAsync(file.FileObjectId);
            }

            await mongo.CommonFiles.DeleteByIdAsync(fileId);
            return Ok();
        }
    }
}