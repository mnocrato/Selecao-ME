using Core.DTO;
using Core.Entidades;

namespace Core.Repository
{
    public interface IPedidoRepository : IRepository<Pedido>
    {
        bool PedidoExiste(string nomePedido);
        Pedido? RetornarPorNomePedido(string nomePedido);
    }
}