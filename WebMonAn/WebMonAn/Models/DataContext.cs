using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMonAn.Models
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<UserModel> userModels { get; set; }
        public DbSet<FoodModel> foodModels { get; set; }
        public DbSet<CategoryModel> categoryModels { get; set; }
        public DbSet<RoleModel> roleModels { get; set; }
        public DbSet<CartModel> cartModels { get; set; }
        public DbSet<CartDetailsModel> cartDetailsModels { get; set; }
    }
}
