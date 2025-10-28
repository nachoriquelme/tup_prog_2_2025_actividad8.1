using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio1.Models
{
    public class Cuenta : IComparable
    {
        public int DNI { get; set; }
        public string Nombre { get; set; }
        public double Importe { get; set; }
        public Cuenta(int dni, string nombre, double importe) 
        {
            DNI = dni;
            Nombre = nombre;
            Importe = importe;
        }

        public int CompareTo (object obj)
        {
            Cuenta otro = obj as Cuenta;
            if (otro  != null)
            {
                return this.DNI.CompareTo(otro.DNI);
            }
            return -1;
        }

        public override string ToString()
        {
            return $"{Nombre} - {DNI} - {Importe}";
        }
    }
}
