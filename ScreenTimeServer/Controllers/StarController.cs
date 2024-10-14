using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        [Route("GetAllGroups")]
        public async Task<List<StarGroupEntity>> GetAllGroups()
        {
            return await _starRepository.GetAllGroupsAsync();
        }

        [HttpGet]
        [Route("GetStarDetails")]
        public async Task<List<StarEntity>> GetStarDetails([FromBody] StarGroupEntity starGroup)
        {
            return await _starRepository.GetStarDetailsAsync(starGroup);
        }

        [HttpPost]
        [Route("AddStar")]
        public async Task AddStar([FromBody] string note)
        {
            await _starRepository.AddStarAsync(note);
        }

        [HttpPost]
        [Route("DeleteStar")]
        public async Task DeleteStar([FromBody] StarGroupEntity star)
        {
            await _starRepository.DeleteStarGroupAsync(star);
        }

        [HttpPost]
        [Route("UpdateStarGroup")]
        public async Task UpdateStarGroup([FromBody] StarGroupEntity starGroup)
        {
            await _starRepository.UpdateStarGroupAsync(starGroup);
        }
    }
}
