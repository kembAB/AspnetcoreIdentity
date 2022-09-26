using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = AccountTypes.Administrator)]
    public class CityController : Controller
    {
        ApplicationDbContext dbContext;
        public CityController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var dbResult = dbContext.Cities.Include("People").Include("Country").ToList();
            return View(dbResult);
        }

        public IActionResult CreateCity()
        {
            ViewBag.Countries = new SelectList(dbContext.Countries, "Id", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult CreateCity(City city)
        {

            if (ModelState.IsValid)
            {
                dbContext.Add(city);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Message = "Please make sure everything is filed in correctly and try again";
            return RedirectToAction("CreateCity");
        }

        public IActionResult DeleteCity(int CityId)
        {
            var toDelete = dbContext.Cities.Include("People").Where(p => p.Id == CityId).Single<City>();

            if (toDelete != null)
            {

                dbContext.Cities.Remove(toDelete);
                dbContext.SaveChanges();
                TempData["Message"] = "City Removed Successfully";
                return RedirectToAction("Index");

            }
            else
            {
                TempData["Message"] = "Could not remove City";
            };
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var toEdit = dbContext.Cities.Where(c => c.Id == id).Single<City>();
            ViewBag.Countries = new SelectList(dbContext.Countries, "Id", "Name");
            return View(toEdit);
        }
        [HttpPost]
        public IActionResult Edit(City city)
        {
            var ResultCity = dbContext.Cities.Where(c => c.Id == city.Id).Single<City>();
            if (ResultCity != null)
            {
                ResultCity.Name = city.Name;
                ResultCity.CountryId = city.CountryId;
                dbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
