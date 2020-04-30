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
        /// Get/Set com o tipo data
        /// </summary>
        public DateTime Data
        {
            set { dtaData = value; }
            get { return dtaData; }
        }

        /// <summary>
        /// Insere horarios na lista
        /// e sempre retorna o ultimo item adicionado
        /// </summary>
        public string LastHorarioRegistrado
        {
            set { lstHorarios.Add(value); }
            get { return lstHorarios.Last(); }
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
