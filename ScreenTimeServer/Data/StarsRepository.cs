using Microsoft.EntityFrameworkCore;

namespace ScreenTimeServer.Data
{
    public class StarsRepository(DataContext context) : IStarsRepository
    {
        public readonly DataContext _context = context;

        public async Task<List<StarsEntity>> GetAllStarsAsync()
        {
            return await _context.Stars.ToListAsync();
        }

        public async Task AddStarAsync(StarsEntity stars)
        {
            await _context.Stars.AddAsync(stars);
            await _context.SaveChangesAsync();
        }

        public async Task UseStarAsync(List<StarsEntity> stars)
        {
            foreach (var star in stars)
            {
                star.Used = true;
            }
            _context.Stars.UpdateRange(stars);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStarAsync(StarsEntity stars)
        {
            _context.Stars.Remove(stars);
            await _context.SaveChangesAsync();
        }
    }
}
