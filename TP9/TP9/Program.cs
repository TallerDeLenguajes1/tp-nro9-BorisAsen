using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers;

namespace TP9
{
    class Program
    {
        static void Main(string[] args)
        {
            SoporteParaConfiguracion.CrearArchivoDeConfiguracion();
            string DataDirectory = SoporteParaConfiguracion.LeerConfiguracion();

            Console.WriteLine("Segun el archivo de configuracion se creo un directorio llamado "+ DataDirectory +", en el cual se guardaran todos los archivos\n");

            Console.Write("Ingrese una frase para traducirla a morse: ");
            string aTraducir = Console.ReadLine();


            //*********************** TEXTO A MORSE ***********************//
            string[] TextToMorse = ConversorDeMorse.TextoAMorse(aTraducir.ToUpper());
            ConversorDeMorse.CrearMorseTxt(TextToMorse,DataDirectory);
            //ConversorDeMorse.MorseToMp3(TextToMorse, DataDirectory);

            //*********************** MORSE A TEXTO ***********************//
            ConversorDeMorse.CrearTextoTxt(DataDirectory);
            

            Console.WriteLine("\nLas corrspondientes traducciones se guardaron dentro del directorio "+DataDirectory);
            Console.ReadKey();
        }
    }
}
