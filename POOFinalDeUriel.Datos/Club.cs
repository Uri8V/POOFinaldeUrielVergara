using POOFinaldeUriel.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POOFinalDeUriel.Datos
{
    public class Club
    {
        private bool hayCambios = false;
        private List<Persona> listaPersona;
        public Club()
        {
            listaPersona = new List<Persona>();
            listaPersona = ManejadorDeArchivoSecuancial.LeerenArchivoSecuencial();
        }


        public void Agregar(Persona persona)
        {
            hayCambios = true;
            listaPersona.Add(persona);
        }

        public int GetCantidad()
        {
            return listaPersona.Count;
        }

        public List<Persona> Getlista()
        {
            return listaPersona;
        }

        public void Borrar(Persona persona)
        {
            if (listaPersona.Contains(persona))
            {
                hayCambios = true;
                listaPersona.Remove(persona);
            }

        }

        public void Editar()
        {
            hayCambios = true;
        }

        public void Guardar()
        {
            if (hayCambios)
            {
                ManejadorDeArchivoSecuancial.Guardarenarchivo(listaPersona);
            }
        }

        public List<Persona> ListaPorEdad(Orden ordenar)
        {
            return ordenar == Orden.Ascendente ? listaPersona.OrderBy(l => l.Edad).ToList() : listaPersona.OrderByDescending(l => l.Edad).ToList();
        }

        public List<Persona> ListaLetra(Orden letra)
        {
            return letra == Orden.Ascendente ? listaPersona.OrderBy(l => l.MostrarNombre()).ToList() : listaPersona.OrderByDescending(l => l.MostrarNombre()).ToList();
        }

        public string Categoria(Persona persona)
        {
            if (persona.GetAntiguedad() >= 7 && persona.GetAntiguedad()<=12)
            {
                return "Infantil";
            }
            else if( persona.GetAntiguedad()>=13 && persona.GetAntiguedad() <= 17)
            {
                return "Juvenil";
            }
            else if (persona.GetAntiguedad() >= 18 && persona.GetAntiguedad() <= 60)
            {
                return "Mayor";
            }
            else
            {
                return "Vitalicio";
            }

        }

        public string ValorDeCuota(Persona persona)
        {
            if (persona.GetAntiguedad() >= 7 && persona.GetAntiguedad() <= 12)
            {
                return "$1000";
            }
            else if (persona.GetAntiguedad() >= 13 && persona.GetAntiguedad() <= 17)
            {
                return "$1500";
            }
            else if (persona.GetAntiguedad() >= 18 && persona.GetAntiguedad() <= 60)
            {
                return "$2000";
            }
            else
            {
                return "$500";
            }

        }
    }
}
