namespace WindowsFormsApp1
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.mnsMenuStrip = new System.Windows.Forms.MenuStrip();
            this.tmiRegistros = new System.Windows.Forms.ToolStripMenuItem();
            this.tmiBaterPonto = new System.Windows.Forms.ToolStripMenuItem();
            this.tmiSelecionarAlmoco = new System.Windows.Forms.ToolStripMenuItem();
            this.tmiSalvarToJSON = new System.Windows.Forms.ToolStripMenuItem();
            this.tmiRel = new System.Windows.Forms.ToolStripMenuItem();
            this.tmiRelDiario = new System.Windows.Forms.ToolStripMenuItem();
            this.tmiRelMensal = new System.Windows.Forms.ToolStripMenuItem();
            this.tmiOpcoes = new System.Windows.Forms.ToolStripMenuItem();
            this.tmiApagarDia = new System.Windows.Forms.ToolStripMenuItem();
            this.tmiSair = new System.Windows.Forms.ToolStripMenuItem();
            this.rtfRel = new System.Windows.Forms.RichTextBox();
            this.btnLimpar = new System.Windows.Forms.Button();
            this.tmiApagarTodosOsRegistros = new System.Windows.Forms.ToolStripMenuItem();
            this.mnsMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnsMenuStrip
            // 
            this.mnsMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmiRegistros,
            this.tmiRel,
            this.tmiOpcoes,
            this.tmiSair});
            this.mnsMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mnsMenuStrip.Name = "mnsMenuStrip";
            this.mnsMenuStrip.Size = new System.Drawing.Size(375, 24);
            this.mnsMenuStrip.TabIndex = 8;
            this.mnsMenuStrip.Text = "menuStrip1";
            // 
            // tmiRegistros
            // 
            this.tmiRegistros.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmiBaterPonto,
            this.tmiSelecionarAlmoco,
            this.tmiSalvarToJSON});
            this.tmiRegistros.Name = "tmiRegistros";
            this.tmiRegistros.Size = new System.Drawing.Size(67, 20);
            this.tmiRegistros.Text = "Registros";
            // 
            // tmiBaterPonto
            // 
            this.tmiBaterPonto.Image = ((System.Drawing.Image)(resources.GetObject("tmiBaterPonto.Image")));
            this.tmiBaterPonto.Name = "tmiBaterPonto";
            this.tmiBaterPonto.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.tmiBaterPonto.Size = new System.Drawing.Size(180, 22);
            this.tmiBaterPonto.Text = "Ponto";
            this.tmiBaterPonto.Click += new System.EventHandler(this.tmiBaterPonto_Click);
            // 
            // tmiSelecionarAlmoco
            // 
            this.tmiSelecionarAlmoco.Image = ((System.Drawing.Image)(resources.GetObject("tmiSelecionarAlmoco.Image")));
            this.tmiSelecionarAlmoco.Name = "tmiSelecionarAlmoco";
            this.tmiSelecionarAlmoco.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.tmiSelecionarAlmoco.Size = new System.Drawing.Size(180, 22);
            this.tmiSelecionarAlmoco.Text = "Almoço";
            this.tmiSelecionarAlmoco.Click += new System.EventHandler(this.btnSelecionarAlmoco_Click);
            // 
            // tmiSalvarToJSON
            // 
            this.tmiSalvarToJSON.Image = ((System.Drawing.Image)(resources.GetObject("tmiSalvarToJSON.Image")));
            this.tmiSalvarToJSON.Name = "tmiSalvarToJSON";
            this.tmiSalvarToJSON.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.tmiSalvarToJSON.Size = new System.Drawing.Size(180, 22);
            this.tmiSalvarToJSON.Text = "Salvar";
            this.tmiSalvarToJSON.Click += new System.EventHandler(this.tmiSalvarToJson_Click);
            // 
            // tmiRel
            // 
            this.tmiRel.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmiRelDiario,
            this.tmiRelMensal});
            this.tmiRel.Name = "tmiRel";
            this.tmiRel.Size = new System.Drawing.Size(71, 20);
            this.tmiRel.Text = "Relatórios";
            // 
            // tmiRelDiario
            // 
            this.tmiRelDiario.Image = ((System.Drawing.Image)(resources.GetObject("tmiRelDiario.Image")));
            this.tmiRelDiario.Name = "tmiRelDiario";
            this.tmiRelDiario.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.tmiRelDiario.Size = new System.Drawing.Size(180, 22);
            this.tmiRelDiario.Text = "Diário";
            this.tmiRelDiario.Click += new System.EventHandler(this.tmiRelDiario_Click);
            // 
            // tmiRelMensal
            // 
            this.tmiRelMensal.Image = ((System.Drawing.Image)(resources.GetObject("tmiRelMensal.Image")));
            this.tmiRelMensal.Name = "tmiRelMensal";
            this.tmiRelMensal.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.tmiRelMensal.Size = new System.Drawing.Size(180, 22);
            this.tmiRelMensal.Text = "Mensal";
            this.tmiRelMensal.Click += new System.EventHandler(this.tmiRelMensal_Click);
            // 
            // tmiOpcoes
            // 
            this.tmiOpcoes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmiApagarDia,
            this.tmiApagarTodosOsRegistros});
            this.tmiOpcoes.Name = "tmiOpcoes";
            this.tmiOpcoes.Size = new System.Drawing.Size(59, 20);
            this.tmiOpcoes.Text = "Opções";
            // 
            // tmiApagarDia
            // 
            this.tmiApagarDia.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tmiApagarDia.Image = ((System.Drawing.Image)(resources.GetObject("tmiApagarDia.Image")));
            this.tmiApagarDia.Name = "tmiApagarDia";
            this.tmiApagarDia.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)));
            this.tmiApagarDia.Size = new System.Drawing.Size(212, 22);
            this.tmiApagarDia.Text = "Apagar dia";
            this.tmiApagarDia.Click += new System.EventHandler(this.tmiApagarRegistrosDia_Click);
            // 
            // tmiSair
            // 
            this.tmiSair.AutoToolTip = true;
            this.tmiSair.Name = "tmiSair";
            this.tmiSair.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.tmiSair.Size = new System.Drawing.Size(38, 20);
            this.tmiSair.Text = "Sair";
            this.tmiSair.ToolTipText = "Sair Ctrl + Q";
            this.tmiSair.Click += new System.EventHandler(this.tmiSair_Click);
            // 
            // rtfRel
            // 
            this.rtfRel.HideSelection = false;
            this.rtfRel.Location = new System.Drawing.Point(12, 36);
            this.rtfRel.Name = "rtfRel";
            this.rtfRel.ReadOnly = true;
            this.rtfRel.Size = new System.Drawing.Size(351, 215);
            this.rtfRel.TabIndex = 9;
            this.rtfRel.Text = "";
            this.rtfRel.Visible = false;
            // 
            // btnLimpar
            // 
            this.btnLimpar.BackColor = System.Drawing.Color.White;
            this.btnLimpar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnLimpar.Location = new System.Drawing.Point(293, 36);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(50, 23);
            this.btnLimpar.TabIndex = 10;
            this.btnLimpar.Text = "Limpar";
            this.btnLimpar.UseVisualStyleBackColor = false;
            this.btnLimpar.Visible = false;
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click);
            // 
            // tmiApagarTodosOsRegistros
            // 
            this.tmiApagarTodosOsRegistros.Image = ((System.Drawing.Image)(resources.GetObject("tmiApagarTodosOsRegistros.Image")));
            this.tmiApagarTodosOsRegistros.Name = "tmiApagarTodosOsRegistros";
            this.tmiApagarTodosOsRegistros.Size = new System.Drawing.Size(212, 22);
            this.tmiApagarTodosOsRegistros.Text = "Apagar Todos os Registros";
            this.tmiApagarTodosOsRegistros.Click += new System.EventHandler(this.tmiApagarTodosOsRegistros_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(375, 263);
            this.Controls.Add(this.btnLimpar);
            this.Controls.Add(this.rtfRel);
            this.Controls.Add(this.mnsMenuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnsMenuStrip;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Text = "Registro de Ponto";
            this.mnsMenuStrip.ResumeLayout(false);
            this.mnsMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip mnsMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem tmiRel;
        private System.Windows.Forms.ToolStripMenuItem tmiRelDiario;
        private System.Windows.Forms.ToolStripMenuItem tmiRelMensal;
        private System.Windows.Forms.ToolStripMenuItem tmiSair;
        private System.Windows.Forms.ToolStripMenuItem tmiSalvarToJSON;
        private System.Windows.Forms.ToolStripMenuItem tmiRegistros;
        private System.Windows.Forms.ToolStripMenuItem tmiBaterPonto;
        private System.Windows.Forms.ToolStripMenuItem tmiSelecionarAlmoco;
        private System.Windows.Forms.ToolStripMenuItem tmiApagarDia;
        private System.Windows.Forms.RichTextBox rtfRel;
        private System.Windows.Forms.Button btnLimpar;

        public void ChangeTextBoxData(string pText)
        {
            this.rtfRel.Text = pText;
        }

        public void ShowTextBox(bool pVisible)
        {
            this.rtfRel.Visible = pVisible;
        }

        public void ShowBtnLimpar(bool pVisible)
        {
            this.btnLimpar.Visible = pVisible;
        }

        private System.Windows.Forms.ToolStripMenuItem tmiOpcoes;
        private System.Windows.Forms.ToolStripMenuItem tmiApagarTodosOsRegistros;
    }
    
}

