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

            Product.EscribeLabelTextPrueba(ref label_ProductInformation.Text);
        }
    }
}
