using Microsoft.VisualBasic;
using System;
using System.Text;
using System.Collections.Generic;
using WindowsFormsApp1.Models;
using Newtonsoft.Json;
using System.IO;

namespace MarcadorDePonto.Controllers
{
    public class Controller
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
        /// <returns>Retorna true para sucesso e false caso contrário</returns>
        public bool ReadDataFromJson()
        {
            try
            {
                string strJsonString;
                strJsonString = File.ReadAllText("database.json");
                lstRegistrosPonto = JsonConvert.DeserializeObject<List<Ponto>>(strJsonString);
                objTimeController = new TimeController(lstRegistrosPonto);
                return true;
            }
            catch (FileNotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// Função para exibir mensagens na tela por meio de uma janela,
        /// utilizando um paramentro para o titulo da janela e outro
        /// para a descrição do a pessoa deve inserir na caixa de texto.
        /// </summary>
        /// <param name="pStrInput">Valor string digitado.</param>
        /// <returns>Retorna True para caso passe nas validações e false caso contrário.</returns>
        public bool InserirHorario(string pStrInput)
        {
            if (ValidacoesDeEntrada(pStrInput))
            {
                return objTimeController.InserirRegistro(pStrInput);
            }
            return false;
        }

        /// <summary>
        /// Chama uma sequência de validações para o horário registrado.
        /// </summary>
        /// <param name="pStrAux">String recebida</param>
        /// <returns>Retorna um boleano True se passou nas validações e False se não passou.</returns>
        public bool ValidacoesDeEntrada(string pStrAux)
        {
            // Finaliza a função aqui, caso não tenha nada,
            // pois o botão cancelar retorna uma string vazia
            if (pStrAux.Length == 0)
            {
                return false;
            }

            if (pStrAux.Length < 5)
            {
                return false;
            }

            bool blnErrado = objTimeController.ValidarNovoRegistroDeHorario(pStrAux);
            if (blnErrado)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Salva os dados do objeto objPonto na lista lstPonto em um arquivo json.
        /// </summary>
        public void SalvarDadosEmJson()
        {
            string strJsonString;
            strJsonString = JsonConvert.SerializeObject(lstRegistrosPonto, Formatting.Indented);
            File.WriteAllText("database.json", strJsonString);
        }

        /// <summary>
        /// Essa função exibe ao usuário em uma caixa de mensagem,
        /// quantas horas e minutos extras fez
        /// e quantos minutos de almoço teve.
        /// </summary>
        /// <returns>Retorna uma string contendo informações do relatório.</returns>
        public string ExibirRelatorioDoDia()
        {
            objRegistroPonto = objTimeController.GetRegistroDePontoAtual();

            if (objRegistroPonto.Horarios.Count < 2)
            {
                return "";
            } else
            {
                objTimeController.SomarHorasDoDia(objRegistroPonto);
                StringBuilder stbConteudoMsg = new StringBuilder();

                stbConteudoMsg.Append("Registros:\n" + MontarStrComHorariosEntradaEsaida(objRegistroPonto.Horarios));
                stbConteudoMsg = MontaHorasExtrasRelatorio(stbConteudoMsg, objRegistroPonto);
                stbConteudoMsg = MontaAlmocoDoRelatorio(stbConteudoMsg, objRegistroPonto);

                stbConteudoMsg.Append(objTimeController.HoraDeSaida(objRegistroPonto));

                return stbConteudoMsg.ToString();
            }
        }

        /// <summary>
        /// Função para anexar ao relatório diário detalhes sobre as horas extras do usuário.
        /// </summary>
        /// <param name="pStbConteudoMsg">Recebe o string builder do relatório</param>
        /// <param name="pPonto">Recebe o objeto Ponto</param>
        /// <returns>Retona uma string contendo o relatório, mais os detalhes das horas extras.</returns>
        public StringBuilder MontaHorasExtrasRelatorio(StringBuilder pStbConteudoMsg, Ponto pPonto)
        {
            pStbConteudoMsg.Append("\nHoras extras: " + pPonto.HorasExtras.ToString() + ":");
            pStbConteudoMsg.Append(pPonto.MinutosExtras.ToString() + "h");

            if (pPonto.Excedeu71min)
            {
                pStbConteudoMsg.Append("\nVocê fez mais que 01:11 horas extras hoje. D:\n");
            }

            if (pPonto.Fez6hDireto)
            {
                pStbConteudoMsg.Append("\nVocê fez 6 horas seguidas hoje! D:.\n");
            }

            return pStbConteudoMsg;
        }

        /// <summary>
        /// Função para anexar ao relatório diário detalhes sobre o almoço do usuário.
        /// </summary>
        /// <param name="pStbConteudoMsg">Recebe o string builder do relatório</param>
        /// <param name="pPonto">Recebe o objeto Ponto</param>
        /// <returns>Retona uma string contendo o relatório, mais os detalhes do almoço.</returns>
        private StringBuilder MontaAlmocoDoRelatorio(StringBuilder pStbConteudoMsg, Ponto pPonto)
        {
            int intHorasAlmoco = (int) (pPonto.MinutosAlmoco / 60);
            int intMinutosAlmoco = (int) (pPonto.MinutosAlmoco % 60);

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
        public string MontarStrComHorariosEntradaEsaida(List<string> pArr)
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
        /// <param name="pStrInput">Recebe uma entrada string do usuário</param>
        /// <returns>Retorna a resposta booleana da função de eliminar registros e caso o usuário negue a operação, retorna false;</returns>
        public bool InvocarZerarRegistrosDia(string pStrInput)
        {
            objRegistroPonto = objTimeController.GetRegistroDePontoAtual();
            return objTimeController.ZerarRegistrosDia();
        }
        
        /// <summary>
        /// Função que APAGA TODOS os registros do JSON.
        /// </summary>
        /// <returns>Retorna True ao eliminar TODOS os registros e caso contrário retorna false;</returns>
        public bool ApagarTodosOsRegistros()
        {
            if (lstRegistrosPonto.Count == 0)
            {
                return false;
            } else
            {
                int intRegistrosCount = lstRegistrosPonto.Count;
                lstRegistrosPonto.RemoveRange(0, intRegistrosCount);
            }
            return false;
        }

        /// <summary>
        /// Função para o usuário selecionar o horário de almoço, dentre os horários registrados.
        /// </summary>
        /// <param name="pStrInicioAlmoco">Hora de inicio</param>
        /// <param name="pStrFimAlmoco">Hora final</param>
        /// <returns>Retorna um booleano</returns>
        public bool SelecionarAlmoco(string pStrInicioAlmoco, string pStrFimAlmoco)
        {
            return objTimeController.DefinirHorarioAlmoco(pStrInicioAlmoco, pStrFimAlmoco);
        }

        /// <summary>
        /// Função para retornar a lista de horários registrados no dia.
        /// </summary>
        /// <returns>Retorna a lista contendo os horários registrados do dia.</returns>
        public List<string> GetHorarios()
        {
            return objTimeController.GetRegistroDePontoAtual().Horarios;
        }

        /// <summary>
        /// Função para gerar o relatório mensal
        /// </summary>
        /// <returns>Retorna a string contendo o relatório</returns>
        public string ExibirRelatorioMensal()
        {
            StringBuilder stbRelatorioMensal = new StringBuilder();
            objRegistroPonto = objTimeController.GetRegistroDePontoAtual();

            if (objRegistroPonto.Horarios.Count < 2)
            {
                return "Existe apenas uma registro de hoje, registre mais pontos, para gerar o relatório mensal!";
            }
            else
            {
                foreach (Ponto objPonto in lstRegistrosPonto)
                {
                    DateTime dtaData = objPonto.Data;
                    objTimeController.SomarHorasDoDia(objPonto);
                    string strDia = (dtaData.Day + "/" + dtaData.Month + "/" + dtaData.Year);
                    stbRelatorioMensal.AppendLine("\nRegistros do dia: " + strDia);
                    stbRelatorioMensal.Append(MontarStrComHorariosEntradaEsaida(objPonto.Horarios));
                    stbRelatorioMensal = MontaHorasExtrasRelatorio(stbRelatorioMensal, objPonto);
                    stbRelatorioMensal = MontaAlmocoDoRelatorio(stbRelatorioMensal, objPonto);
                    stbRelatorioMensal.Append("\n------------------------------------------------------------------------");
                }
            }

            return stbRelatorioMensal.ToString();
        }

        /// <summary>
        /// Função para calcular o saldo total do usuário.
        /// </summary>
        /// <returns>Retorna a string contendo o saldo total.</returns>
        public string GetSaldoTotal()
        {
            int intTotalSaldoHoras = 0;
            int intTotalSaldoMinutos = 0;

            foreach (Ponto objPonto in lstRegistrosPonto)
            {
                intTotalSaldoHoras += (int) objPonto.HorasExtras;

                if (objPonto.HorasExtras < 0)
                {
                    intTotalSaldoMinutos -= (int) objPonto.MinutosExtras;
                } else
                {
                    intTotalSaldoMinutos += (int) objPonto.MinutosExtras;
                }
            }

            intTotalSaldoHoras += (intTotalSaldoMinutos / 60);
            intTotalSaldoMinutos = (intTotalSaldoMinutos % 60);

            return ("Seu saldo total é: " + intTotalSaldoHoras + ":" + Math.Abs(intTotalSaldoMinutos) + "h");
        }
    }
}