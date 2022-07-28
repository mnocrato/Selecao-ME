using Core.DTO;
using Core.Entidades;

namespace Core.Repository
{
    public interface IPedidoRepository : IRepository<Pedido>
    {
        Pedido? RetornarPorNomePedido(string nomePedido);
    }
}