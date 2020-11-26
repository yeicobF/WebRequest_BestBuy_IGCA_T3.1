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
        public static void SearchQuery(TextBox _query, string sortBy)
        {
            string query = _query.Text;
            /* Cadena que guardará la forma de ordenamiento de la búsqueda.
             * Así es como se accede a un elemento de la ComboBox y se pasa
             *  a cadena. **/
            /* currentURL: Será el URL para buscar TODOS los elementos
             *  de la página actual. Se iterará hasta que ya no existan
             *  más páginas.
             * productURL: La URL de cada producto.
             * productName: El nombre de cada producto.**/
            string currentPageURL = "", productURL = "", productName = "";
            /* baseURL: URL base para TODAS las búsquedas de Best Buy. **/
            string baseURL = "https://www.bestbuy.com.mx/c/buscar-best-buy/buscar?";
            /* lastURL: El anterior URL para verificar que no se haga
             *  la solicitud al sitio más de una vez.
             * onceRedirectedURL: Para verificar que no nos vuelva a
             *  redirigir a un sitio en donde ya estuvimos.**/
            string webpageSourceCode = "", lastURL = "", onceRedirectedURL = "";
            /* bool para indicar que ya se encontró un searchRedirect, el
             *  cual indica que el enlace al que hicimos la petición
             *  nos redirigió a otra página.**/
            bool searchRedirectFound = false;
            /* bool para ver qué valores se agregarán al URL.**/
            bool addQuery = true, addSortBy = true, addPageNumber = true;
            /* Variable que guardará el índice actual en la consulta de productos de cada página. **/
            int currentIndex;
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
            /* Número de productos que se encuentran en la página actual.**/
            string productsInPage;
            HttpWebResponse response;
            Stream dataStream = null;
            StreamReader reader = null;

            /* Hay que limpiar la lista porque es estático por si
             *  tiene elementos.**/
            if (productList.Count > 0)
                productList.Clear();
            /* Necesito que haga el procedimiento antes que la verificación
             *  de la condición. Es decir, que primero haga la solicitud y
             *  luego verifique. Podría poner un bloque "Try-Catch" para
             *  cuando no encuentre más páginas en la búsqueda y salta una
             *  excepción, así terminar el proceso.**/
            /* Ciclo infinito hasta que no se encuentren más páginas o
             *  las páginas se empiecen a ciclar. Ahí es cuando se saldrá
             *  del ciclo con un break.**/
            while(true)
            {
                /* Inicializar el índice.**/
                currentIndex = 0;
                /* Si atrapa una excepción significa que no encontró
                 *  más páginas y podemos terminar el proceso.
                 * Solo terminará el ciclo cuando se entre al catch.
                 * Utilizo esta forma para guardar el URL para que sea
                 *  más fácil identificar el patrón. La cuestión es
                 *  que solo se puede hacer concatenando, entonces
                 *  antes de hacerlo cada vez tengo que reiniciar la cadena.**/
                currentPageURL = baseURL;
                /* Agregar de uno por uno si lo indicamos.
                 * Esto por si hay un URL que tengo que tomar
                 *  como el URI base y ya tiene estos elementos.
                 * Si los agrego el URL se empieza a agrandar mal.**/
                if (addQuery)
                    currentPageURL += "query=" + query;
                if(addSortBy)
                    currentPageURL += "&sort=" + sortBy;
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
                // Display the status.
                // Aquí escribimos el estado en la consola.
                //Console.WriteLine(response.StatusDescription);

                // Get the stream containing content returned by the server.
                // Aquí se guarda un archivo en memoria con la información obtenida en el response.
                dataStream = response.GetResponseStream();

                // Open the stream using a StreamReader for easy access.
                // Con este lector podemos iterar en el archivo.
                reader = new StreamReader(dataStream);

                // Read the content.
                // Guardamos el texto del archivo guardado en memoria para luego mostrarlo.
                webpageSourceCode = reader.ReadToEnd();
                /* Si es la última página, salir del ciclo y no instanciar un nuevo producto.**/
                if (IsLastWebPage(webpageSourceCode))
                    break;
                Uri uri = response.ResponseUri;

                /* Si nos redirige, irnos moviéndonos de página para
                 *  ver si hay más, si no, nos seguirá redirigiendo a la misma.**/
                if (uri.AbsoluteUri.Contains("searchRedirect") && !searchRedirectFound)
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
                    if(uri.AbsoluteUri.IndexOf("searchRedirect=" + query) + ("searchRedirect=" + query).Length == uri.AbsoluteUri.Length)
                    {
                        /* Aumentar el contador de página. Esto porque hará un continue y reiniciará
                         *  el ciclo sin avanzar al otro pageCounter.**/
                        pageCounter++;
                        if(uri.AbsoluteUri.Contains("&searchRedirect="))
                            /* Hay que tomar el nuevo URL pero sin el "&searchRedirect=".**/
                            baseURL = uri.AbsoluteUri.Substring(0, uri.AbsoluteUri.IndexOf("&searchRedirect="));
                        else
                            /* Hay URL que no tienen el ampersand "&" antes del parámetro searchRedirect.**/
                            if(uri.AbsoluteUri.Contains("searchRedirect="))
                                baseURL = uri.AbsoluteUri.Substring(0, uri.AbsoluteUri.IndexOf("searchRedirect="));
                        /* Si el enlace ya tiene algunos de los elementos de la consulta,
                         *  ya no agregarlos.**/
                        if (uri.AbsoluteUri.Contains("query="))
                            addQuery = false;
                        if (uri.AbsoluteUri.Contains("&sort="))
                            addSortBy = false;
                        if (uri.AbsoluteUri.Contains("&page="))
                            addPageNumber = false;
                        /* Continue para ya no seguir con los siguientes procesos.**/
                        continue;
                    }
                        
                    /* Reiniciar contador para buscar en las páginas hacia donde nos redirigieron.**/
                    pageCounter = 1;
                    /* Hay que guardar esta URL por si nos vuelve a redirigir, ya no repetir proceso.**/
                    onceRedirectedURL = uri.AbsoluteUri;
                    //currentPageURL = uri.AbsoluteUri + "&page=" + pageCounter;
                    /* Hacemos la base el nuevo enlace al que se redirigió. Esto 
                     * encontrando el último índice de la subcadena que lo indica.**/
                    baseURL = uri.AbsoluteUri.Substring(0, uri.AbsoluteUri.IndexOf("&searchRedirect=") + "&searchRedirect=".Length);
                    /* Indicar que ya se encontró el texto.**/
                    searchRedirectFound = true;
                    continue;
                }

                /* Si la URL anterior es igual a la actual, romper ciclo.
                 * De otra forma se estará accediendo a la misma una y otra vez
                 *  porque hay búsquedas que redirigen a otras URLs que no son
                 *  las que nosotros buscamos.**/
                if (lastURL.Equals(uri.AbsoluteUri) || uri.AbsoluteUri.Equals(onceRedirectedURL))
                    break;
                else /* Si la URL anterior no es igual a la actual, guardar esta como
                      * la nueva anterior.**/
                    lastURL = uri.AbsoluteUri;

                /* Mensajes en consola para comprobar que la URL se esté manejando
                 *  correctamente.**/
                Console.WriteLine("\n - URI DE RESPUESTA: " + uri.AbsoluteUri);
                Console.WriteLine("\n - URI DE RESPUESTA: " + uri.PathAndQuery);
                Console.WriteLine("\n - URI DE RESPUESTA: " + uri.AbsolutePath);
                Console.WriteLine("\n - URI DE RESPUESTA: " + uri.ToString());
                /* - AQUÍ ESTABLECERÍAMOS LOS HEADER Y ESO DE SER NECESARIO.*/

                

                // Display the content.
                //Console.WriteLine(webpageSourceCode);

                
                Console.WriteLine(" -> Response\n");
                /* Buscar el índice inicial en donde comienza toda la información.
                 * La información comienza con la cadena de texto: "window.INITIAL_PAGE_STATE".**/
                currentIndex = webpageSourceCode.IndexOf("window.INITIAL_PAGE_STATE") + "window.INITIAL_PAGE_STATE".Length;
                /* Buscar el índice de la cadena que nos deja el número de 
                 *  productos mostrados en la página.
                 *  El índice se busca desde donde encontramos que inicia la información.
                 *      \"totalCount\":númeroDeElementosEnPágina
                 *  **/
                /* Subcadena que irá del índice actual al último índice de la cadena
                 *  que contiene todo el código fuente del sitio web.
                 * Irá cambiando cada que cambie el índice.**/
                string substrInCurrIndex = webpageSourceCode.Substring(currentIndex, webpageSourceCode.Length - currentIndex);
                //currentIndex = substrInCurrIndex.IndexOf("\"totalCount\":") + "\"totalCount\":".Length;
                currentIndex = substrInCurrIndex.IndexOf(@"\""totalCount\"":") + @"\""totalCount\"":".Length;
                //currentIndex = substrInCurrIndex.IndexOf(@"\""totalCount\"":");
                
                /* No puedo hacer la búsqueda en la cadena original porque el índice es de
                 *  la subcadena creada, el cual es diferente porque es más pequeña
                 *  que la original. A partir de ahora se debe manejar la original.**/
                //productsInPage = webpageSourceCode.Substring(currentIndex, 1);
                /* Reiniciar la cadena.**/
                // productsInPage = "";
                productsInPage = substrInCurrIndex.Substring(currentIndex, 1);
                currentIndex++;
                /* Revisamos si el siguiente caracter también es un número.
                 * Si sí es un número, concatenarlo a la cadena del número
                 *  de elementos en la página. Luego se hará un cast a entero
                 *  para buscar todos los productos.
                 * 
                 * - FUENTE: [StackOverflow] Identify if a string is a number 
                 *    https://stackoverflow.com/questions/894263/identify-if-a-string-is-a-number#:~:text=If%20you%20want%20to%20know%20if%20a%20string%20is%20a,check%20if%20your%20parsing%20succeeded.
                 * **/
                if (int.TryParse(webpageSourceCode.Substring(currentIndex, 1), out _))
                    productsInPage += webpageSourceCode.Substring(currentIndex, 1);
                /* Ahora hacemos un ciclo para crear cada objeto con su nombre
                 *  y URL.**/
                for(int i = 0; i < int.Parse(productsInPage); i++)
                {
                    substrInCurrIndex = substrInCurrIndex.Substring(currentIndex, substrInCurrIndex.Length - currentIndex);
                    /* NOMBRE DEL PRODUCTO: \"title\":\"
                     * - Este se busca de la siguiente manera:
                     *      @"\""title\"":\"""
                     * -> Se pone el @ para no utilizar secuencias de escape,
                     * pero para las comillas no funciona, por lo que se tienen
                     * que poner unas comillas más para que cuenten. Por ejemplo,
                     * si queremos poner [ " hola " ] (sin contar los corchetes),
                     * tendremos que poner [ "" hola ""].
                     * 
                     * FUENTE: [StackOverflow] How to use “\” in a string without making it an escape sequence - C#?
                     *  https://stackoverflow.com/questions/1768023/how-to-use-in-a-string-without-making-it-an-escape-sequence-c
                     * **/
                    currentIndex = substrInCurrIndex.IndexOf(@"\""title\"":\""") + @"\""title\"":\""".Length;
                    /* Aquí tenemos la cadena del índice en donde comienza el título del producto
                     *  hasta el final de todo el código fuente.**/
                    substrInCurrIndex = substrInCurrIndex.Substring(currentIndex, substrInCurrIndex.Length - currentIndex);
                    /* Ahora hay que obtener el nombre del producto.
                     * Después del nombre del producto vienen estos caracteres:
                     *      [ \", ] por lo cual serán los delimitadores para la subcadena.**/
                    int lastIndex = substrInCurrIndex.IndexOf(@"\"",");
                    /* Ahora obtener el nombre desde el índice actual hasta el último.**/
                    productName = substrInCurrIndex.Substring(currentIndex, lastIndex);

                    /* Ahora hay que OBTENER EL URL DEL PRODUCTO. 
                     * Las incidencias son:
                     *      INICIO: \"seoPdpUrl\":\"
                     *      FINAL: \",
                     * **/
                    /* Ahora hacemos que la subcadena comience del último caracter
                     *  del nombre del producto para buscar desde ahí.**/
                    substrInCurrIndex.Substring(lastIndex, substrInCurrIndex.Length - lastIndex);
                    /* Ahora buscar la incidencia del URL.**/
                    currentIndex = substrInCurrIndex.IndexOf(@"\""seoPdpUrl\"":\""") + @"\""seoPdpUrl\"":\""".Length;
                    /* Aquí tenemos la cadena del índice en donde comienza el título del producto
                     *  hasta el final de todo el código fuente.**/
                    substrInCurrIndex = substrInCurrIndex.Substring(currentIndex, substrInCurrIndex.Length - currentIndex);
                    /* Ahora hay que obtener el nombre del producto.
                     * Después del nombre del producto vienen estos caracteres:
                     *      [ \", ] por lo cual serán los delimitadores para la subcadena.**/
                    lastIndex = substrInCurrIndex.IndexOf(@"\"",");
                    productURL = substrInCurrIndex.Substring(currentIndex, lastIndex);

                    /* Si llegó hasta aquí significa que no ha pasado la última
                     *  página y podemos instanciar un elemento de producto.**/
                    productList.Add(new Product(productName, productURL));
                    /* Imprimir para ver si los objetos se crearon correctamente.**/
                    Console.WriteLine("\n\n - PRODUCTO CREADO: Nombre: " + productList.ElementAt(productList.Count - 1).Name + ", URL: " + productList.ElementAt(productList.Count - 1).URL);
                }

                pageCounter++;
                /* Esta cadena en el código fuente de la página indica
                 *  que ya no hay resultados de la búsqueda:
                 *      <p class=\"plp-no-results\">
                 * Así podemos darnos cuenta de que ya no hay más resultados.
                 * Aún así, esto lo podemos poner en un if para que no
                 *  haga procedimientos innecesarios y salga con un break.**/
            }
            //&& webpageSourceCode.Contains("<li class=\"category-facet-item \">"));
            // Cleanup the streams and the response.
            reader.Close();
            dataStream.Close();
            response.Close();
        }
        /* Método que verifica si el sitio web actual es el último.
         * Si encuentra lo que está en el return significa que ya se
         *  encuentra actualmente en la última página.**/
        private static bool IsLastWebPage(string webpageSourceCode)
        {
            return webpageSourceCode.Contains("<p class=\"plp-no-results\">");
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
