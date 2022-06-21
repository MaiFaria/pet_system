using PS.Tests.Builders;

namespace PS.Tests.Utils
{
    public static class CNPJ
    {
        public static string Gerar()
        {
            var tamanhoDocumento = 14;
            const int TAMANHO_DIGITO_VERIFICADOR = 2;
            tamanhoDocumento -= TAMANHO_DIGITO_VERIFICADOR;

            var faker = FakerBuilder.New().Build();
            var cnpj = string.Join(string.Empty, faker.Random.Digits(count: tamanhoDocumento).Select(valor => valor.ToString()));

            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != tamanhoDocumento)
                return string.Empty;
            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj += digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito += resto.ToString();
            return cnpj + digito;
        }
    }
}
