namespace PS.WebApp.Models
{
    public class OrderViewModel
    {
        #region Pedido

        public int Code { get; set; }

        // Autorizado = 1,
        // Pago = 2,
        // Recusado = 3,
        // Entregue = 4,
        // Cancelado = 5

        public int Status { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalValue { get; set; }

        public decimal Discount { get; set; }
        public bool VoucherUsed { get; set; }

        public List<OrderItensViewModel> OrderItens { get; set; } = new List<OrderItensViewModel>();

        #endregion

        #region Item Pedido

        public class OrderItensViewModel
        {
            public Guid ProductId { get; set; }
            public string Name { get; set; }
            public int Quantity { get; set; }
            public decimal Value { get; set; }
            public string Image { get; set; }
        }

        #endregion

        #region Endereco

        public AddressViewModel Address { get; set; }

        #endregion
    }
}