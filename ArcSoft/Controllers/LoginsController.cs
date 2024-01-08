using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace ArcSoft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        ILoginService _loginService;
        IBearerTokenService _authManager;
        IVoteService _voteService;
        public LoginsController(ILoginService loginService, IBearerTokenService authManager, IVoteService voteService)
        {
            _loginService = loginService;
            _authManager = authManager;
            _voteService = voteService;
        }


        [HttpGet]
        public List<LoginMaterials> Get()
        {
            var result = _loginService.GetAll();
            return result;
        }

        [HttpPost]
        public IActionResult Add([FromBody] LoginMaterials entity)
        {
            if(entity == null)
            {
                return Ok(StatusCode(404,"Invalid data received."));

            }
            var entityy = new VoteMaterials {

                VoteMaterialId = entity.VoteMaterialId,
            };
            var entiti = new LoginMaterials
            {
                LoginMaterialId = entity.LoginMaterialId,
                Email = entity.Email,
                UserName = entity.UserName,
                Password = entity.Password,
                isAdmin =  entity.isAdmin,
                VoteMaterialId = entity.VoteMaterialId,
            };
            _loginService.Add(entiti);
            return Ok(StatusCode(200,"Person added successfully."));
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginMaterials model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.Password))
            {
                return BadRequest("Email veya Şifre Boş Bırakılamaz.");
            }

            bool isAuthenticated = _loginService.CheckCredentials(model.Email, model.Password);

            if (!isAuthenticated)
            {
                return Unauthorized("Yanlış Şifre Veya Kullanıcı Adı.");
            }
            var token = _authManager.GenerateToken(model.LoginMaterialId);

            return Ok(model);
        }
    }

}
