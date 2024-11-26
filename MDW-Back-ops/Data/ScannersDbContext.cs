using MDW_Back_ops.Models;
using Microsoft.EntityFrameworkCore;

namespace MDW_Back_ops.Data
{
    public class ScannersDbContext : DbContext
    {
        public ScannersDbContext(DbContextOptions<ScannersDbContext> options) : base(options) { }

        public DbSet<Scanner> Scanners { get; set; }
    }

}
