using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class GamesController : Controller
    {
        public IActionResult Index()
        {
            
            return RedirectToAction("NumberGuesser");
        }

        public IActionResult NumberGuesser()
        {

            HttpContext.Session.SetInt32("NumberToGuess", new GuessingGameModel().GetNumber());
            return View();
        }
        [HttpPost]
        public IActionResult NumberGuesser(int GuessedNumber)
        {
            GuessingGameModel.CheckWin(HttpContext.Session,GuessedNumber,this);
            return View();
        }
    }
}
