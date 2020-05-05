using System;
using System.Collections.Generic;
using WindowsFormsApp1.Models;

namespace MarcadorDePonto.Controllers
{
    class TimeController
    {
        /// <summary>
        /// Mensagem pré-fixada pedindo que o usuário digite novamente.
        /// </summary>
        private string strMsgDeExemplo = "Insira novamente a horario como o exemplo (Ex: 07:42)";

        /// <summary>
        /// Lista do tipo ponto.
        /// </summary>
        private List<Ponto> lstRegistrosPonto = new List<Ponto>();

        /// <summary>
        /// Objeto do tipo Ponto.
        /// </summary>
        private Ponto objRegistroPonto = new Ponto();
        
        /// <summary>
        /// Lista string de caracteres liberados, ou seja:
        /// 07:42 - 12:00 = 258 minutos,
        /// 12:00 - 13:00 = 60 minutos,
        /// 12:00 - 13:30 = 90 minutos,
        /// 13:30 - 18:00 = 270 minutos.
        /// </summary>
        private static int[] intMinutosEntreHorasDeTrabalho = { 258, 270 };
        
        /// <summary>
        /// Lista string de caracteres liberados.
        /// </summary>
        private static string[] strCaracteresLiberados = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", ":" };

        public TimeController(List<Ponto> pLstRegistrosPonto)
        {
            lstRegistrosPonto = pLstRegistrosPonto;
            FindRegistroPontoDeHoje();
            VerificaSeExisteADataDeHoje();
        }

        /// <summary>
        /// Captura o objeto ponto com a manipulação mais recente.
        /// </summary>
        /// <returns>Retorna o Objeto ponto atual</returns>
        public Ponto GetRegistroDePontoAtual()
        {
            return objRegistroPonto;
        }

        /// <summary>
        /// Encontra o registro de ponto de hoje e o atribui ao objPonto.
        /// </summary>
        private void FindRegistroPontoDeHoje()
        {
            objRegistroPonto.Data = DateTime.Today;
            int intDateIndex = FindIndexRegistroPonto();
            if (intDateIndex > -1)
            {
                objRegistroPonto = lstRegistrosPonto[intDateIndex];
            }
        }

        /// <summary>
        /// Lê os dados do lstPonto e antes de salvar verifica se o dado já existe,
        /// se existir já salva por cima do que existe.
        /// </summary>
        private void VerificaSeExisteADataDeHoje()
        {
            int intDateIndex = FindIndexRegistroPonto();
            if (intDateIndex > -1)
            {
                lstRegistrosPonto[intDateIndex] = objRegistroPonto;
            }
            else
            {
                lstRegistrosPonto.Add(objRegistroPonto);
            }
        }

        /// <summary>
        /// Busca na lista de objetos lstPonto um que seja do dia de hoje,
        /// se encontrar, retorna o index do mesmo.
        /// </summary>
        /// <returns>Retorna um index inteiro</returns>
        private int FindIndexRegistroPonto()
        {
            for (int intI = 0; intI < lstRegistrosPonto.Count; intI++)
            {
                if (objRegistroPonto.Data == lstRegistrosPonto[intI].Data)
                {
                    return intI;
                }
            }
            return -1;
        }

        /// <summary>
        /// Função para verificar caracteres diferentes de numeros ou de : ,
        /// se tiver qualquer caracter diferente dos especificados, a função se torna recursiva,
        /// se não, o horario é salvo.
        /// </summary>
        /// <param name="pHorario">Objeto do tipo string</param>
        /// <returns>Retorna um booleano False para Ok e True para algum Erro</returns>
        public bool ValidarNovoRegistroDeHorario(string pHorario)
        {
            bool blnHorarioErrado = true;
            if (!VerificarCaracteresAceitos(pHorario))
            {
                return VerificarFormatoDoHorario(pHorario);
            }
            return blnHorarioErrado;
        }

