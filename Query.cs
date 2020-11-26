using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

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
            /* currentURL: Será el URL para buscar TODOS los elementos
             *  de la página actual. Se iterará hasta que ya no existan
             *  más páginas.
             * productURL: La URL de cada producto.
             * productName: El nombre de cada producto.**/
            string currentPageURL, productURL = "", productName = "";
            /* baseURL: URL base para TODAS las búsquedas de Best Buy. **/
            string baseURL = "https://www.bestbuy.com.mx/c/buscar-best-buy/buscar?query=";
            /* lastURL: El anterior URL para verificar que no se haga
             *  la solicitud al sitio más de una vez.
             * onceRedirectedURL: Para verificar que no nos vuelva a
             *  redirigir a un sitio en donde ya estuvimos.**/
            string responseFromServer = "", lastURL = "", onceRedirectedURL = "";
            /* bool para indicar que ya se encontró un searchRedirect.**/
            bool searchRedirectFound = false;
            /* bool para ver qué valores se agregarán al URL.**/
            bool addQuery = true, addSortBy = true, addPageNumber = true;
            HttpWebResponse response;
            Stream dataStream = null;
            StreamReader reader = null;

            /* Hay que limpiar la lista porque es estático por si
             *  tiene elementos.**/
            if (productList.Count > 0)
                productList.Clear();
            /* Para hacer la búsqueda hay que hacer un String.Trim()
             *     para que busque sin los espacios al inicio o
             *     al final por si se ingresó así la búsqueda.
             * Aunque esto es opcional realmente porque de cualquier
             *     forma el sitio web decide si se puede hacer la
             *     búsqueda o no, y nosotros solo la mandamos
             *     tal como el usuario la hizo.**/

            /* Iterador para las páginas de los sitios web.
             * Los números de página comienzan desde el 1.**/
            int pageCounter = 1;
            /* Necesito que haga el procedimiento antes que la verificación
             *  de la condición. Es decir, que primero haga la solicitud y
             *  luego verifique. Podría poner un bloque "Try-Catch" para
             *  cuando no encuentre más páginas en la búsqueda y salta una
             *  excepción, así terminar el proceso.**/
            do
            {
                /* Si atrapa una excepción significa que no encontró
                 *  más páginas y podemos terminar el proceso.
                 * Solo terminará el ciclo cuando se entre al catch.
                 * Utilizo esta forma para guardar el URL para que sea
                 *  más fácil identificar el patrón. La cuestión es
                 *  que solo se puede hacer concatenando, entonces
                 *  antes de hacerlo cada vez tengo que reiniciar la cadena.**/
                currentPageURL = "";
                currentPageURL = baseURL;
                /* Agregar de uno por uno si lo indicamos.
                 * Esto por si hay un URL que tengo que tomar
                 *  como el URI base y ya tiene estos elementos.
                 * Si los agrego el URL se empieza a agrandar mal.**/
                if (addQuery)
                    currentPageURL += query;
                if(addSortBy)
                    currentPageURL += "&sort=Best-Match";
                if(addPageNumber)
                    currentPageURL += "&page=" + pageCounter;
                else /* Si no, solo agregamos el número.**/
                {
                    /* Aquí tomo el índice en donde encuentro lo del número
                     *  de página, para quitar el número actual y poner el nuevo.**/
                    int pageNumberIndex = currentPageURL.IndexOf("&page=") + "&page=".Length;
                    //int pageNumberIndex = 25;
                    /* Aquí cambio SOLO el número de página y lo demás lo dejo tal y como está.
                     * Esto lo hago así porque el número de página es el que siempre cambiará
                     *  a pesar de las condiciones. Lo que sigue del número de página queda
                     *  tal y como está.**/
                    currentPageURL = currentPageURL.Substring(0, pageNumberIndex) + pageCounter 
                        + currentPageURL.Substring(pageNumberIndex + 1, currentPageURL.Length - pageNumberIndex - 1);
                }
                //currentPageURL += ("{url/query=}{query}&sort={sortBy}&page={pageNumber}", baseURL, query, sortBy, pageCounter);
                // + "&sort=" + "Price-Low-To-High" + "&page=" + pageCounter;

                

                //currentPageURL = "https://www.bestbuy.com.mx/c/buscar-best-buy/buscar?query=ps5&sort=Best-Match" + "&page=" + pageCounter;
                Console.WriteLine("\n - URL ACTUAL: " + currentPageURL + "\n -> Request\n");

                // Create a request for the URL. 		
                //WebRequest request = WebRequest.Create("AQUÍ VA LA URL");
                WebRequest request = WebRequest.Create(currentPageURL);
                // If required by the server, set the credentials.
                request.Credentials = CredentialCache.DefaultCredentials;

                // Get the response.
                response = (HttpWebResponse)request.GetResponse();
                Uri s = response.ResponseUri;

                /* Si nos redirige, irnos moviéndonos de página para
                 *  ver si hay más, si no, nos seguirá redirigiendo a la misma.**/
                if (s.AbsoluteUri.Contains("searchRedirect") && !searchRedirectFound)
                {
                    /* Hay ocasiones en que el "searchRedirect" está al final de
                     *  la URL y no al inicio. En esos casos no podemos concatenarle
                     *  la consulta porque crecerá exponencialmente. Ahí simplemente
                     *  lo ignoramos y continuamos con el conteo normal de páginas.
                     *  Ese al menos fue el comportamiento que vi con la búsqueda
                     *      de "Bose".
                     *  Si el último índice de esa cadena es igual al tamaño
                     *      de la cadena del URL, ignorar y continuar con la búsqueda
                     *      en la siguiente página.**/
                    if(s.AbsoluteUri.IndexOf("searchRedirect=" + query) + ("searchRedirect=" + query).Length == s.AbsoluteUri.Length)
                    {
                        /* Aumentar el contador de página. Esto porque hará un continue y reiniciará
                         *  el ciclo sin avanzar al otro pageCounter.**/
                        pageCounter++;
                        /* Hay que tomar el nuevo URL pero sin el "&searchRedirect=".**/
                        baseURL = s.AbsoluteUri.Substring(0, s.AbsoluteUri.IndexOf("&searchRedirect="));
                        /* Si el enlace ya tiene algunos de los elementos de la consulta,
                         *  ya no agregarlos.**/
                        if (s.AbsoluteUri.Contains("query="))
                            addQuery = false;
                        if (s.AbsoluteUri.Contains("&sort="))
                            addSortBy = false;
                        if (s.AbsoluteUri.Contains("&page="))
                            addPageNumber = false;
                        /* Continue para ya no seguir con los siguientes procesos.**/
                        continue;
                    }
                        
                    /* Reiniciar contador para buscar en las páginas hacia donde nos redirigieron.**/
                    pageCounter = 1;
                    /* Hay que guardar esta URL por si nos vuelve a redirigir, ya no repetir proceso.**/
                    onceRedirectedURL = s.AbsoluteUri;
                    //currentPageURL = s.AbsoluteUri + "&page=" + pageCounter;
                    /* Hacemos la base el nuevo enlace al que se redirigió. Esto 
                     * encontrando el último índice de la subcadena que lo indica.**/
                    baseURL = s.AbsoluteUri.Substring(0, s.AbsoluteUri.IndexOf("&searchRedirect=") + "&searchRedirect=".Length);
                    /* Indicar que ya se encontró el texto.**/
                    searchRedirectFound = true;
                    continue;
                }

                /* Si la URL anterior es igual a la actual, romper ciclo.
                 * De otra forma se estará accediendo a la misma una y otra vez
                 *  porque hay búsquedas que redirigen a otras URLs que no son
                 *  las que nosotros buscamos.**/
                if (lastURL.Equals(s.AbsoluteUri) || s.AbsoluteUri.Equals(onceRedirectedURL))
                    break;
                else /* Si la URL anterior no es igual a la actual, guardar esta como
                      * la nueva anterior.**/
                    lastURL = s.AbsoluteUri;

                /* Mensajes en consola para comprobar que la URL se esté manejando
                 *  correctamente.**/
                Console.WriteLine("\n - URI DE RESPUESTA: " + s.AbsoluteUri);
                Console.WriteLine("\n - URI DE RESPUESTA: " + s.PathAndQuery);
                Console.WriteLine("\n - URI DE RESPUESTA: " + s.AbsolutePath);
                Console.WriteLine("\n - URI DE RESPUESTA: " + s.ToString());
                /* - AQUÍ ESTABLECERÍAMOS LOS HEADER Y ESO DE SER NECESARIO.*/

                // Display the status.
                // Aquí escribimos el estado en la consola.
                Console.WriteLine(response.StatusDescription);

                // Get the stream containing content returned by the server.
                // Aquí se guarda un archivo en memoria con la información obtenida en el response.
                dataStream = response.GetResponseStream();

                // Open the stream using a StreamReader for easy access.
                // Con este lector podemos iterar en el archivo.
                reader = new StreamReader(dataStream);

                // Read the content.
                // Guardamos el texto del archivo guardado en memoria para luego mostrarlo.
                responseFromServer = reader.ReadToEnd();

                // Display the content.
                //Console.WriteLine(responseFromServer);

                
                Console.WriteLine(" -> Response\n");

                pageCounter++;
                /* Esta cadena en el código fuente de la página indica
                 *  que ya no hay resultados de la búsqueda:
                 *      <p class=\"plp-no-results\">
                 * Así podemos darnos cuenta de que ya no hay más resultados.
                 * Aún así, esto lo podemos poner en un if para que no
                 *  haga procedimientos innecesarios y salga con un break.**/
            } while (!responseFromServer.Contains("<p class=\"plp-no-results\">"));
            //&& responseFromServer.Contains("<li class=\"category-facet-item \">"));
            // Cleanup the streams and the response.
            reader.Close();
            dataStream.Close();
            response.Close();
        }
        /* Método que mostrará los nombres de los productos encontrados
         *  en una lista de productos. El que se seleccione mostrará sus
         *  detalles en la segunda ventana.**/
        public static void ShowQueryResults()
        {

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
            catch (System.NullReferenceException){ } /* No hace nada en el catch porque quiero que
                       * de cualquier forma si se llega acá,
                       * salga el pop-up.**/
            /* Mostrar el pop-up si hubo una excepción o la búsqueda
             *  no es válida A.K.A. no hay texto en la textBox.
             *  
             * - SE UTILIZARÁ UN ERROR PROVIDER, pero se encontrará
             * en el Form1.cs.**/
            
            return false; /* No se pudo hacer la búsqueda.**/
        }
    }
}
