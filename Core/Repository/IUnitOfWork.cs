using Core.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Core.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        DatabaseFacade Database { get; }
        EntityEntry Entry(object targetValue);
        int SaveChanges();
        void Rollback();
        DbSet<T> Set<T>() where T : class;

        DbSet<Pedido> Pedidos { get; set; }
    }
}
