﻿using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace sprintplanningtoolbackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        // GET: api/<ReportsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ReportsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ReportsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ReportsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ReportsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
