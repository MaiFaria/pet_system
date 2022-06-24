namespace PS.Core.Helper
{
    public static class ResourceString
    {
        public static class Campos
        {
            public const string NOME = "nome";
            public const string CPF = "CPF";
            public const string EMAIL = "E-mail";
            public const string PASSWORD = "Senha";
            public const string PASSWORDCONFIRMATION = "Confirmação de Senha";
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

            public static string MSG_NOME_OBRIGATORIO => string.Format(MSG_GEN_CAMPO_OBRIGATORIO, Campos.NOME);
            public static string MSG_CPF_OBRIGATORIO => string.Format(MSG_GEN_CAMPO_OBRIGATORIO, Campos.CPF);
            public static string MSG_EMAIL_OBRIGATORIO => string.Format(MSG_GEN_CAMPO_OBRIGATORIO, Campos.EMAIL);
            public static string MSG_SENHA_NAO_CONFERE => string.Format(MSG_GEN_SENHA_NAO_CONFERE, Campos.PASSWORDCONFIRMATION);
        }
    }
}
