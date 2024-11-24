using JsonFlatFileDataStore;
using Microsoft.AspNetCore.Mvc;
using SharedService.DBContext;
using SharedService.LogProvider.Interface;
using SharedService.Models.RestSample;

namespace WebApiServices.Controllers.RestSampleController
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestSampleController : ControllerBase
    { 
        private readonly ILoggerService _logger;
        private readonly DatasContext _datasContext;
        private readonly IDocumentCollection<UserModel> _restSample;

        public RestSampleController(ILoggerService logger, DatasContext datasContext)
        {
            _logger = logger;
            _datasContext = datasContext;
            var store = new DataStore("restSample.json");
            _restSample = store.GetCollection<UserModel>();
        }

        [HttpPost]
        public void Post([FromBody] UserModel restSample) 
        {
            _restSample.InsertOne(restSample);
        }

        [HttpGet]
        public IEnumerable<UserModel> Get()
        {
            return _restSample.AsQueryable().ToList();
        }

        [HttpGet("{id:int}")]
        public UserModel GetById(int id)
        {
            return _restSample.AsQueryable().FirstOrDefault(f => f.Id == id);
        }

        // ------------------------------------------------------------------------------------------------------------
        // ------------------------------------------------------------------------------------------------------------

        [HttpGet("RestSample2")]
        public IActionResult GetRestSample()
        {
            return Ok(_datasContext.RestSamples.ToList());
        }

        [HttpPost("RestSample2")]
        public IActionResult PostRestSample([FromBody] RestSampleModel restSample)
        {
            var exist = _datasContext.RestSamples.Any(u => u.Id == restSample.Id);
            if (exist)
            {
                return Conflict();
            }

            var entry = _datasContext.Add(restSample);
            _datasContext.SaveChanges();

            var newRestSample = entry.Entity;
            return CreatedAtAction(nameof(PostRestSample), new { id = newRestSample.Id }, newRestSample);
        }

        [HttpGet("RestSample2/{id:int}")]
        public IActionResult GetByIDRestSample2(int id)
        {
            var existingRestSample = _datasContext.RestSamples.FirstOrDefault(u => u.Id == id);
            if (existingRestSample == null)
            {
                return NotFound();
            }
            return Ok(existingRestSample);
        }

        [HttpPut("RestSample2/{id:int}")]
        public IActionResult PutRestSample2(int id, [FromBody] RestSampleModel restSample)
        {
            var existingResSample = _datasContext.RestSamples.FirstOrDefault(u => u.Id == id);
            if (existingResSample == null)
            {
                return NotFound();
            }

            existingResSample.Name = restSample.Name;
            _datasContext.RestSamples.Update(existingResSample);
            _datasContext.SaveChanges();

            return Ok();
        }

        [HttpDelete("RestDample2/{id:int}")]
        public IActionResult DeleteRestSample(int id)
        {
            var existingRestSample = _datasContext.RestSamples.FirstOrDefault(u => u.Id == id);
            if (existingRestSample == null)
            {
                return NotFound();
            }

            _datasContext.RestSamples.Remove(existingRestSample);
            _datasContext.SaveChanges();

            return Ok();
        }
    }
}


// dotnet add package JsonFlatFileDataStore --version 2.3.0

// ctrl + shift + f5

// dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 6.0.10