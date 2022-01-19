using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RecuperatorioMVCCore.DAL;
using System.ComponentModel.DataAnnotations;

namespace RecuperatorioMVCCore.Models
{
    public class IndexVM
    {
        public int? Id { get; set; }

        public int OperadorA { get; set; }
        public int OperadorB { get; set; }
        public int Resultado { get; set; }

        /// <summary>
        /// Puede tomar los valores "*" o "+"
        /// </summary>
        public string OperacionMat { get; set; }

        public List<Operacion> Operaciones = new List<Operacion>();
    }
}
