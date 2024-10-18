using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ScreenTimeServer.Auth;
using ScreenTimeServer.Data;

namespace ScreenTimeServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StarController(ILogger<StarController> logger, IStarsRepository starRepository) : ControllerBase
    {
        private readonly ILogger<StarController> _logger = logger;
        private readonly IStarsRepository _starRepository = starRepository;

        [HttpGet]
        [ApiKey]
        [Route("GetAllGroups")]
        public async Task<List<StarGroupEntity>> GetAllGroups()
        {
            return await _starRepository.GetAllGroupsAsync();
        }

        [HttpPost]
        [ApiKey]
        [Route("AddStar")]
        public async Task AddStar([FromBody] string note)
        {
            await _starRepository.AddStarAsync(note);
        }

        [HttpPost]
        [ApiKey]
        [Route("DeleteStar")]
        public async Task DeleteStar([FromBody] StarGroupEntity star)
        {
            await _starRepository.DeleteStarGroupAsync(star);
        }

        [HttpPost]
        [ApiKey]
        [Route("UpdateStarGroup")]
        public async Task UpdateStarGroup([FromBody] StarGroupEntity starGroup)
        {
            await _starRepository.UpdateStarGroupAsync(starGroup);
        }
    }
}
