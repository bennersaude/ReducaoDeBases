namespace reducaoDeBases
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.run = new System.Windows.Forms.Button();
            this.saida = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.servidor = new System.Windows.Forms.TextBox();
            this.dataBase = new System.Windows.Forms.TextBox();
            this.usuario = new System.Windows.Forms.TextBox();
            this.senha = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.testarConexao = new System.Windows.Forms.Button();
            this.numLinhas = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tabelaInicial = new System.Windows.Forms.TextBox();
            this.execComandos = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.where = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.barraDeProgresso = new System.Windows.Forms.ProgressBar();
            this.tempoPassado = new System.Windows.Forms.Label();
            this.tempoRestante = new System.Windows.Forms.Label();
            this.contador = new System.Windows.Forms.Label();
            this.percent = new System.Windows.Forms.Label();
            this.numDelete = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // run
            // 
            this.run.Location = new System.Drawing.Point(573, 539);
            this.run.Name = "run";
            this.run.Size = new System.Drawing.Size(120, 23);
            this.run.TabIndex = 0;
            this.run.Text = "Script de Cascade";
            this.run.UseVisualStyleBackColor = true;
            this.run.Click += new System.EventHandler(this.run_Click);
            // 
            // saida
            // 
            this.saida.Location = new System.Drawing.Point(11, 74);
            this.saida.Name = "saida";
            this.saida.ReadOnly = true;
            this.saida.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.saida.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.saida.Size = new System.Drawing.Size(696, 344);
            this.saida.TabIndex = 1;
            this.saida.Text = "";
            this.saida.WordWrap = false;
            this.saida.TextChanged += new System.EventHandler(this.saida_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(11, 538);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(111, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Drop Constraints";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // servidor
            // 
            this.servidor.Location = new System.Drawing.Point(80, 42);
            this.servidor.Name = "servidor";
            this.servidor.Size = new System.Drawing.Size(100, 20);
            this.servidor.TabIndex = 3;
            this.servidor.Text = "MGA-SQL010";
            // 
            // dataBase
            // 
            this.dataBase.Location = new System.Drawing.Point(185, 42);
            this.dataBase.Name = "dataBase";
            this.dataBase.Size = new System.Drawing.Size(100, 20);
            this.dataBase.TabIndex = 4;
            this.dataBase.Text = "DESENV_AG_SQL";
            // 
            // usuario
            // 
            this.usuario.Location = new System.Drawing.Point(290, 42);
            this.usuario.Name = "usuario";
            this.usuario.Size = new System.Drawing.Size(100, 20);
            this.usuario.TabIndex = 5;
            this.usuario.Text = "des";
            // 
            // senha
            // 
            this.senha.Location = new System.Drawing.Point(397, 42);
            this.senha.Name = "senha";
            this.senha.Size = new System.Drawing.Size(100, 20);
            this.senha.TabIndex = 6;
            this.senha.Text = "benner";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(107, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Servidor";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(211, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "DataBase";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(311, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Usuario";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(420, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Senha";
            // 
            // testarConexao
            // 
            this.testarConexao.AccessibleDescription = "Testar Conexão";
            this.testarConexao.Location = new System.Drawing.Point(549, 18);
            this.testarConexao.Name = "testarConexao";
            this.testarConexao.Size = new System.Drawing.Size(98, 44);
            this.testarConexao.TabIndex = 11;
            this.testarConexao.Text = "Testar Conexão";
            this.testarConexao.UseVisualStyleBackColor = true;
            this.testarConexao.Click += new System.EventHandler(this.testarConexao_Click);
            // 
            // numLinhas
            // 
            this.numLinhas.AutoSize = true;
            this.numLinhas.Location = new System.Drawing.Point(11, 427);
            this.numLinhas.Name = "numLinhas";
            this.numLinhas.Size = new System.Drawing.Size(47, 13);
            this.numLinhas.TabIndex = 12;
            this.numLinhas.Text = "0 Linhas";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(128, 543);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Tabela inicial:";
            // 
            // tabelaInicial
            // 
            this.tabelaInicial.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tabelaInicial.Location = new System.Drawing.Point(206, 540);
            this.tabelaInicial.Name = "tabelaInicial";
            this.tabelaInicial.Size = new System.Drawing.Size(361, 20);
            this.tabelaInicial.TabIndex = 13;
            this.tabelaInicial.Text = "SAM_PEG";
            // 
            // execComandos
            // 
            this.execComandos.Location = new System.Drawing.Point(573, 450);
            this.execComandos.Name = "execComandos";
            this.execComandos.Size = new System.Drawing.Size(120, 23);
            this.execComandos.TabIndex = 15;
            this.execComandos.Text = "Executar Comandos";
            this.execComandos.UseVisualStyleBackColor = true;
            this.execComandos.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(573, 506);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(120, 23);
            this.button3.TabIndex = 16;
            this.button3.Text = "Script de Delete";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // where
            // 
            this.where.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.where.Location = new System.Drawing.Point(182, 508);
            this.where.Name = "where";
            this.where.Size = new System.Drawing.Size(385, 20);
            this.where.TabIndex = 17;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(134, 511);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Where:";
            // 
            // barraDeProgresso
            // 
            this.barraDeProgresso.Location = new System.Drawing.Point(11, 450);
            this.barraDeProgresso.Name = "barraDeProgresso";
            this.barraDeProgresso.Size = new System.Drawing.Size(556, 23);
            this.barraDeProgresso.TabIndex = 19;
            // 
            // tempoPassado
            // 
            this.tempoPassado.AutoSize = true;
            this.tempoPassado.Location = new System.Drawing.Point(501, 482);
            this.tempoPassado.Name = "tempoPassado";
            this.tempoPassado.Size = new System.Drawing.Size(88, 13);
            this.tempoPassado.TabIndex = 20;
            this.tempoPassado.Text = "Tempo: 00:00:00";
            // 
            // tempoRestante
            // 
            this.tempoRestante.AutoSize = true;
            this.tempoRestante.Location = new System.Drawing.Point(595, 482);
            this.tempoRestante.Name = "tempoRestante";
            this.tempoRestante.Size = new System.Drawing.Size(98, 13);
            this.tempoRestante.TabIndex = 21;
            this.tempoRestante.Text = "Restante: 00:00:00";
            // 
            // contador
            // 
            this.contador.AutoSize = true;
            this.contador.Location = new System.Drawing.Point(14, 482);
            this.contador.Name = "contador";
            this.contador.Size = new System.Drawing.Size(24, 13);
            this.contador.TabIndex = 22;
            this.contador.Text = "0/0";
            // 
            // percent
            // 
            this.percent.AutoSize = true;
            this.percent.Location = new System.Drawing.Point(274, 455);
            this.percent.Name = "percent";
            this.percent.Size = new System.Drawing.Size(21, 13);
            this.percent.TabIndex = 23;
            this.percent.Text = "0%";
            // 
            // numDelete
            // 
            this.numDelete.Location = new System.Drawing.Point(93, 508);
            this.numDelete.Name = "numDelete";
            this.numDelete.Size = new System.Drawing.Size(35, 20);
            this.numDelete.TabIndex = 24;
            this.numDelete.Text = "50";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 511);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 13);
            this.label7.TabIndex = 25;
            this.label7.Text = "Num de delete:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 573);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.numDelete);
            this.Controls.Add(this.percent);
            this.Controls.Add(this.contador);
            this.Controls.Add(this.tempoRestante);
            this.Controls.Add(this.tempoPassado);
            this.Controls.Add(this.barraDeProgresso);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.where);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.execComandos);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tabelaInicial);
            this.Controls.Add(this.numLinhas);
            this.Controls.Add(this.testarConexao);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.senha);
            this.Controls.Add(this.usuario);
            this.Controls.Add(this.dataBase);
            this.Controls.Add(this.servidor);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.saida);
            this.Controls.Add(this.run);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Redução de Bases";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button run;
        private System.Windows.Forms.RichTextBox saida;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox servidor;
        private System.Windows.Forms.TextBox dataBase;
        private System.Windows.Forms.TextBox usuario;
        private System.Windows.Forms.TextBox senha;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button testarConexao;
        private System.Windows.Forms.Label numLinhas;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tabelaInicial;
        private System.Windows.Forms.Button execComandos;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox where;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ProgressBar barraDeProgresso;
        private System.Windows.Forms.Label tempoPassado;
        private System.Windows.Forms.Label tempoRestante;
        private System.Windows.Forms.Label contador;
        private System.Windows.Forms.Label percent;
        private System.Windows.Forms.TextBox numDelete;
        private System.Windows.Forms.Label label7;
    }
}

