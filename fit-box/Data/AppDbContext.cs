namespace fit_box.Data
{
    using fit_box.Models;
    using Microsoft.EntityFrameworkCore;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Login> Logins { get; set; }
        public DbSet<Ingredientes> Ingredientes { get; set; }
        public DbSet<Marmita> Marmitas { get; set; }
        public DbSet<MarmitaIngrediente> MarmitaIngredientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurações para a tabela Login
            modelBuilder.Entity<Login>()
                .ToTable("Logins")
                .HasKey(l => l.Id);

            modelBuilder.Entity<Login>()
                .Property(l => l.Username)
                .IsRequired()
                .HasMaxLength(100);

            // Configurações para a tabela Ingredientes
            modelBuilder.Entity<Ingredientes>()
                .ToTable("Ingredientes")
                .HasKey(i => i.Id);

            modelBuilder.Entity<Ingredientes>()
                .Property(i => i.NameIngrediente)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Ingredientes>()
                .Property(i => i.QuantidadeEmEstoque)
                .IsRequired();

            modelBuilder.Entity<Ingredientes>()
                .HasOne(i => i.Login)
                .WithMany(l => l.Ingredientes)
                .HasForeignKey(i => i.LoginId)
                .OnDelete(DeleteBehavior.Restrict); // Impede exclusão em cascata

            // Configurações para a tabela Marmita
            modelBuilder.Entity<Marmita>()
                .ToTable("Marmitas")
                .HasKey(m => m.Id);

            modelBuilder.Entity<Marmita>()
                .Property(m => m.NameMarmita)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Marmita>()
                .Property(m => m.TamanhoMarmita)
                .IsRequired();

            modelBuilder.Entity<Marmita>()
                .HasOne(m => m.Login)
                .WithMany(l => l.Marmitas)
                .HasForeignKey(m => m.LoginId)
                .OnDelete(DeleteBehavior.Restrict); // Impede exclusão em cascata

            // Configurações para a tabela MarmitaIngrediente
            modelBuilder.Entity<MarmitaIngrediente>()
                .ToTable("MarmitaIngredientes")
                .HasKey(mi => new { mi.MarmitaId, mi.IngredienteId });

            modelBuilder.Entity<MarmitaIngrediente>()
                .HasOne(mi => mi.Marmita)
                .WithMany(m => m.MarmitaIngredientes)
                .HasForeignKey(mi => mi.MarmitaId)
                .OnDelete(DeleteBehavior.Cascade); // Permite exclusão em cascata

            modelBuilder.Entity<MarmitaIngrediente>()
                .HasOne(mi => mi.Ingrediente)
                .WithMany()
                .HasForeignKey(mi => mi.IngredienteId)
                .OnDelete(DeleteBehavior.Restrict); // Impede exclusão em cascata

            modelBuilder.Entity<MarmitaIngrediente>()
                .Property(mi => mi.QuantidadeEmGramas)
                .IsRequired();
        }
    }
}
