using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TCCP.PlayBall.GroupManagement.Web.Models;

namespace TCCP.PlayBall.GroupManagement.Web.Controllers
{
    [Route("groups")]
    public class GroupsController : Controller
    {
        private static long currentGroupId = 1;
        private static List<GroupViewModel> groups = new List<GroupViewModel> { new GroupViewModel { Id = 1, Name = "Sample Group" }, new GroupViewModel { Id = 2, Name = "Sample Group - 2" } };

        [HttpGet]
        [Route("")] //not needed because Index would be used as default anyway
        public IActionResult Index()
        {
            return View(groups);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Details(long id)
        {
            var group = groups.SingleOrDefault(g => g.Id == id);

            if (group == null)
            {
                return NotFound();
            }

            return View(group);
        }

        [HttpPost]
        [Route("{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, GroupViewModel model)
        {
            var group = groups.SingleOrDefault(g => g.Id == id);

            if (group == null)
            {
                return NotFound();
            }

            group.Name = model.Name;

            return RedirectToAction("Index");
        }

    }
}