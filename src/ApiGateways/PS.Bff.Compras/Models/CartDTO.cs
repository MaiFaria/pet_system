namespace PS.Bff.Compras.Models
{
    public class CartDTO
    {
        public decimal TotalValue { get; set; }
        public VoucherDTO Voucher { get; set; }
        public bool VoucherUsed { get; set; }
        public decimal Discount { get; set; }
        public List<CartItemDTO> Itens { get; set; } = new List<CartItemDTO>();
    }
}