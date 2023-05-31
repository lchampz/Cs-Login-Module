using LoginModule.Entities;
using LoginModule.ValueObjects;
using Microsoft.EntityFrameworkCore;


namespace LoginModule.Context {
    public class AppDbContext : DbContext {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {

        }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<User> User { get; set; }
    }
}
