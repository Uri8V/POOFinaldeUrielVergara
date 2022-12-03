using POOFinaldeUriel.Entidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POOFinalDeUriel.Datos
{
    public class ManejadorDeArchivoSecuancial
    {
        private static string archivo = "Personas.txt";

        public static void Guardarenarchivo(List<Persona> listaPersonas)
        {
            using (var escritor = new StreamWriter(archivo))
            {
                foreach (var persona in listaPersonas)
                {
                    string linea = ConstruirLinea(persona);
                    escritor.WriteLine(linea);
                }
            }
        }

        private static string ConstruirLinea(Persona persona)
        {
            return $"{persona.PrimerNombre}| {persona.SegundoNombre}|{persona.Apellido}|{persona.Documento}|{persona.Edad}|{(int)persona.sexo}";
        }

        public static List<Persona> LeerenArchivoSecuencial()
        {
            List<Persona> lista = new List<Persona>();
            if (File.Exists(archivo))
            {
                using (var lector = new StreamReader(archivo))
                {
                    while (!lector.EndOfStream)
                    {
                        string linea = lector.ReadLine();
                        Persona persona = ConstruirPesona(linea);
                        lista.Add(persona);
                    }
                }
            }
            return lista;
        }

        private static Persona ConstruirPesona(string linea)
        {
            var campos = linea.Split('|');
            Persona persona = new Persona()
            {
                PrimerNombre = campos[0],
                SegundoNombre = campos[1],
                Apellido = campos[2],
                Documento = long.Parse(campos[3]),
                Edad = DateTime.Parse(campos[4]),
                sexo = (Sexo)int.Parse(campos[5])
            };
            return persona;
        }
    }
}
