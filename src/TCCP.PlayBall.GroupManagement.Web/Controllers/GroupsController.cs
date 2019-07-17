using Microsoft.AspNetCore.Mvc;

namespace TCCP.PlayBall.GroupManagement.Web.Controllers
{
    [Route("groups")]
    public class GroupsController: Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return Content("Heloooooow!!!");            
        }
    }
}