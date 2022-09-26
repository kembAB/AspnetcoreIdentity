
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using WebApplication1.Models;
using WebApplication1.Models.Languages;
using WebApplication1.Models.People;

namespace WebApplication1.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {  
        }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<PersonLanguage> PersonLanguage { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Country>().HasMany<City>().WithOne().OnDelete(DeleteBehavior.SetNull);

            Language english = new Language() { Id = 1, Name = "English" };
            Language swedish = new Language() { Id = 2, Name = "Swedish" };
            Language japanese = new Language() { Id = 3, Name = "Japanese" };

            modelBuilder.Entity<Language>().HasData(english, swedish, japanese);

            modelBuilder.Entity<Country>().HasData(new Country() { Id = 1, Name = "Sweden" }, new Country() { Id=2, Name="Denmark"});

            modelBuilder.Entity<City>().HasData(new City() { Id = 1, Name = "Gothenburg", CountryId = 1 }, new City() { Id = 2, Name = "Stockholm", CountryId = 1 }, new City() { Id = 3, Name = "Kopenhagen", CountryId = 2 });
            modelBuilder.Entity<Person>().HasData(

           new Person(1, "Benjamin Nordin", 555123, 1) { },
           new Person(2, "Eda Clawthorn", 6694875, 1) {  },
           new Person(3, "King Clawthorn", 555213345, 1),
           new Person(4, "Marcy Wou", 777485632, 1),
           new Person(5, "Jonas Edenstav", 031222666, 1)
           /*OK？　本当に、コドが悪い*/
               );
            
            modelBuilder.Entity<Country>().HasMany(o => o.Cities).WithOne(o => o.Country).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<City>().HasMany(o => o.People).WithOne(o => o.PersonCity).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PersonLanguage>().HasKey(pl => new { pl.PersonId, pl.LanguageId });
            modelBuilder.Entity<PersonLanguage>().HasOne(pl => pl.Person).WithMany(pl => pl.LanguagesLinkObject).HasForeignKey(pl => pl.PersonId);
            modelBuilder.Entity<PersonLanguage>().HasOne(pl => pl.Language).WithMany(pl => pl.People).HasForeignKey(pl => pl.LanguageId);

            modelBuilder.Entity<PersonLanguage>().HasData(new PersonLanguage {PersonId = 1,LanguageId = 1});
            modelBuilder.Entity<PersonLanguage>().HasData(new PersonLanguage {PersonId = 2,LanguageId = 1});
            modelBuilder.Entity<PersonLanguage>().HasData(new PersonLanguage {PersonId = 3,LanguageId = 1});
            modelBuilder.Entity<PersonLanguage>().HasData(new PersonLanguage {PersonId = 4,LanguageId = 1});
            
            modelBuilder.Entity<PersonLanguage>().HasData(new PersonLanguage { PersonId = 1, LanguageId = 3 });
            modelBuilder.Entity<PersonLanguage>().HasData(new PersonLanguage { PersonId = 1, LanguageId = 2 });


            // Adding user roles

            string adminRoleId = Guid.NewGuid().ToString();
            string StandardRoleId = Guid.NewGuid().ToString();
            string userId = Guid.NewGuid().ToString();

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = adminRoleId, Name = AccountTypes.Administrator, NormalizedName = AccountTypes.AdministratorNormalized });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = StandardRoleId, Name = AccountTypes.Standard, NormalizedName = AccountTypes.StandardNormalized});

            PasswordHasher<ApplicationUser> hasher = new PasswordHasher<ApplicationUser>();

            modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = userId,
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                PasswordHash = hasher.HashPassword(null, "password")

            }) ;

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = adminRoleId,
                UserId = userId
            }) ;
            

        }


    }
        

}
