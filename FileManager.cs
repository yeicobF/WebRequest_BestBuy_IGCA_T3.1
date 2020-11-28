using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/* CLASE QUE MANEJARÁ TODO LO QUE TENGA QUE
 *  VER CON ARCHIVOS.
 *  
 *  NO SE PUEDE LLAMAR File PORQUE YA EXISTE UNA
 *  CLASE LLAMADA ASÍ.
 *  
 * Será estática porque no necesitamos instanciarla
 *  ni con otros derivados de esta.
 *  
 *  https://stackoverflow.com/questions/2390767/whats-the-difference-between-an-abstract-class-and-a-static-one#:~:text=Abstract%20classes%20get%20instantiated%20indirectly,provided%20by%20derived%20concrete%20classes.&text=Static%20classes%20cannot%20be%20instantiated,rather%20than%20the%20instance%20level.
 * **/

namespace _T3._1__WebRequest_con_BestBuy
{
    class FileManager
    {
        // Este atributo indica el tamaño de la cadena del nombre del archivo.
        private static int tamNombre = "Sin título".Length;
        // Atributo que indica el nombre del archivo actual.
        private static string nombreArchivo = "Sin título";
        // Aquí se guardará el directorio del archivo actual.
        private static string directorio = "";
        // Atributo que indica si se abrió un archivo.
        private static bool archivoAbierto = false;
        // Atributo que indicará si está abierto un archivo existente o no.
        private static bool existeArchivo = false;
        /* Pondremos el directorio inicial que sea la carpeta de documentos.**/
        private static string directorioInicial = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        /* Método que abre el cuadro para abrir un archivo,
            y regresa su nombre.*/
        public static void AbrirArchivo(RichTextBox texto)
        {
            OpenFileDialog abrir = new OpenFileDialog();
            // Poner un filtro para el tipo de archivos que se muestran.
            abrir.Filter = "Archivos de texto|*.txt";
            // Se se aceptó abrir un archivo, lo mostrará
            if (abrir.ShowDialog() == DialogResult.OK)
            {
                // Aquí muestra el archivo en la caja de texto.
                texto.Text = File.ReadAllText(abrir.FileName);
                // Aquí guarda el nombre del archivo con su extensión.
                nombreArchivo = abrir.SafeFileName;
                // Aquí cambiamos el tamaño del nombre restándole su extensión.
                tamNombre = nombreArchivo.Length - 4;
                // Aquí se guarda el directorio con el nombre del archivo.
                directorio = abrir.FileName;
                // Indicar que el archivo actual ya está guardado.
                existeArchivo = true;
                // Indicar que ya hay un archivo abierto.
                archivoAbierto = true;
                //// Cambiamos el título de la ventana.
                //CambiarTitulo(nombreArchivo);
            }
            //return abrir.SafeFileName;
        }
        /* Método que mostrará la ventana de guardado.*/
        public static void SaveAs(string nombreInicialArchivo, string contenido)
        {
            //nombreArchivo = nombreInicialArchivo;
            //tamNombre = nombreArchivo.Length;

            // Aquí se inicializa el cuadro de dialogo para guardar.
            SaveFileDialog guardar = new SaveFileDialog();
            // Variable que me dejará sacar el nombre del archivo sin directorio.
            OpenFileDialog abrir = new OpenFileDialog();
            // Esto filtrará el tipo de archivos que salgan en pantalla.
            guardar.Filter = "Archivos de texto|*.txt";
            /* Establecemos el directorio para guardar como el actual en donde se corrió el programa.
             * 
             * Devuelve el directorio actual, que es donde se ejecuta el programa:
             *  bin/debug: Environment.CurrentDirectory;
             * 
             * **/
            //guardar.DefaultExt = ".txt";
            // guardar.InitialDirectory = directorioInicial;
            guardar.FileName = nombreInicialArchivo + ".txt"; // Este nombre es el que saldrá al abrir la ventana de guardado.
            // Aquí se abre un cuadro de diálogo.
            // Si presionamos "save" en el cuadro de diálogo, guardar el archivo.
            if (guardar.ShowDialog() == DialogResult.OK)
            {   // Aquí se escribe el contenido al archivo que creamos.
                File.WriteAllText(guardar.FileName, contenido);
                // Si al leer el archivo es igual a la cadena de contenido, mostrar emergente.
                if (File.ReadAllText(guardar.FileName).Equals(contenido))
                {
                    // Asignamos a abrir.FileName el directorio completo del guardado.
                    abrir.FileName = guardar.FileName;
                    // Aquí guarda el nombre del archivo con su extensión con "abrir", que "guardar" no tiene el método.
                    nombreArchivo = abrir.SafeFileName;
                    // Aquí cambiamos el tamaño del nombre restándole su extensión.
                    tamNombre = nombreArchivo.Length - 4;
                    // Aquí se guarda el directorio con el nombre del archivo.
                    directorio = guardar.FileName;
                    // Indicar que el archivo actual ya está guardado.
                    existeArchivo = true;
                    // Indicar que ya hay un archivo abierto.
                    archivoAbierto = true;
                    //// Cambiamos el título de la ventana.
                    //CambiarTitulo(nombreArchivo);
                    MessageBox.Show("Archivo guardado con éxito.", nombreArchivo);
                }
            }
            else
                MessageBox.Show("No se guardó el archivo. No se presionó el botón de guardar.", "ATENCIÓN");
        }
        /* Método que guarda el texto en un archivo que ya está creado.
            Si se trata de uno nuevo, mostrar el SaveAs, si es uno abierto
            o guardado anteriormente, solo sobreescribirlo.*/
        public static void Save(string nombreInicialArchivo, string contenido)
        {
            // Si el archivo ya existe, sobreescribirlo.
            /* Esto extrae el nombre del archivo que se guardó anteriormente:
             *  nombreArchivo.Substring(0, tamNombre)
             * Si EL NOMBRE es igual al archivo actual, solo sobreescribir.
             * 
             * - Si el contenido es el mismo, no sobreescribir.**/
            if (nombreInicialArchivo.Equals(nombreArchivo.Substring(0, tamNombre)))
                /* Aquí revisa si el contenido del archivo es igual al nuevo
                 *  enviado, y si sí es igual no lo guarda.**/
                if(!directorio.Equals("") && !File.ReadAllText(directorio).Equals(contenido))
                    File.WriteAllText(directorio, contenido); // Aquí no se cambia ningún atributo porque ya existía.
            else // Si no existe el archivo, llamar a SaveAs().
                SaveAs(nombreInicialArchivo, contenido);
        }


        // Método que devuelve si hay un archivo abierto actualmente,
        public static bool IsArchivoAbierto()
        {
            return archivoAbierto;
        }
        // Método que regresa el nombre del archivo actual.
        public static string GetNombreArchivo()
        {
            return nombreArchivo;
        }
        // Método que establece que el archivo no existe.
        public static void SetExisteArchivoFalse()
        {
            existeArchivo = false;
        }
    }
}
