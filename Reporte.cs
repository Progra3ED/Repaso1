using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repaso1
{
    internal class Reporte
    {
        string nombre;
        string apellido;
        decimal sueldoMes;
        int mes;

        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellido { get => apellido; set => apellido = value; }
        public decimal SueldoMes { get => sueldoMes; set => sueldoMes = value; }
        public int Mes { get => mes; set => mes = value; }
    }
}
