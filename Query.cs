using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* CLASE PARA HACER LA BÚSQUEDA DE ACUERDO A LA QUERY ESPECÍFICADA:
 *  - Elemento/Producto a buscar
 *  - Parámetro de ordenación de búsqueda
 * Al hacer la búsqueda se guardará en un arreglo de Product
 *  (Product[] products) el nombre y el enlace de cada uno.
 *  Ya que se seleccione el que se quiera mostrar, se ingresará
 *      al enlace y se obtendrá su demás información.
 * Jueves, 25 de noviembre del 2020
 * 
 * - LA CLASE SERÁ ESTÁTICA PORQUE NO REQUIERE SER INSTANCIADA.
 *    Solo se ocupará su método de búsqueda, pero la información
 *      será guardada en la clase Product, de la cual se manejará
 *      un arreglo estático para que sea global.
 * **/


namespace _T3._1__WebRequest_con_BestBuy
{
    static class Query
    {
        /* Arreglo estático de los productos, en donde
         *  se guardarán el nombre del producto y su URL.**/
        private static Product[] productArray;
        /* Método que hará la búsqueda deseada recibiendo como
         *  parámetros la "query", que es la búsqueda, y la forma
         *  de ordenar "sortBy". 
         *  
         * No quiero ser ambiguo, por lo que recibiré los
         *  parámetros directamente del tipo de dato, y
         *  crearé variables de cadenas ya específicas. De 
         *  esta manera se da a entender mejor el proceso. **/
        public static void SearchQuery(TextBox _query, ComboBox _sortBy)
        {
            string query = _query.Text;
            /* Cadena que guardará la forma de ordenamiento de la búsqueda.
             * Así es como se accede a un elemento de la ComboBox y se pasa
             *  a cadena. **/
            string sortBy = _sortBy.SelectedItem.ToString();
            _query.Text = " SE PUEDE REALIZAR LA BÚSQUEDA. ";

        }
        /* Método estático que verifica si se puede hacer una búsqueda.
         * Para esto tiene que haber un texto en el cuadro de búsqueda
         *  y un método de ordenación seleccionados. **/
        public static bool IsSearchable(string query, string sortBy)
        {
            /* Regresará FALSE si las dos cadenas están vacías.
             * - Utilicé query.Trim().Equals(""), ya que quitará
             *      los espacios del inicio y del final y revisará
             *      si así quedan solo espacios en blanco. **/
            return !query.Trim().Equals("") && !sortBy.Equals("");
        }
    }
}
