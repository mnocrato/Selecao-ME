using Core.Entidades;
using Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.Context
{
    public class MEContext : DbContext, IUnitOfWork
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("Teste");
        }

        public DbSet<Pedido> Pedidos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pedido>().HasKey(u => new
            {
                u.Id,
                u.Nome
            });
            modelBuilder.Entity<Pedido>().Property("Itens").IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
