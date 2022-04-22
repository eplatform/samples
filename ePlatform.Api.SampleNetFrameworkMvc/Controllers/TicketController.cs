using System.Threading.Tasks;
using System.Web.Mvc;

namespace ePlatform.Api.SampleNetFrameworkMvc
{
    public class TicketController : Controller
    {
        private readonly IEventTicketClient _eventTicketClient;
        private readonly IPassengerTicketClient _passengerTicketClient;

        public TicketController(IEventTicketClient eventTicketClient, 
            IPassengerTicketClient passengerTicketClient)
        {
            _eventTicketClient = eventTicketClient;
            _passengerTicketClient = passengerTicketClient;
        }

        public async Task<ActionResult> EventTickets()
        {
            var approvedEventTicketList = await _eventTicketClient.GetTicketList(
                new QueryFilterBuilder<EventTicketModel>()
                    .PageSize(10)
                    .QueryFor(ticket => ticket.Status, Operator.Equal, TicketStatus.Approved)
                    .Build());
            
            return View(approvedEventTicketList.Items);
        }
        
        public async Task<ActionResult> PassengerTickets()
        {
            var approvedPassengerTicketList = await _passengerTicketClient.GetTicketList(
                new QueryFilterBuilder<PassengerTicketModel>()
                    .PageSize(10)
                    .QueryFor(ticket => ticket.Status, Operator.Equal, TicketStatus.Approved)
                    .Build());
            
            return View(approvedPassengerTicketList.Items);
        }
    }
}
