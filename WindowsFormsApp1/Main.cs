﻿using MarcadorDePonto.Controllers;
using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Main : Form
    {
        /// <summary>
        /// Objeto da classe controller
        /// </summary>
        private Controller objController = new Controller();

        public Main()
        {
            InitializeComponent();
            objController.ReadDataFromJson();
        }

        private void btnBaterPonto_Click(object sender, EventArgs e)
        {
            objController.InserirHorario();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            objController.SalvarDadosEmJson();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnHorasExtrasDoDia_Click(object sender, EventArgs e)
        {
            objController.ExibirRelatorioDoDia();
        }

        private void btnZerarRegistrosDia_Click(object sender, EventArgs e)
        {
            objController.InvocarZerarRegistrosDia();
        }

        private void btnSelecionarAlmoco_Click(object sender, EventArgs e)
        {
            objController.SelecionarAlmoco();
        }
    }
}
