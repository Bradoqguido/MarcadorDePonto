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
        /// Registro de ponto atual
        /// </summary>
        private Ponto objRegistroPonto = new Ponto();

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
                MessageBox.Show("Não encontramos o arquivo com suas horas :c", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Função para exibir mensagens na tela por meio de uma janela,
        /// utilizando um paramentro para o titulo da janela e outro
        /// para a descrição do a pessoa deve inserir na caixa de texto.
        /// </summary>
        public void InserirHorario()
        {
            string strAux = Interaction.InputBox("Insira a hora de entrada (Ex: 07:42)", "Insira o horario", "");
            if (ValidacoesDeEntrada(strAux))
            {
                objTimeController.InserirRegistro(strAux);
            }
        }

        /// <summary>
        /// Salva os dados do objeto objPonto na lista lstPonto em um arquivo json.
        /// </summary>
        public void SalvarDadosEmJson()
        {
            string strJsonString;
            strJsonString = JsonConvert.SerializeObject(lstRegistrosPonto, Formatting.Indented);
            File.WriteAllText("jsonFileTest.json", strJsonString);
            MessageBox.Show("Seus registros foram salvos com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Essa função exibe ao usuário em uma caixa de mensagem,
        /// quantas horas e minutos extras fez
        /// e quantos minutos de almoço teve.
        /// </summary>
        public void ExibirRelatorioDoDia()
        {
            objRegistroPonto = objTimeController.GetRegistroDePontoAtual();

            if (objRegistroPonto.Horarios.Count < 2)
            {
                MessageBox.Show("Existe apenas uma registro de hoje, registre mais pontos, para gerar o relatório do dia!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            } else
            {
                objTimeController.SomarHorasDoDia();
                StringBuilder stbConteudoMsg = new StringBuilder();

                stbConteudoMsg.Append("Registros:\n" + MontarStrComHorariosEntradaEsaida(objRegistroPonto.Horarios));
                stbConteudoMsg = MontaHorasExtrasRelatorio(stbConteudoMsg);
                stbConteudoMsg = MontaAlmocoDoRelatorio(stbConteudoMsg);

                MessageBox.Show(stbConteudoMsg.ToString(), "Horas extras do dia", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Função para anexar ao relatório diário detalhes sobre as horas extras do usuário.
        /// </summary>
        /// <param name="pStbConteudoMsg">Recebe o string builder do relatório</param>
        /// <returns>Retona uma string contendo o relatório, mais os detalhes das horas extras.</returns>
        private StringBuilder MontaHorasExtrasRelatorio(StringBuilder pStbConteudoMsg)
        {
            pStbConteudoMsg.Append("\nHoras extras: " + objRegistroPonto.HorasExtras.ToString() + ":");
            pStbConteudoMsg.Append(objRegistroPonto.MinutosExtras.ToString() + "h");

            if (objRegistroPonto.Excedeu71min)
            {
                pStbConteudoMsg.Append("\nVocê fez mais que 01:11 horas extras hoje. D:\n");
            }

            if (objRegistroPonto.Fez6hDireto)
            {
                pStbConteudoMsg.Append("\nVocê fez 6 horas seguidas hoje! D:.\n");
            }

            return pStbConteudoMsg;
        }

        /// <summary>
        /// Função para anexar ao relatório diário detalhes sobre o almoço do usuário.
        /// </summary>
        /// <param name="pStbConteudoMsg">Recebe o string builder do relatório</param>
        /// <returns>Retona uma string contendo o relatório, mais os detalhes do almoço.</returns>
        private StringBuilder MontaAlmocoDoRelatorio(StringBuilder pStbConteudoMsg)
        {
            int intHorasAlmoco = (int) (objRegistroPonto.MinutosAlmoco / 60);
            int intMinutosAlmoco = (int) (objRegistroPonto.MinutosAlmoco % 60);

            if (intHorasAlmoco.ToString().Length < 2)
            {
                pStbConteudoMsg.Append("\nSeu almoço durou: 0" + intHorasAlmoco.ToString() + ":");
            }
            else
            {
                pStbConteudoMsg.Append("\nSeu almoço durou: " + intHorasAlmoco.ToString() + ":");
            }

            if (intMinutosAlmoco.ToString().Length < 2)
            {
                pStbConteudoMsg.Append("0" + intMinutosAlmoco.ToString() + "h");
            }
            else
            {
                pStbConteudoMsg.Append(intMinutosAlmoco.ToString() + "h");
            }

            if (objRegistroPonto.MinutosAlmoco <= 60)
            {
                pStbConteudoMsg.Append("\nO almoço deve ser de no minímo 60 minutos.");
                pStbConteudoMsg.Append("\n12:00 até 13:00 = 60 minutos");
            }

            if (objRegistroPonto.MinutosAlmoco >= 90)
            {
                pStbConteudoMsg.Append("\nO almoço deve ser de até 90 minutos.");
                pStbConteudoMsg.Append("\n12:00 até 13:30 = 90 minutos");
            }

            return pStbConteudoMsg;
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

            // Reduz 1 do tamanho, deixando em formato de array/list
            intNRegistros--;

            for (int intI = 0; intI < intNRegistros; intI+=2)
            {
                stbBuilder.Append(pArr[intI] + " - " + pArr[intI + 1] + "\n");
            }

            if (pArr.Count % 2 == 1)
            {
                stbBuilder.Append(pArr[intNRegistros] + " - " + "\n");
            }

            return stbBuilder.ToString();
        }

        /// <summary>
        /// Invoca a função que zera os registros do dia da classe TimeController.
        /// </summary>
        public void InvocarZerarRegistrosDia()
        {
            objRegistroPonto = objTimeController.GetRegistroDePontoAtual();
            string strAux = Interaction.InputBox("Você tem certeza que deseja apagar TODOS os horários Registrados hoje? (S)Sim / (N)Não\n\nEssa operação não pode ser desfeita!! D:", "Atenção", "");
            
            if (strAux.Contains("S") || strAux.Contains("s"))
            {
                objTimeController.ZerarRegistrosDia();
                MessageBox.Show("Horários apagados!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                SalvarDadosEmJson();
            } else
            {
                MessageBox.Show("Os Dados NÃO foram apagados!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        /// <summary>
        /// Função para o usuário selecionar o horário de almoço, dentre os horários registrados.
        /// </summary>
        public void SelecionarAlmoco()
        {
            objRegistroPonto = objTimeController.GetRegistroDePontoAtual();
            string strResgistrosDeHorarios = MontarStrComHorariosEntradaEsaida(objRegistroPonto.Horarios);

            if (!(strResgistrosDeHorarios.Length > 0))
            {
                MessageBox.Show("Você não possui registros o suficiente para selecionar o almoço!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string strInicioAlmoco = Interaction.InputBox("Digite o INÍCIO do seu almoço dentre os horários registrados:\n" + strResgistrosDeHorarios, "Insira o horario de almoço", "");
            if (!ValidacoesDeEntrada(strInicioAlmoco))
            {
                return;
            }

            string strFimAlmoco = Interaction.InputBox("Digite o FIM do seu almoço dentre os horários registrados:\n" + strResgistrosDeHorarios, "Insira o horario de almoço", "");
            if (ValidacoesDeEntrada(strFimAlmoco))
            {
                objTimeController.DefinirHorarioAlmoco(strInicioAlmoco, strFimAlmoco);
            }
        }

        /// <summary>
        /// Chama uma sequência de validações para o horário registrado.
        /// </summary>
        /// <param name="pStrAux">String recebida</param>
        /// <returns>Retorna um boleano True se passou nas validações e False se não passou.</returns>
        private bool ValidacoesDeEntrada(string pStrAux)
        {
            // Finaliza a função aqui, caso não tenha nada,
            // pois o botão cancelar retorna uma string vazia
            if (pStrAux.Length == 0)
            {
                return false;
            }

            if (pStrAux.Length < 5)
            {
                MessageBox.Show("O registro não foi inserido, pois o horário não é válido!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            bool blnErrado = objTimeController.ValidarNovoRegistroDeHorario(pStrAux);
            if (blnErrado)
            {
                MessageBox.Show("O registro não foi inserido, pois o horário não é válido!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
    }
}