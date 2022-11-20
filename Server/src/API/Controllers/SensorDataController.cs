using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using API.services;
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
        /// Reads an OBS CSV file into the local database.
        /// If file is empty or faulty a 400 is raised.
        /// </summary>
        /// <param name="file"></param>
        [HttpPost("ParseSensorData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
                ShouldSkipRecord = x => x.Row[0].StartsWith("OBSDataFormat"),
                HasHeaderRecord = true,
                Delimiter = ";",
                MissingFieldFound = null
            };
            var fileName = Path.GetFileName(file.FileName);
            if (fileName.Contains('-'))
            {
                fileName = fileName[..fileName.LastIndexOf("-", StringComparison.Ordinal)];
            }

            var rideId = ObjectId.GenerateNewId().ToString();
            if (fileName.Length >= 18) rideId = fileName + "_" + rideId;
            try
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                using (var csv = new CsvReader(reader, config))
                {
                    var records = csv.GetRecords<SensorDataMixin>().ToList();
                    if (records.IsNullOrEmpty()) throw new Exception("records are empty");
                    var uploadTimeStamp = DateTime.Now;
                    dataPoints.AddRange(records.Select(record => new SensorData
                    {
                        UploadTimeStamp = uploadTimeStamp,
                        RideId = rideId,
                        Date = record.Date,
                        DistanceLeft = record.DistanceLeft, //.IsNullOrEmpty() ? 0 : int.Parse(record.DistanceLeft),
                        DistanceRight = record.DistanceRight, //.IsNullOrEmpty() ? 0 : int.Parse(record.DistanceRight),
                        Measurements = record.Measurements, //.IsNullOrEmpty() ? 0 : int.Parse(record.Measurements),
                        Speed = record.Speed, //.IsNullOrEmpty() ? 0f : float.Parse(record.Speed),
                        Altitude = record.Altitude, //.IsNullOrEmpty() ? 0f : float.Parse(record.Altitude),
                        Timestamp = record.Timestamp,
                        ButtonPressed = record.ButtonPressed,
                        XCoord = record.XCoord,
                        YCoord = record.YCoord
                    }));
                }

                await mongo.SensorDataPoints.InsertManyAsync(dataPoints);
            }
            catch (Exception e)
            {
                return BadRequest("Can not upload entries, file seems to be faulty.\nError Message:\n" + e);
            }

            return Ok(dataPoints.Count + " entries have been added to the database.");
        }


        /// <summary>
        /// Loads all SensorDataPoints into singleton.
        /// If no datapoints are found a 404 is raised.
        /// </summary>
        [HttpGet("LoadAllSensorDataPoints")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Task<ActionResult> LoadAllSensorDataPoints()
        {
            List<SensorData> sensorData = mongo.SensorDataPoints.FilterBy(x => true).ToList();

            if (sensorData != null)
            {
                SensorDataService.Instance.SetAllSensorDataPoints(sensorData);
                return Task.FromResult<ActionResult>(Ok());
            }

            return Task.FromResult<ActionResult>(NotFound());
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


        /// <summary>
        /// Gets all notable documented Height and Speed Differences.
        /// Also gets all documented overtakes on the left side and categorizes them in steps of 5.
        /// </summary>
        [HttpGet("GetNotableDifferencesAndOvertakes")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ArrayList))]
        public Task<ActionResult<ArrayList>> GetNotableDifferencesAndOvertakes()
        {
            return Task.FromResult<ActionResult<ArrayList>>(SensorDataService.Instance.GetNotableDifferencesAndOvertakes());
        }
    }
}