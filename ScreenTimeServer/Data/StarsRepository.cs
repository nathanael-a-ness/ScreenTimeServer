using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace ScreenTimeServer.Data
{
    public class StarsRepository(DataContext context) : IStarsRepository
    {
        public readonly DataContext _context = context;

        public async Task<List<StarGroupEntity>> GetAllGroupsAsync()
        {
            return await _context.StarGroups.ToListAsync();
        }

        public async Task AddStarAsync(string note)
        {
            var group = _context.StarGroups.Include(s => s.Stars).FirstOrDefault(s => s.Earned < 10);
            var insert = group == null;
            group ??= new StarGroupEntity
            {
                Id = Guid.NewGuid().ToString(),
                Earned = 1,
                Used = false,
                Note = "",
                Date = DateTime.MinValue.ToString("o", CultureInfo.InvariantCulture),
                Stars = new List<StarEntity>()
            };

            var star = new StarEntity
            {
                Id = Guid.NewGuid().ToString(),
                Group = group,
                Note = note,                
                Date = DateTimeOffset.Now.ToString("o", CultureInfo.InvariantCulture)
            };
            group.Stars.Add(star);

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

        public async Task<List<StarEntity>> GetStarDetailsAsync(StarGroupEntity starGroup)
        {
            var group = await _context.StarGroups.Include(s => s.Stars).FirstOrDefaultAsync(s => s.Id == starGroup.Id);
            return [.. group?.Stars];
        }
    }
}
