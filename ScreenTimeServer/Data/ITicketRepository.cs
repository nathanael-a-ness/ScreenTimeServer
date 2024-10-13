
namespace ScreenTimeServer.Data
{
    public interface ITicketRepository
    {
        Task AddTicketAsync(TicketEntity ticket);
        Task<List<TicketEntity>> GetUsedTicketsAsync();
        Task<List<TicketEntity>> GetActiveTicketsAsync();
        Task CreateDailyTicketAsync();
        Task UpdateTicketAsync(TicketEntity ticket);
        int ScreenTimeUsedToday();
        Task<List<TicketEntity>> GetAllTicketsAsync();
    }
}