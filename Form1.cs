using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/* PROGRAMA PARA EL PROYECTO DE WEBREQUEST DEL
 * TERCER PARCIAL DE INTERFACES GRÁFICAS CON
 *  APLICACIONES. 
 * Se hará un WebRequest de una búsqueda específicada
 *  por el nombre del producto a búscar y el parámetro
 *  de ordenamiento, se mostrará la lista de los
 *  nombres de productos encontrados, y luego se
 *  mostrará en otro cuadro la información del
 *  objeto seleccionado.
 * - La WebRequest será con el sitio web de Best Buy.
 * 
 * Jueves, 25 de noviembre del 2020.
 * FECHA DE ENTREGA: Viernes, 26 de noviembre del 2020.
 * **/

namespace _T3._1__WebRequest_con_BestBuy
{
    public partial class SearchWindow : Form
    {
        public SearchWindow()
        {
            InitializeComponent();

            //Product.EscribeLabelTextPrueba(label_ProductInformation);
        }
        /* Método que efectuará la búsqueda cuando se dé click (con el
         *  mouse o presionando la tecla "enter" sobre el espacio)
         *  a la barra de búsqueda o al botón de búsqueda.
         * 
         * - Debe haber un texto en la búsqueda y un 
         *  método de ordenamiento seleccionado. **/
        private void button_WebSearch_Click(object sender, EventArgs e)
        {
            /* Si se puede hacer la búsqueda, realizarla.**/
            if (Query.IsSearchable(textBox_WebQuery, comboBox_SortBy))
            {
                /* Borrar el error de "searchErrorProvider" si es que había alguno. **/
                searchErrorProvider.SetError(comboBox_SortBy, String.Empty);
                searchErrorProvider.SetError(button_WebSearch, String.Empty);
                /* Realizar la búsqueda tomando el elemento a buscar y la
                 *  categoría.**/
                /* Arreglo tipo Object en donde mandaremos el ErrorProvider
                 *  y el ComboBox para cuando se redirija la consulta a un
                 *  URL distinto al que nosotros especificamos y no se aplique
                 *  el filtro de ordenación.**/
                Object[] errorAndCombobox = new object[2] { searchErrorProvider, comboBox_SortBy };
                Query.SearchQuery(textBox_WebQuery, Query.GetSortedByQuery(comboBox_SortBy), errorAndCombobox);

                /* Método que después de hacer la búsqueda mostrará la lista
                 *  de productos que encontró y que se pueden seleccionar.**/
                Query.ShowQueryResults(listBox_Products);

                /* Cuando ya se muestre la lista de productos para seleccionar,
                *  agregar un texto que indique que seleccione un producto.
                * - Hay que verificar que este texto no exista ya.**/
                if(!label_Products.Text.Contains("\n - PRESIONA ENTER EN UN PRODUCTO PARA VER DETALLES -"))
                    label_Products.Text += "\n - PRESIONA ENTER EN UN PRODUCTO PARA VER DETALLES - ";
                /* Mostrar en pantalla el número de elementos encontrados
                 *  y el número de páginas encontradas.**/
                label_NumberOfFoundElements.Text = "NÚMERO TOTAL DE PRODUCTOS ENCONTRADOS: " + Query.NumberOfFoundElements;
                label_NumberOfFoundElements.Text += "\r\nNÚMERO TOTAL DE PÁGINAS DE LA BÚSQUEDA: " + Query.NumberOfFoundWebPages;
                label_NumberOfFoundElements.Text += "\r\n-> NÚMERO DE PRODUCTO SELECCIONADO: --";
            }
            else
                /* Si no se pudo hacer la búsqueda mostrar el error. 
                 * - Se especifica el control asociado al error y la
                 * cadena de texto que mostrará el error.**/
                searchErrorProvider.SetError(button_WebSearch, "Tú búsqueda no es válida.\nNo ingresaste texto o no\nseleccionaste método de ordenación.");
        }
        /* Método que activará la búsqueda actual al presionar la tecla "enter"
         *  cuando se esté en la barra de búsqueda.
         * 
         * - Debe haber un texto en la búsqueda y un 
         * método de ordenamiento seleccionado.**/
        private void textBox_WebQuery_KeyDown(object sender, KeyEventArgs e)
        {
            /* Si se presionó enter, realizar la búsqueda llamando al método
             *  del botón.**/
            if (e.KeyCode == Keys.Enter)
                button_WebSearch_Click(sender, e);
        }
        /* Método que activará la búsqueda actual al presionar la tecla "enter"
             *  cuando se esté en la lista de categorías.
             * 
             * - Debe haber un texto en la búsqueda y un 
             * método de ordenamiento seleccionado.**/
        private void comboBox_SortBy_KeyDown(object sender, KeyEventArgs e)
        {
            /* Si se presionó enter, realizar la búsqueda llamando al método
             *  del botón.**/
            if (e.KeyCode == Keys.Enter)
                button_WebSearch_Click(sender, e);
        }
        /* Método que se ejecutará al dar un doble click con el mouse
         *  a un elemento de la lista de productos, o cuando se presione
         *  "enter" en un elemento seleccionado.**/
        private void listBox_Products_Click(object sender, MouseEventArgs e)
        {
            /* Hay que ver si la lista tiene elementos o no.**/
            if(listBox_Products.Items.Count > 0)
                /* De esta forma accedemos al producto del índice seleccionado
                 *  y llamamos a su método ShowDetails para que muestre sus detalles.**/
                Query.ProductList.ElementAt(listBox_Products.SelectedIndex).ShowDetails(richTextBox_ProductDetails);
        }

