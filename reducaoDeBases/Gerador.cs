using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace reducaoDeBases
{
    public class Gerador
    {
        public SqlConnection Cnn { get; private set; }
        List<LinkedList<Tabelas>> Arvore = new List<LinkedList<Tabelas>>(); 

        public Gerador(SqlConnection cnn)
        {
            Cnn = cnn;
        }

        public string Gerar(string tabelaInicial)
        {
            //var raiz = new Tabelas();
            //raiz.Nome = "SAM_AUTORIZ";
            //raiz.Handle = "1598";
            //Arvore.Add(new LinkedList<Tabelas>());
            //Arvore[0].AddFirst(raiz);

            //BuscaEmArvoreRecursivo(1, raiz);

            var raiz = BuscaTabelaRaiz(tabelaInicial);
            if (!string.IsNullOrEmpty(raiz.Handle))
            {
                BuscaEmArvore(raiz);
                return GerarScriptCascade();
            }
            return "";
        }

        public Tabelas BuscaTabelaRaiz(string raiz)
        {
            var sql = String.Format(@"SELECT HANDLE, NOME
                                        FROM Z_TABELAS
                                       WHERE NOME = '{0}'", raiz);

            var comando = new SqlCommand(sql, Cnn);
            var read = comando.ExecuteReader();

            if (!read.HasRows)
            {
                MessageBox.Show("Não foi possível encontrar a tabela");
                return new Tabelas();
            }
            else
            {
                read.Read();
                var tabela = new Tabelas();
                tabela.Nome = read["NOME"].ToString();
                tabela.Handle = read["HANDLE"].ToString();
                return tabela;
            }
                
        }

        public string GerarScriptCascade()
        {
            var retorno = new StringBuilder();
            var nivel = 0;
            var contator = 0;
            foreach (var lista in Arvore)
            {
                foreach (var tabela in lista)
                {
                    if (tabela.Pai != null)
                    {
                        retorno.AppendLine("ALTER TABLE " + tabela.Nome + " WITH NOCHECK ADD CONSTRAINT [" + tabela.Assumir +
                                           "] FOREIGN KEY([" + tabela.CampoFk + "]) REFERENCES " + tabela.Pai.Nome +
                                           "([HANDLE]) ON DELETE CASCADE;");
                        contator++;
                    }
                }
                nivel++;
            }
            //retorno.AppendLine();
            //retorno.AppendLine("Total de tabelas: " + contator);
            return retorno.ToString();
        }

        public string ImprimirArvore()
        {
            var retorno = new StringBuilder();
            var nivel = 0;
            var contator = 0;
            foreach (var lista in Arvore)
            {
                foreach (var tabela in lista)
                {
                    for (int i = nivel - 1; i >= 0; i--)
                    {
                        retorno.Append(" = ");
                    }
                    retorno.AppendLine(tabela.Nome);
                    contator++;
                }
                nivel++;
            }
            retorno.AppendLine();
            retorno.AppendLine("Total de tabelas: " + contator);
            return retorno.ToString();
        }

        public void BuscaEmArvoreRecursivo(int nivel, Tabelas pai)
        {
            var sql = String.Format(@"SELECT B.HANDLE, B.NOME, A.NOME CAMPO, A.ASSUMIR
                                        FROM Z_CAMPOS A
                                        JOIN Z_TABELAS B ON A.TABELA = B.HANDLE
                                       WHERE A.PESQUISAR = {0}
                                         AND A.TABELA != A.PESQUISAR
                                         AND B.LOCAL = 'N'
                                         AND B.AGENDA = 'N'
                                         AND NOT ASSUMIR IS NULL 
                                       GROUP BY B.HANDLE, B.NOME, A.NOME, A.ASSUMIR, A.OPCIONAL
                                       ORDER BY A.OPCIONAL, B.HANDLE", pai.Handle);

            var comando = new SqlCommand(sql, Cnn);
            var read = comando.ExecuteReader();

            if (!read.HasRows)
                return;
            
            while (read.Read())
            {
                var tabela = new Tabelas();
                tabela.Nome = read["NOME"].ToString();
                tabela.Handle = read["HANDLE"].ToString();
                tabela.CampoFk = read["CAMPO"].ToString();
                tabela.Assumir = read["ASSUMIR"].ToString();
                tabela.Pai = pai;
                if (Tabelas.ExisteNaArvore(Arvore, tabela))
                    continue;


                if (Arvore.Count - 1 < nivel)
                {
                    Arvore.Add(new LinkedList<Tabelas>());
                }

                Arvore[nivel].AddFirst(tabela);
                BuscaEmArvoreRecursivo(nivel + 1, tabela);

            }
        }

        public void BuscaEmArvore(Tabelas raiz)
        {
            Arvore.Add(new LinkedList<Tabelas>());
            Arvore[0].AddFirst(raiz);

            //Executa primeiro para pegar as ligações obrigatórias
            var nivel = 0;
            while (Arvore.Count() > nivel)
            {
                foreach (var pai in Arvore[nivel])
                {


                    var sql = String.Format(@"SELECT B.HANDLE, B.NOME, A.NOME CAMPO, A.ASSUMIR
                                                FROM Z_CAMPOS A
                                                JOIN Z_TABELAS B ON A.TABELA = B.HANDLE
                                               WHERE A.PESQUISAR = {0}
                                                 AND A.TABELA != A.PESQUISAR
                                                 AND B.LOCAL = 'N'
                                                 AND B.AGENDA = 'N'
                                                 AND A.OPCIONAL = 'N'
                                                 AND NOT ASSUMIR IS NULL
                                                 AND A.CAMPOMESTRE IS NULL
                                               GROUP BY B.HANDLE, B.NOME, A.NOME, A.ASSUMIR, A.OPCIONAL
                                               ORDER BY A.OPCIONAL, B.HANDLE", pai.Handle);

                    var comando = new SqlCommand(sql, Cnn);
                    var read = comando.ExecuteReader();

                    if (!read.HasRows)
                        continue;

                    while (read.Read())
                    {
                        var tabela = new Tabelas();
                        tabela.Nome = read["NOME"].ToString();
                        tabela.Handle = read["HANDLE"].ToString();
                        tabela.CampoFk = read["CAMPO"].ToString();
                        tabela.Assumir = read["ASSUMIR"].ToString();
                        tabela.Pai = pai;
                        if (Tabelas.ExisteNaArvore(Arvore, tabela))
                            continue;

                        if (Tabelas.TabelaNãoPodeSerDeletada(tabela))
                            continue;


                        if (Arvore.Count - 1  < nivel + 1)
                        {
                            Arvore.Add(new LinkedList<Tabelas>());
                        }

                        Arvore[nivel + 1].AddFirst(tabela);

                    }


                }
                nivel++;
            }

            //Executa para pegar as outras ligações
            nivel = 0;
            while (Arvore.Count() > nivel)
            {
                foreach (var pai in Arvore[nivel])
                {


                    var sql = String.Format(@"SELECT B.HANDLE, B.NOME, A.NOME CAMPO, A.ASSUMIR
                                        FROM Z_CAMPOS A
                                        JOIN Z_TABELAS B ON A.TABELA = B.HANDLE
                                       WHERE A.PESQUISAR = {0}
                                         AND A.TABELA != A.PESQUISAR
                                         AND B.LOCAL = 'N'
                                         AND B.AGENDA = 'N'
                                         AND NOT ASSUMIR IS NULL 
                                       GROUP BY B.HANDLE, B.NOME, A.NOME, A.ASSUMIR, A.OPCIONAL
                                       ORDER BY A.OPCIONAL, B.HANDLE", pai.Handle);

                    var comando = new SqlCommand(sql, Cnn);
                    var read = comando.ExecuteReader();

                    if (!read.HasRows)
                        continue;

                    while (read.Read())
                    {
                        var tabela = new Tabelas();
                        tabela.Nome = read["NOME"].ToString();
                        tabela.Handle = read["HANDLE"].ToString();
                        tabela.CampoFk = read["CAMPO"].ToString();
                        tabela.Assumir = read["ASSUMIR"].ToString();
                        tabela.Pai = pai;
                        if (Tabelas.ExisteNaArvore(Arvore, tabela))
                            continue;

                        if (Tabelas.TabelaNãoPodeSerDeletada(tabela))
                            continue;

                        if (Arvore.Count - 1 < nivel + 1)
                        {
                            Arvore.Add(new LinkedList<Tabelas>());
                        }

                        Arvore[nivel + 1].AddFirst(tabela);

                    }


                }
                nivel++;
            }
        }

        public string ScriptDrop()
        {
            var sql = String.Format(@"select
                                      DropStmt = 'ALTER TABLE [' + ForeignKeys.ForeignTableSchema + 
                                          '].[' + ForeignKeys.ForeignTableName + 
                                          '] DROP CONSTRAINT [' + ForeignKeys.ForeignKeyName + ']; '
                                    ,  CreateStmt = 'ALTER TABLE [' + ForeignKeys.ForeignTableSchema + 
                                          '].[' + ForeignKeys.ForeignTableName + 
                                          '] WITH CHECK ADD CONSTRAINT [' +  ForeignKeys.ForeignKeyName + 
                                          '] FOREIGN KEY([' + ForeignKeys.ForeignTableColumn + 
                                          ']) REFERENCES [' + schema_name(sys.objects.schema_id) + '].[' +
                                      sys.objects.[name] + ']([' +
                                      sys.columns.[name] + ']) ON DELETE CASCADE; '
                                     from sys.objects
                                      inner join sys.columns
                                        on (sys.columns.[object_id] = sys.objects.[object_id])
                                      inner join (
                                        select sys.foreign_keys.[name] as ForeignKeyName
                                         ,schema_name(sys.objects.schema_id) as ForeignTableSchema
                                         ,sys.objects.[name] as ForeignTableName
                                         ,sys.columns.[name]  as ForeignTableColumn
                                         ,sys.foreign_keys.referenced_object_id as referenced_object_id
                                         ,sys.foreign_key_columns.referenced_column_id as referenced_column_id
                                         from sys.foreign_keys
                                          inner join sys.foreign_key_columns
                                            on (sys.foreign_key_columns.constraint_object_id
                                              = sys.foreign_keys.[object_id])
                                          inner join sys.objects
                                            on (sys.objects.[object_id]
                                              = sys.foreign_keys.parent_object_id)
                                            inner join sys.columns
                                              on (sys.columns.[object_id]
                                                = sys.objects.[object_id])
                                               and (sys.columns.column_id
                                                = sys.foreign_key_columns.parent_column_id)
                                        ) ForeignKeys
                                        on (ForeignKeys.referenced_object_id = sys.objects.[object_id])
                                         and (ForeignKeys.referenced_column_id = sys.columns.column_id)
                                     where (sys.objects.[type] = 'U')
                                      and (sys.objects.[name] not in ('sysdiagrams'))");

            var comando = new SqlCommand(sql, Cnn);
            var read = comando.ExecuteReader();
            if (!read.HasRows)
                return "";

            var resultado = new StringBuilder();
            while (read.Read())
            {
                resultado.AppendLine(read["DropStmt"].ToString());
            }

            return resultado.ToString();
        }

    }
}
