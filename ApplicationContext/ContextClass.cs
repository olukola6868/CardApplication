using HarnyCardApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace HarnyCardApplication.ApplicationContext
{
    public class ContextClass : DbContext
    {   
        public ContextClass(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Card> Cards { get; set;}
        public DbSet<Category> Categories{get;set;}
        public DbSet<Customer> Customers{get;set;}
        public DbSet<Manager> Managers{get;set;}
        public DbSet<Network> Networks{get;set;}
        public DbSet<NetworkCategory> NetworkCategories{get;set;}
        public DbSet<Role> Roles{get;set;}
        public DbSet<User> Users{get;set;}
        public DbSet<UserRole> UserRoles{get;set;}
    }
}