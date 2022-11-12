using System.ComponentModel.DataAnnotations;
using Context;
using Context.DAL;
using Context.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InfoController : ControllerBase
    {
        MongoDBUnitOfWork mongo = MonitoringFacade.Instance.MongoDB;


        /// <summary>
        /// Creates a new info.
        /// If info is empty a 404 is raised.
        /// </summary>
        /// <param name="info"></param>
        [HttpPost("AddInfo")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Info))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Info>> AddInfo([Required] [FromBody] Info info)
        {
            Info s = await mongo.Infos.InsertOrUpdateOneAsync(info);

            if (s != null)
            {
                return s;
            }

            return NotFound();
        }


        /// <summary>
        /// Gets all existing Infos.
        /// If no Infos are found a 404 is raised.
        /// </summary>
        [HttpGet("ListLawInfos")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Info))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Info>>> ListLawInfos()
        {
            List<Info> Info = mongo.Infos.FilterBy(x => x.Category == Category.Law).ToList();

            if (Info != null)
            {
                return Info;
            }

            return NotFound();
        }


        /// <summary>
        /// Gets all existing Infos.
        /// If no Infos are found a 404 is raised.
        /// </summary>
        [HttpGet("ListFirstAidInfos")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Info))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Info>>> ListFirstAidInfos()
        {
            List<Info> Info = mongo.Infos.FilterBy(x => x.Category == Category.FirstAid).ToList();

            if (Info != null)
            {
                return Info;
            }

            return NotFound();
        }


        /// <summary>
        /// Gets all existing Infos.
        /// If no Infos are found a 404 is raised.
        /// </summary>
        [HttpGet("ListInfos")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Info))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Info>>> ListInfos()
        {
            List<Info> Info = mongo.Infos.FilterBy(x => true).ToList();

            if (Info != null)
            {
                return Info;
            }

            return NotFound();
        }


        /// <summary>
        /// Gets an Info by their id.
        /// Raises a 404 if info is not found.
        /// </summary>
        /// <param name="infoId"></param>
        [HttpGet("GetInfo")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Info))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Info>> GetInfo([Required] [FromQuery] string infoId)
        {
            var Info = await mongo.Infos.FindByIdAsync(infoId);

            if (Info != null)
            {
                return Info;
            }

            return NotFound();
        }
    }
}