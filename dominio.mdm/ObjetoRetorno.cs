using System;
using System.Collections.Generic;
using System.Text;

namespace dominio.mdm
{
    public class ObjetoRetorno<T>
    {
        string situacao;
        T retorno;
        string mensagem;

        public string Situacao { get => situacao; set => situacao = value; }
        public T Retorno { get => retorno; set => retorno = value; }
        public string Mensagem { get => mensagem; set => mensagem = value; }
    }
}
