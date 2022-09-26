using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<City> Cities { get; set; }
        [ForeignKey("CitiesId")]
        public int? CitiesId{ get; set; }

        public Country(int id, string name, int citiesId)
        {
            Id = id;
            Name = name;
        }

        public Country()
        {
        }
    }
}
