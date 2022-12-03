using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POOFinaldeUriel.Entidades
{
    public class Persona
    {
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string Apellido { get; set; }
        public long Documento { get; set; }
        public DateTime Edad { get; set; }

        public Sexo sexo { get; set; }


        public string MostrarNombre()
        {
           return $"{Apellido.ToUpper()},{PrimerNombre}/{SegundoNombre}.";
        }

        public int GetAntiguedad()
        {
            int edad = DateTime.Today.Year - Edad.Year;
            if (Edad.AddYears(edad) > DateTime.Today)
            {
                edad--;
            }
            return edad;

        }

    }
}
