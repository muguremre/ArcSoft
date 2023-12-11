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
        public LoginsController(ILoginService loginService)
        {
            _loginService = loginService;
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
            var entiti = new LoginMaterials
            {
                Email = entity.Email,
                UserName = entity.UserName,
                Password = entity.Password,
                LoginMaterialId = entity.LoginMaterialId,
            };
            _loginService.Add(entiti);
            return Ok(StatusCode(200,"Data added successfully."));
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest model)
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

            return Ok("Giriş başarılı");
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
