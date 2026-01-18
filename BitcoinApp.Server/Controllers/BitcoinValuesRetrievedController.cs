using BitcoinApp.Server.Api;
using BitcoinApp.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace BitcoinApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BitcoinValuesRetrievedController : ControllerBase
    {
        private readonly ILogger<BitcoinValuesController> _logger;
        private readonly RetrievedValuesService _retrievedValuesService;

        public BitcoinValuesRetrievedController(ILogger<BitcoinValuesController> logger)
        {
            _logger = logger;
        }

        // GET: api/BitcoinValues
        // Returns a collection of BitcoinValueRecord (sample data).
        [HttpGet]
        public ActionResult<IEnumerable<BitcoinValueRetrievedDto>> Get()
        {
            var data = _retrievedValuesService.GetRetrievedValues();

            return Ok(data);
        }
    }
}