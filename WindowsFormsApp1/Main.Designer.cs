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
            this.btnBaterPonto = new System.Windows.Forms.Button();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnHorasExtraDoDia = new System.Windows.Forms.Button();
            this.btnZerarRegistrosDia = new System.Windows.Forms.Button();
            this.btnSelecionarAlmoco = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnBaterPonto
            // 
            this.btnBaterPonto.BackColor = System.Drawing.SystemColors.Control;
            this.btnBaterPonto.Location = new System.Drawing.Point(111, 12);
            this.btnBaterPonto.Name = "btnBaterPonto";
            this.btnBaterPonto.Size = new System.Drawing.Size(96, 23);
            this.btnBaterPonto.TabIndex = 1;
            this.btnBaterPonto.Text = "Marcar ponto";
            this.btnBaterPonto.UseVisualStyleBackColor = false;
            this.btnBaterPonto.Click += new System.EventHandler(this.btnBaterPonto_Click);
            // 
            // btnSalvar
            // 
            this.btnSalvar.BackColor = System.Drawing.SystemColors.Control;
            this.btnSalvar.Location = new System.Drawing.Point(12, 99);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(138, 23);
            this.btnSalvar.TabIndex = 3;
            this.btnSalvar.Text = "Salvar em formato JSON";
            this.btnSalvar.UseVisualStyleBackColor = false;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // btnSair
            // 
            this.btnSair.BackColor = System.Drawing.SystemColors.Control;
            this.btnSair.Location = new System.Drawing.Point(120, 128);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 23);
            this.btnSair.TabIndex = 4;
            this.btnSair.Text = "Sair";
            this.btnSair.UseVisualStyleBackColor = false;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // btnHorasExtraDoDia
            // 
            this.btnHorasExtraDoDia.BackColor = System.Drawing.SystemColors.Control;
            this.btnHorasExtraDoDia.Location = new System.Drawing.Point(111, 70);
            this.btnHorasExtraDoDia.Name = "btnHorasExtraDoDia";
            this.btnHorasExtraDoDia.Size = new System.Drawing.Size(95, 23);
            this.btnHorasExtraDoDia.TabIndex = 5;
            this.btnHorasExtraDoDia.Text = "Relatório do dia";
            this.btnHorasExtraDoDia.UseVisualStyleBackColor = false;
            this.btnHorasExtraDoDia.Click += new System.EventHandler(this.btnHorasExtrasDoDia_Click);
            // 
            // btnZerarRegistrosDia
            // 
            this.btnZerarRegistrosDia.BackColor = System.Drawing.SystemColors.Control;
            this.btnZerarRegistrosDia.Location = new System.Drawing.Point(160, 99);
            this.btnZerarRegistrosDia.Name = "btnZerarRegistrosDia";
            this.btnZerarRegistrosDia.Size = new System.Drawing.Size(138, 23);
            this.btnZerarRegistrosDia.TabIndex = 6;
            this.btnZerarRegistrosDia.Text = "Zerar registros do dia";
            this.btnZerarRegistrosDia.UseVisualStyleBackColor = false;
            this.btnZerarRegistrosDia.Click += new System.EventHandler(this.btnZerarRegistrosDia_Click);
            // 
            // btnSelecionarAlmoco
            // 
            this.btnSelecionarAlmoco.BackColor = System.Drawing.SystemColors.Control;
            this.btnSelecionarAlmoco.Location = new System.Drawing.Point(101, 41);
            this.btnSelecionarAlmoco.Name = "btnSelecionarAlmoco";
            this.btnSelecionarAlmoco.Size = new System.Drawing.Size(118, 23);
            this.btnSelecionarAlmoco.TabIndex = 7;
            this.btnSelecionarAlmoco.Text = "Selecionar almoço";
            this.btnSelecionarAlmoco.UseVisualStyleBackColor = false;
            this.btnSelecionarAlmoco.Click += new System.EventHandler(this.btnSelecionarAlmoco_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 162);
            this.Controls.Add(this.btnSelecionarAlmoco);
            this.Controls.Add(this.btnZerarRegistrosDia);
            this.Controls.Add(this.btnHorasExtraDoDia);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.btnBaterPonto);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Main";
            this.Text = "Auto ponto";
            this.ResumeLayout(false);

        }

        #endregion
        
        private System.Windows.Forms.Button btnBaterPonto;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnHorasExtraDoDia;
        private System.Windows.Forms.Button btnZerarRegistrosDia;
        private System.Windows.Forms.Button btnSelecionarAlmoco;
    }
}

