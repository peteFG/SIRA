using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Context;
using Context.DAL;
using Context.UnitOfWork;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SensorDataController : ControllerBase
    {
        MongoDBUnitOfWork mongo = MonitoringFacade.Instance.MongoDB;


        /// <summary>
        /// Creates a new sensor data pont.
        /// If datapoint is empty a 404 is raised.
        /// </summary>
        /// <param name="sensorData"></param>
        [HttpPost("AddSensorData")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SensorData))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SensorData>> AddSensorData([Required] [FromBody] SensorData sensorData)
        {
            SensorData s = await mongo.SensorDataPoints.InsertOrUpdateOneAsync(sensorData);

            if (s != null)
            {
                return s;
            }

            return NotFound();
        }

        /// <summary>
        /// Reads the OBS CSV into the local database.
        /// If datapoint is empty a 404 is raised.
        /// </summary>
        [HttpPost("ParseSensorData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> ParseSensorData(IFormFile file)
        {
            if (file == null || file.Length == 0 || file.FileName.IsNullOrEmpty())
            {
                return BadRequest("File is empty.");
            }

            if (!Path.GetExtension(file.FileName).Equals(".csv"))
            {
                return BadRequest("File can only be of datatype CSV.");
            }

            var dataPoints = new List<SensorData>();
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
                Delimiter = ";",
                MissingFieldFound = null
            };
            var fileName = Path.GetFileName(file.FileName);
            fileName = fileName[..(fileName.LastIndexOf("o", StringComparison.Ordinal) - 1)];
            //TODO: maybe better filename?
            var rideId = fileName + "\\" + ObjectId.GenerateNewId().ToString();
            using (var reader = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(reader, config))
            {
                var records = csv.GetRecords<SensorDataMixin>().ToList();
                dataPoints.AddRange(records.Select(record => new SensorData
                {
                    RideId = rideId,
                    Date = record.Date,
                    DistanceLeft = record.DistanceLeft, //.IsNullOrEmpty() ? 0 : int.Parse(record.DistanceLeft),
                    DistanceRight = record.DistanceRight, //.IsNullOrEmpty() ? 0 : int.Parse(record.DistanceRight),
                    Measurements = record.Measurements, //.IsNullOrEmpty() ? 0 : int.Parse(record.Measurements),
                    Speed = record.Speed, //.IsNullOrEmpty() ? 0f : float.Parse(record.Speed),
                    Altitude = record.Altitude, //.IsNullOrEmpty() ? 0f : float.Parse(record.Altitude),
                    Timestamp = record.Timestamp,
                    Marked = record.Marked,
                    XCoord = record.XCoord,
                    YCoord = record.YCoord
                }));
            }

            try
            {
                await mongo.SensorDataPoints.InsertManyAsync(dataPoints);
            }
            catch (Exception e)
            {
                return BadRequest("Can not upload entries, file seems to be faulty.\n Error Message:\n" + e);
            }

            return Ok();
        }


        /// <summary>
        /// Gets all existing SensorDataPoints.
        /// If no datapoints are found a 404 is raised.
        /// </summary>
        [HttpGet("ListSensorDataPoints")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SensorData))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<SensorData>>> ListSensorDataPoints()
        {
            List<SensorData> sensorData = mongo.SensorDataPoints.FilterBy(x => true).ToList();

            if (sensorData != null)
            {
                return sensorData;
            }

            return NotFound();
        }


        /// <summary>
        /// Gets a datapoint by their id.
        /// Raises a 404 if datapoint is not found.
        /// </summary>
        /// <param name="sensorDataId"></param>
        [HttpGet("GetSensorData")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SensorData))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SensorData>> GetSensorData([Required] [FromQuery] string sensorDataId)
        {
            var sensorData = await mongo.SensorDataPoints.FindByIdAsync(sensorDataId);

            if (sensorData != null)
            {
                return sensorData;
            }

            return NotFound();
        }
    }
}