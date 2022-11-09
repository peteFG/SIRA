using System.ComponentModel.DataAnnotations;
using Context;
using Context.DAL;
using Context.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DangerZoneController : ControllerBase
    {
        MongoDBUnitOfWork mongo = MonitoringFacade.Instance.MongoDB;


        /// <summary>
        /// Creates a new user.
        /// If a dangerzone is empty a 404 is raised.
        /// </summary>
        /// <param name="dangerZone"></param>
        [HttpPost("AddDangerZone")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DangerZone))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DangerZone>> AddDangerZone([Required] [FromBody] DangerZone dangerZone)
        {
            DangerZone s = await mongo.DangerZones.InsertOrUpdateOneAsync(dangerZone);

            if (s != null)
            {
                return s;
            }

            return NotFound();
        }


        /// <summary>
        /// Gets all existing dangerzones.
        /// If no zones are found a 404 is raised.
        /// </summary>
        [HttpGet("ListDangerZones")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Sample))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<DangerZone>>> ListDangerZones()
        {
            List<DangerZone> dangerZones = mongo.DangerZones.FilterBy(x => true).ToList();

            if (dangerZones != null)
            {
                return dangerZones;
            }

            return NotFound();
        }


        /// <summary>
        /// Gets a dangerZone by their id.
        /// Raises a 404 if dangerZone is not found.
        /// </summary>
        /// <param name="userId"></param>
        [HttpGet("GetDangerZone")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Sample))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DangerZone>> GetDangerZone([Required] [FromQuery] string dangerZoneId)
        {
            var dangerZone = await mongo.DangerZones.FindByIdAsync(dangerZoneId);

            if (dangerZone != null)
            {
                return dangerZone;
            }

            return NotFound();
        }
    }
}