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
        /* Lista estática de los productos, en donde
         *  se guardarán el nombre del producto y su URL.
         * Inicializar aquí porque no tenemos constructor de Query,
         *  y en Query siempre se limpiará si tiene elementos.**/
        private static List<Product> productList = new List<Product>();
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
            /* Hay que limpiar la lista porque es estático por si
             *  tiene elementos.**/
             if(productList.Count > 0)
                productList.Clear();

            _query.Text = " SE PUEDE REALIZAR LA BÚSQUEDA. ";

        }
        /* Método estático que verifica si se puede hacer una búsqueda.
         * Para esto tiene que haber un texto en el cuadro de búsqueda.
         * - No requiere recibir el método de ordenación porque ese
         *      ya se verifica en el proceso principal con un bloque
         *      "Try-Catch".
         * - Aunque ahora que lo pienso, aquí podría checar y mandar la
         * excepción recibiendo los dos parámetros, y si se puede hacer
         * la búsqueda, que regrese true y continúe.**/
        public static bool IsSearchable(TextBox query, ComboBox sortBy)
        {
            /* Para revisar si hay elemento seleccionado en el comboBox
             *  necesitamos utilizar un bloque "Try-Catch", en el que si
             *  hay una excepción porque no se seleccionó ningún elemento
             *  de la lista, no entra a verificar si hay texto en la barra
             *  de búsqueda.**/
            try
            {
                /* Lanzará una excepción si no hay nada seleccionado. **/
                sortBy.SelectedItem.ToString();
                /* Si hay algo seleccionado no saltará excepción
                 *  y se podrá hacer la búsqueda. Pero para esto
                 *  debe haber texto escrito en la barra de búsqueda.
                 * 
                 * Si hay texto en el cuadro de búsqueda (textBox) y hay un método
                 *  de ordenamiento seleccionado, se puede realizar la búsqueda. **/

                /* Regresará FALSE si la cadena de búsqueda está vacía.
                 * - Utilicé query.Trim().Equals(""), ya que quitará
                 *      los espacios del inicio y del final y revisará
                 *      si así quedan solo espacios en blanco. 
                 * - Aquí saltará el pop-up si no se puede hacer la búsqueda
                 * y luego devolverá el TRUE o FALSE. **/
                if (!query.Text.Trim().Equals(""))
                    return true;
                /* Si no se puede, mostrar el pop-up.**/
            }
            /* Si no regresó un true de que se puede hacer la búsqueda,
             *  llegará hasta acá porque atrapó la excepción o porque
             *  no se podía hacer la búsqueda.**/
            /* Si no entró en el Try significa que sí es posible hacer
             *  la búsqueda, pero si no entró significa que llegó hasta
             *  acá y saltó el pop-up, por lo que regresamos FALSE. **/
            catch { } /* No hace nada en el catch porque quiero que
                       * de cualquier forma si se llega acá,
                       * salga el pop-up.**/
            /* Mostrar el pop-up si hubo una excepción o la búsqueda
             *  no es válida A.K.A. no hay texto en la textBox.**/
            return false; /* No se pudo hacer la búsqueda.**/
        }
    }
}
