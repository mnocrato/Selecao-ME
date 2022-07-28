using Core.Entidades;
using Core.Repository;

namespace Data.Repository.Context
{
    public class PedidoRepository : Repository<Pedido>, IPedidoRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public PedidoRepository(IUnitOfWork unitOfWork) : base(unitOfWork) => _unitOfWork = unitOfWork;

        public Pedido? RetornarPorNomePedido(string nomePedido)
            => _unitOfWork.Pedidos.FirstOrDefault(w => w.Nome == nomePedido);
        
        public bool PedidoExiste(string nomePedido)
            => _unitOfWork.Pedidos.Any(w => w.Nome == nomePedido);
    }
}