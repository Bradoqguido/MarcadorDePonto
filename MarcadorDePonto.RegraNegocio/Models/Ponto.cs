using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{
    public class Ponto
    {

        /// <summary>
        /// Salva o dia atual
        /// </summary>
        private DateTime dtaData = new DateTime();

        /// <summary>
        /// Variável para armazenar os horarios
        /// </summary>
        private List<string> lstHorarios = new List<string>();

        /// <summary>
        /// Variavel para armazenar os minutos de almoço.
        /// </summary>
        private double dobMinutosAlmoco = 0;

        /// <summary>
        /// Variavel para armazenar as horas extra.
        /// </summary>
        private double dobHorasExtras = 0;

        /// <summary>
        /// Variavel para armazenar os minutos de horas extra.
        /// </summary>
        private double dobMinutosExtras = 0;
        
        /// <summary>
        /// Variavel para armazenar se o usuário fez ou não
        /// mais do que 71 minutos de hora extra.
        /// </summary>
        private bool blnHoraExtraExcedeu71min = false;

        /// <summary>
        /// Variavel para armazenar se o usuário fez ou não
        /// mais do 6 horas sem descanço.
        /// </summary>
        private bool blnFez6horasDireto = false;

        /// <summary>
        /// Get/Set com o tipo data
        /// </summary>
        public DateTime Data
        {
            set { dtaData = value; }
            get { return dtaData; }
        }

        /// <summary>
        /// Recebe quanto tempo de almoço
        /// por dia trabalhado que o usuário fez
        /// </summary>
        public double HorasExtras
        {
            set { dobHorasExtras = value; }
            get { return dobHorasExtras; }
        }

        /// <summary>
        /// Recebe quanto tempo de almoço
        /// por dia trabalhado que o usuário fez
        /// </summary>
        public double MinutosExtras
        {
            set { dobMinutosExtras = value; }
            get { return dobMinutosExtras; }
        }

        /// <summary>
        /// Recebe quanto tempo de almoço
        /// por dia trabalhado que o usuário fez
        /// </summary>
        public double MinutosAlmoco
        {
            set { dobMinutosAlmoco = value; }
            get { return dobMinutosAlmoco; }
        }

        /// <summary>
        /// Armazena se o usuário fez mais do que 71 minutos
        /// ou 1:11h de hora extra
        /// </summary>
        public bool Excedeu71min
        {
            set { blnHoraExtraExcedeu71min = value; }
            get { return blnHoraExtraExcedeu71min; }
        }

        /// <summary>
        /// Armazena se o usuário fez mais do que 71 minutos
        /// ou 1:11h de hora extra
        /// </summary>
        public bool Fez6hDireto
        {
            set { blnFez6horasDireto = value; }
            get { return blnFez6horasDireto; }
        }

        /// <summary>
        /// Recupera os horarios
        /// </summary>
        public List<string> Horarios
        {
            get { return lstHorarios; }
        }
    }
}
