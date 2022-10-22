using System.ComponentModel.DataAnnotations;
using Context;
using Context.DAL;
using Context.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SampleController : ControllerBase
    {
        MongoDBUnitOfWork mongo = MonitoringFacade.Instance.MongoDB;

        /// <summary>
        /// Logs a certain user in and issues a JWT Bearer token.
        /// If the user is inactive, or the bearer token is invalid or null a 401 is raised.
        /// </summary>
        /// <param name="cred"></param>
        //[HttpPost("Login")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponse))]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginCredentials cred)
        //{
        //    User usr = await mongo.Users.Login(cred.Username, cred.Password);
        //    AuthenticationInformation token = await auth.Authenticate(usr);
        //    if (usr != null && !usr.Active)
        //        return Unauthorized();
        //
        //    if (token != null)
        //    {
        //        LoginResponse returnmodel = new LoginResponse();
        //        returnmodel.User = usr;
        //        returnmodel.Authentication = token;
        //        return returnmodel;
        //    }
        //
        //    return Unauthorized();
        //}


        /// <summary>
        /// Creates a new user.
        /// If user or wallet are empty a 404 is raised.
        /// </summary>
        /// <param name="sample"></param>
        [HttpPost("AddSample")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Sample))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Sample>> AddSample([Required] [FromBody] Sample sample)
        {
            Sample s = await mongo.Samples.InsertOrUpdateOneAsync(sample);

            if (s != null)
            {
                return s;
            }

            return NotFound();
        }


        /// <summary>
        /// Gets all existing users.
        /// If no users are found a 404 is raised.
        /// </summary>
        [HttpGet("ListSamples")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Sample))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Sample>>> ListSamples()
        {
            List<Sample> sample = mongo.Samples.FilterBy(x => true).ToList();

            if (sample != null)
            {
                return sample;
            }

            return NotFound();
        }


        /// <summary>
        /// Gets a user by their id.
        /// Raises a 404 if user is not found.
        /// </summary>
        /// <param name="userId"></param>
        [HttpGet("GetSample")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Sample))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Sample>> GetSample([Required] [FromQuery] string sampleId)
        {
            var sample = await mongo.Samples.FindByIdAsync(sampleId);

            if (sample != null)
            {
                return sample;
            }

            return NotFound();
        }
    }
}