using dominio.mdm;
using dominio.mdm.cliente.interfacemodd;
using dominio.mdm.cliente.modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace aplicacao.mdm
{
    public class AplicacaoCliente: IAplicacaoCliente
    {
        readonly IRepositorioCliente _repositorioCliente;
        public AplicacaoCliente(IRepositorioCliente repositorioCliente) 
        {
            _repositorioCliente = repositorioCliente;
        }

        public ObjetoRetorno<bool> ExcluirCliente(int idCliente)
        {
            var retorno = new ObjetoRetorno<bool>();

            try
            {
                retorno.Situacao = TipoRetono.SUCESSO.ToString();
                retorno.Mensagem = "Cliente excluido com sucesso.";
                retorno.Retorno = _repositorioCliente.ExcluirCliente(idCliente);
            }
            catch (Exception ex)
            {
                retorno.Retorno = false;
                retorno.Situacao = TipoRetono.ERRO.ToString();
                retorno.Mensagem = ex.Message;
            }

            return retorno;
        }

        public ObjetoRetorno<Cliente> ObterCliente(int idCliente)
        {
            var retorno = new ObjetoRetorno<Cliente>();

            try
            {
                retorno.Situacao = TipoRetono.SUCESSO.ToString();
                retorno.Mensagem = "Cliente obtido com sucesso.";
                retorno.Retorno = _repositorioCliente.ObterCliente(idCliente);
            }
            catch (Exception ex)
            {
                retorno.Situacao = TipoRetono.ERRO.ToString();
                retorno.Mensagem = ex.Message;
            }

            return retorno;
        }

        public ObjetoRetorno<List<Cliente>> ObterListaCliente(FiltroClienteDto filtro)
        {
            var retorno = new ObjetoRetorno<List<Cliente>>();

            try
            {
                retorno.Situacao = TipoRetono.SUCESSO.ToString();
                retorno.Mensagem = "Cliente obtido com sucesso.";
                retorno.Retorno = _repositorioCliente.ObterListaCliente(filtro);
            }
            catch (Exception ex)
            {
                retorno.Situacao = TipoRetono.ERRO.ToString();
                retorno.Mensagem = ex.Message;
            }

            return retorno;
        }

        public ObjetoRetorno<List<string>> ObterPorteCliente()
        {
            var retorno = new ObjetoRetorno<List<string>>();
            retorno.Retorno = new List<string>();
            retorno.Situacao = TipoRetono.SUCESSO.ToString();
            retorno.Mensagem = "lista obtida com sucesso.";
            foreach (var porte in System.Enum.GetNames(typeof(PorteEmpresa)))
            {
                retorno.Retorno.Add(porte.ToString());
            }
            return retorno;

        }

        public ObjetoRetorno<bool> SalvarCliente(Cliente cliente)
        {
            var retorno = new ObjetoRetorno<bool>();

            try
            {
                retorno.Situacao = TipoRetono.SUCESSO.ToString();
                retorno.Mensagem = "Cliente obtido com sucesso.";
                retorno.Retorno = _repositorioCliente.SalvarCliente(cliente);
            }
            catch (Exception ex)
            {
                retorno.Situacao = TipoRetono.ERRO.ToString();
                retorno.Mensagem = ex.Message;
            }

            return retorno;
        }
    }
}
