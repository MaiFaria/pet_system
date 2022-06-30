namespace PS.Core.Helpers
{
    public static class ResourceString
    {
        public static class Campos
        {
            public const string ID   = "ID";
            public const string NOME = "nome";
            public const string CPF = "CPF";
            public const string EMAIL = "E-mail";
            public const string PASSWORD = "Senha";
            public const string PASSWORDCONFIRMATION = "Confirmação de Senha";
            public const string LOGRADOURO   = "Logradouro";
            public const string NUMERO   = "Número";
            public const string BAIRRO   = "Bairro";
            public const string CEP   = "Cep";
            public const string CIDADE   = "Cidade";
            public const string ESTADO   = "Estado";
            public const string DESCRICAO   = "Descrição";
            public const string PRECO   = "Price";
            public const string DATAREGISTRO   = "Data de Registro";
            public const string IMAGEM   = "Imagem";
            public const string QUANTIDADEESTOQUE   = "Quantidade em estoque";
        }

        public static class Mensagens
        {
            public const string MSG_GEN_CAMPO_OBRIGATORIO = @"O campo {0} é obrigatório!";
            public const string MSG_GEN_TAMANHO_CAMPO = @"O campo {0} deve conter entre {1} e {2} caracteres.";
            public const string MSG_GEN_TAMANHO_EXATO_CAMPO = @"O campo {0} deve ter {1} caracteres.";
            public const string MSG_GEN_TAMANHO_MAXIMO_CAMPO = @"O campo {0} deve conter no máximo {1} caracteres.";
            public const string MSG_GEN_CAMPO_INVALIDO = @"O campo {0} é inválido!";
            public const string MSG_GEN_SENHA_NAO_CONFERE = @"A {0} não confere, tente novamente!";
            public const string MSG_GEN_VERIFIQUE_PROPRIEDADES_FILTRO = "Verifique as propriedades do filtro";
            public const string MSG_GEN_JA_EXISTE_REGISTRO_COM_DADO_INFORMADO = @"Já existe {0} com {1} informado.";
            public const string MSG_GEN_NAO_ENCONTRADO = @"{0} não encontrado!";
            public const string MSG_GEN_DADOS_INEXISTENTES = "Não existem dados a serem exibidos";
            public const string MSG_GEN_PESSOAID_INEXISTENTE = "É necessário vincular uma pessoa ao endereço";

            public static string MSG_ID_OBRIGATORIO => string.Format(MSG_GEN_CAMPO_OBRIGATORIO, Campos.ID);
            public static string MSG_NOME_OBRIGATORIO => string.Format(MSG_GEN_CAMPO_OBRIGATORIO, Campos.NOME);
            public static string MSG_CPF_OBRIGATORIO => string.Format(MSG_GEN_CAMPO_OBRIGATORIO, Campos.CPF);
            public static string MSG_EMAIL_OBRIGATORIO => string.Format(MSG_GEN_CAMPO_OBRIGATORIO, Campos.EMAIL);
            public static string MSG_SENHA_NAO_CONFERE => string.Format(MSG_GEN_SENHA_NAO_CONFERE, Campos.PASSWORDCONFIRMATION);
            public static string MSG_LOGRADOURO_OBRIGATORIO => string.Format(MSG_GEN_CAMPO_OBRIGATORIO, Campos.LOGRADOURO);
            public static string MSG_NUMERO_OBRIGATORIO => string.Format(MSG_GEN_CAMPO_OBRIGATORIO, Campos.NUMERO);
            public static string MSG_BAIRRO_OBRIGATORIO => string.Format(MSG_GEN_CAMPO_OBRIGATORIO, Campos.BAIRRO);
            public static string MSG_CEP_OBRIGATORIO => string.Format(MSG_GEN_CAMPO_OBRIGATORIO, Campos.CEP);
            public static string MSG_CIDADE_OBRIGATORIO => string.Format(MSG_GEN_CAMPO_OBRIGATORIO, Campos.CIDADE);
            public static string MSG_ESTADO_OBRIGATORIO => string.Format(MSG_GEN_CAMPO_OBRIGATORIO, Campos.ESTADO);
            public static string MSG_DESCRICAO_OBRIGATORIO => string.Format(MSG_GEN_CAMPO_OBRIGATORIO, Campos.DESCRICAO);
            public static string MSG_PRECO_OBRIGATORIO => string.Format(MSG_GEN_CAMPO_OBRIGATORIO, Campos.PRECO);
            public static string MSG_DATAREGISTRO_OBRIGATORIO => string.Format(MSG_GEN_CAMPO_OBRIGATORIO, Campos.DATAREGISTRO);
            public static string MSG_IMAGEM_OBRIGATORIO => string.Format(MSG_GEN_CAMPO_OBRIGATORIO, Campos.IMAGEM);
            public static string MSG_QUANTIDADEESTOQUE_OBRIGATORIO => string.Format(MSG_GEN_CAMPO_OBRIGATORIO, Campos.QUANTIDADEESTOQUE);
        }
    }
}
