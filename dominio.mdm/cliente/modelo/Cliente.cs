using System;
using System.Collections.Generic;
using System.Text;

namespace dominio.mdm.cliente.modelo
{
    public class Cliente
    {
        int id;
        string nome;
        string porte;

        public int Id { get => id; set => id = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Porte { get => porte; set => porte = value; }
    }
}
