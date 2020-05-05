using MarcadorDePonto.Controllers;
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

        /// <summary>
        /// Função de clique do botão bater ponto
        /// </summary>
        /// <param name="sender">Parametro de objeto</param>
        /// <param name="e">Parametro de envento</param>
        private void btnBaterPonto_Click(object sender, EventArgs e)
        {
            string strHorario = objController.ShowInputBox("Insira o horario", "Insira a hora de entrada (Ex: 07:42)");
            objController.VerifyWrongTime(strHorario);
        }

        /// <summary>
        /// Função de clique do botão de salvar
        /// </summary>
        /// <param name="sender">Parametro de objeto</param>
        /// <param name="e">Parametro de envento</param>
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            objController.VerifyDataToSave();
        }

        /// <summary>
        /// Função de clique do botão de sair
        /// </summary>
        /// <param name="sender">Parametro de objeto</param>
        /// <param name="e">Parametro de envento</param>
        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Função de clique do botão de gerar relatorios
        /// </summary>
        /// <param name="sender">Parametro de objeto</param>
        /// <param name="e">Parametro de envento</param>
        private void btnHorasExtrasDoDia_Click(object sender, EventArgs e)
        {
            objController.ExibirRelatorioDoDia();
        }
    }
}
