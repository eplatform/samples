using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ePlatform.Api.SampleNetCoreApp
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengerTicketController : ControllerBase
    {
        private readonly PassengerTicketClient _passengerTicketClient;

        public PassengerTicketController(PassengerTicketClient passengerTicketClient)
        {
            _passengerTicketClient = passengerTicketClient;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PassengerTicketModel>> Get(Guid id)
        {
            return await _passengerTicketClient.Get(id);
        }
        
        [HttpGet("detail/{id}")]
        public async Task<ActionResult<TicketBuilderModel>> GetDetail(Guid id)
        {
            return await _passengerTicketClient.GetDetail(id);
        }
    }
}
