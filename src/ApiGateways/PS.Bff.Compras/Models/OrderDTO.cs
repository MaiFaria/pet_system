using PS.Core.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PS.Bff.Compras.Models
{
    public class OrderDTO
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
        public string VoucherCode { get; set; }
        public bool VoucherUsed { get; set; }

        public List<CartItemDTO> OrderItens { get; set; }

        #endregion

        #region Endereco

        public AddressDTO Address { get; set; }

        #endregion

        #region Cartão

        [Required(ErrorMessage = "Informe o número do cartão")]
        [DisplayName("Número do Cartão")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Informe o nome do portador do cartão")]
        [DisplayName("Nome do Portador")]
        public string CardName { get; set; }

        [RegularExpression(@"(0[1-9]|1[0-2])\/[0-9]{2}", ErrorMessage = "O vencimento deve estar no padrão MM/AA")]
        [CartaoExpiracao(ErrorMessage = "Cartão Expirado")]
        [Required(ErrorMessage = "Informe o vencimento")]
        [DisplayName("Data de Vencimento MM/AA")]
        public string CardExpiration { get; set; }

        [Required(ErrorMessage = "Informe o código de segurança")]
        [DisplayName("Código de Segurança")]
        public string CvvCard { get; set; }

        #endregion
    }
}