using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = AccountTypes.Administrator)]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this._roleManager = roleManager;
            this._userManager = userManager;
        }
        public IActionResult Index()
        {
            return View(_roleManager.Roles);
        }

        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(string name)
        {
            IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
            if(result.Succeeded)
            {
                TempData["Message"] = "Successfully Created Role";
                return RedirectToAction("Index");
            }
            TempData["Message"] = "Role not created successfully";
            return View();
        }


        //Todo Add User To Role Functionallity

        public IActionResult AssignRole()
        {
            ViewBag.Roles = new SelectList(_roleManager.Roles, "Name", "Name");
            ViewBag.Users = new SelectList(_userManager.Users, "Id", "UserName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AssignRole(string user, string role)
        {
            ViewBag.Roles = new SelectList(_roleManager.Roles, "Name", "Name");
            ViewBag.Users = new SelectList(_userManager.Users, "Id", "UserName");


            var _user = await _userManager.FindByIdAsync(user);
            IdentityResult result = await _userManager.AddToRoleAsync(_user, role);
            if(result.Succeeded)
            {
                TempData["Message"] = "Assigned role Successfully, Role changes wont take effect untill next login";
                return RedirectToAction("Index");
            }
            TempData["Message"] = "Could not Assign Role, Make sure the user doesn't already have the role";
            return View();
        }

    }
}
