using Ha_shemNetworksApiCommon.Entities;
using Ha_shemNetworksApiCommon.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ha_shemNetworksApi.Api
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options)
        : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Add default data to database
            modelBuilder.Entity<User>().HasData(new User() {Id = 1, FirstName = "Admin", LastName = "Admin", Password = new PasswordHasher().HashPassword("Admin0183$"), Role = Role.Admin, Username = "Admin"});
            modelBuilder.Entity<Book>().HasData(new Book()
            {
                Title = "In Search of Lost Time",
                Author = "Marcel Proust",
                Available = true,
                ISBN = "",
                Id = 1,
                Status = true
            });
            modelBuilder.Entity<Book>().HasData(new Book()
            {
                Title = "Don Quixote",
                Author = "Miguel de Cervantes",
                Available = true,
                ISBN = "",
                Id = 2,
                Status = true
            });
        }
    }
}
