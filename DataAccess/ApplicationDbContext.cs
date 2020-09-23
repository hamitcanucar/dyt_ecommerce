using dytsenayasar.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace dytsenayasar.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ContentCategory> ContentCategories { get; set; }
        public DbSet<UserContent> UserContents { get; set; }
        public DbSet<UserRequest> UserRequests { get; set; }
        public DbSet<UserClient> UserClients { get; set; }
        public DbSet<UserForm> UserForms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
               
    }
}