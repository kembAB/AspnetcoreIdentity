namespace WebApplication1.Models
{
    public class AccountTypes
    {
        public const string Administrator = "Admin";
        public static string AdministratorNormalized = Administrator.ToUpper();

        public const string Standard = "Standard";
        public static string StandardNormalized = Standard.ToUpper();

    }
}
