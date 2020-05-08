using MarcadorDePonto.Controllers;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
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

            if (!objController.ReadDataFromJson())
            {
                MessageBox.Show("Não encontramos o arquivo JSON com suas horas :c", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tmiBaterPonto_Click(object sender, EventArgs e)
        {
            string strInput = Interaction.InputBox("Insira a hora do ponto (Ex: 07:42)", "Inserir o ponto", "");

            if (strInput.Length <= 0)
            {
                return;
            }

            bool blnOutput = objController.InserirHorario(strInput);
            if (blnOutput)
            {
                MessageBox.Show("Seu registro foi inserido com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.None);
            } else
            {
                MessageBox.Show("O registro não foi inserido, pois o horário digitado é inválido" +
                    "\nO registro deve estar no padrão hh:mm -> 15:25," +
                    "\nO registro não pode conter caracteres especiais além do caracter ':'," +
                    "\nO registro inserido não pode ser menor que o registro anterior!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            string strOutup = objController.ExibirRelatorioDoDia();
            if (strOutup.Length > 0)
            {
                ChangeTextBoxData(strOutup + "\n\n" + objController.GetSaldoTotal());
                ShowBtnLimpar(true);
                ShowTextBox(true);
            }
        }

        private void tmiSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
            objController.SalvarDadosEmJson();
        }

        private void tmiApagarRegistrosDia_Click(object sender, EventArgs e)
        {
            string strInput = Interaction.InputBox("Você tem certeza que deseja apagar TODOS os horários Registrados hoje? (S)Sim / (N)Não\n\nEssa operação não pode ser desfeita!! D:", "Atenção", "");

            if (strInput.Length <= 0)
            {
                return;
            }
            if (strInput.Contains("S") || strInput.Contains("s"))
            {
                bool blnOutput = objController.InvocarZerarRegistrosDia(strInput);
                if (blnOutput)
                {
                    MessageBox.Show("Registros do dia apagados!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Não há registros para apagar, insira novos!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            } else
            {

                MessageBox.Show("Os Dados NÃO foram apagados!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSelecionarAlmoco_Click(object sender, EventArgs e)
        {
            List<string> lstRegistrosHoras = objController.GetHorarios();

            if (!(lstRegistrosHoras.Count > 0))
            {
                MessageBox.Show("Você não possui registros o suficiente para selecionar o almoço!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!InputsAlmoco(lstRegistrosHoras))
            {
                MessageBox.Show("O horário de almoço inserido, não pode ser diferente dos registros inseridos! :c", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            string strOutup = objController.ExibirRelatorioDoDia();
            if (strOutup.Length > 0)
            {
                ChangeTextBoxData(strOutup + "\n\n" + objController.GetSaldoTotal());
                ShowBtnLimpar(true);
                ShowTextBox(true);
            }
        }

        /// <summary>
        /// Função que contém os InputsBox's para selecionar os horários de almoço.
        /// </summary>
        /// <param name="pLstRegistrosHoras">Lista string com os registros de horas.</param>
        /// <returns>Retorna um boleano, True para sucesso e false caso contrário.</returns>
        private bool InputsAlmoco(List<string> pLstRegistrosHoras)
        {
            string strResgistrosDeHorarios = objController.MontarStrComHorariosEntradaEsaida(pLstRegistrosHoras);
            string strInicioAlmoco = Interaction.InputBox("Digite o INÍCIO do seu almoço dentre os horários registrados:\n" + strResgistrosDeHorarios, "Insira o horario de almoço", "");

            if (strInicioAlmoco.Length <= 0)
            {
                return true; // falso positivo sem ação
            }

            if (!objController.ValidacoesDeEntrada(strInicioAlmoco) && !pLstRegistrosHoras.Contains(strInicioAlmoco))
            {
                return false;
            }

            string strFimAlmoco = Interaction.InputBox("Digite o FIM do seu almoço dentre os horários registrados:\n" + strResgistrosDeHorarios, "Insira o horario de almoço", "");

            if (strFimAlmoco.Length <= 0)
            {
                return true; // falso positivo sem ação
            }

            if (!objController.ValidacoesDeEntrada(strFimAlmoco) && !pLstRegistrosHoras.Contains(strFimAlmoco))
            {
                return false;
            }

            if (objController.SelecionarAlmoco(strInicioAlmoco, strFimAlmoco))
            {
                MessageBox.Show("Horário de almoço definido: " + (strInicioAlmoco + " até " + strFimAlmoco), "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.None);
                return true;
            }

            return false;
        }

        private void tmiRelMensal_Click(object sender, EventArgs e)
        {
            ChangeTextBoxData(objController.ExibirRelatorioMensal());
            ShowBtnLimpar(true);
            ShowTextBox(true);
        }

        private void tmiRelDiario_Click(object sender, EventArgs e)
        {
            string strOutput = objController.ExibirRelatorioDoDia();

            if (strOutput.Length == 0)
            {
                MessageBox.Show("Existe apenas uma registro de hoje, registre mais pontos, para gerar o relatório do dia!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            } else
            {
                ChangeTextBoxData(strOutput + "\n\n" + objController.GetSaldoTotal());
                ShowBtnLimpar(true);
                ShowTextBox(true);
            }
        }

        private void tmiSalvarToJson_Click(object sender, EventArgs e)
        {
            objController.SalvarDadosEmJson();
            MessageBox.Show("Seus registros foram salvos com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            ChangeTextBoxData("");
            ShowTextBox(false);
            ShowBtnLimpar(false);
        }

        private void tmiApagarTodosOsRegistros_Click(object sender, EventArgs e)
        {
            string strInput = Interaction.InputBox("Você tem certeza que deseja apagar TODOS os horários Registrados hoje? (S)Sim / (N)Não\n\nEssa operação não pode ser desfeita!! D:", "Atenção", "");

            if (strInput.Length <= 0)
            {
                return;
            }
            if (strInput.Contains("S") || strInput.Contains("s"))
            {
                if (objController.ApagarTodosOsRegistros())
                {
                    MessageBox.Show("TODOS os registros foram apagados!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Não há registros para apagar, insira novos!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Os registros NÃO foram apagados!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
