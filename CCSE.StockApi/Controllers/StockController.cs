using Microsoft.AspNetCore.Mvc;
using CCSE.StockApi.Repositories;
using CCSE.StockApi.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CCSE.StockApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        // GET: api/<StockController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<StockController>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            StockRepository stockRepository = new StockRepository();
            return Ok(stockRepository.Get(id));
        }

        // POST api/<StockController>
        [HttpPost]
        public IActionResult Post([FromBody] Stock stock)
        {
            StockRepository stockRepository = new StockRepository();
            stockRepository.Add(stock);
            return Ok();
        }

        // PUT api/<StockController>/5
        [HttpPut]
        public IActionResult  Put( [FromBody] Stock stock)
        {
            StockRepository stockRepository = new StockRepository();
            stockRepository.Add(stock);
            return Ok();

        }

        // DELETE api/<StockController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            StockRepository stockRepository = new StockRepository();
            stockRepository.Delete(id);
            return Ok();

        }
    }
}
