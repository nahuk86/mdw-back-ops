using MDW_Back_ops.Models;
using Microsoft.EntityFrameworkCore;


namespace MDW_Back_ops.Data
{

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Bitacora> Bitacoras { get; set; }

    }
}

