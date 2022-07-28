using Core.Entidades;
using Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.Context
{
    public class Repository<T> : IRepository<T> where T : EntidadeBase
    {
        protected IUnitOfWork Context;

        public Repository(IUnitOfWork unitOfWork) => Context = unitOfWork;

        public virtual void Inserir(T entidade)
        {
            try
            {
                Context.Set<T>().Add(entidade);
                Salvar();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void Remover(T entidade)
        {
            Context.Set<T>().Remove(entidade);
            Salvar();
        }

        public virtual void Atualizar(T entidade)
        {
            var entityEntry = Context.Entry(entidade);
            entityEntry.State = EntityState.Modified;
            Salvar();
        }

        public void Salvar() => Context.SaveChanges();

        public virtual T? RetornarPorId(string id)
            => Context.Set<T>().FirstOrDefault(e => e.Id == id);
    }
}