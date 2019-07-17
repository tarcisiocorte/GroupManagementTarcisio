using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TCCP.PlayBall.GroupManagement.Web.Models;

namespace TCCP.PlayBall.GroupManagement.Web.Controllers
{
    [Route("groups")]
    public class GroupsController: Controller
    {
        private static List<GroupViewModel> groups = new List<GroupViewModel>
            {new GroupViewModel() {Id = 1, Name = "Isso é test"}};

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View(groups);
        }
    }
}