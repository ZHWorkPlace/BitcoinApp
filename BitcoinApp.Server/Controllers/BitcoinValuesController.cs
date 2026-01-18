using BitcoinApp.Server.Api;
using Microsoft.AspNetCore.Mvc;

namespace BitcoinApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BitcoinValuesController : ControllerBase
    {
        private readonly ILogger<BitcoinValuesController> _logger;

        public BitcoinValuesController(ILogger<BitcoinValuesController> logger)
        {
            _logger = logger;
        }

        // GET: api/BitcoinValues
        // Returns a collection of BitcoinValueRecord (sample data).
        [HttpGet]
        public ActionResult<IEnumerable<BitcoinValueRecordDto>> Get()
        {
            var now = DateTime.UtcNow;
            var sample = new List<BitcoinValueRecordDto>
            {
                new BitcoinValueRecordDto { Id = 1, RetrievedAt = now, BitcoinValue = 40000.50m, Note = "sample" },
                new BitcoinValueRecordDto { Id = 2, RetrievedAt = now.AddMinutes(-5), BitcoinValue = 39980.12m, Note = "sample" }
            };

            return Ok(sample);
        }

        // POST: api/BitcoinValues
        // Accepts a collection of BitcoinValueRecord. Currently echoes back count.
        [HttpPost]
        public ActionResult Post([FromBody] IEnumerable<BitcoinValueRecordDto>? records)
        {
            if (records is null)
            {
                return BadRequest("Request body must be a JSON array of BitcoinValueRecord.");
            }

            var list = records.ToList();
            _logger.LogInformation("Received {Count} BitcoinValueRecord(s) via POST", list.Count);

            // TODO: persist records (DB, file, etc.). For now respond with count.
            return Accepted(new { received = list.Count });
        }
    }
}