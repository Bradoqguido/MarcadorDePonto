using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Newtonsoft.Json;
using WindowsFormsApp1.Models;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Main : Form
    {
        /// <summary>
        /// Mensagem pré-fixada pedindo que o usuário digite novamente
        /// </summary>
        private static string strMsgDeExemplo = "Insira novamente a horario como o exemplo (Ex: 07:42)";
        
        /// <summary>
        /// Lista string de caracteres liberados
        /// </summary>
        private static string[] strCaracteresLiberados = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", ":" };
        
        /// <summary>
        /// Lista do tipo ponto
        /// </summary>
        private List<Ponto> lstPonto = new List<Ponto>();
        
        /// <summary>
        /// Objeto do tipo Ponto
        /// </summary>
        private Ponto objPonto = new Ponto();

        public Main()
        {
            InitializeComponent();
            ReadDataFromJson();
        }

        /// <summary>
        /// Função de clique do botão bater ponto
        /// </summary>
        /// <param name="sender">Parametro de objeto</param>
        /// <param name="e">Parametro de envento</param>
        private void btnBaterPonto_Click(object sender, EventArgs e)
        {
            string strHorario = ShowInputBox("Insira o horario", "Insira a hora de entrada (Ex: 07:42)");
            VerifyChars(strHorario);
        }

        /// <summary>
        /// Função para verificar caracteres diferentes de numeros ou de :
        /// se tiver qualquer caracter diferente dos especificados, a função se torna recursiva
        /// se não, o horario é salvo
        /// </summary>
        /// <param name="pHorario">Objeto do tipo string</param>
        public void VerifyChars(string pHorario)
        {
            bool blnHorarioErrado = false;
            foreach(string strLiberado in strCaracteresLiberados)
            {
                if (pHorario.Contains(strLiberado))
                {
                    blnHorarioErrado = false;
                } else
                {
                    blnHorarioErrado = true;
                }
            }

            /// verifica a posição do :
            if (pHorario.IndexOf(":") != 2)
            {
                blnHorarioErrado = true;
            }

            // verifica hora
            if (int.Parse(pHorario.Substring(0, 2)) > 23)
            {
                blnHorarioErrado = true;
            }

            // verifica minutos
            if (int.Parse(pHorario.Substring(3, 2)) > 59)
            {
                blnHorarioErrado = true;
            }

            // verifica o tamanho do horario
            if (pHorario.Length > 5)
            {
                blnHorarioErrado = true;
            }


            if (blnHorarioErrado)
            {
                pHorario = ShowInputBox("Insira o horario", strMsgDeExemplo);
                VerifyChars(pHorario);
            } else
            {
                objPonto.LastHorarioRegistrado = pHorario;
            }
        }

        /// <summary>
        /// Função para exibir mensagens na tela por meio de uma janela,
        /// utilizando um paramentro para o titulo da janela e outro
        /// para a descrição do a pessoa deve inserir na caixa de texto
        /// </summary>
        /// <param name="pTitleText">Titulo da janela</param>
        /// <param name="pDescText">Descrição</param>
        /// <returns>Retorna o valor que a pessoa digitou, ou a data de hoje</returns>
        private string ShowInputBox(string pTitleText, string pDescText) {
            string strAux = Interaction.InputBox(pDescText, (pTitleText), "");

            if (strAux != null)
            {
                if (strAux.Length > 0)
                {
                    return strAux;
                } else
                {
                    return DateAndTime.TimeString;
                }
            }

            return DateAndTime.TimeString;
        }

        /// <summary>
        /// Função de clique do botão de salvar
        /// </summary>
        /// <param name="sender">Parametro de objeto</param>
        /// <param name="e">Parametro de envento</param>
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            SaveDataToJson();
        }

        /// <summary>
        /// Salva os dados do objeto objPonto na lista lstPonto em um arquivo json
        /// </summary>
        private void SaveDataToJson() {
            objPonto.Data = DateTime.Today;

            if (objPonto.Horarios.Count == 0)
            {
                string strHorario = ShowInputBox("Insira o horario", "Insira ao menos um horario no formato (Ex: 12:45)");
                VerifyChars(strHorario);
            }

            VerificaSeExisteADataDeHoje();

            string strJsonString;
            strJsonString = JsonConvert.SerializeObject(lstPonto, Formatting.Indented);
            File.WriteAllText("jsonFileTest.json", strJsonString);
        }

        /// <summary>
        /// Lê os dados do arquivo json e salva na lista lstPonto
        /// </summary>
        private void ReadDataFromJson()
        {
            string strJsonString;
            strJsonString = File.ReadAllText("jsonFileTest.json");
            lstPonto = JsonConvert.DeserializeObject<List<Ponto>>(strJsonString);
        }

        /// <summary>
        /// Lê os dados do lstPonto e antes de salvar verifica se o dado já existe
        /// se existir já salva por cima do que existe
        /// </summary>
        private void VerificaSeExisteADataDeHoje()
        {
            for (int intI = 0; intI < lstPonto.Count; intI++)
            {
                if (objPonto.Data == lstPonto[intI].Data)
                {
                    lstPonto[intI] = objPonto;
                }
                else
                {
                    lstPonto.Add(objPonto);
                }
            }
        }

        /// <summary>
        /// Zera os dados do ponto
        /// </summary>
        private void ResetPonto()
        {
            objPonto = new Ponto();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
