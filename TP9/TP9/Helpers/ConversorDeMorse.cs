using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Helpers
{
    class ConversorDeMorse
    {
        public static string[] morse = { " ", ".-", "-...", "-.-.", "-..", ".", "..-.", "--.", "....", "..", ".---", "-.-", ".-..", "--", "-.", "---", ".--.", "--.-", ".-.", "...", "-", "..-", "...-", ".--", "-..-", "-.--", "--.." };
        public static char[] letras = { ' ', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

        //public static char[] letras = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

        public static string[] TextoAMorse(string toConvert)
        {
            string[] traducido = new string[toConvert.Length];

            for (int i = 0; i < toConvert.Length; i++)
            {
                for (int j = 0; j < letras.Length; j++)
                {
                    if (toConvert[i] == letras[j])
                    {
                        traducido[i] = morse[j];
                    }
                }
            }
            return traducido;
        }

        public static string MorseATexto(string[] toConvert)
        {
            string traducido = "";
            for (int i = 0; i < toConvert.Length; i++)
            {
                for (int j = 0; j < morse.Length; j++)
                {
                    if (toConvert[i] == morse[j])
                    {
                        traducido = traducido + letras[j];
                    }
                }
            }
            return traducido;
        }

        public static void CrearMorseTxt(string[] morse, string destDirectory)
        {
            string morseDirectory = destDirectory + @"\Morse";
            if (!Directory.Exists(morseDirectory))
            {
                Directory.CreateDirectory(morseDirectory);
            }

            string morseFileName = morseDirectory+@"\morse_" + DateTime.Now.ToString("dd_MM_yyyy_h_m_s") + ".txt";

            FileStream morseFile = File.Create(morseFileName);
            using (StreamWriter morseWriter = new StreamWriter(morseFile))
            {
                foreach (string m in morse)
                {
                    morseWriter.WriteLine(m);
                }
                morseWriter.Close();
            }
        }

        public static void CrearTextoTxt(string destDirectory)
        {
            string textDirectory = destDirectory + @"\Morse";
            if (!Directory.Exists(textDirectory))
            {
                Directory.CreateDirectory(textDirectory);
            }
            string textFileName = textDirectory + @"\texto_" + DateTime.Now.ToString("dd_MM_yyyy_h_m_s") + ".txt";
            string morseFileName = textDirectory + @"\morse_" + DateTime.Now.ToString("dd_MM_yyyy_h_m_s") + ".txt";


            FileStream toConvert = new FileStream(morseFileName, FileMode.Open);
            string line;
            string morseContent = "";
            using (StreamReader morseReader = new StreamReader(toConvert))
            {
                while ((line=morseReader.ReadLine()) != null)
                {
                    morseContent = morseContent+";"+line;
                }
            }
            string[] morseArray = morseContent.Split(';');
            string MorseToText = ConversorDeMorse.MorseATexto(morseArray);

            FileStream textFile = File.Create(textFileName);
            using (StreamWriter textWriter = new StreamWriter(textFile))
            {
                textWriter.WriteLine(MorseToText);
                textWriter.Close();
            }
        }

        //public static void MorseToMp3(string[] toConvert, string destDirectory)
        //{
        //    string directory = destDirectory + @"\Morse";
        //    if (!Directory.Exists(directory))
        //    {
        //        Directory.CreateDirectory(directory);
        //    }
        //    string mp3FileName = directory + @"\audio_" + DateTime.Now.ToString("dd_MM_yyyy_h_m_s") + ".mp3";

        //    //Cargo el punto en un byte
        //    byte puntoMp3;
        //    string puntoPath = destDirectory + @"\punto.mp3";
        //    FileStream punto = new FileStream(puntoPath, FileMode.Open);
        //    using (StreamReader puntoReader = new StreamReader(punto))
        //    {
        //        puntoMp3 = Convert.ToByte( puntoReader.ReadLine() );
        //    }

        //    //Cargo la raya en un byte
        //    byte rayaMp3;
        //    string rayaPath = destDirectory + @"\raya.mp3";
        //    FileStream raya = new FileStream(rayaPath, FileMode.Open);
        //    using (StreamReader rayaReader = new StreamReader(raya))
        //    {
        //        rayaMp3 = Convert.ToByte(rayaReader.ReadLine());
        //    }

        //    //Convierto el arreglo  de entrada (donde cada elemento contiene una letra en morse) a un string concatenado
        //    string stringToMp3="";
        //    int i = 0;
        //    while (i<= toConvert.Length)
        //    {
        //        stringToMp3 = stringToMp3 + toConvert[i];
        //        i++;
        //    }

            //Guardo el string en el mp3
            //FileStream mp3File = File.Create(mp3FileName);
            //using (StreamWriter textWriter = new StreamWriter(mp3File))
            //{
            //    textWriter.WriteLine(MorseToText);
            //    textWriter.Close();
            //}
            //int j = 0;
            //while (j <= stringToMp3.Length)
            //{

            //}
        //}
    }
}
