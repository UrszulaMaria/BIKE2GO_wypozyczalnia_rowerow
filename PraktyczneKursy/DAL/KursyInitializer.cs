using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using PraktyczneKursy.Models;
using PraktyczneKursy.Migrations;
using System.Data.Entity.Migrations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PraktyczneKursy.DAL
{
    public class KursyInitializer : MigrateDatabaseToLatestVersion<KursyContext, Configuration>
    {
        public static void SeedKursyData(KursyContext context)
        {
            var kategorie = new List<Kategoria>
            {
                new Kategoria() { KategoriaId=1, NazwaKategorii="Rower_10min", NazwaPlikuIkony="turbina.png", OpisKategorii=" Turbina" },
                new Kategoria() { KategoriaId=2, NazwaKategorii="Rower_20min", NazwaPlikuIkony="turbina.png", OpisKategorii="Turbina" },
                new Kategoria() { KategoriaId=3, NazwaKategorii="Rower_30min", NazwaPlikuIkony="turbina.png", OpisKategorii="Turbina" },
                new Kategoria() { KategoriaId=4, NazwaKategorii="Rower_120min", NazwaPlikuIkony="turbina.png", OpisKategorii="Turbina" },
                new Kategoria() { KategoriaId=5, NazwaKategorii="Rower_180min", NazwaPlikuIkony="pompa.png", OpisKategorii="pompa" },
                new Kategoria() { KategoriaId=6, NazwaKategorii="Rower_360min", NazwaPlikuIkony="panel.png", OpisKategorii="panel" },
                 new Kategoria() { KategoriaId=7, NazwaKategorii="Rower_1440min", NazwaPlikuIkony="panel.png", OpisKategorii="panel" },

            };

            kategorie.ForEach(k => context.Kategorie.AddOrUpdate(k));
            context.SaveChanges();

            var kursy = new List<Kurs>
            {
                new Kurs() { KursId=1, AutorKursu="Wypożyczenie", TytulKursu="Rower_10min", KategoriaId=1, CenaKursu=5, Bestseller=true, NazwaPlikuObrazka="1.jpg",
                DataDodania = DateTime.Now, OpisKursu="Rower 10 min"},
                new Kurs() { KursId=2, AutorKursu="Wypożyczenie", TytulKursu="Rower_10min", KategoriaId=1, CenaKursu=15, Bestseller=true, NazwaPlikuObrazka="1.jpg",
                DataDodania = DateTime.Now, OpisKursu="Rower 10 min"},
                new Kurs() { KursId=3, AutorKursu="Wypożyczenie", TytulKursu="Rower_20min", KategoriaId=2, CenaKursu=20, Bestseller=true, NazwaPlikuObrazka="1.jpg",
                DataDodania = DateTime.Now, OpisKursu="Rower 30 min"},

                new Kurs() { KursId=4, AutorKursu="Wypożyczenie", TytulKursu="Rower_20min", KategoriaId=2, CenaKursu=25, Bestseller=false, NazwaPlikuObrazka="2.jpg",
                DataDodania = DateTime.Now, OpisKursu="Rower 30 min"},
                new Kurs() { KursId=5, AutorKursu="Wypożyczenie", TytulKursu="Rower_30min", KategoriaId=3, CenaKursu=30, Bestseller=true, NazwaPlikuObrazka="2.jpg",
                DataDodania = DateTime.Now, OpisKursu="Rower 30 min"},
                new Kurs() { KursId=6, AutorKursu="Wypożyczenie", TytulKursu="Rower_120min", KategoriaId=4, CenaKursu=35, Bestseller=false, NazwaPlikuObrazka="2.jpg",
                DataDodania = DateTime.Now, OpisKursu="Rower 120 min"},

                new Kurs() { KursId=7, AutorKursu="Wypożyczenie", TytulKursu="Rower_120min", KategoriaId=4, CenaKursu=40, Bestseller=false, NazwaPlikuObrazka="2.jpg",
                DataDodania = DateTime.Now, OpisKursu="Rower 120 min"},
                new Kurs() { KursId=8, AutorKursu="Wypożyczenie", TytulKursu="Rower_180min", KategoriaId=5, CenaKursu=45, Bestseller=false, NazwaPlikuObrazka="3.jpg",
                DataDodania = DateTime.Now, OpisKursu="Rower 180 min"},
                new Kurs() { KursId=9, AutorKursu="Wypożyczenie", TytulKursu="Rower_360min", KategoriaId=6, CenaKursu=48, Bestseller=true, NazwaPlikuObrazka="3.jpg",
                DataDodania = DateTime.Now, OpisKursu="Rower 360 min"},
                 new Kurs() { KursId=10, AutorKursu="Wypożyczenie",TytulKursu="Rower_1440min", KategoriaId=7, CenaKursu=50, Bestseller=true, NazwaPlikuObrazka="3.jpg",
                DataDodania = DateTime.Now, OpisKursu="Rower 1440 min"},


            };
            kursy.ForEach(k => context.Kursy.AddOrUpdate(k));
            context.SaveChanges();

        }

        public static void SeedUzytkownicy(KursyContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            const string name = "admin@vivende.pl";
            const string password = "Tomasz1995!";
            const string roleName = "Admin";

            var user = userManager.FindByName(name);
            if (user == null)
            {
                user = new ApplicationUser { UserName = name, Email = name, DaneUzytkownika = new DaneUzytkownika() };
                var result = userManager.Create(user, password);
            }

            // utworzenie roli Admin jeśli nie istnieje 
            var role = roleManager.FindByName(roleName);
            if (role == null)
            {
                role = new IdentityRole(roleName);
                var roleresult = roleManager.Create(role);
            }

            // dodanie uzytkownika do roli Admin jesli juz nie jest w roli
            var rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                var result = userManager.AddToRole(user.Id, role.Name);
            }
        }
    }
}