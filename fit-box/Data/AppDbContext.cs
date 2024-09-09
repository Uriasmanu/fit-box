namespace fit_box.Data
{
    using fit_box.Models;
    using Microsoft.EntityFrameworkCore;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // String de conexão
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=tcp:fit-box-server.database.windows.net,1433;Initial Catalog=sql-fit-box;Persist Security Info=False;User ID=manoela;Password=senha123#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        // Definição das tabelas
        public DbSet<Login> Logins { get; set; }
        public DbSet<Marmita> Marmitas { get; set; }
        public DbSet<Ingredientes> Ingredientes { get; set; }

        // Configurações do modelo
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Login>()
                .ToTable("Logins")
                .HasKey(l => l.Id);

            modelBuilder.Entity<Login>()
                .Property(l => l.Username)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Marmita>()
                .ToTable("Marmitas")
                .HasKey(m => m.Id);

            modelBuilder.Entity<Ingredientes>()
                .ToTable("Ingredientes")
                .HasKey(i => i.Id);

            // Configurando o relacionamento muitos-para-muitos entre Marmita e Ingredientes
            modelBuilder.Entity<Marmita>()
                .HasMany(m => m.Ingredientes)
                .WithMany(i => i.Marmitas)
                .UsingEntity(j => j.ToTable("MarmitaIngredientes"));

            // Configurando o relacionamento entre Marmita e Login
            modelBuilder.Entity<Marmita>()
                .HasOne(m => m.Login)
                .WithMany(l => l.Marmitas)
                .HasForeignKey(m => m.LoginId);
        }

    }
}
