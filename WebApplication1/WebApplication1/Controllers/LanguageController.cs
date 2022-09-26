using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.Languages;
using WebApplication1.Models.People;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = AccountTypes.Administrator + ", " + AccountTypes.Standard)]
    public class LanguageController : Controller
    {

        ApplicationDbContext dbContext;
        public LanguageController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [Authorize(Roles = AccountTypes.Administrator)]
        public IActionResult Index()
        {
            var dbResult = dbContext.Languages.Include("People.Person").ToList();
            return View(dbResult);
        }

        [Authorize(Roles = AccountTypes.Administrator)]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = AccountTypes.Administrator)]
        public IActionResult Create(Language language)
        {

            dbContext.Languages.Add(language);
            dbContext.SaveChanges();
            TempData["Message"] = $"Language {language.Name} Created";
            return RedirectToAction("Index");
        }
        [Authorize(Roles = AccountTypes.Standard + ", " + AccountTypes.Administrator)]
        public IActionResult Assign(int id)
        {
            ViewBag.People = new SelectList(dbContext.People,"Id", "Name");
            ViewBag.Languages = new SelectList(dbContext.Languages, "Id", "Name");
            return View(id);
        }

        [HttpPost]
        [Authorize(Roles = AccountTypes.Standard + ", " + AccountTypes.Administrator)]
        public IActionResult Assign(int personId, int languageId)
        {
            var alreadyknownlanguage = dbContext.PersonLanguage.Find(personId, languageId);
            if(alreadyknownlanguage == null)
            {
                dbContext.PersonLanguage.Add(new PersonLanguage { PersonId = personId, LanguageId = languageId });
                dbContext.SaveChanges();
                TempData["Message"] = $"Language Assigned";

            }
            else
            {
                TempData["Message"] = $"Language Not Assigned, Language is already known by person";
            }

            if(User.IsInRole(AccountTypes.Administrator))
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "People");


        }
        [Authorize(Roles = AccountTypes.Standard + ", " + AccountTypes.Administrator)]

        //TODO Make sure it redirects to the last page the user was on before unassigning
        public IActionResult Unassign(int personId, int languageId)
        {
            var toDelete = dbContext.PersonLanguage.Where(o => o.PersonId == personId).Where(o => o.LanguageId == languageId).Single();
            dbContext.PersonLanguage.Remove(toDelete);
            dbContext.SaveChanges();
            TempData["Message"] = "Successfully unassigned language from person";
            if (User.IsInRole(AccountTypes.Administrator))
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "People");
        }

        [Authorize(Roles = AccountTypes.Administrator)]
        public IActionResult Delete(int languageId)
        {
            var toDelete = dbContext.Languages.Where(l => l.Id == languageId).Single();
            if(toDelete != null)
            {
                dbContext.Languages.Remove(toDelete);
                dbContext.SaveChanges();
                TempData["Message"] = "Language Successfully Deleted";
            }
            return RedirectToAction("Index");
        }
    }
}
