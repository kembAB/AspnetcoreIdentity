using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class GuessingGameModel
    {
        private static int randomNumber;
        private Random random = new Random();

        public GuessingGameModel()
        {
            randomNumber = random.Next(1, 101);
        }
        public int GetNumber()
        {
            return randomNumber;
        }

        public static void CheckWin(ISession session,int GuessedNumber,  Controller con)
        {
            if(GuessedNumber == null)
            {
                con.ViewBag.Message = $"Invalid Value Detected in guess, make sure you use numbers";
                return;
            }
            if (GuessedNumber == session.GetInt32("NumberToGuess"))
            {
                con.ViewBag.Message = $"You won, the number was {session.GetInt32("NumberToGuess")}. I've picked a new one if you want to continue playing";
                session.SetInt32("NumberToGuess", new GuessingGameModel().GetNumber());

            }
            else if (GuessedNumber > session.GetInt32("NumberToGuess"))
            {
                con.ViewBag.Message = $"Go Lower";
            }
            else if (GuessedNumber < session.GetInt32("NumberToGuess"))
            {
                con.ViewBag.Message = $"Go Higher";
            }
        }

    }
}
