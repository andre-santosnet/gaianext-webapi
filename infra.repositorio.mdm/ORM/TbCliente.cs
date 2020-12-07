using System;
using System.Collections.Generic;

#nullable disable

namespace infra.repositorio.mdm.ORM
{
    public partial class TbCliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Porte { get; set; }
    }
}
