
namespace ScreenTimeServer.Data
{
    public interface IStarsRepository
    {
        Task AddStarAsync(StarsEntity stars);
        Task DeleteStarAsync(StarsEntity stars);
        Task<List<StarsEntity>> GetAllStarsAsync();
        Task UseStarAsync(List<StarsEntity> stars);
    }
}