using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Thunder.Platform.AspNetCore.Controllers;
using Thunder.Platform.Core.Context;
using Thunder.Platform.Core.Logging;
using Thunder.Platform.Core.Timing;

namespace Thunder.Hosting.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ApplicationApiController
    {
        public ValuesController(IUserContext userContext, IOptions<ThunderLoggingOptions> options)
        {
            var a = userContext.GetValue<string>(CommonUserContextKeys.Username);
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public TestClass Get(int id)
        {
            return new TestClass
            {
                Checked = true,
                Time = Clock.Now
            };
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        public class TestClass
        {
            public DateTime? Time { get; set; }

            public bool Checked { get; set; }
        }
    }
}
