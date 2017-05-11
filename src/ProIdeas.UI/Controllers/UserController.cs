using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProIdeas.UI.Controllers
{
    [Authorize]    
    public class UserController : Controller
    {
        // GET: /<controller>/
        [Route("profile/:userName")]
        public IActionResult Details(string userName)
        {
            return View();
        }
    }
}
