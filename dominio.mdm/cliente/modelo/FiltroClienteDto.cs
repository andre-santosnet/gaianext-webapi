using System;
using System.Collections.Generic;
using System.Text;

namespace dominio.mdm.cliente.modelo
{
    public class FiltroClienteDto
    {
        string nome;
        string porte;

        public string Nome { get => nome; set => nome = value; }
        public string Porte { get => porte; set => porte = value; }
    }
}
