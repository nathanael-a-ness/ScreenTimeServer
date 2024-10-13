using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace ScreenTimeServer.Data;

public class TicketRepository(DataContext context) : ITicketRepository
{
    public readonly DataContext _context = context;

    public async Task<List<TicketEntity>> GetAllTicketsAsync()
    {
        return await _context.Tickets.ToListAsync();
    }

    public async Task<List<TicketEntity>> GetActiveTicketsAsync()
    {
        var today = DateTime.Now.ToString("yyyy-MM-dd");
        return await _context.Tickets
            .Where(t => t.Used == false || t.UsedDate.Contains(today))
            .ToListAsync();
    }
    public async Task<List<TicketEntity>> GetUsedTicketsAsync()
    {
        return await _context.Tickets
            .Where(t => t.Used == true)
            .ToListAsync();
    }

    public async Task AddTicketAsync(TicketEntity ticket)
    {
        await _context.Tickets.AddAsync(ticket);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTicketAsync(TicketEntity ticket)
    {
        _context.Tickets.Update(ticket);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteOldDailyTickets()
    {
        var today = DateTime.Now.ToString("yyyy-MM-dd");
        var existingTickets = await _context.Tickets
            .Where(t => 
                (t.Type == "DAILY" || t.Type == "WEEKEND" || t.Type == "MOIVE_NIGHT")
                && t.Used == false
                && !t.earnedDate.Contains(today))
            .FirstOrDefaultAsync();

        if(existingTickets != null)
        {
            _context.Tickets.Remove(existingTickets);
            await _context.SaveChangesAsync();
        }
    }

    public async Task CreateDailyTicketAsync()
    {
        await DeleteOldDailyTickets();
        var today = DateTime.Now.ToString("yyyy-MM-dd");
        var existingTicket = await _context.Tickets
            .Where(t => (t.Type == "DAILY" || t.Type == "WEEKEND" || t.Type == "MOIVE_NIGHT") && t.earnedDate.Contains(today))
            .FirstOrDefaultAsync();
        var icon = DateTime.Now.DayOfWeek switch
        {
            DayOfWeek.Sunday => "WEEKEND",
            DayOfWeek.Saturday => "WEEKEND",
            DayOfWeek.Friday => "MOIVE_NIGHT",
            _ => "DAILY"
        };
        var isBonus = DateTime.Today.DayOfWeek == DayOfWeek.Friday || DateTime.Today.DayOfWeek == DayOfWeek.Saturday || DateTime.Today.DayOfWeek == DayOfWeek.Sunday;
        var ticketCount = isBonus ? 3 : 1;
        if(existingTicket == null)
        {
            for (int i = 0; i < ticketCount; i++)
            {
                var exclamationId = Exclamation.Ids[new Random().Next(0, Exclamation.Ids.Count)];
                var newTicket = new TicketEntity
                {
                    Id = Guid.NewGuid().ToString(),
                    ExclamationId = exclamationId,
                    Note = "Daily Screen Time Ticket",
                    earnedDate = DateTime.Now.ToString("o", CultureInfo.InvariantCulture),
                    Type = icon,
                    UsedDate = DateTimeOffset.MinValue.ToString("o", CultureInfo.InvariantCulture),
                    Used = false,
                    Redemption = "NONE",
                    Time = 20
                };
                await _context.Tickets.AddAsync(newTicket);
            }            
            await _context.SaveChangesAsync();
        }
    }

    public int ScreenTimeUsedToday()
    {
        var today = DateTime.Now.ToString("yyyy-MM-dd");
        var tickets = _context.Tickets
            .Where(t => t.Used == true
                && t.earnedDate.Contains(today));
        var time = tickets.ToList().Aggregate(0, (acc, t) => acc + t.Time);
        return time;
    }
}
