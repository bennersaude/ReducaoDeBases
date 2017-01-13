using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace reducaoDeBases
{
    public class BancoDeDados
    {
        public static SqlConnection Cnn;
        public static string Servidor;


        public static void FimConexao()
        {
            BancoDeDados.Cnn.Close();
        }

        public static Boolean Conectar(string servidor, string database, string usuario, string senha, bool mensagem = false)
        {
            string connetionString = null;

            connetionString = string.Format("MultipleActiveResultSets=true;Data Source={0};Initial Catalog={1};User ID={2};Password={3}", servidor, database, usuario, senha);
            Cnn = new SqlConnection(connetionString);
            try
            {
                Cnn.Open();
                if (mensagem)
                    MessageBox.Show("Conexão efetuada com sucesso!");

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível Conectar ao Banco. Erro: \r\n" + ex.Message);
                return false;
            }
        }

        public static void ExecutarComando(string sql)
        {
            try
            {
                var comando = new SqlCommand(sql, Cnn);
                comando.CommandTimeout = 0;
                comando.ExecuteNonQuery();
                MessageBox.Show("Comando executado com sucesso.");
            }
            catch (Exception e)
            {
                MessageBox.Show("Erro: \r\n" + e.Message);
            }
            
        }

        public static void ExecutarComandoAsync(string sql, string servidor, string database, string usuario, string senha)
        {
            try
            {
                var connetionString = string.Format("MultipleActiveResultSets=true;Data Source={0};Initial Catalog={1};User ID={2};Password={3}", servidor, database, usuario, senha);
                var conexao = new SqlConnection(connetionString);
                conexao.Open();
                var comando = new SqlCommand(sql, conexao);
                comando.CommandTimeout = 0;
                comando.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Erro: \r\n" + e.Message);
            }

        }

        public static int GetTotal(string tabela, string where)
        {
            try
            {
                var sql = String.Format(@"SELECT COUNT(1) TOTAL FROM {0} WITH (NOLOCK) WHERE {1}", tabela, where);
                var comando = new SqlCommand(sql, Cnn);
                comando.CommandTimeout = 0;
                var read = comando.ExecuteReader();
                read.Read();
                return read["TOTAL"] as int? ?? 0;
            }
            catch (Exception e)
            {
                MessageBox.Show("Erro: \r\n" + e.Message);
                return 0;
            }

        }

    }
}
