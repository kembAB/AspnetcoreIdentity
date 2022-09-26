using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class TemperatureModel
    {
        public string CheckFever(double temp)
        {
            if(temp >= 37.7)
            {
                return "You Have a Fever";
            } else
            {
                return "You Do Not Have a Fever";
            }
        }
    }
}
