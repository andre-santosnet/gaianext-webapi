using dominio.mdm.cliente.modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace dominio.mdm.cliente.interfacemodd
{
    public interface IRepositorioCliente
    {
        bool SalvarCliente(Cliente cliente);
        List<Cliente> ObterListaCliente(FiltroClienteDto filtro);
        Cliente ObterCliente(int idCliente);
        bool ExcluirCliente(int idCliente);
    }
}
