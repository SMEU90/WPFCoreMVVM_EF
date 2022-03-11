using WPFCoreMVVM_EF.Models;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Linq;
using System.Text;
using System;
using Microsoft.Extensions.Configuration;

namespace WPFCoreMVVM_EF.Models.Base
{
    public class ContextDB : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<Personal> Personals { get; set; }

        public ContextDB(DbContextOptions<ContextDB> opt) : base(opt)
        {
            Database.EnsureCreated();
        }

        private static ContextDB _context;
        public static ContextDB GetContext()
        {
            if (_context == null)
            {



                var configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
               .Build();

                var db_options = new DbContextOptionsBuilder<ContextDB>()
                   .UseSqlServer(configuration.GetConnectionString("Default"))
                   .Options;

                _context = new ContextDB(db_options);


                string login = "SMEU";
                string password = "dfrfrfgf";

               /* User user = _context.Users.FirstOrDefault(p => p.Login == login && p.Password == password);
                //User user = _contex.Users.AsQueryable().Where(p => p.Login == login.Trim() && p.Password == password.Trim()).FirstOrDefault();
                if (user != null)
                {
                    MessageBox.Show("Good");
                }
               */



               /// MessageBox.Show("Good");
            }    
                
            return _context; ;
        }
    }
}
