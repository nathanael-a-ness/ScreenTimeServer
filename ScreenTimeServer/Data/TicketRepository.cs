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
        var today = Utility.GetToday();
        return await _context.Tickets
            .Where(t => t.Used == false || t.UsedDate > today)
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
        ticket.earnedDate = DateTime.UtcNow;
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
        var today = Utility.GetToday();
        var existingTickets = await _context.Tickets
            .Where(t => 
                (t.Type == "DAILY" || t.Type == "WEEKEND" || t.Type == "MOVIE_NIGHT")
                && t.Used == false
                && t.earnedDate < today).ToListAsync();

        if(existingTickets != null)
        {
            _context.Tickets.RemoveRange(existingTickets);
            await _context.SaveChangesAsync();
        }
    }

    public async Task CreateDailyTicketAsync()
    {
        await DeleteOldDailyTickets();
        var today = Utility.GetToday();
        var existingTicket = await _context.Tickets
            .Where(t => (t.Type == "DAILY" || t.Type == "WEEKEND" || t.Type == "MOVIE_NIGHT") && t.earnedDate > today)
            .FirstOrDefaultAsync();
        var icon = DateTime.Now.DayOfWeek switch
        {
            DayOfWeek.Sunday => "WEEKEND",
            DayOfWeek.Saturday => "WEEKEND",
            DayOfWeek.Friday => "MOVIE_NIGHT",
            _ => "DAILY"
        };
        var isBonus = DateTime.Today.DayOfWeek == DayOfWeek.Friday || DateTime.Today.DayOfWeek == DayOfWeek.Saturday || DateTime.Today.DayOfWeek == DayOfWeek.Sunday;
        var ticketCount = isBonus ? 3 : 1;
        if(existingTicket == null)
        {
            for (int i = 0; i < ticketCount; i++)
            {
                var newTicket = new TicketEntity
                {
                    Id = Guid.NewGuid().ToString(),
                    ExclamationId = new Random().Next(0, 20),
                    Note = "Daily Screen Time Ticket",
                    earnedDate = DateTime.UtcNow,
                    Type = icon,
                    UsedDate = DateTime.MinValue,
                    Used = false,
                    Redemption = "NONE",
                    Time = 20
                };
                await _context.Tickets.AddAsync(newTicket);
            }            
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteTicketAsync(string ticketId)
    {
        _ = _context.Tickets.Remove(new TicketEntity { Id = ticketId });
        await _context.SaveChangesAsync();
    }

    public int ScreenTimeUsedToday()
    {
        var today = Utility.GetToday();
        Console.WriteLine($"Today: {today}");
        var tickets = _context.Tickets
            .Where(t => t.Used == true && t.earnedDate > today);
        foreach (var ticket in tickets)
        {
            Console.WriteLine(ticket.earnedDate);
        }
        Console.WriteLine(tickets?.FirstOrDefault()?.earnedDate > today);
        var time = tickets?.ToList().Aggregate(0, (acc, t) => acc + t.Time);
        return time ?? 0;
    }    
}
