using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ePlatform.Api.SampleNetCoreApp
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventTicketController : ControllerBase
    {
        private readonly EventTicketClient _eventTicketClient;

        public EventTicketController(EventTicketClient eventTicketClient)
        {
            _eventTicketClient = eventTicketClient;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventTicketModel>> Get(Guid id)
        {
            return await _eventTicketClient.Get(id);
        }
        
        [HttpGet("detail/{id}")]
        public async Task<ActionResult<TicketBuilderModel>> GetDetail(Guid id)
        {
            return await _eventTicketClient.GetDetail(id);
        }
    }
}
