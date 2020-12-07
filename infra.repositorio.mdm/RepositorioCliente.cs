using dominio.mdm.cliente.interfacemodd;
using dominio.mdm.cliente.modelo;
using infra.repositorio.mdm.ORM;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace infra.repositorio.mdm
{
    public class RepositorioCliente : IRepositorioCliente
    {

        readonly ORMContexto _ORMContexto;

        public RepositorioCliente(ORMContexto TCContexto)
        {
            _ORMContexto = TCContexto;
        }

        public bool ExcluirCliente(int idCliente)
        {
            bool retorno = false;
            try
            {
                var cliente = _ORMContexto.TbClientes.FirstOrDefault(n => n.Id == idCliente);
                if (cliente != null)
                {
                    _ORMContexto.TbClientes.Attach(cliente);
                    _ORMContexto.TbClientes.Remove(cliente);
                    _ORMContexto.SaveChanges();
                    retorno = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retorno;
        }

        public Cliente ObterCliente(int idCliente)
        {
            var retorno = new Cliente();
            try
            {
                var dadoDB = _ORMContexto.TbClientes.Where(n => n.Id == idCliente).FirstOrDefault();
                if (dadoDB != null)
                {
                    retorno.Nome = dadoDB.Nome;
                    retorno.Porte = dadoDB.Porte;
                    retorno.Id = dadoDB.Id;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retorno;
        }

        public List<Cliente> ObterListaCliente(FiltroClienteDto filtro)
        {
            var resultado = new List<Cliente>();

            _ORMContexto.Database.OpenConnection();
            DbCommand cmd = _ORMContexto.Database.GetDbConnection().CreateCommand();

            cmd.CommandText = "SELECT nome, porte, id  FROM [dbo].[TB_Cliente] where 1 = 1 ";

            if (!string.IsNullOrEmpty(filtro.Nome))
            {
                cmd.Parameters.Add(new SqlParameter("@Nome", filtro.Nome));
                cmd.CommandText += " AND  nome = @Nome ";
            }

            if (!string.IsNullOrEmpty(filtro.Porte))
            {
                cmd.Parameters.Add(new SqlParameter("@Porte", filtro.Porte));
                cmd.CommandText += " AND  porte = @Porte ";
            }

            using (var leitor = cmd.ExecuteReader())
            {

                while (leitor.Read())
                {
                    var cliente = new Cliente();
                    var id = leitor["Id"];
                    if (id != DBNull.Value)
                    {
                        cliente.Id = Convert.ToInt32(id);
                    }
                    var nome = leitor["Nome"];
                    if (nome != DBNull.Value)
                    {
                        cliente.Nome = nome.ToString();
                    }
                    var porte = leitor["Porte"];
                    if (porte != DBNull.Value)
                    {
                        cliente.Porte = porte.ToString();
                    }
                    resultado.Add(cliente);
                }
            }
            return resultado;
        }

        public bool SalvarCliente(Cliente cliente)
        {
            if (cliente.Id == 0)
            {
                return IncluirCliente(cliente);
            }
            else
            {
                return AlterarCliente(cliente);
            }
        }

        private bool IncluirCliente(Cliente cliente)
        {
            bool retorno = false;
            try
            {
                var dadoDB = _ORMContexto.TbClientes.Where(n => n.Porte == cliente.Porte && n.Nome == cliente.Nome).FirstOrDefault();
                if (dadoDB == null)
                {
                    var dado = new TbCliente
                    {
                        Nome = cliente.Nome,
                        Porte = cliente.Porte,
                    };

                    _ORMContexto.TbClientes.Add(dado);
                    _ORMContexto.SaveChanges();
                    retorno = true;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retorno;
        }

        private bool AlterarCliente(Cliente cliente)
        {
            bool retorno = false;
            try
            {
                var dado = _ORMContexto.TbClientes.Where(n => n.Id == cliente.Id).FirstOrDefault();
                if (dado != null)
                {
                    dado.Nome = cliente.Nome;
                    dado.Porte = cliente.Porte;
                    _ORMContexto.SaveChanges();
                    retorno = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retorno;
        }


    }
}
