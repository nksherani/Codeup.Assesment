using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Codeup.Assesment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderManagementController : ControllerBase
    {
        // GET: api/<OrderManagement>
        [HttpGet("orders")]
        public IEnumerable<string> GetOrder()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<OrderManagement>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<OrderManagement>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<OrderManagement>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrderManagement>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
