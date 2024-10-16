
namespace ScreenTimeServer.Data
{
    public interface IStarsRepository
    {
        Task AddStarAsync(string note);
        Task DeleteStarGroupAsync(StarGroupEntity starGroup);
        Task<List<StarGroupEntity>> GetAllGroupsAsync();
        Task UpdateStarGroupAsync(StarGroupEntity starGroup);
    }
}