        private void listBox_Products_KeyDown(object sender, KeyEventArgs e)
        {
            /* Hay que ver si la lista tiene elementos o no.
             * listBox_Products.SelectedIndex = -1 significa que no hay nada seleccionado.**/
            if (listBox_Products.Items.Count > 0 && listBox_Products.SelectedIndex >= 0 && e.KeyCode == Keys.Enter)
                /* De esta forma accedemos al producto del índice seleccionado
                 *  y llamamos a su método ShowDetails para que muestre sus detalles.**/
                Query.ProductList.ElementAt(listBox_Products.SelectedIndex).ShowDetails(richTextBox_ProductDetails);
        }
        /* Método que al presionar el botón que indica el reinicio de todo,
         *  pondrá todos los elementos como al inicio:
         *      - Cuadro de búsqueda vacío
         *      - Índice de categoría: -1, así indica que no hay selección
         *      - ListBox con elementos quitárselos
         *      - TextBox con descripción de productos reiniciar
         *      - ErrorProvider con ComboBox y el botón de buscar reiniciar.**/
        private void button_Reset_Click(object sender, EventArgs e)
        {
            /* Limpiar cuadro de búsqueda**/
            textBox_WebQuery.Text = "";
            /* Quitar selección de "Ordenar por: "**/
            comboBox_SortBy.SelectedIndex = -1;
            /* Borrar la lista de los productos. **/
            listBox_Products.Items.Clear();
            /* Borrar los detalles de productos sí es que hay y poner el texto predeterminado.**/
            richTextBox_ProductDetails.Text = "Descripción del producto seleccionado.";
            /* Borar el número de elementos encontrados y poner como está al incio.**/
            label_NumberOfFoundElements.Text = "NÚMERO TOTAL DE PRODUCTOS ENCONTRADOS: --";
            label_NumberOfFoundElements.Text += "\r\nNÚMERO TOTAL DE PÁGINAS DE LA BÚSQUEDA: --";
            label_NumberOfFoundElements.Text += "\r\n-> NÚMERO DE PRODUCTO SELECCIONADO: --";
            /* Borrar el error de "searchErrorProvider" si es que había alguno. **/
            searchErrorProvider.SetError(comboBox_SortBy, String.Empty);
            searchErrorProvider.SetError(button_WebSearch, String.Empty);
        }
        /* Método que termina el programa.**/
        private void button_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /* Método que ostrará el número del objeto que se ha seleccionado.
         * Va del 1 al numberOfFoundObjects.**/
        private void listBox_Products_SelectedIndexChanged(object sender, EventArgs e)
        {
            label_NumberOfFoundElements.Text = "NÚMERO TOTAL DE PRODUCTOS ENCONTRADOS: " + Query.NumberOfFoundElements;
            label_NumberOfFoundElements.Text += "\r\nNÚMERO TOTAL DE PÁGINAS DE LA BÚSQUEDA: " + Query.NumberOfFoundWebPages;
            /* Se le aumenta 1 al índice porque el índice va desde el 0
             *  y el número de elemento comienza desde el 1.**/
            label_NumberOfFoundElements.Text += "\r\n-> NÚMERO DE PRODUCTO SELECCIONADO: " + (listBox_Products.SelectedIndex + 1);
        }
        /* Método que modifica el cuadro de texto en donde indica:
         *      
         *      label_NumberOfFoundElements.Text = "NÚMERO TOTAL DE PRODUCTOS ENCONTRADOS: --";
                label_NumberOfFoundElements.Text += "\r\nNÚMERO TOTAL DE PÁGINAS DE LA BÚSQUEDA: --";
                label_NumberOfFoundElements.Text += "\r\n-> NÚMERO DE PRODUCTO SELECCIONADO: --";
         * 
         * Cambia los valores dependiendo de los parámetros que enviemos.**/
        private void ModifyFoundElementsText(string numberOFoundElements, string numberOfFoundWebPages, string currentSelectedElement)
        {
            label_NumberOfFoundElements.Text = "NÚMERO TOTAL DE PRODUCTOS ENCONTRADOS: " + numberOFoundElements;
            label_NumberOfFoundElements.Text += "\r\nNÚMERO TOTAL DE PÁGINAS DE LA BÚSQUEDA: " + numberOfFoundWebPages;
            label_NumberOfFoundElements.Text += "\r\n-> NÚMERO DE PRODUCTO SELECCIONADO: " + currentSelectedElement;
        }
    }
}
