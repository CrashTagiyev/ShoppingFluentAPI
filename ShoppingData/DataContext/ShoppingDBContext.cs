using Entities.Abstracts;
using Entities.Concretes.Orders;
using Entities.Concretes.Products;
using Entities.Concretes.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ShoppingData.DataContext.ProductDataContexts
{
    public class ShoppingDBContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-HFMQLBA\\MSSQLSERVER01;Initial Catalog=Shopping;Integrated Security=True;Connect Timeout=30;Encrypt=False;");
            base.OnConfiguring(optionsBuilder);
        }



        //Product entities
        //Products
        public DbSet<ProductEntity> Products { get; set; }
     
        ////Category
        public DbSet<Categories> Categories { get; set; }

        //Users
        public DbSet<UsernamePassword> UsernamePasswords { get; set; }
        public DbSet<UsersAccounts> UserAccounts { get; set; }

        //Orders
        public DbSet<ProductsOrders> ProductsOrders { get; set; }
 
    }
}
