using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace ScreenTimeServer.Data
{
    public class StarsRepository(DataContext context) : IStarsRepository
    {
        public readonly DataContext _context = context;

        public async Task<List<StarGroupEntity>> GetAllGroupsAsync()
        {
            return await _context.StarGroups.Include(s => s.Stars).ToListAsync();
        }

        public async Task AddStarAsync(string note)
        {
            var group = _context.StarGroups.Include(s => s.Stars).FirstOrDefault(s => s.Earned < 10);
            var insert = group == null;
            group ??= new StarGroupEntity
            {
                Id = Guid.NewGuid().ToString(),
                Earned = 0,
                Used = false,
                Note = "",
                Date = DateTime.MinValue,
                Stars = []
            };
            group.Earned += 1;

            var star = new StarEntity
            {
                Id = Guid.NewGuid().ToString(),
                Group = group,
                Note = note,                
                Date = DateTime.UtcNow
            };
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            group.Stars.Add(star);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            if (insert)
            {
                await _context.StarGroups.AddAsync(group);
            }
            else
            {
                _context.StarGroups.Update(group);
            }
            
            await _context.Stars.AddAsync(star);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStarGroupAsync(StarGroupEntity starGroup)
        {
            _context.StarGroups.Update(starGroup);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStarGroupAsync(StarGroupEntity starGroup)
        {
            _context.StarGroups.Remove(starGroup);
            await _context.SaveChangesAsync();
        }
    }
}
