using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    
    public class HomeController : ApiController
    {
        [Authorize]
        [Route(nameof(Index))]
        public ActionResult Index()
        {
            return Ok("Imamaoneyking");
        }
        
    }
}