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
                if(!label_Products.Text.Contains("\n - DÉ CLICK A UN PRODUCTO PARA VER DETALLES -"))
                    label_Products.Text += "\n - DÉ CLICK A UN PRODUCTO PARA VER DETALLES -";
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
    }
}
