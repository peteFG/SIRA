using System.ComponentModel.DataAnnotations;
using Context;
using Context.DAL;
using Context.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubmittedDataController : ControllerBase
    {
        MongoDBUnitOfWork mongo = MonitoringFacade.Instance.MongoDB;

        /// <summary>
        /// Creates a new submitted data point.
        /// If datapoint is empty a 404 is raised.
        /// </summary>
        /// <param name="submittedData"></param>
        [HttpPost("AddSubmittedData")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubmittedData))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SubmittedData>> AddSubmittedData([Required] [FromBody] SubmittedData submittedData)
        {
            SubmittedData s = await mongo.SubmittedDataPoints.InsertOrUpdateOneAsync(submittedData);

            if (s != null)
            {
                return s;
            }

            return NotFound();
        }


        /// <summary>
        /// Gets all existing submitted datapoints.
        /// If no datapoints are found a 404 is raised.
        /// </summary>
        [HttpGet("ListSubmittedDataPoints")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubmittedData))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<SubmittedData>>> ListSubmittedDataPoints()
        {
            List<SubmittedData> submittedData = mongo.SubmittedDataPoints.FilterBy(x => true).ToList();

            if (submittedData != null)
            {
                return submittedData;
            }

            return NotFound();
        }


        /// <summary>
        /// Gets a submitted datapoint by their id.
        /// Raises a 404 if datapoint is not found.
        /// </summary>
        /// <param name="submittedDataId"></param>
        [HttpGet("GetSubmittedData")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubmittedData))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SubmittedData>> GetSubmittedData([Required] [FromQuery] string submittedDataId)
        {
            var submittedData = await mongo.SubmittedDataPoints.FindByIdAsync(submittedDataId);

            if (submittedData != null)
            {
                return submittedData;
            }

            return NotFound();
        }
    }
}