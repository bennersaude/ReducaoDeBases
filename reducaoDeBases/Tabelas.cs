using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reducaoDeBases
{
    public class Tabelas
    {
        public string Nome { get; set; }
        public string Handle { get; set; }
        public Tabelas Pai { get; set; }
        public string CampoFk { get; set; }
        public string Assumir { get; set; }
        

        public static bool ExisteNaArvore(List<LinkedList<Tabelas>> arvore, Tabelas procurado)
        {
            return arvore.Any(lista => lista.Any(tabela => tabela.Handle == procurado.Handle));
        }

        public static bool TabelaNãoPodeSerDeletada(Tabelas tabela)
        {
            //Lista de tabelas que não podem entrar no DELETE
            var lista = new List<String>();
           // lista.Add("SAM_PRONTUARIO");

            return lista.Contains(tabela.Nome);
        }
    }
}
