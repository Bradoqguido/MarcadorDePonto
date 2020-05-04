﻿using Microsoft.VisualBasic;
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
        /// Mensagem pré-fixada pedindo que o usuário digite novamente.
        /// </summary>
        private static string strMsgDeExemplo = "Insira novamente a horario como o exemplo (Ex: 07:42)";

        /// <summary>
        /// Lista string de caracteres liberados.
        /// </summary>
        private static string[] strCaracteresLiberados = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", ":" };

        /// <summary>
        /// Lista string de caracteres liberados, ou seja:
        /// 12:00 - 07:42 = 258 minutos,
        /// 12:00 - 13:00 = 60 minutos,
        /// 12:00 - 13:30 = 90 minutos,
        /// 13:30 - 18:00 = 270 minutos.
        /// </summary>
        private static int[] intMinutosEntreHorasDeTrabalho = { 258, 270 };

        /// <summary>
        /// Lista do tipo ponto.
        /// </summary>
        private List<Ponto> lstPonto = new List<Ponto>();

        /// <summary>
        /// Objeto do tipo Ponto.
        /// </summary>
        private Ponto objPonto = new Ponto();



        /// <summary>
        /// Lê os dados do arquivo json e salva na lista lstPonto.
        /// </summary>
        public void ReadDataFromJson()
        {
            try
            {
                string strJsonString;
                strJsonString = File.ReadAllText("jsonFileTest.json");
                lstPonto = JsonConvert.DeserializeObject<List<Ponto>>(strJsonString);
                GetPontoFromToday();
            }
            catch (FileNotFoundException)
            {
                ShowWarnMessageBox("Não encontramos o arquivo com suas horas :c", "Atenção");
            }
        }

        /// <summary>
        /// Encontra o Ponto de hoje e o atribui ao objPonto.
        /// </summary>
        private void GetPontoFromToday()
        {
            objPonto.Data = DateTime.Today;

            int intDateIndex = FindPontoObjIndex();
            if (intDateIndex > -1)
            {
                objPonto = lstPonto[intDateIndex];
            }
        }

        /// <summary>
        /// Busca na lista de objetos lstPonto um que seja do dia de hoje,
        /// se encontrar, retorna o index do mesmo.
        /// </summary>
        /// <returns>Retorna um index inteiro</returns>
        private int FindPontoObjIndex()
        {
            for (int intI = 0; intI < lstPonto.Count; intI++)
            {
                if (objPonto.Data == lstPonto[intI].Data)
                {
                    return intI;
                }
            }
            return -1;
        }

        /// <summary>
        /// Função para exibir mensagens na tela por meio de uma janela,
        /// utilizando um paramentro para o titulo da janela e outro
        /// para a descrição do a pessoa deve inserir na caixa de texto.
        /// </summary>
        /// <param name="pTitleText">Titulo da janela</param>
        /// <param name="pDescText">Descrição</param>
        /// <returns>Retorna o valor que a pessoa digitou, ou a data de hoje</returns>
        public string ShowInputBox(string pTitleText, string pDescText)
        {
            string strAux = Interaction.InputBox(pDescText, (pTitleText), "");

            if (strAux != null)
            {
                if (strAux.Length > 0)
                {
                    return strAux;
                }
                else
                {
                    return DateAndTime.TimeString;
                }
            }

            return DateAndTime.TimeString;
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
        /// Função para verificar caracteres diferentes de numeros ou de : ,
        /// se tiver qualquer caracter diferente dos especificados, a função se torna recursiva,
        /// se não, o horario é salvo.
        /// </summary>
        /// <param name="pHorario">Objeto do tipo string</param>
        public void VerifyWrongTime(string pHorario)
        {
            // Finaliza a função aqui, caso não tenha nada,
            // pois o botão cancelar retorna uma string vazia
            if (pHorario.Length == 0)
            {
                return;
            }

            bool blnHorarioErrado = false;

            blnHorarioErrado = VerifyAceptedChars(pHorario);
            
            // adiciona 0 na frente da hora se tiver só um número
            if (pHorario.Substring(0, 2).Contains(":"))
            {
                pHorario = ("0" + pHorario);
            }

            if (pHorario.IndexOf(":") != 2)
            {
                blnHorarioErrado = true;
            }

            blnHorarioErrado = VerifyTime(pHorario);
            VerifyIfTimeIsOk(blnHorarioErrado, pHorario);
        }

        /// <summary>
        /// função para verificar se os caracteres digitados estão na lista de aceitos.
        /// </summary>
        /// <param name="pHorario">Horario que o usuário digitou</param>
        /// <returns>Retorna um Boolean</returns>
        private bool VerifyAceptedChars(string pHorario)
        {
            bool blnHorarioErrado = true;
            foreach (string strLiberado in strCaracteresLiberados)
            {
                if (pHorario.Contains(strLiberado))
                {
                    blnHorarioErrado = false;
                }
                else
                {
                    blnHorarioErrado = true;
                }
            }
            return blnHorarioErrado;
        }

        /// <summary>
        /// Função para verificar a disposição dos caracteres na hora.
        /// </summary>
        /// <param name="pHorario">Horario que o usuário digitou</param>
        /// <returns>Retorna um Booelan</returns>
        private bool VerifyTime(string pHorario)
        {
            // verifica hora
            if (int.Parse(pHorario.Substring(0, 2)) > 23)
            {
                return true;
            }

            // verifica minutos
            if (int.Parse(pHorario.Substring(3, 2)) > 59)
            {
                return true;
            }

            // verifica o tamanho do horario
            if (pHorario.Length > 5)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Função para verificar se o horario foi inserido errado,
        /// se foi, o usuário precisará inserir novamente,
        /// senao, só adiciona no objeto.
        /// </summary>
        /// <param name="pBlnHorarioErrado">Boolean contendo o resultado das verificações</param>
        /// <param name="pHorario">Horario que o usuário digitou</param>
        private void VerifyIfTimeIsOk(bool pBlnHorarioErrado, string pHorario)
        {
            if (pBlnHorarioErrado)
            {
                pHorario = ShowInputBox("Insira o horario", strMsgDeExemplo);
                VerifyWrongTime(pHorario);
            }
            else
            {
                objPonto.LastHorarioRegistrado = pHorario;
            }
        }

        /// <summary>
        /// Verifica se todos os dados estão corretos para salvar.
        /// </summary>
        public void VerifyDataToSave()
        {
            objPonto.Data = DateTime.Today;
            InputTimeIfDontHaveOne();
            VerificaSeExisteADataDeHoje();
            VerificarHorasDuplicados();
            SaveDataToJson();
        }

        /// <summary>
        /// Função para verificar se existem horarios cadastrados ao salvar as horas.
        /// </summary>
        private void InputTimeIfDontHaveOne()
        {
            if (objPonto.Horarios.Count == 0)
            {
                string strHorario = ShowInputBox("Insira o horario", "Insira ao menos um horario no formato (Ex: 12:45)");
                VerifyWrongTime(strHorario);
            }

            // callback
            if (objPonto.Horarios.Count == 0)
            {
                InputTimeIfDontHaveOne();
            }
        }

        /// <summary>
        /// Lê os dados do lstPonto e antes de salvar verifica se o dado já existe,
        /// se existir já salva por cima do que existe.
        /// </summary>
        public void VerificaSeExisteADataDeHoje()
        {
            int intDateIndex = FindPontoObjIndex();
            if (intDateIndex > -1)
            {
                lstPonto[intDateIndex] = objPonto;
            }
            else
            {
                lstPonto.Add(objPonto);
            }
        }

        /// <summary>
        /// Salva os dados do objeto objPonto na lista lstPonto em um arquivo json.
        /// </summary>
        private void SaveDataToJson()
        {
            string strJsonString;
            strJsonString = JsonConvert.SerializeObject(lstPonto, Formatting.Indented);
            File.WriteAllText("jsonFileTest.json", strJsonString);
        }

        /// <summary>
        /// Soma as horas extras do dia.
        /// </summary>
        public void SomarHorasAlmocoEextrasPorDia()
        {
            if (lstPonto.Count < 2)
            {
                ShowInfoMessageBox("Existe apenas uma hora registrada ainda hoje, registre mais para ","");
                return;
            }

            int intRegistros = objPonto.Horarios.Count;
            if (!VerificaPar(intRegistros))
            {
                intRegistros--;
            }
            
            // Soma todos os minutos extras
            int intSomaMinutosExtras = 0;
            for (int intI = 0; intI < intRegistros; intI += 2)
            {
                intSomaMinutosExtras += SubtrairHoras(objPonto.Horarios[intI].ToString(), objPonto.Horarios[intI + 1].ToString());
            }

            objPonto.MinutosExtras = intSomaMinutosExtras -= 528;

            // Trata os 10 minutos de sobra
            if (intSomaMinutosExtras >= -10 && intSomaMinutosExtras <= 10)
            {
                objPonto.MinutosExtras = intSomaMinutosExtras = 0;
            }

            // Pega as hora horas extras
            objPonto.HorasExtras = (intSomaMinutosExtras / 60);

            // Pega as horas do almoço
            objPonto.MinutosAlmoco = EncontraAlmoco();
        }

        /// <summary>
        /// Verifica se o número é par.
        /// </summary>
        /// <param name="pNumero">Número inteiro</param>
        /// <returns>Retorna true seo número for par</returns>
        public bool VerificaPar(int pNumero)
        {
            if (pNumero % 2 == 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Função que subtrai uma hora inicial em uma final.
        /// </summary>
        /// <param name="pHoraInicio">Hora que será subtraida na final</param>
        /// <param name="pHoraFim">Hora que subtrai a inicial</param>
        /// <returns>Retorna a diferença entre as horas em minutos</returns>
        private int SubtrairHoras(string pHoraInicio, string pHoraFim)
        {
            DateTime dtaHora1 = DateTime.Parse(pHoraInicio);
            DateTime dtaHora2 = DateTime.Parse(pHoraFim);

            int intMinutos = int.Parse((dtaHora2.Subtract(dtaHora1).TotalMinutes).ToString());
            if (-528 < intMinutos)
            {
                intMinutos = int.Parse((dtaHora1.Subtract(dtaHora2).TotalMinutes).ToString());
            }

            return intMinutos;
        }

        /// <summary>
        /// Encontra o mais próxim ou oúltimo registro após as 12:00
        /// E o primeiro registro após as 13:00.
        /// </summary>
        /// <returns>Retorna os minutos de almoço do usuário</returns>
        private int EncontraAlmoco()
        {
            // pega a primeira hora após o almoço
            // int intIndexFimHoraAlmoco = objPonto.Horarios.FindIndex(e => e.Contains("13:"));
            int intIndexFimHoraAlmoco = objPonto.Horarios.FindLastIndex(e => e.Contains("12:"));
            return SubtrairHoras(objPonto.Horarios[intIndexFimHoraAlmoco], objPonto.Horarios[intIndexFimHoraAlmoco]);
            
        }

        /// <summary>
        /// Essa função exibe ao usuário em uma caixa de mensagem,
        /// quantas horas e minutos extras fez
        /// e quantos minutos de almoço teve.
        /// </summary>
        public void ExibirRelatorioDoDia()
        {
            StringBuilder stbConteudoMsg = new StringBuilder();
            stbConteudoMsg.Append("Você possui: " + objPonto.HorasExtras.ToString() + " Horas extras");
            if (objPonto.MinutosExtras > 0)
            {
                stbConteudoMsg.Append("\nVocê possui: " + objPonto.MinutosExtras.ToString() + " Minutos extras");
            }
            stbConteudoMsg.Append("\nVocê fez: " + objPonto.MinutosAlmoco.ToString() + " Minutos de almoço");
            if (objPonto.MinutosAlmoco < 60)
            {
                stbConteudoMsg.Append("\nO almoço deve ser de no minimo 60 minutos.");
                stbConteudoMsg.Append("\n12:00 até 13:00 = 60 minutos");
                stbConteudoMsg.Append("\nO almoço deve ser de no maximo 90 minutos.");
                stbConteudoMsg.Append("\n12:00 até 13:30 = 90 minutos");
            }
            ShowInfoMessageBox(stbConteudoMsg.ToString(), "Horas Extras");
        }

        /// <summary>
        /// Verifica se na lista lstPonto existem dados que duplicaram,
        /// ou seja, que estava no campo LastHorarioRegistrado
        /// e foi ao primeiro indice da lista
        /// </summary>
        private void VerificarHorasDuplicados()
        {
            foreach (Ponto objPonto in lstPonto)
            {
                int intIndexHoraDuplicada = objPonto.Horarios.FindIndex(h => h.Equals(objPonto.LastHorarioRegistrado));
                if (intIndexHoraDuplicada > -1)
                {
                    objPonto.Horarios.RemoveAt(intIndexHoraDuplicada);
                }
            }
        }
    }
}

//[
//  {
//    "Data": "2020-04-29T00:00:00-03:00",
//    "LastHorarioRegistrado": "18:04",
//    "Horarios": [
//      "07:42",
//      "12:06",
//      "13:31",
//      "18:04"
//    ]
//  },
//  {
//    "Data": "2020-04-30T00:00:00-03:00",
//    "LastHorarioRegistrado": "18:08",
//    "Horarios": [
//      "07:38",
//      "08:37",
//      "11:04",
//      "12:03",
//      "13:10",
//      "18:08"
//    ]
//  },
//  {
//    "Data": "2020-05-04T00:00:00-03:00",
//    "LastHorarioRegistrado": "13:30",
//    "Horarios": [
//      "07:40",
//      "12:15",
//      "13:30"
//    ]
//  },
//{
//  "Data": "2020-05-05T00:00:00-03:00",
//  "LastHorarioRegistrado": "17:40",
//  "Horarios": [
//    "08:00",
//    "12:00",
//    "13:01",
//    "17:40"
//  ]
//}
//]