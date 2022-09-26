using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;

namespace WebApplication1.Models.People
{
    public class CreatePersonViewModel
    {
        //TODO Fix bug where leading zero in phone numbers are removed.

        public CreatePersonViewModel()
        {

        }
        
        [Required]
        [StringLength(50)]
        public string PersonName { get; set; }
        [Required]
        [Range(0, 9999999999)]
        public int PersonPhoneNumber { get; set; }
        
        [StringLength(40)]
        public string PersonCity { get; set; }
        [Required]
        public int personCityId { get; set; }

        public Person Create(ApplicationDbContext db,int cityId)
        {
            Person p = new Person(PersonName, PersonPhoneNumber, cityId);
            db.People.Add(p);
            db.SaveChanges();
            return p;
        }

        public Person Create(ApplicationDbContext db)
        {
            Person p = new Person(PersonName, PersonPhoneNumber, PersonCity);
            db.People.Add(p);
            db.SaveChanges();
            return p;
        }

    }
}
