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
        public Product(string name, string sku, string itemType, string itemCategory,
                       string modelNumber, string publisher, string releaseDate,
                       string customerPrice, string regularPrice, string url, string brand)
        {
            this.name = name;
            this.sku = sku;
            this.itemType = itemType;
            this.itemCategory = itemCategory;
            this.modelNumber = modelNumber;
            this.publisher = publisher;
            this.releaseDate = releaseDate;
            this.customerPrice = customerPrice;
            this.regularPrice = regularPrice;
            this.url = url;
            this.brand = brand;
            description = reviewCount = averageOverallRating = "";
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
        private void GetDetails()
        {
            /* Hacer consulta a la URL. **/
            /* 
             * 1. NÚMERO DE REVIEWS: 
             *   - Inicio: "reviewCount":
             *   - Final: ,
             * 2. RATING(CALIFICACIÓN) PROMEDIO:
             *   - INICIO: "averageOverallRating":
             *   - FIN: ,
             * 3. DESCRIPCIÓN:
             *   - Primer encuentro: < div class="bbmx-product-description">
             *   - Inicio de descripción: <!-- react-text: 4 -->
             *   - Final de descripción: <!-- /react-text -->
             **/
            string sourceCode = "";
            /* Los índices que irán extrayendo los elementos
             *  de la cadena del código fuente.**/
            int firstIndex = 0, lastIndex = 0;
            Query.MakeWebRequest(url, ref sourceCode);
            /* Hacer una subcadena desde el índice que acabamos de encontrar.**/
            sourceCode = sourceCode.Substring(firstIndex, sourceCode.Length - firstIndex);
            /* Ahora sí utilizar el método para obtener la descripción.
             * Enviaré el delimitador inicial y el final.**/
            reviewCount = Query.GetElementFromWebPage(ref sourceCode, "\"reviewCount\":", ",", ref firstIndex, ref lastIndex);
            averageOverallRating = Query.GetElementFromWebPage(ref sourceCode, "\"averageOverallRating\":", ",",  ref firstIndex, ref lastIndex);

            /* Aquí guardamos el primer índice en donde se encuentra 
             * la descripción más adelante.**/
            firstIndex = lastIndex = sourceCode.IndexOf("< div class=\"bbmx - product - description\">") + "< div class=\"bbmx - product - description\">".Length;
            description = Query.GetElementFromWebPage(ref sourceCode, "<!-- react-text: 4 -->", "<!-- /react-text -->", ref firstIndex, ref lastIndex);
            //Console.WriteLine("\n\n -> Description: {0}, reviewCount: {1}, averageOverallRating: {2}\n\n", description, reviewCount, averageOverallRating);
        }
        /* Método para mostrar la información del producto
         *  en un cuadro de texto.
         *  
         * Información a mostrar:
         *  name, sku, itemType, itemCategory, modelNumber, publisher, releaseDate,
                customerPrice, regularPrice, url, brand, description, reviewCount,
                averageOverallRating**/
        public void ShowDetails(TextBox info)
        {
            /* Antes de mostrar los detalles hay que obtener
             *  los que faltan:**/
            GetDetails();
            /* Reiniciamos el texto de la etiqueta por si tenía
             *  algo escrito.**/
            info.Text = "";
            /* Mandar la información al cuadro de texto. **/
            info.Text += " - NOMBRE DEL PRODUCTO: " + name;
            info.Text += "\r\n - PRECIO: " + customerPrice;
            // Si el producto sí tiene fecha de lanzamiento.
            if (!releaseDate.Equals(""))
                info.Text += "\r\n - FECHA DE LANZAMIENTO: " + releaseDate;
            info.Text += "\r\n - MARCA: " + brand;
            // Si sí tiene detalles del publisher el producto.
            if (!publisher.Equals(""))
                info.Text += "\r\n - PUBLISHER: " + publisher;

            /* Separar hasta publisher por una línea vacía.**/
            info.Text += "\r\n";

            // Si sí tiene detalles del modelo el producto.
            if (!modelNumber.Equals(""))
               info.Text += "\r\n - NÚMERO DE MODELO: " + modelNumber;
            info.Text += "\r\n - SKU: " + sku;
            info.Text += "\r\n - CATEGORÍA: " + itemCategory;
            /* Línea vací anetre los textos.**/
            info.Text += "\r\n\r\n - NÚMERO DE RESEÑAS: " + reviewCount;
            info.Text += "\r\n - CALIFICACIÓN: " + averageOverallRating;
            info.Text += "\r\n\r\n - DESCRIPCIÓN: " + description;
            /* Que el URL vaya al final de la página.**/
            info.Text += "\r\n\r\n - URL: " + url;

            /* Agregamos el URL a una etiqueta en donde se puede
             *  acceder al enlace.**/


            /* Agregamos las características y reviews que están en listas
             *  en la cadena del Label de los detalles.**/
            //AppendCharacteristics(info);
            //AppendReviews(info);
        }
        /* Método para agregar las características del producto
         *  a la cadena del cuadro de texto. Esto para no mandarle
         *  la "label" como parámetro, ya que sería algo redundante. 
         *  
         *  - ACTUALIZACIÓN: Vi que se puede pasar la Label aunque
         *  no sea por referencia y se va a actualizar. Aunque sea
         *  redundante es menos rollo.**/
        //private void AppendCharacteristics(Label label)
        //{
        //    label.Text += "\n\n - CARACTERÍSTICAS: ";
        //    /* Agregamos las características a la cadena. **/
        //    label.Text += GetBulletsOfList(characteristics);
        //}
        /* Método para concatenar las reviews del producto
         *  a la cadena del cuadro de texto. Esto además de para
         *  no ser redundante y mandar "label" como parámetro,
         *  ser más conciso tomando en cuenta de que solo
         *  requerimos este proceso para agregarlo al cuadro
         *  de texto.**/
        //private void AppendReviews(Label label)
        //{
        //    label.Text = "\n\n - RESEÑAS DEL PRODUCTO: ";
        //    /* Agregamos las reviews a la cadena. **/
        //    label.Text += GetBulletsOfList(reviews);

        //    /* No se puede poner un contador y lo necesito para el número
        //     *  de elemento. Mejor opto por la otra opción. **/
        //    //foreach (string review in reviews)
        //    //{
        //    //    /* Aquí concatenamos cada característica en una línea nueva
        //    //     *  indicando el número de elemento. **/
        //    //    reviewsString += ("\n  {0}.- {1}", review, characteristics.ElementAt(i));
        //    //}
        //}
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
        /* Método que imprimirá los detalles del producto.**/
        public void PrintDetails()
        {
            Console.WriteLine("\n\n - Nombre: {0}, SKU: {1}, itemType: {2}, itemCategory: {3}," +
                "\nmodelNumber: {4}, publisher: {5}, releaseDate: {6}, customerPrice: {7}," +
                "\nregularPrice: {8}, url: {9}, brand: {10}", name, sku, itemType, itemCategory, 
                    modelNumber, publisher, releaseDate, customerPrice, regularPrice, url, brand);
        }
    }
}
