using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;

namespace WebApplication1.Models.People
{
    public class PeopleViewModel
    {
        public static List<Person> PeopleList = new List<Person> { };

        public string searchTerm;

        public IConfiguration Configuration;

        public List<Person> getPeople()
        {
            return PeopleList;
        }

        public List<Person> getPeople(string searchTerm)
        {
            List<Person> sortedPeople = new List<Person>();
            foreach(Person person in PeopleList)
            {
                if (searchTerm == null)
                {
                    sortedPeople = getPeople();
                    break;
                }
                if (person.Name.Contains(searchTerm) || person.PersonCity.Name.Contains(searchTerm))
                {
                    sortedPeople.Add(person);
                }
            }
            return sortedPeople;
        }

        public List<Person> getPeople(int searchTerm)
        {
            List<Person> sortedPeople = new List<Person>();
            foreach (Person person in PeopleList)
            {
                if (searchTerm == 0)
                {
                    sortedPeople = getPeople();
                    break;
                }
                if (person.Id == searchTerm)
                {
                    sortedPeople.Add(person);
                }
            }
            return sortedPeople;
        }

        public Person FindByID(int personid)
        {
            Person resultPerson = null;
            foreach (Person p in this.getPeople())
            {
                if (p.Id == personid)
                {
                    resultPerson = p;
                    break;
                }

            }
            return resultPerson;
        }

    }
}
