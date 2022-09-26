using System.ComponentModel.DataAnnotations;
using WebApplication1.Models.Languages;

namespace WebApplication1.Models.People
{
    public class PersonLanguage
    {
        public int PersonId { get; set; }
        public Person Person { get; set; }

        public int LanguageId { get; set; }
        public Language Language { get; set; }

    }
}
