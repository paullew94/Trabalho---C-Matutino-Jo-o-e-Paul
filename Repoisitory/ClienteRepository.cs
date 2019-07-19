using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repoisitory
{
    public class ClienteRepository
    {
        Conexao conexao = new Conexao();
        public int Inserir(Cliente cliente)
        {
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = @"INSERT INTO clientes (nome,cpf,data_nascimento,numero,complemento,logradouro,cep,id_cidade)
OUTPUT INSERTED.ID VALUES (@NOME,@CPF,@DATA_NASCIMENTO,@NUMERO,@COMPLEMENTO,@LOGRADOURO,@CEP,@ID_CIDADE)";
            comando.Parameters.AddWithValue("@NOME", cliente.Nome);
            comando.Parameters.AddWithValue("@CPF", cliente.Cpf);
            comando.Parameters.AddWithValue("@DATA_NASCIMENTO", cliente.DataNascimento);
            comando.Parameters.AddWithValue("@NUMERO", cliente.Numero);
            comando.Parameters.AddWithValue("@COMPLEMENTO", cliente.Complemento);
            comando.Parameters.AddWithValue("@LOGRADOURO", cliente.Logradouro);
            comando.Parameters.AddWithValue("@CEP", cliente.Cep);
            comando.Parameters.AddWithValue("@ID_CIDADE", cliente.IdCidade);
            int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();
            return id;
        }
        public List<Cliente> ObterTodos()
        {
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = @"SELECT clientes.id AS 'ClientesId',
clientes.nome AS 'ClienteNome',
clientes.cpf AS 'ClienteCpf',
clientes.data_nascimento AS 'ClienteDataNascimento',
clientes.numero AS 'ClienteNumero',
clientes.complemento AS 'ClienteComplemento',
clientes.logradouro AS 'ClienteLogradouro',
clientes.cep AS 'ClienteCep',
clientes.id_cidade AS 'ClienteIdCidade',
cidades.nome AS'CidadeNome'
FROM clientes INNER JOIN cidades ON (clientes.id_cidade = cidades.id)";
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();

            List<Cliente> clientes = new List<Cliente>();
            foreach (DataRow linha in tabela.Rows)
            {
                Cliente cliente = new Cliente();
                cliente.Id = Convert.ToInt32(linha["ClientesId"]);
                cliente.Nome = linha["ClienteNome"].ToString();
                cliente.Cpf = linha["ClienteCpf"].ToString();
                cliente.DataNascimento = Convert.ToDateTime(linha["ClienteDataNascimento"]);
                cliente.Numero = Convert.ToInt32(linha["ClienteNumero"]);
                cliente.Complemento = linha["ClienteComplemento"].ToString();
                cliente.Logradouro = linha["ClienteLogradouro"].ToString();
                cliente.Cep = linha["ClienteCep"].ToString();
                cliente.IdCidade = Convert.ToInt32(linha["ClienteIdCidade"]);
                cliente.Cidade = new Cidade();
                cliente.Cidade.Nome = linha["CidadeNome"].ToString();
                clientes.Add(cliente);
            }
            return clientes;

        }
        public bool Apagar(int id)
        {
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = "DELETE FROM clientes WHERE @ID = id";
            comando.Parameters.AddWithValue("@ID", id);
            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeAfetada == 1;
        }
        public bool Alterar(Cliente cliente)

        {
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = @"UPDATE clientes SET nome=@NOME,cpf=@CPF,data_nascimento=@DATA_NASCIMENTO,numero=@NUMERO,complemento=@COMPLEMENTO,logradouro=@LOGRADOURO,cep=@CEP,id_cidade=@ID_CIDADE WHERE id = @Id";
            comando.Parameters.AddWithValue("@NOME", cliente.Nome);
            comando.Parameters.AddWithValue("@CPF", cliente.Cpf);
            comando.Parameters.AddWithValue("@DATA_NASCIMENTO", cliente.DataNascimento);
            comando.Parameters.AddWithValue("@NUMERO", cliente.Numero);
            comando.Parameters.AddWithValue("@COMPLEMENTO", cliente.Complemento);
            comando.Parameters.AddWithValue("@LOGRADOURO", cliente.Logradouro);
            comando.Parameters.AddWithValue("@CEP", cliente.Cep);
            comando.Parameters.AddWithValue("@ID_CIDADE", cliente.IdCidade);
            comando.Parameters.AddWithValue("@ID", cliente.Id);
            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeAfetada == 1;
        }
        public Cliente ObterPeloId(int id)
        {
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = @" SELECT clientes.id AS 'ClientesId',
clientes.nome AS 'ClienteNome',
clientes.cpf AS 'ClienteCpf',
clientes.data_nascimento AS 'ClienteDataNascimento',
clientes.numero AS 'ClienteNumero',
clientes.complemento AS 'ClienteComplemento',
clientes.logradouro AS 'ClienteLogradouro',
clientes.cep AS 'ClienteCep',
clientes.id_cidade AS 'ClienteIdCidade',
cidades.nome AS'CidadeNome'
FROM clientes 
INNER JOIN cidades ON (clientes.id_cidade = cidades.id)
WHERE clientes.id = @ID";

            comando.Parameters.AddWithValue("@ID", id);
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();
            List<Cidade> cidades = new List<Cidade>();
            if (tabela.Rows.Count == 0)
            {
                return null;
            }
            DataRow linha = tabela.Rows[0];
            
                Cliente cliente= new Cliente();
                cliente.Id = Convert.ToInt32(linha["ClientesId"]);
                cliente.Nome = linha["ClienteNome"].ToString();
                cliente.Cpf = linha["ClienteCpf"].ToString();
                cliente.DataNascimento = Convert.ToDateTime(linha["ClienteDataNascimento"]);
                cliente.Numero = Convert.ToInt32(linha["ClienteNumero"]);
                cliente.Complemento = linha["ClienteComplemento"].ToString();
                cliente.Logradouro = linha["ClienteLogradouro"].ToString();
                cliente.Cep = linha["ClienteCep"].ToString();
                cliente.IdCidade = Convert.ToInt32(linha["ClienteIdCidade"]);
                cliente.Cidade = new Cidade();
                cliente.Cidade.Nome = linha["CidadeNome"].ToString();
                return cliente;
            
        }
    }
}
