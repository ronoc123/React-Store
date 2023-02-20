using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using react_store.Entities;

namespace react_store.Data
{
    public class ReactStoreContext : DbContext
    {
        public ReactStoreContext(DbContextOptions options) : base(options)
        {  
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=reactstoredb;Trusted_Connection=true;TrustServerCertificate=true;");
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Basket> Baskets {get; set;}
    }
}