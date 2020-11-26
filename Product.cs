using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* CLASE PARA GUARDAR LOS ELEMENTOS ENCONTRADOS EN LA BÚSQUEDA.
 * Guardará el elemento con su nombre y enlace al producto.
 * Cuando se seleccione para mostrar se consultará su información
 *  en el enlace y se guardará para después mostrarla.
 * Jueves, 25 de noviembre del 2020
 * **/

namespace _T3._1__WebRequest_con_BestBuy
{
    class Product
    {
        /* characteristics:
         *  Lista de las características del objeto. 
         *  Una característica por línea. 
         * reviews:
         *  También agregué una lista de las reviews.
         *  Se podrían mostrar unas 5 al menos.*/
        List<string> characteristics, reviews;
        /* Para los detalles del producto. 
         * - model o publisher varían. Estos se encuentran a veces y a veces no
         *      en los detalles del producto.
         * - releaseDate también es opcional. No sale en todos los productos. **/
        string name, url, model, publisher, sku, price, rating, description, releaseDate;
        /* Manejo de variables con propiedades para utilizar un
         *  Getter sin tener que poner Product.GetName(), sino
         *  Product.Name, sin más.
         * 
         * FUENTE: [StackOverflow] Looking for a short & simple example of getters/setters in C#
         *  https://stackoverflow.com/questions/11159438/looking-for-a-short-simple-example-of-getters-setters-in-c-sharp
         * **/
        public string Name
        {
            get { return name; }
        }
        /* Lo mismo que arriba pero en una línea.**/
        public string URL { get { return url; } }
        public Product(string name, string url)
        {
            this.name = name;
            this.url = url;
            /* Inicialización de las listas de cadenas. **/
            characteristics = new List<string>();
            reviews = new List<string>(5); // Lista de 5 características.
            /* Inicialización de las cadenas. Necesario en el caso de las variables
             *  en donde no se muestran algunos detalles. Servirá para evaluar si la
             *  cadena está vacía no mostrar la info. **/
            model = publisher = sku = price = rating = releaseDate = description = "";
        }
        /* Método para probar si las Label se modifican en pantalla
         *  aunque no se pasen por referencia. **/
        public static void EscribeLabelTextPrueba(Label label)
        {
            label.Text += "HOLA";
        }
        /* Método que obtendrá la información restante
         *  del producto haciendo una consulta a la URL
         *  ya guardada en memoria. **/
        public void GetDetails()
        {
            /* Hacer consulta a la URL. **/
        }
        /* Método para mostrar la información del producto
         *  en un cuadro de texto.**/
        public void ShowDetails(Label info)
        {
            /* Mandar la información al cuadro de texto. **/
            info.Text += " - NOMBRE DEL PRODUCTO: " + name;
            // Si sí tiene detalles del modelo el producto.
            if (!model.Equals(""))
               info.Text += "\n - MODELO: " + model;
            // Si sí tiene detalles del publisher el producto.
            if (!publisher.Equals(""))
                info.Text += "\n - PUBLISHER: " + publisher;
            info.Text += "\n - SKU: " + sku;
            info.Text += "\n - PRECIO" + price;
            info.Text += "\n - CALIFICACIÓN: " + rating;
            // Si el producto sí tiene fecha de lanzamiento.
            if(!releaseDate.Equals(""))
                info.Text += "\n - FECHA DE LANZAMIENTO: " + releaseDate;
            info.Text += "\n - DESCRIPCIÓN: " + description;
            /* Agregamos las características y reviews que están en listas
             *  en la cadena del Label de los detalles.**/
            AppendCharacteristics(info);
            AppendReviews(info);

        }
        /* Método para agregar las características del producto
         *  a la cadena del cuadro de texto. Esto para no mandarle
         *  la "label" como parámetro, ya que sería algo redundante. 
         *  
         *  - ACTUALIZACIÓN: Vi que se puede pasar la Label aunque
         *  no sea por referencia y se va a actualizar. Aunque sea
         *  redundante es menos rollo.**/
        private void AppendCharacteristics(Label label)
        {
            label.Text += "\n\n - CARACTERÍSTICAS: ";
            /* Agregamos las características a la cadena. **/
            label.Text += GetBulletsOfList(characteristics);
        }
        /* Método para concatenar las reviews del producto
         *  a la cadena del cuadro de texto. Esto además de para
         *  no ser redundante y mandar "label" como parámetro,
         *  ser más conciso tomando en cuenta de que solo
         *  requerimos este proceso para agregarlo al cuadro
         *  de texto.**/
        private void AppendReviews(Label label)
        {
            label.Text = "\n\n - RESEÑAS DEL PRODUCTO: ";
            /* Agregamos las reviews a la cadena. **/
            label.Text += GetBulletsOfList(reviews);

            /* No se puede poner un contador y lo necesito para el número
             *  de elemento. Mejor opto por la otra opción. **/
            //foreach (string review in reviews)
            //{
            //    /* Aquí concatenamos cada característica en una línea nueva
            //     *  indicando el número de elemento. **/
            //    reviewsString += ("\n  {0}.- {1}", review, characteristics.ElementAt(i));
            //}
        }
        /* Método para obtener los puntos de una lista
         *  enumerados.**/
        private string GetBulletsOfList(List<string> elementsList)
        {
            string bulletList = "";

            for (int i = 0; i < elementsList.Count; i++)
                /* Aquí concatenamos cada característica en una línea nueva
                 *  indicando el número de característica. **/
                bulletList += ("\n  {0}.- {1}", i, elementsList.ElementAt(i));

            return bulletList;
        }
    }
}
