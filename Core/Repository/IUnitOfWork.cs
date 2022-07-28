using Core.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Core.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        EntityEntry Entry(object targetValue);
        int SaveChanges();
        DbSet<T> Set<T>() where T : class;

        DbSet<Pedido> Pedidos { get; set; }
    }
}
