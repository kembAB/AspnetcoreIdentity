using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class DoctorController : Controller
    {
        public IActionResult FeverCheck()
        {

            return View();
        }
        [HttpPost]
        public IActionResult FeverCheck(double Temperature)
        {
            TemperatureModel model = new TemperatureModel();
            ViewBag.Message = model.CheckFever(Temperature);
            return View();
        }
    }
}
