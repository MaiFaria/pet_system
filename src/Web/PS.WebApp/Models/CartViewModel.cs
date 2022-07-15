namespace PS.WebApp.Models
{
    public class CartViewModel
    {
        public decimal TotalValue { get; set; }
        public VoucherViewModel Voucher { get; set; }
        public bool VoucherUsed { get; set; }
        public decimal Discount { get; set; }
        public List<CartItemViewModel> Itens { get; set; } = new List<CartItemViewModel>();
    }

    public class CartItemViewModel
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Value { get; set; }
        public string Image { get; set; }
    }
}