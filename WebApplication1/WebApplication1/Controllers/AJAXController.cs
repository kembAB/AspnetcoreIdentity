using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models.People;

namespace WebApplication1.Controllers
{
    public class AJAXController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Get(int personid)
        {
            PeopleViewModel wm = new PeopleViewModel();
            Person resultPerson = null;
            return InputResponseView(personid, wm, ref resultPerson);
        }

        private IActionResult InputResponseView(int personid, PeopleViewModel wm, ref Person foundPerson)
        {
            if (ModelState.IsValid)
            {
                foundPerson = wm.FindByID(personid);

                bool isPersonInvalid = (foundPerson == null);
                if (isPersonInvalid)
                {
                    TempData["Message"] = $"Could not find detail about Person with ID: {personid}";
                    return PartialView("/Views/AJAX/_Message.cshtml");
                }
                //Found Person Propperly
                return PartialView("/Views/People/_Person.cshtml", foundPerson.Id);
            }
            // Invalid ModelState
            TempData["Message"] = "Invalid Input in form, please make sure it is filled correctly before submitting";
            return PartialView("/Views/AJAX/_Message.cshtml");
        }

        [HttpPost]
        public IActionResult Delete(int personid)
        {
            Person.Delete(personid, this);
            return PartialView("/Views/AJAX/_Message.cshtml");

        }

        public IActionResult GetAll()
        {
            return PartialView("/Views/People/_Person.cshtml");
        }

    }
}
