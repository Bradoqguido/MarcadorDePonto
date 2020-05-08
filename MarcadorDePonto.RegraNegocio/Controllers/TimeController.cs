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
        // private string strMsgDeExemplo = "Insira novamente a horario como o exemplo (Ex: 07:42)";

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
        private readonly static int[] intMinutosEntreHorasDeTrabalho = { 258, 270 };

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
        /// <returns>Retorna um Booelan true caso passe pelos testes, false caso não passe </returns>
        private bool VerificarFormatoDoHorario(string pHorario)
        {
            // verifica hora
            int intHora = 0;
            if (int.TryParse(pHorario.Substring(0, 2), out intHora))
            {
                return false;
            }

            if (intHora > 23 || intHora < 0)
            {
                return false;
            }

            // verifica minutos
            int intMinuto = 0;
            if (!int.TryParse(pHorario.Substring(3, 2), out intMinuto))
            {
                return false;
            }

            if (intMinuto > 59 || intMinuto < 0)
            {
                return false;
            }

            // verifica o tamanho do horario
            if (pHorario.Length > 5)
            {
                return false;
            }

            if (pHorario.Contains("-0") || pHorario.Contains("+0"))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Valida se o dado inserido é menor que o dado inserido anteriormente, se não for,
        /// será registrado no Json.
        /// </summary>
        /// <param name="pHorario">Variável de tipo string.</param>
        /// <returns>Retorna True para caso passe nas validações e false caso contrário.</returns>
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
                    return false;
                } else
                {
                    objRegistroPonto.Horarios.Add(pHorario);
                }
            }

            return true;
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
        /// <param name="pPonto">Recebe o objeto ponto.</param>
        public void SomarHorasDoDia(Ponto pPonto)
        {
            int intRegistros = pPonto.Horarios.Count;

            // Se for impar é retirado um registro
            if (!(intRegistros % 2 == 0))
            {
                intRegistros--;
            }

            // Soma todos os minutos extras
            int intSomaMinutosExtras = 0;
            for (int intI = 0; intI < intRegistros; intI += 2)
            {
                intSomaMinutosExtras += SubtrairHoras(pPonto.Horarios[intI].ToString(), pPonto.Horarios[intI + 1].ToString());
            }

            AjusteFinoDeHorario(intSomaMinutosExtras, pPonto);
        }

        /// <summary>
        /// Ajusta as horas, subtraindo as horas restantes da variável
        /// </summary>
        /// <param name="pIntSomaMinutosExtras">Variável do tipo inteiro</param>
        /// <param name="pPonto">Recebe o objeto ponto.</param>
        private void AjusteFinoDeHorario(int pIntSomaMinutosExtras, Ponto pPonto)
        {
            // Remove as horas de trabalho normais (528 minutos)
            if (pIntSomaMinutosExtras > 0)
            {
                pPonto.MinutosExtras = pIntSomaMinutosExtras -= 528;
            }

            // Trata os 10 minutos de sobra
            if (pIntSomaMinutosExtras >= -10 && pIntSomaMinutosExtras <= 10)
            {
                pIntSomaMinutosExtras = 0;
                pPonto.MinutosExtras = 0;
            }

            // Maximo de hora extra = 71 minutos
            if (pIntSomaMinutosExtras > 71)
            {
                pPonto.Excedeu71min = true;
            } else
            {
                pPonto.Excedeu71min = false;
            }

            // Pega as hora horas extras
            pPonto.HorasExtras = (pIntSomaMinutosExtras / 60);

            if (pPonto.HorasExtras >= 6)
            {
                pPonto.Fez6hDireto = true;
            } else
            {
                pPonto.Fez6hDireto = false;
            }

            // Pega os minutos extras
            pPonto.MinutosExtras = Math.Abs((pIntSomaMinutosExtras % 60));
        }

        /// <summary>
        /// Recebe as horas de inicio e fim do almoço do usuário,
        /// para saber quanto de almoço ele fez.
        /// </summary>
        /// <param name="pStrInicio">Início do almoço.</param>
        /// <param name="pStrFim">Fim do almoço.</param>
        /// <returns>Retorna um booleano, True para sucesso e False caso contrário.</returns>
        public bool DefinirHorarioAlmoco(string pStrInicio, string pStrFim)
        {
            int intMinutosDeAlmoco = SubtrairHoras(pStrInicio, pStrFim);
            if (intMinutosDeAlmoco > 0)
            {
                objRegistroPonto.MinutosAlmoco = intMinutosDeAlmoco;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Zera os registros do dia.
        /// </summary>
        /// <returns>Retorna True se eliminar os registros e false caso não tenha registros.</returns>
        public bool ZerarRegistrosDia()
        {
            int intHorariosCount = objRegistroPonto.Horarios.Count;
            if (intHorariosCount > 0)
            {
                objRegistroPonto.Horarios.RemoveRange(0, intHorariosCount);
                objRegistroPonto.MinutosAlmoco = 0;
                objRegistroPonto.Excedeu71min = false;
                objRegistroPonto.Fez6hDireto = false;
                objRegistroPonto.HorasExtras = 0;
                objRegistroPonto.MinutosExtras = 0;
                return true;
            } else
            {
                return false;
            }
        }

        /// <summary>
        /// Função para descobrir que horas o usuário pode sair.
        /// </summary>
        /// <param name="pPonto">Recebe o registro de ponto que o usuário está usando.</param>
        /// <returns>Retorna uma string contendo a hora:minutos que o usuário poderia sair.</returns>
        public string HoraDeSaida(Ponto pPonto)
        {
            if (pPonto.HorasExtras == 0 && pPonto.MinutosExtras == 0)
            {
                return "";
            }

            DateTime dtaUltimoPonto = DateTime.Parse(pPonto.Horarios[pPonto.Horarios.Count - 1]);
            DateTime dtaHorasFaltantes = DateTime.Parse(Math.Abs(pPonto.HorasExtras) + ":" + pPonto.MinutosExtras);

            DateTime dtaHoraDeSaida = dtaUltimoPonto.AddHours(dtaHorasFaltantes.Hour);
            dtaHoraDeSaida.AddMinutes(dtaHorasFaltantes.Minute);

            if (dtaHoraDeSaida.Hour < 17 || dtaHoraDeSaida.Minute < 40)
            {
                return "";
            }

            return "\n\nVocê poderá sair as: " + dtaHoraDeSaida.ToShortTimeString() + "h";
        }
    }
}
