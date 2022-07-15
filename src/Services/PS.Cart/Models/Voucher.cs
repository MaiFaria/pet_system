namespace PS.Cart.Models
{
    public class Voucher
    {
        public decimal? Percent { get; set; }
        public decimal? DiscountValue { get; set; }
        public string Code { get; set; }
        public DiscountTypeVoucher DiscountType { get; set; }
    }

    public enum DiscountTypeVoucher
    {
        Percentage = 0,
        Value = 1
    }
}