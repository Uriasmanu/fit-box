namespace fit_box.Data
{
    using fit_box.Models;
    using Microsoft.EntityFrameworkCore;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet <Login> Logins {get; set; }


    }

}
