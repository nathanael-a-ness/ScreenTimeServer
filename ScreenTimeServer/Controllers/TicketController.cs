using Microsoft.AspNetCore.Mvc;
using ScreenTimeServer.Auth;
using ScreenTimeServer.Data;

namespace ScreenTimeServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController(ILogger<TicketController> logger, ITicketRepository ticketRepository) : ControllerBase
    {
        private readonly ILogger<TicketController> _logger = logger;
        private readonly ITicketRepository _ticketRepository = ticketRepository;

        [HttpPost]
        [ApiKey]
        [Route("CreateDailyTicket")]
        public async Task CreateDailyTicket()
        {
            await _ticketRepository.CreateDailyTicketAsync();
        }

        [HttpGet]
        [ApiKey]
        [Route("GetAllTickets")]
        public async Task<List<TicketEntity>> GetAllTickets()
        {
            return await _ticketRepository.GetAllTicketsAsync();
        }

        [HttpGet]
        [ApiKey]
        [Route("GetActiveTickets")]
        public async Task<List<TicketEntity>> GetActiveTickets()
        {
            return await _ticketRepository.GetActiveTicketsAsync();
        }

        [HttpGet]
        [ApiKey]
        [Route("GetUsedTickets")]
        public async Task<List<TicketEntity>> GetUsedTickets()
        {
            return await _ticketRepository.GetUsedTicketsAsync();
        }

        [HttpPost]
        [ApiKey]
        [Route("UpdateTicket")]
        public async Task UpdateTicket([FromBody] TicketEntity ticket)
        {
            await _ticketRepository.UpdateTicketAsync(ticket);
        }

        [HttpPost]
        [ApiKey]
        [Route("AddTicket")]
        public async Task AddTicket([FromBody] TicketEntity ticket)
        {
            await _ticketRepository.AddTicketAsync(ticket);
        }

        [HttpDelete]
        [ApiKey]
        [Route("DeleteTicket")]
        public async Task DeleteTicket([FromQuery] string ticketId)
        {
            await _ticketRepository.DeleteTicketAsync(ticketId);
        }

        [HttpGet]
        [ApiKey]
        [Route("TodaysUsedScreenTime")]
        public int TodaysUsedScreenTime()
        {
            return _ticketRepository.ScreenTimeUsedToday();
        }
    }
}
