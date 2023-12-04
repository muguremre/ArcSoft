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
            return Ok(StatusCode(200,"Computer added successfully."));
        }
    }
}