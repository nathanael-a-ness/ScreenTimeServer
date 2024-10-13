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
        [Route("GetAllStars")]
        public async Task<List<StarsEntity>> GetAllStars()
        {
            return await _starRepository.GetAllStarsAsync();
        }

        [HttpPost]
        [Route("AddStar")]
        public async Task AddStar([FromBody] StarsEntity star)
        {
            await _starRepository.AddStarAsync(star);
        }

        [HttpPost]
        [Route("DeleteStar")]
        public async Task DeleteStar([FromBody] StarsEntity star)
        {
            await _starRepository.DeleteStarAsync(star);
        }

        [HttpPost]
        [Route("UseStars")]
        public async Task UseStars([FromBody] List<StarsEntity> stars)
        {
            await _starRepository.UseStarAsync(stars);
        }
    }
}
