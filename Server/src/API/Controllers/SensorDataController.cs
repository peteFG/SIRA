using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Context;
using Context.DAL;
using Context.UnitOfWork;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> ParseSensorData()
        {
            var dataPoints = new List<SensorData>();
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ";",
                //MissingFieldFound = null
            };
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                @"..\..\..\..\Utilities\data\2021-12-15T10.37.17-88c5.obsdata.csv");
            using (var reader = new StreamReader(Path.GetFullPath(filePath)))
            using (var csv = new CsvReader(reader, config))
            {
                //TODO: filename abspeichern -> id für Fahrt
                var records = csv.GetRecords<SensorDataMixin>().ToList();
                dataPoints.AddRange(records.Select(record => new SensorData
                {
                    //ID
                    Altitude = record.Altitude,
                    Date = record.Date,
                    DistanceLeft = record.DistanceLeft,
                    DistanceRight = record.DistanceRight,
                    Measurements = record.Measurements,
                    Speed = record.Speed,
                    Timestamp = record.Timestamp,
                    Marked = record.Marked,
                    XCoord = record.XCoord,
                    YCoord = record.YCoord
                }));
            }

            await mongo.SensorDataPoints.InsertManyAsync(dataPoints);

            if (!dataPoints.IsNullOrEmpty())
            {
                return Ok();
            }

            return NotFound();
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