namespace Core.Entidades
{
    public class Pedido : EntidadeBase
    {
        public Pedido(string nome, string itens)
        {
            Nome = nome;
            Itens = itens;
        }

        public string Nome { get; set; }
        public string Itens { get; set; }
    }
}
