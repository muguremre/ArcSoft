using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace ArcSoft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoteController : ControllerBase
    {
        IVoteService _voteService;

        public VoteController(IVoteService voteService)
        {
            _voteService = voteService;
        }



        [HttpGet]
        public List<VoteMaterials> Get()
        {
            var result = _voteService.GetAll();
            return result;
        }

        [HttpPost]
        public IActionResult Add([FromBody] VoteMaterials entity)
        {
            if (entity == null)
            {
                return Ok(StatusCode(404, "Invalid data received."));

            }
            var entiti = new VoteMaterials
            {
                VoteTime = entity.VoteTime,
                VoteCount = entity.VoteCount,

                VoteMaterialId= entity.VoteMaterialId,
                VoteName = entity.VoteName,
            };
            _voteService.Add(entiti);
            return Ok(StatusCode(200, "Vote added successfully."));
        }
    }
}
