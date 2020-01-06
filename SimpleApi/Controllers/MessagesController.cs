using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace SimpleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IValuesService valuesService;
        private readonly IQueueClientFactory queueClientFactory;

        public MessagesController(IValuesService valuesService, IQueueClientFactory queueClientFactory)
        {
            this.valuesService = valuesService;
            this.queueClientFactory = queueClientFactory;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { valuesService.GetTraceIdentifier() };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] MessageObject value)
        {
            var msg = new Microsoft.Azure.ServiceBus.Message(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(value)));
            msg.CorrelationId = valuesService.GetTraceIdentifier();

            queueClientFactory.GetQueueClient().SendAsync(msg);
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
    }
}
