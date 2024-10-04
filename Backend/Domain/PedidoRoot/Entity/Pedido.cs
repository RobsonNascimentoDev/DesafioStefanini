using Domain.ItensPedidoRoot.Entity;

namespace Domain.PedidoRoot.Entity
{
    public class Pedido
    {
        public int Id { get; set; }
        public string NomeCliente { get; set; }
        public string EmailCliente { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Pago { get; set; }
        public List<ItensPedido> Itens { get; set; } = new List<ItensPedido>();
    }
}
