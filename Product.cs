using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

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
        // List<string> characteristics, reviews;
        /* Para los detalles del producto. 
         * - model o publisher varían. Estos se encuentran a veces y a veces no
         *      en los detalles del producto.
         * - releaseDate también es opcional. No sale en todos los productos. **/
        /* 
         * VALORES POSIBLES DE ENCONTRAR DESDE LA VISTA GENERAL DE LA BÚSQUEDA:
         * [Podría pasar estos valores mediante el constructor]
         *      * Name: title
         *      * SKU: skuId
         *      * Item type: itemType
         *      * Item category: itemCategory
         *      * Model number: modelNumber
         *      * Publisher: publisher
         *      * Release date: releaseDate
         *      * Price: customerPrice
         *      * Regular price: regularPrice
         *      * Imagen (en .jpg, por si la quisiera mostrar): imageURL
         *      * URL: seoPdpUrl
         *      * Brand: brand
         * VALORES POSIBLES DE ENCONTRAR DESDE EL URL DEL PRODUCTO.
         *      * DESCRIPCIÓN:
         *          Primer encuentro: <div class="bbmx-product-description">
         *          Inicio de descripción: <!-- react-text: 4 -->
         *          Final de descripción: <!-- /react-text -->
         *      * NÚMERO DE REVIEWS: 
         *          Inicio: "reviewCount":
         *          Final: ,
         *      * RATING (CALIFICACIÓN) PROMEDIO:
         *          INICIO: "averageOverallRating":
         *          FIN: ,
         */
        // string name, url, model, publisher, sku, price, rating, description, releaseDate;
        string name, sku, itemType, itemCategory, modelNumber, publisher, releaseDate,
                customerPrice, regularPrice, url, brand, description, reviewCount,
                averageOverallRating;
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
        /* PARÁMETROS PARA EL CONSTRUCTOR, ya que se pueden
         * conseguir desde la búsqueda general.
         * 
         * * Name: title
         * * SKU: skuId
         * * Item type: itemType
         * * Item category: itemCategory
         * * Model number: modelNumber
         * * Publisher: publisher
         * * Release date: releaseDate
         * * Price: customerPrice
         * * Regular price: regularPrice
         * * Imagen (en .jpg, por si la quisiera mostrar): imageURL
         * * URL: seoPdpUrl
         * * Brand: brand**/
        public Product(string name, string url)
        {
            this.name = name;
            this.url = url;
            /* Inicialización de las listas de cadenas. **/
            // characteristics = new List<string>();
            // reviews = new List<string>(5); // Lista de 5 características.
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
         *  ya guardada en memoria. 
         * Para esto utilizaremos el método:
         * 
         *      Grid.private static Uri MakeWebRequest(string URL, ref string webpageSourceCode);
         * 
         * Aunque no requerimos la URI, solo el código fuente.
         * Después de eso utilizaremos el método:
         * 
         *      Grid.public static string GetElementFromWebPage(ref string substrInCurrIndex, string initialDelimiter,
                                                           ref int currentIndex, ref int lastIndex);
         * 
         * Ahí mandaremos los identificadores para cada elemento, pero antes
         *  de hacer la búsqueda hay que ver si el dato se encuentra
         *  en el sitio web con el método:
         *  
         *      Grid.public static bool IsElementInWebPage(string webpageSourceCoude, string element);
         *  
         *  Necesitaremos tener una cadena que se vaya modificando
         *      mientras se hacen las búsquedas, que se vaya
         *      convirtiendo en subcadenas.
         *  
         * - DATOS A OBTENER:
         *  model, publisher, sku, price, rating, description, releaseDate;
         *  
         * - IDENTIFICADORES PARA CADA ATRIBUTO EN ORDEN:
         *  Algunos datos se encuentran en la página con todos los
         *   objetos, que son los siguientes:
         *      * Name: title
         *      * SKU: skuId
         *      * Item type: itemType
         *      * Item category: itemCategory
         *      * Model number: modelNumber
         *      * Publisher: publisher
         *      * Release date: releaseDate
         *      * Price: customerPrice
         *      * Regular price: regularPrice
         *      * Imagen (en .jpg, por si la quisiera mostrar): imageURL
         *      * URL: seoPdpUrl
         *      * Brand: brand
         * Datos que solo obtenemos desde el URL del producto:
         *  - Los identificadores aquí son diferentes, por lo que lo tendremos que hacer de otra forma.
         *      * DESCRIPCIÓN:
         *          Primer encuentro: <div class="bbmx-product-description">
         *          Inicio de descripción: <!-- react-text: 4 -->
         *          Final de descripción: <!-- /react-text -->
         *      * NÚMERO DE REVIEWS: 
         *          Inicio: "reviewCount":
         *          Final: ,
         *      * RATING (CALIFICACIÓN) PROMEDIO:
         *          INICIO: "averageOverallRating":
         *          FIN: ,
         *          
         * - Esto de arriba es lo único que pondría del URL directamente.
         * - Método a utilizar para hacer la búsqueda personalizada:
         *      
         *      Grid.public static string GetElementFromWebPage(ref string substrInCurrIndex,
                                                   string initialDelimiter, string finalDelimiter,
                                                   ref int currentIndex, ref int lastIndex);
         * 
         * **/
        public void GetDetails()
        {
            /* Hacer consulta a la URL. **/
            /* DESCRIPCIÓN:
             *   - Primer encuentro: < div class="bbmx-product-description">
             *   - Inicio de descripción: <!-- react-text: 4 -->
             *   - Final de descripción: <!-- /react-text -->
             * NÚMERO DE REVIEWS: 
             *   - Inicio: "reviewCount":
             *   - Final: ,
             * RATING(CALIFICACIÓN) PROMEDIO:
             *   - INICIO: "averageOverallRating":
             *   - FIN: ,
             **/

        }
        /* Método para mostrar la información del producto
         *  en un cuadro de texto.**/
        public void ShowDetails(Label info)
        {
            /* Reiniciamos el texto de la etiqueta por si tenía
             *  algo escrito.**/
            info.Text = "";
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
