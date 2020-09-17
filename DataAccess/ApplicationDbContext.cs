using dytsenayasar.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace dytsenayasar.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }       
    }
}