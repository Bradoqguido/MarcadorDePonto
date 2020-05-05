using Microsoft.VisualBasic;
using System;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using WindowsFormsApp1.Models;
using Newtonsoft.Json;
using System.IO;

namespace MarcadorDePonto.Controllers
{
    class Controller
    {   
        /// <summary>
        /// Lista do tipo ponto.
        /// </summary>
        private List<Ponto> lstRegistrosPonto = new List<Ponto>();

        /// <summary>
        /// Controlador com as funções de manipulação de horas.
        /// </summary>
        private TimeController objTimeController;

        /// <summary>
        /// Lê os dados do arquivo json e salva na lista lstPonto.
        /// </summary>
        public void ReadDataFromJson()
        {
            try
            {
                string strJsonString;
                strJsonString = File.ReadAllText("jsonFileTest.json");
                lstRegistrosPonto = JsonConvert.DeserializeObject<List<Ponto>>(strJsonString);
                objTimeController = new TimeController(lstRegistrosPonto);
            }
            catch (FileNotFoundException)
            {
                ShowWarnMessageBox("Não encontramos o arquivo com suas horas :c", "Atenção");
            }
        }

        /// <summary>
        /// Função para exibir mensagens na tela por meio de uma janela,
        /// utilizando um paramentro para o titulo da janela e outro
        /// para a descrição do a pessoa deve inserir na caixa de texto.
        /// </summary>
        /// <param name="pTitleText">Titulo da janela</param>
        /// <param name="pDescText">Descrição</param>
        public void ShowInputBox(string pTitleText, string pDescText)
        {
            string strAux = Interaction.InputBox(pDescText, (pTitleText), "");

            // Finaliza a função aqui, caso não tenha nada,
            // pois o botão cancelar retorna uma string vazia
            if (strAux.Length == 0)
            {
                return;
            }

            if (strAux.Length < 5)
            {
                ShowWarnMessageBox("O registro não foi inserido, pois não é válido!", "Atenção");
                return;
            }

            bool blnErrado = objTimeController.ValidarNovoRegistroDeHorario(strAux);
            if (blnErrado)
            {
                ShowWarnMessageBox("O registro não foi inserido, pois não é válido!", "Atenção");
            }

            bool blnHoraMenorQueAnterior = objTimeController.InserirRegistro(strAux);
            if (blnHoraMenorQueAnterior)
            {
                ShowWarnMessageBox("O registro não foi inserido, pois o horário digitado é menor que o anterior!", "Atenção");
            }
        }

