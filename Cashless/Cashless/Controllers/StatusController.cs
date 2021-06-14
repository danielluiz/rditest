using Cashless.Domain.Data;
using Cashless.Domain.Data.Class;
using Microsoft.AspNetCore.Mvc;


namespace Cashless.Web.Controllers
{
    /// <summary>
    /// Checks the Status of the data in the app, for testing only, doesn't require authentication
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {

        private readonly IStatus _status;
        /// <summary>
        /// Default controller for injection
        /// </summary>
        /// <param name="status"></param>
        public StatusController(IStatus status)
        {
            _status = status;
        }
        /// <summary>
        /// Gets the status of the application
        /// </summary>
        /// <returns>Everything that's in the memory data</returns>
        [HttpGet]
        public ApplicationStatus Get()
        {
            return _status.GetStatus();
        }

        /// <summary>
        /// Gets the jwt token of the custmer 1, test only
        /// </summary>
        /// <returns></returns>
        [HttpGet("token1")]
        public string GetCustomer1Token()
        {
            return AuthHandler.GenerateToken("CUS1");
        }


        /// <summary>
        /// Gets the jwt token of the custmer 1, test only
        /// </summary>
        /// <returns></returns>
        [HttpGet("token2")]
        public string GetCustomer2Token()
        {
            return AuthHandler.GenerateToken("CUS2");
        }
    }
}
