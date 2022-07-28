using Core.Entidades;
using Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.Context
{
    public class MEContext : DbContext, IUnitOfWork
    {
        private bool _rolledback;

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

        public void Rollback()
        {
            if (Database.CurrentTransaction == null || _rolledback) return;
            Database.CurrentTransaction.Rollback();
            _rolledback = true;
        }
    }
}
