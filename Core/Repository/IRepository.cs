using Core.Entidades;

namespace Core.Repository
{
    public interface IRepository<T> where T : EntidadeBase
    {
        void Inserir(T entidade);
        T? RetornarPorId(string id);
        void Atualizar(T entidade);
        void Remover(T entidade);
    }
}