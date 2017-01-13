using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using Timer = System.Windows.Forms.Timer;

namespace reducaoDeBases
{
    public partial class Form1 : Form
    {
        private string _arquivoConfig = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                                        @"\reducaoDeBases.xml";

        private DateTime InicioTime;

        private long total = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void run_Click(object sender, EventArgs e)
        {
            if (BancoDeDados.Conectar(servidor.Text, dataBase.Text, usuario.Text, senha.Text))
            {
                var gerador = new Gerador(BancoDeDados.Cnn);
                saida.Text = gerador.Gerar(tabelaInicial.Text);
                BancoDeDados.FimConexao();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (BancoDeDados.Conectar(servidor.Text, dataBase.Text, usuario.Text, senha.Text))
            {
                var gerador = new Gerador(BancoDeDados.Cnn);
                saida.Text = gerador.ScriptDrop();
                BancoDeDados.FimConexao();
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists(_arquivoConfig))
            {
                XDocument doc = XDocument.Load(_arquivoConfig);
                if (doc.Root == null)
                    return;
                if (doc.Root.Element("servidor") != null)
                    servidor.Text = doc.Root.Element("servidor").Value;

                if (doc.Root.Element("dataBase") != null)
                    dataBase.Text = doc.Root.Element("dataBase").Value;

                if (doc.Root.Element("usuario") != null)
                    usuario.Text = doc.Root.Element("usuario").Value;

                if (doc.Root.Element("senha") != null)
                    senha.Text = doc.Root.Element("senha").Value;

                if (doc.Root.Element("tabelaInicial") != null)
                    tabelaInicial.Text = doc.Root.Element("tabelaInicial").Value;

                if (doc.Root.Element("where") != null)
                    where.Text = doc.Root.Element("where").Value;

                if (doc.Root.Element("numDelete") != null)
                    numDelete.Text = doc.Root.Element("numDelete").Value;

            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            new XDocument(
                new XElement("root",
                    new XElement("servidor", servidor.Text),
                    new XElement("dataBase", dataBase.Text),
                    new XElement("usuario", usuario.Text),
                    new XElement("senha", senha.Text),
                    new XElement("tabelaInicial", tabelaInicial.Text),
                    new XElement("where", where.Text),
                    new XElement("numDelete", numDelete.Text)
                    )
                )
                .Save(_arquivoConfig);
            try
            {
                Environment.Exit(0);
            }
            catch
            {
            }
        }

        private void testarConexao_Click(object sender, EventArgs e)
        {
            BancoDeDados.Conectar(servidor.Text, dataBase.Text, usuario.Text, senha.Text, true);
            BancoDeDados.FimConexao();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (BancoDeDados.Conectar(servidor.Text, dataBase.Text, usuario.Text, senha.Text))
            {
                DialogResult result = MessageBox.Show("Tem certeza que deseja executar este comando?", "*** ATENÇÃO ***",
                    MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                    return;

                if (saida.Text.Contains("DECLARE @Deleted_Rows INT;"))
                {
                    DeletaRegistros();
                }
                else
                {
                    BancoDeDados.ExecutarComando(saida.Text);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var sql = string.Format(
                @"DECLARE @Deleted_Rows INT;
SET @Deleted_Rows = 1;

WHILE (@Deleted_Rows > 0)
    BEGIN
    BEGIN TRANSACTION
        DELETE TOP ({2})  {0}
        WHERE {1}     
    SET @Deleted_Rows = @@ROWCOUNT;
    COMMIT TRANSACTION
    CHECKPOINT  
END"
                , tabelaInicial.Text, where.Text, numDelete.Text);
            saida.Text = sql;
        }

        private void saida_TextChanged(object sender, EventArgs e)
        {
            numLinhas.Text = Regex.Matches(saida.Text, "\n").Count + " Linhas";
        }

        private void timer_Tick(object sender, System.EventArgs e)
        {
            var time = DateTime.Now.Subtract(InicioTime);
            tempoPassado.Text = string.Format("Tempo: {0:D2}:{1:D2}:{2:D2}", time.Hours, time.Minutes, time.Seconds);
        }

        private void AtualizatempoRestante(int deletado, int total)
        {
            var tempo = DateTime.Now.Subtract(InicioTime);
            var velocidade = (double) deletado/tempo.TotalSeconds;
            var restante = TimeSpan.FromSeconds((total - deletado)/velocidade);
            tempoRestante.BeginInvoke(
                        new Action(() =>
                        {
                            tempoRestante.Text = string.Format("Restante: {0:D2}:{1:D2}:{2:D2}", restante.Hours, restante.Minutes, restante.Seconds); ;
                        }
                            ));
        }

        private void DeletaRegistros()
        {
            DisableControls(this);

            InicioTime = DateTime.Now;
            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += new System.EventHandler(timer_Tick);
            timer.Start();


            Thread threadBarraDeProgresso = new Thread(
                () =>
                {

                    var total = BancoDeDados.GetTotal(tabelaInicial.Text, @where.Text);
                    var deletado = 0;
                    var variacao = 0;
                    contador.BeginInvoke(new Action(() =>
                    {
                        contador.Text = "0/" + total;
                    }
                        ));
                    barraDeProgresso.BeginInvoke(
                        new Action(() =>
                        {
                            barraDeProgresso.Maximum = total;
                        }
                            ));
                    while (deletado < total)
                    {
                        deletado = total - BancoDeDados.GetTotal(tabelaInicial.Text, @where.Text);
                        if (variacao != deletado)
                        {
                            AtualizatempoRestante(deletado, total);
                            variacao = deletado;
                        }

                        var deletado1 = deletado;
                        
                        contador.BeginInvoke(new Action(() =>
                        {
                            contador.Text = deletado1 + "/" + total;
                        }
                            ));
                        var percentual = (deletado * 100 / total);
                        percent.BeginInvoke(new Action(() =>
                        {
                            percent.Text = string.Format("{0:D}%", percentual);
                        }
                            ));
                        barraDeProgresso.BeginInvoke(
                            new Action(() =>
                            {
                                barraDeProgresso.Value = deletado1;
                            }
                                ));
                        Thread.Sleep(1000);
                    }

                    timer.Dispose();
                    MessageBox.Show("Fim!");
                    EnableControls(this);

                    barraDeProgresso.BeginInvoke(
                        new Action(() =>
                        {
                            barraDeProgresso.Value = 0;
                        }
                            ));
                });

            threadBarraDeProgresso.Start();
            var comando = saida.Text;
            Thread threadDelete = new Thread(
                () =>
                {
                    BancoDeDados.ExecutarComandoAsync(comando, servidor.Text, dataBase.Text, usuario.Text, senha.Text);
                });
            threadDelete.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Tem certeza que deseja finalizar o programa?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void DisableControls(Control con)
        {
            execComandos.Enabled = false;
            where.Enabled = false;
            button3.Enabled = false;
            button1.Enabled = false;
            run.Enabled = false;
            tabelaInicial.Enabled = false;
            numDelete.Enabled = false;
        }

        private void EnableControls(Control con)
        {
            execComandos.BeginInvoke(new Action(() => { execComandos.Enabled = true; }));
            where.BeginInvoke(new Action(() => { where.Enabled = true; }));
            button3.BeginInvoke(new Action(() => { button3.Enabled = true; }));
            button1.BeginInvoke(new Action(() => { button1.Enabled = true; }));
            run.BeginInvoke(new Action(() => { run.Enabled = true; }));
            tabelaInicial.BeginInvoke(new Action(() => { tabelaInicial.Enabled = true; }));
            numDelete.BeginInvoke(new Action(() => { numDelete.Enabled = true; }));
            
        }



    }

}
