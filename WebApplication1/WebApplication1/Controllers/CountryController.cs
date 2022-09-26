using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = AccountTypes.Administrator)]

    public class CountryController : Controller
    {

        ApplicationDbContext dbContext;
        public CountryController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var databaseResult = dbContext.Countries.Include("Cities").ToList();
            return View(databaseResult);
        }

        public IActionResult CreateCountry()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateCountry(Country country)
        {
            if (ModelState.IsValid)
            {
                dbContext.Countries.Add(country);
                dbContext.SaveChanges();
                ViewData["Message"] = "Country Created Successfully";
                return RedirectToAction("Index");
            }
            ViewData["Message"] = "Make sure you've filled in the form correctly";
            return View();
        }

        public IActionResult DeleteCountry(int CountryId)
        {
            var toDelete = dbContext.Countries.Include("Cities").Where(p => p.Id == CountryId).Single<Country>();

            if (toDelete != null)
            {
                dbContext.Countries.Remove(toDelete);
                dbContext.SaveChanges();
                TempData["Message"] = "Country Removed Successfully";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Message"] = "Could not remove Country";
            };

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var dbResult = dbContext.Countries.Where(p => p.Id == id).SingleOrDefault();
            return View(dbResult);
        }
        [HttpPost]
        public IActionResult Edit(int id, string name)
        {
            var dbResult = dbContext.Countries.Where(p => p.Id == id).SingleOrDefault();
            dbResult.Name = name;
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
