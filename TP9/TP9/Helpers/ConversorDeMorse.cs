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
        public static string[] morse = { " ", ".- ", "-... ", "-.-. ", "-.. ", ". ", "..-. ", "--. ", ".... ", ".. ", ".--- ", "-.- ", ".-.. ", "-- ", "-. ", "--- ", ".--. ", "--.- ", ".-. ", "... ", "- ", "..- ", "...- ", ".-- ", "-..- ", "-.-- ", "--.. " };
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

        public static string CrearMorseTxt(string[] morse, string destDirectory)
        {
            string morseDirectory = destDirectory + @"\Morse";
            if (!Directory.Exists(morseDirectory))
            {
                Directory.CreateDirectory(morseDirectory);
            }

            string fecha = DateTime.Now.ToString("dd_MM_yyyy_h_m_s");
            string morseFileName = morseDirectory+@"\morse_" + fecha + ".txt";

            FileStream morseFile = File.Create(morseFileName);
            using (StreamWriter morseWriter = new StreamWriter(morseFile))
            {
                foreach (string m in morse)
                {
                    morseWriter.WriteLine(m);
                }
                morseWriter.Close();
            }

            return fecha;
        }

        public static void CrearTextoTxt(string destDirectory, string fecha)
        {
            string textDirectory = destDirectory + @"\Morse";
            if (!Directory.Exists(textDirectory))
            {
                Directory.CreateDirectory(textDirectory);
            }
            string textFileName = textDirectory + @"\texto_" + fecha + ".txt";
            string morseFileName = textDirectory + @"\morse_" + fecha + ".txt";

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
            string MorseToText = MorseATexto(morseArray);

            FileStream textFile = File.Create(textFileName);
            using (StreamWriter textWriter = new StreamWriter(textFile))
            {
                textWriter.WriteLine(MorseToText);
                textWriter.Close();
            }
        }

        public static byte[] FullBinaryReader(Stream stream)
        {
            byte[] buffer = new byte[32768];
            using (MemoryStream ms = new MemoryStream()) 
            {
                while (true)
                {
                    int read = stream.Read(buffer, 0, buffer.Length);
                    if (read <= 0)
                        return ms.ToArray();
                    ms.Write(buffer, 0, read);
                }
            }
        }

        public static void MorseToMp3(string[] toConvert, string destDirectory, string fecha)
        {
            //Establezco el directorio de destino y el nombre del mp3 de salida 
            string directory = destDirectory + @"\Morse";
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            //Obtengo las rutas de los archivos de entrada y salida
            string mp3FileName = directory + @"\audio_" + fecha + ".mp3";
            string puntoPath = destDirectory + @"\punto.mp3";
            string rayaPath = destDirectory + @"\raya.mp3";
            string silencioPath = destDirectory + @"\silencio.mp3";

            //Convierto el arreglo de string de entrada a string 
            string result = String.Concat(toConvert);

            //Convierto el string a un arreglo de caracteres para analizarlo caracter a caracter
            char[] charMorse = result.ToArray();

            //Abro los archivos de entrada para lectura
            Stream punto = File.OpenRead(puntoPath);
            Stream raya = File.OpenRead(rayaPath);
            Stream silencio = File.OpenRead(silencioPath);

            //Creo arrays de bytes para guardar el contenido de los archivos de audio
            byte[] puntoBuffer;
            byte[] rayaBuffer;
            byte[] silencioBuffer;

            //Cargo el contenido de los archivos mp3 en los buffer correspondientes
            puntoBuffer = FullBinaryReader(punto);
            punto.Close();
            rayaBuffer = FullBinaryReader(raya);
            raya.Close();
            silencioBuffer = FullBinaryReader(silencio);
            silencio.Close();

            //Creo el objeto FileStream para depositar todo el audio concatenado
            FileStream mp3File = new FileStream(mp3FileName,FileMode.Create);
            //Stream mp3File = File.OpenWrite(mp3FileName);

            //Analizo cada caracter del arreglo de caracteres y asigno el sonido que corresponda
            foreach (char c in charMorse)
            {
                if (c == '.')
                {
                    //punto.CopyTo(mp3File);
                    mp3File.Write(puntoBuffer, 0, puntoBuffer.Length);
                }

                if (c == '-')
                {
                    //raya.CopyTo(mp3File);
                    mp3File.Write(rayaBuffer, 0, rayaBuffer.Length);
                }

                if (c == ' ')
                {
                    //silencio.CopyTo(mp3File);
                    mp3File.Write(silencioBuffer, 0, silencioBuffer.Length);
                }
            }
            mp3File.Close();
        }

        //public static void MorseToMp3(string[] toConvert, string destDirectory)
        //{
        //    string directory = destDirectory + @"\Morse";
        //    if (!Directory.Exists(directory))
        //    {
        //        Directory.CreateDirectory(directory);
        //    }
        //    string mp3FileName = directory + @"\audio_" + DateTime.Now.ToString("dd_MM_yyyy_h_m_s") + ".mp3";

        //    string result = String.Concat(toConvert);
        //    char[] charMorse = result.ToArray();
        //    //Console.WriteLine("string impreso: "+result);

        //    string puntoPath = destDirectory + @"\punto.mp3";
        //    string rayaPath = destDirectory + @"\raya.mp3";
        //    string silencioPath = destDirectory + @"\silencio.mp3";

        //    Stream mp3File = File.OpenWrite(mp3FileName);
        //    Stream punto = File.OpenRead(puntoPath);
        //    Stream raya = File.OpenRead(rayaPath);
        //    Stream silencio = File.OpenRead(silencioPath);

        //    ////Console.WriteLine("char[] impreso: " + result);
        //    //foreach (char c in charMorse)
        //    //{
        //    //    if (c=='.')
        //    //    {
        //    //        punto.CopyTo(mp3File);
        //    //    }

        //    //    if (c == '-')
        //    //    {
        //    //        raya.CopyTo(mp3File);
        //    //    }

        //    //    //if (c == ' ')
        //    //    //{
        //    //    //    silencio.CopyTo(mp3File);
        //    //    //}
        //    //    Console.Write(c);
        //    //}

        //    punto.CopyTo(mp3File);

        //    punto.CopyTo(mp3File);

        //    punto.CopyTo(mp3File);

        //    punto.CopyTo(mp3File);


        //    punto.Close();
        //    raya.Close();
        //    silencio.Close();
        //    mp3File.Flush();
        //    mp3File.Close();

        //}
    }
}
