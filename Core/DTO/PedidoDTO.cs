using System.ComponentModel.DataAnnotations;

namespace Core.DTO
{
    public struct PedidoDTO
    {
        [Required(ErrorMessage = "Pedido é obrigatório")]
        public string Pedido { get; set; }

        [Required(ErrorMessage = "Itens é obrigatório")]
        public ItemDTO[] Itens { get; set; }
    }
}
