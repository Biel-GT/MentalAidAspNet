using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TesteAdoNET.Data.Config;
using TesteAdoNET.Data.Entities;

namespace TesteAdoNET.Data
{
    public class AppContext : DbContext
    {
        public AppContext() : base ("MentalAid") { }
       
        public static AppContext Create()
        {
            return new AppContext();
        }

        public DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configure entities here
            modelBuilder.Configurations.Add(new UsuarioMap());
        }
    }
}