        /// <summary>
        /// função para verificar se os caracteres digitados estão na lista de aceitos.
        /// </summary>
        /// <param name="pHorario">Horario que o usuário digitou</param>
        /// <returns>Retorna um Boolean</returns>
        private bool VerificarCaracteresAceitos(string pHorario)
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
        private bool VerificarFormatoDoHorario(string pHorario)
        {
            // verifica hora
            int intHora = 0;
            if (!int.TryParse(pHorario.Substring(0, 2), out intHora))
            {
                return true;
            }

            if (intHora > 23 || intHora < 0)
            {
                return true;
            }

            // verifica minutos
            int intMinuto = 0;
            if (!int.TryParse(pHorario.Substring(3, 2), out intMinuto))
            {
                return true;
            }

            if (intMinuto > 59 || intMinuto < 0)
            {
                return true;
            }

            // verifica o tamanho do horario
            if (pHorario.Length > 5)
            {
                return true;
            }

            if (pHorario.Contains("-0") || pHorario.Contains("+0"))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Valida se o dado inserido é menor que o dado inserido anteriormente, se não for,
        /// será registrado no Json.
        /// </summary>
        /// <param name="pHorario">Variável de tipo string.</param>
        /// <returns>retorna um booleano, se for True é menor, se não é maior e será registrado.</returns>
        public bool InserirRegistro(string pHorario)
        {
            int intRegistrosCount = objRegistroPonto.Horarios.Count;

            if (intRegistrosCount == 0)
            {
                objRegistroPonto.Horarios.Add(pHorario);
            } else
            {
                string strUltimaHora = objRegistroPonto.Horarios[intRegistrosCount - 1];
                
                int intUltimoHorario = 0;
                int.TryParse(strUltimaHora.Remove(2, 1), out intUltimoHorario);

                int intHorarioAtual = 0;
                int.TryParse(pHorario.Remove(2, 1), out intHorarioAtual);

                if (intUltimoHorario > intHorarioAtual)
                {
                    return true;
                } else
                {
                    objRegistroPonto.Horarios.Add(pHorario);
                }
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
            return int.Parse((dtaHora2.Subtract(dtaHora1).TotalMinutes).ToString());
        }

        /// <summary>
        /// Soma as horas extras do dia.
        /// </summary>
        public void SomarHorasDoDia()
        {
            int intRegistros = objRegistroPonto.Horarios.Count;

            // Se for impar é retirado um registro
            if (!(intRegistros % 2 == 0))
            {
                intRegistros--;
            }

            // Soma todos os minutos extras
            int intSomaMinutosExtras = 0;
            for (int intI = 0; intI < intRegistros; intI += 2)
            {
                intSomaMinutosExtras += SubtrairHoras(objRegistroPonto.Horarios[intI].ToString(), objRegistroPonto.Horarios[intI + 1].ToString());
            }

            AjusteFinoDeHorario(intSomaMinutosExtras);

            // Pega as horas do almoço
            objRegistroPonto.MinutosAlmoco = EncontraHorarioAlmoco();
        }

        /// <summary>
        /// Ajusta as horas, subtraindo as horas restantes da variável
        /// </summary>
        /// <param name="pIntSomaMinutosExtras">Variável do tipo inteiro</param>
        private void AjusteFinoDeHorario(int pIntSomaMinutosExtras)
        {
            // Remove as horas de trabalho normais (528 minutos)
            if (pIntSomaMinutosExtras > 0)
            {
                objRegistroPonto.MinutosExtras = pIntSomaMinutosExtras -= 528;
            }

            // Trata os 10 minutos de sobra
            if (pIntSomaMinutosExtras >= -10 && pIntSomaMinutosExtras <= 10)
            {
                pIntSomaMinutosExtras = 0;
                objRegistroPonto.MinutosExtras = 0;
            }

            // Pega as hora horas extras
            objRegistroPonto.HorasExtras = (pIntSomaMinutosExtras / 60);

            // 112 minutos são divisiveis por 60, mas não cabem em uma variável do tipo inteiro
            // Caso positivo
            if (pIntSomaMinutosExtras > 0 && pIntSomaMinutosExtras < 120)
            {
                pIntSomaMinutosExtras = pIntSomaMinutosExtras - 60; // Remove uma hora dos minutos
                objRegistroPonto.MinutosExtras = pIntSomaMinutosExtras;
                objRegistroPonto.HorasExtras += 1; // Adiciona uma hora
            }

            // Caso negativo
            if (pIntSomaMinutosExtras > -120 && pIntSomaMinutosExtras < 0)
            {
                pIntSomaMinutosExtras = pIntSomaMinutosExtras + 60; // Remove uma hora dos minutos
                objRegistroPonto.MinutosExtras = (pIntSomaMinutosExtras * -1);
                objRegistroPonto.HorasExtras -= 1; // Remove uma hora
            }
        }

        /// <summary>
        /// Encontra o mais próxim ou oúltimo registro após as 12:00
        /// E o primeiro registro após as 13:00.
        /// </summary>
        /// <returns>Retorna os minutos de almoço do usuário</returns>
        private int EncontraHorarioAlmoco()
        {
            // pega a primeira hora após o almoço
            int intIndexFimHoraAlmoco = objRegistroPonto.Horarios.FindIndex(e => e.Contains("13:"));
            if (intIndexFimHoraAlmoco > -1)
            {
                return SubtrairHoras(objRegistroPonto.Horarios[intIndexFimHoraAlmoco - 1], objRegistroPonto.Horarios[intIndexFimHoraAlmoco]);
            }
            return 0;
        }

        /// <summary>
        /// Zera os registros do dia.
        /// </summary>
        public void ZerarRegistrosDia()
        {
            int intHorariosCount = objRegistroPonto.Horarios.Count;
            objRegistroPonto.Horarios.RemoveRange(0, intHorariosCount);
        }
    }
}
