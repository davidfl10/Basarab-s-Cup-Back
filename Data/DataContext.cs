using finalexam_back.Models;
using Microsoft.EntityFrameworkCore;

namespace finalexam_back.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

            modelBuilder.Entity<Team>().HasIndex(t => t.Name).IsUnique();

            modelBuilder.Entity<Team>()
                .HasOne(t => t.User)
                .WithOne(u => u.Team)
                .HasForeignKey<Team>(u => u.UserId);

            modelBuilder.Entity<Cart>()
                .HasOne(c => c.Product)
                .WithMany(p => p.Carts)
                .HasForeignKey(c => c.ProductId);

            modelBuilder.Entity<Cart>()
                .HasOne(c => c.User)
                .WithMany(u => u.Carts)
                .HasForeignKey(c => c.UserId);
        }
    }
}
