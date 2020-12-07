using dominio.mdm.cliente.modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace dominio.mdm.cliente.interfacemodd
{
    public interface IAplicacaoCliente
    {
        ObjetoRetorno<bool> SalvarCliente(Cliente cliente);
        ObjetoRetorno<List<Cliente>> ObterListaCliente(FiltroClienteDto filtro);
        ObjetoRetorno<Cliente> ObterCliente(int idCliente);
        ObjetoRetorno<bool> ExcluirCliente(int idCliente);
        ObjetoRetorno<List<string>> ObterPorteCliente();
    }
}
