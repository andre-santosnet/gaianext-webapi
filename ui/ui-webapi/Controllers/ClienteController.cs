using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dominio.mdm;
using dominio.mdm.cliente.interfacemodd;
using dominio.mdm.cliente.modelo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ui_webapi.Controllers
{
    
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {

        readonly IAplicacaoCliente _aplicacaoCliente;

        public ClienteController(IAplicacaoCliente aplicacaoCliente)
        {
            _aplicacaoCliente = aplicacaoCliente;
        }

        [HttpPost("[Action]")]
        public ObjetoRetorno<bool> SalvarCliente([FromBody]Cliente cliente)
        {
            return _aplicacaoCliente.SalvarCliente(cliente);
        }

        [HttpPost("[Action]")]
        public ObjetoRetorno<List<Cliente>> ObterListaCliente([FromBody]FiltroClienteDto filtro)
        {
            return _aplicacaoCliente.ObterListaCliente(filtro);
        }

        [HttpGet("ObterCliente/{idCliente}")]
        public ObjetoRetorno<Cliente> ObterCliente(int idCliente)
        {
            return _aplicacaoCliente.ObterCliente(idCliente);
        }

        [HttpGet("ExcluirCliente/{idCliente}")]
        public ObjetoRetorno<bool> ExcluirCliente(int idCliente)
        {
            return _aplicacaoCliente.ExcluirCliente(idCliente);
        }

        [HttpGet("ObterPorteCliente")]
        public ObjetoRetorno<List<string>> ObterPorteCliente()
        {
            return _aplicacaoCliente.ObterPorteCliente();
        }

    }
}