        /// <summary>
        /// Sumarização da função MessageBox:
        /// Botões: OK,
        /// Icone: Information,
        /// Sendo necessário apenas inserir os textos.
        /// </summary>
        /// <param name="pText">Conteúdo/Mensagem</param>
        /// <param name="pTitle">Titulo da janela</param>
        private void ShowInfoMessageBox(string pText, string pTitle)
        {
            MessageBox.Show(pText, pTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Sumarização da função MessageBox:
        /// Botões: OK,
        /// Icone: Warning,
        /// Sendo necessário apenas inserir os textos.
        /// </summary>
        /// <param name="pText">Conteúdo/Mensagem</param>
        /// <param name="pTitle">Titulo da janela</param>
        private void ShowWarnMessageBox(string pText, string pTitle)
        {
            MessageBox.Show(pText, pTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Salva os dados do objeto objPonto na lista lstPonto em um arquivo json.
        /// </summary>
        public void SalvarDadosEmJson()
        {
            string strJsonString;
            strJsonString = JsonConvert.SerializeObject(lstRegistrosPonto, Formatting.Indented);
            File.WriteAllText("jsonFileTest.json", strJsonString);
            ShowInfoMessageBox("Seus registros foram salvos com sucesso!", "Sucesso");
        }

        /// <summary>
        /// Essa função exibe ao usuário em uma caixa de mensagem,
        /// quantas horas e minutos extras fez
        /// e quantos minutos de almoço teve.
        /// </summary>
        public void ExibirRelatorioDoDia()
        {
            Ponto objRegistroPonto = objTimeController.GetRegistroDePontoAtual();

            if (objRegistroPonto.Horarios.Count < 2)
            {
                ShowInfoMessageBox("Existe apenas uma hora registrada ainda hoje, registre mais pontos, para gerar o relatório do dia!", "Info");
            } else
            {
                objTimeController.SomarHorasDoDia();
                StringBuilder stbConteudoMsg = new StringBuilder();

                stbConteudoMsg.Append("Registros:\n" + MontarStrComHorariosEntradaEsaida(objRegistroPonto.Horarios));

                stbConteudoMsg.Append("\nHoras extras: " + objRegistroPonto.HorasExtras.ToString() + ":");
                stbConteudoMsg.Append(Math.Abs(objRegistroPonto.MinutosExtras).ToString() + "h");
                
                int intHorasAlmoco = (int)(objRegistroPonto.MinutosAlmoco / 60);
                int intMinutosAlmoco = (int)(objRegistroPonto.MinutosAlmoco % 60);

                if (intHorasAlmoco.ToString().Length < 2)
                {
                    stbConteudoMsg.Append("\nSeu almoço durou: 0" + intHorasAlmoco.ToString() + ":");
                }
                else
                {
                    stbConteudoMsg.Append("\nSeu almoço durou: " + intHorasAlmoco.ToString() + ":");
                }

                if (intMinutosAlmoco.ToString().Length < 2)
                {
                    stbConteudoMsg.Append("0" + intMinutosAlmoco.ToString() + "h");
                } else
                {
                    stbConteudoMsg.Append(intMinutosAlmoco.ToString() + "h");
                }

                if (objRegistroPonto.MinutosAlmoco < 60)
                {
                    stbConteudoMsg.Append("\nO almoço deve ser de no minimo 60 minutos.");
                    stbConteudoMsg.Append("\n12:00 até 13:00 = 60 minutos");
                    stbConteudoMsg.Append("\nO almoço deve ser de no maximo 90 minutos.");
                    stbConteudoMsg.Append("\n12:00 até 13:30 = 90 minutos");
                }

                // ShowInfoMessageBox(stbConteudoMsg.ToString(), "Horas extras do dia");
                ShowWarnMessageBox("Relatório momentaneamente desabilitado!", "Atenção");
            }
        }

        /// <summary>
        /// Essa função monta uma string com as horas
        /// no formato "entrada - saida"
        /// </summary>
        /// <param name="pArr">Array de horas</param>
        /// <returns>Retorna um stringBuilder com os dados formatados</returns>
        private string MontarStrComHorariosEntradaEsaida(List<string> pArr)
        {
            StringBuilder stbBuilder = new StringBuilder();

            int intNRegistros = pArr.Count;

            // Se for impar é retirado um registro
            if (!(pArr.Count % 2 == 0))
            {
                intNRegistros--;
            }

            for (int intI = 0; intI < intNRegistros; intI+=2)
            {
                stbBuilder.Append(pArr[intI] + " - " + pArr[intI + 1] + "\n");
            }
            
            return stbBuilder.ToString();
        }

        /// <summary>
        /// Invoca a função que zera os registros do dia da classe TimeController.
        /// </summary>
        public void InvocarZerarRegistrosDia()
        {
            string strAux = Interaction.InputBox("Você tem certeza que deseja apagar TODOS os horários Registrados hoje? (S)Sim / (N)Não\n\nEssa operação não pode ser desfeita!! D:", "Atenção", "");
            
            if (strAux.Contains("S") || strAux.Contains("s"))
            {
                objTimeController.ZerarRegistrosDia();
                ShowWarnMessageBox("Horários apagados!", "Atenção");
                SalvarDadosEmJson();
            } else
            {
                ShowInfoMessageBox("Os Dados NÃO foram apagados!", "Atenção");
            }
            
        }
    }
}