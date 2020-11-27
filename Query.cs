using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;

/* CLASE PARA HACER LA BÚSQUEDA DE ACUERDO A LA QUERY ESPECÍFICADA:
 *  - Elemento/Producto a buscar
 *  - Parámetro de ordenación de búsqueda
 * Al hacer la búsqueda se guardará en un arreglo de Product
 *  (Product[] products) el nombre y el enlace de cada uno.
 *  Ya que se seleccione el que se quiera mostrar, se ingresará
 *      al enlace y se obtendrá su demás información.
 * - Jueves, 26 de noviembre del 2020
 * 
 * - LA CLASE SERÁ ESTÁTICA PORQUE NO REQUIERE SER INSTANCIADA.
 *    Solo se ocupará su método de búsqueda, pero la información
 *      será guardada en la clase Product, de la cual se manejará
 *      un arreglo estático para que sea global.
 *      
 * - Viernes, 27/NOV [04:22AM] quité comentarios y código innecesario,
 * y separé el código en métodos para que quedara más ordenado.
 * [EL ANTIGUO CÓDIGO QUEDÓ EN LA CLASE OldQuery, ya que tiene
 *  comentarios útiles].
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
        /* Variables estáticas que podrán ser accedidas en
         *  todos los métodos.**/
        private static string baseURL, query, sortBy;
        /* Método que hará la búsqueda deseada recibiendo como
         *  parámetros la "query", que es la búsqueda, y la forma
         *  de ordenar "sortBy". 
         *  
         * No quiero ser ambiguo, por lo que recibiré los
         *  parámetros directamente del tipo de dato, y
         *  crearé variables de cadenas ya específicas. De 
         *  esta manera se da a entender mejor el proceso. 
         * Recibimos un arreglo de objetos para tener el
         *  combobox y el ErrorProvider para mostrar un avis
         *  cuando se haya redirigido la búsqueda.**/
        public static void SearchQuery(TextBox _query, string _sortBy, Object[] errorAndCombobox)
        {
            /* Guardamos el texto de la consulta como cadena.**/
            query = _query.Text;
            /* baseURL: URL base para TODAS las búsquedas de Best Buy. **/
            baseURL = "https://www.bestbuy.com.mx/c/buscar-best-buy/buscar?";
            /* Cadena que guardará la forma de ordenamiento de la búsqueda.
             * Así es como se accede a un elemento de la ComboBox y se pasa
             *  a cadena. **/
            sortBy = _sortBy;
            /* URI del URL actual. URL es un tipo de URI.**/
            Uri currentURI; 
            /* currentPageURL: Será el URL para buscar TODOS los elementos
             *  de la página actual. Se iterará hasta que ya no existan
             *  más páginas.**/
            string currentPageURL = "";
            /* lastURL: El anterior URL para verificar que no se haga
             *  la solicitud al sitio más de una vez.
             * onceRedirectedURL: Para verificar que no nos vuelva a
             *  redirigir a un sitio en donde ya estuvimos.**/
            string webpageSourceCode = "", lastURL = "", onceRedirectedURL = "";

            /* bool para ver qué valores se agregarán al URL.**/
            bool addQuery = true, addSortBy = true, addPageNumber = true;

            /* bool para indicar que ya se encontró un searchRedirect, el
             *  cual indica que el enlace al que hicimos la petición
             *  nos redirigió a otra página.**/
            bool searchRedirectFound = false;

            /* Iterador para las páginas de los sitios web.
             * Los números de página comienzan desde el 1.**/
            int currentPageNumber = 1;

            /* Hay que limpiar la lista porque es estático por si
             *  tiene elementos.**/
            if (productList.Count > 0)
                productList.Clear();
            /* Ciclo infinito hasta que no se encuentren más páginas o
             *  las páginas se empiecen a ciclar. Ahí es cuando se saldrá
             *  del ciclo con un break.**/
            while(true)
            {
                /* Agregamos los parámetros a la URL.**/
                AddQueryElements(ref currentPageURL, addQuery, addSortBy, addPageNumber, currentPageNumber);
                Console.WriteLine("\n - NÚMERO DE PÁGINA ACTUAL: [" + currentPageNumber + "]\n - URL ACTUAL: " + currentPageURL);

                /* Hacemos el WebRequest y guardamos la información de la URI.**/
                currentURI = MakeWebRequest(currentPageURL, ref webpageSourceCode);

                /* Si es la última página, salir del ciclo y no instanciar un nuevo producto.**/
                if (IsLastWebPage(webpageSourceCode))
                    break;


                /* Así mostramos la información del URL actual.**/
                ShowURIInfo(currentURI);

                /* Revisará si la página actual ya fue accedida alguna vez.
                 * Si sí ha sido accedida se romperá el ciclo, y si no, dentro
                 * del método se modificará la variable lastURL, por eso pasa
                 * por referencia.**/
                if (CheckIfAccessedPageAlreadyAccessed(currentURI, ref lastURL, onceRedirectedURL))
                    break;
                /* Si se encuentra con la cadena "searchRedirect" significa que el
                 *  enlace ha sido redirigido a otro. Se hace el procedimiento
                 *  requerido y devuelve un FALSE si es la primera vez que sucede.
                 * Si ya se había redirigido esta página anteriormente, regresa TRUE
                 *  y se hace un break para salir del ciclo.**/
                if (CheckIfURLWasRedirectedBefore(currentURI, ref searchRedirectFound, ref onceRedirectedURL,
                                                  ref addQuery, ref addSortBy, ref addPageNumber,
                                                  errorAndCombobox))
                    break;

                /* Aquí ya hace todo el proceso de búsqueda de los productos al recorrer
                 *  todos los elementos de la página y guardar su nombre y URL.**/
                GetAllElementsFromCurrentPageNumber(webpageSourceCode);

                currentPageNumber++;
                /* Esta cadena en el código fuente de la página indica
                 *  que ya no hay resultados de la búsqueda:
                 *      <p class=\"plp-no-results\">
                 * Así podemos darnos cuenta de que ya no hay más resultados.
                 * Aún así, esto lo podemos poner en un if para que no
                 *  haga procedimientos innecesarios y salga con un break.**/
            }
        }
        /* Método que hará el webRequest y regresará la URI
         *  obtenida junto con el código fuente de la página.**/
        private static Uri MakeWebRequest(string currentPageURL, ref string webpageSourceCode)
        {
            // Create a request for the URL. 		
            //WebRequest request = WebRequest.Create("AQUÍ VA LA URL");
            WebRequest request = WebRequest.Create(currentPageURL);
            // If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials;

            // Get the response.
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            // Display the status.
            // Aquí escribimos el estado en la consola.
            //Console.WriteLine(response.StatusDescription);

            // Get the stream containing content returned by the server.
            // Aquí se guarda un archivo en memoria con la información obtenida en el response.
            Stream dataStream = response.GetResponseStream();

            // Open the stream using a StreamReader for easy access.
            // Con este lector podemos iterar en el archivo.
            StreamReader reader = new StreamReader(dataStream);

            // Read the content.
            // Guardamos el texto del archivo guardado en memoria para luego mostrarlo.
            /* Aquí se guardará como cadena el código fuente de TODA la página
             *  actual del sitio web.**/
            webpageSourceCode = reader.ReadToEnd();

            // Cleanup the streams and the response.
            reader.Close();
            dataStream.Close();
            response.Close();

            /* Return URI information.**/
            return response.ResponseUri;
        }
        /* Método que revisará si la página actual ya fue accedida
         *  en la query anterior o fue accedida alguna vez. Esto
         *  para que no se repita una y otra vez la misma búsqueda.**/
        private static bool CheckIfAccessedPageAlreadyAccessed(Uri currentURI, ref string lastURL, string onceRedirectedURL)
        {
            /* Si la URL anterior es igual a la actual, romper ciclo.
                 * De otra forma se estará accediendo a la misma una y otra vez
                 *  porque hay búsquedas que redirigen a otras URLs que no son
                 *  las que nosotros buscamos.**/
            if (lastURL.Equals(currentURI.AbsoluteUri) || currentURI.AbsoluteUri.Equals(onceRedirectedURL))
                /* Si ya ha sido accesida regresar true para que se haga un break en el ciclo.**/
                return true;
            /* Si no ha sido accedida asignaremos la URL actual a la variable de la anterior.
             * Por esto se pasa por referencia la variable.**/
            else /* Si la URL anterior no es igual a la actual, guardar esta como
                      * la nueva anterior.**/
                lastURL = currentURI.AbsoluteUri;
            /* Si no entra al if llegará hasta acá, por lo que regresamos un false.**/
            return false;
        }
        /* Método que revisa si la webRequest fue redirigida y si ya
         *  había sido redirigida con anterioridad.**/
        private static bool CheckIfURLWasRedirectedBefore(Uri currentURI, ref bool searchRedirectFound, ref string onceRedirectedURL,
                                                          ref bool addQuery, ref bool addSortBy, ref bool addPageNumber,
                                                          Object[] errorAndCombobox)
        {
            /* Si nos redirige, irnos moviéndonos de página para
                 *  ver si hay más, si no, nos seguirá redirigiendo a la misma.**/
            if (currentURI.AbsoluteUri.Contains("searchRedirect") && !searchRedirectFound)
            {
                /* Si redirigió a otra página, mostrar un cuadro de error (aunque
                 *  no es un error sino un aviso) informando que dado a que
                 *  se redirigió la búsqueda, los filtros de búsqueda no se han
                 *  podido aplicar.**/
                ErrorProvider error = (ErrorProvider)errorAndCombobox[0];
                ComboBox comboBox_sortBy = (ComboBox)errorAndCombobox[1];
                error.SetError(comboBox_sortBy, "AVISO: El sitio web de Best Buy ha"
                                            + "\n redirigido la página a un URL distinto."
                                            + "\nA causa de esto, el filtro de ordenación"
                                            + "\n NO se ha aplicado a la búsqueda.");

                /* Hay ocasiones en que el "searchRedirect" está al final de
                 *  la URL y no al inicio. En esos casos no podemos concatenarle
                 *  la consulta porque crecerá exponencialmente. Ahí simplemente
                 *  lo ignoramos y continuamos con el conteo normal de páginas.
                 *  Ese al menos fue el comportamiento que vi con la búsqueda
                 *      de "Bose".
                 *  Si el último índice de esa cadena es igual al tamaño
                 *      de la cadena del URL, ignorar y continuar con la búsqueda
                 *      en la siguiente página.**/
                if (currentURI.AbsoluteUri.IndexOf("searchRedirect=" + query) + ("searchRedirect=" + query).Length == currentURI.AbsoluteUri.Length)
                {
                    if (currentURI.AbsoluteUri.Contains("&searchRedirect="))
                        /* Hay que tomar el nuevo URL pero sin el "&searchRedirect=".**/
                        baseURL = currentURI.AbsoluteUri.Substring(0, currentURI.AbsoluteUri.IndexOf("&searchRedirect="));
                    else
                        /* Hay URL que no tienen el ampersand "&" antes del parámetro searchRedirect.**/
                        if (currentURI.AbsoluteUri.Contains("searchRedirect="))
                        baseURL = currentURI.AbsoluteUri.Substring(0, currentURI.AbsoluteUri.IndexOf("searchRedirect="));
                    /* Si el enlace ya tiene algunos de los elementos de la consulta,
                     *  ya no agregarlos.**/
                    if (currentURI.AbsoluteUri.Contains("query="))
                        addQuery = false;
                    if (currentURI.AbsoluteUri.Contains("&sort="))
                        addSortBy = false;
                    if (currentURI.AbsoluteUri.Contains("&page="))
                        addPageNumber = false;
                }
                else
                    /* Si vuelve a redirigir después de hacerlo con anterioridad,
                     *  regresar true para hacer un break en el ciclo.*/
                    if (searchRedirectFound)
                        return true;
                /* Hay que guardar esta URL por si nos vuelve a redirigir, ya no repetir proceso.**/
                onceRedirectedURL = currentURI.AbsoluteUri;
                /* Indicar que ya se encontró el texto y ya ha sido redirigida.**/
                searchRedirectFound = true;
            }

            /* Regresa FALSE porque es la primera vez que se había redirigido la página.
             * O no se encontró la cadena, o no había sido redirigida la página
             *  anteriormente.**/
            return false;
        }

        /* Método que establece la Query actual dependiendo de
         *  si se puede o no.
         * El que cambiará será el URL de la página actual, y
         *  se agregarán los parámetros de consulta si las
         *  variables bool lo indican.
         * El número de página siempre se va a modificar.**/
        private static void AddQueryElements(ref string currentPageURL,
                                             bool addQuery, bool addSortBy, bool addPageNumber,
                                             int currentPageNumber)
        {
            /* Utilizo esta forma para guardar el URL para que sea
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
            if (addSortBy)
                currentPageURL += "&sort=" + sortBy;
            if (addPageNumber)
                currentPageURL += "&page=" + currentPageNumber;
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
                currentPageURL = currentPageURL.Substring(0, pageNumberIndex) + currentPageNumber
                    + currentPageURL.Substring(pageNumberIndex + 1, currentPageURL.Length - pageNumberIndex - 1);
            }
        }

        /* Método que obtendrá la información de un elemento
         *  que estará delimitado por una cadena inicial y una final.
         * Al final regresa la cadena obtenida.
         * Aquí se podrán obtener el nombre del producto, url,
         *  y más dependiendo los parámetros que se envíen.
         * Primero verificará si existe el elemento, y si sí existe
         *  entonces obtenerlo.
         * Pasan también el índice actual y el último como referencia.
         * **/
        public static string GetElementFromWebPage(ref string substrInCurrIndex, string initialDelimiter,
                                                   ref int currentIndex, ref int lastIndex)
        {
            /* The found element string.**/
            string element;

            substrInCurrIndex = substrInCurrIndex.Substring(currentIndex, substrInCurrIndex.Length - currentIndex);
            /* DELIMITADOR DEL PRODUCTO: initialDelimiter **/
            currentIndex = substrInCurrIndex.IndexOf(initialDelimiter) + initialDelimiter.Length;
            /* Aquí tenemos la cadena del índice en donde comienza el título del producto
             *  hasta el final de todo el código fuente.**/
            substrInCurrIndex = substrInCurrIndex.Substring(currentIndex, substrInCurrIndex.Length - currentIndex);
            /* Ahora hay que obtener el nombre del producto.
             * Después del nombre del producto vienen estos caracteres:
             *      [ \", ] por lo cual serán los delimitadores para la subcadena.
             * Esto indica el final de la cadena con el elemento que buscamos.**/
            lastIndex = substrInCurrIndex.IndexOf(@"\"",");
            /* Ahora obtener el nombre desde el índice actual hasta el último.
             * Como la nueva subcadena ya comienza desde el índice actual entonces
             *  el primer parámetro de la subcadena será 0.
             *  - Esto quiere decir que la cadena empieza desde el inicio
             *  del nombre del producto hasta su fin.**/
            element = substrInCurrIndex.Substring(0, lastIndex);
            /* Ahora hacemos que la subcadena comience del último caracter
             *  del nombre del producto para buscar desde ahí.**/
            substrInCurrIndex.Substring(lastIndex, substrInCurrIndex.Length - lastIndex);
            /* Indicamos que el índice actual  el último encontrado son 0, ya que
             *  acabamos de crear una nueva subcadena.**/
            currentIndex = lastIndex = 0;

            /* Regresar la cadena con el elemento encontrado.**/
            return element;
        }
        /* Método que obtiene todos los elementos de la página
         *  actual del sitio web y los guarda en la lista de
         *  productos.
         * Hay que recordar que la lista de productos es estática y
         *  accesible desde cualquier parte del código actual.**/
        private static void GetAllElementsFromCurrentPageNumber(string webpageSourceCode)
        {
            /* Declaramos el índice inicial y final con los que se manejarán
             *  las subcadenas para guardar el nombre y el URL de cada
             *  producto.
             * 
             * - currentIndex: Variable que guardará el índice actual de la cadena en la consulta
             *  de productos de cada página. 
             * - lastIndex: Guardará el último índice de incidencia para así
             *  obtener el texto del elemento buscado con una
             *  subcadena que va desde currentIndex hasta el lastIndex**/
            int currentIndex, lastIndex;
            /* Definimos las cadenas en donde se guardarán tanto 
             *  el nombre de cada producto como su URL.
             * 
             * - productURL: La URL de cada producto.
             * - productName: El nombre de cada producto.
             * - substrInCurrIndex: Subcadena que irá del índice actual al último índice de la cadena
             *  que contiene todo el código fuente del sitio web. Irá cambiando cada que cambie el índice.**/
            string productName, productURL, substrInCurrIndex;

            /* Buscar el índice inicial en donde comienza toda la información.
             * La información comienza con la cadena de texto: "window.INITIAL_PAGE_STATE".**/
            currentIndex = webpageSourceCode.IndexOf("window.INITIAL_PAGE_STATE") + "window.INITIAL_PAGE_STATE".Length;
                
            /* Obtenemos la subcadena incial desde donde empieza la secuencia
                *  con los datos de cada objeto hasta el final del código
                *  fuente original.**/
            substrInCurrIndex = webpageSourceCode.Substring(currentIndex, webpageSourceCode.Length - currentIndex);

            /* Ciclo que seguirá iterando si encuentra desde el índice actual el 
             *  delimitador de nombre de objeto, lo que significa que aún
             *  hay más elementos.
             * Aquí se recorren todos los elementos de la página hasta que ya no hayan más.**/
            while (IsElementInWebPage(substrInCurrIndex, @"\""title\"":\"""))
            {
                /* Inicializamos en 0 los índices, ya que es el primer índice de la subcadena actual.**/
                lastIndex = currentIndex = 0;
                /* -> Se pone el @ para no utilizar secuencias de escape,
                    * pero para las comillas no funciona, por lo que se tienen
                    * que poner unas comillas más para que cuenten. Por ejemplo,
                    * si queremos poner [ " hola " ] (sin contar los corchetes),
                    * tendremos que poner [ "" hola ""].
                    * 
                    * FUENTE: [StackOverflow] How to use “\” in a string without making it an escape sequence - C#?
                    *  https://stackoverflow.com/questions/1768023/how-to-use-in-a-string-without-making-it-an-escape-sequence-c
                    * **/
                /* Obtenemos el nombre del producto.**/
                productName = GetElementFromWebPage(ref substrInCurrIndex, @"\""title\"":\""", ref currentIndex, ref lastIndex);
                /* Obtenemos el URL del producto.**/    
                productURL = GetElementFromWebPage(ref substrInCurrIndex, @"\""seoPdpUrl\"":\""", ref currentIndex, ref lastIndex);
                    
                /* Si llegó hasta aquí significa que no ha pasado la última
                    *  página y podemos instanciar un elemento de producto.**/
                productList.Add(new Product(productName, productURL));
                /* Imprimir para ver si los objetos se crearon correctamente.**/
                Console.WriteLine("\n\n - PRODUCTO CREADO: Nombre: " + productList.ElementAt(productList.Count - 1).Name + ", URL: " + productList.ElementAt(productList.Count - 1).URL);
            }
        }
        /* Método que revisa si el elemento deseado se encuentra en el
         *  código fuente de la página web dada su cadena actual.**/
        public static bool IsElementInWebPage(string webpageSourceCoude, string element)
        {
            return webpageSourceCoude.Contains(element);
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
        /* Método para decidir cuál será el método de ordenación
         *  que se enviará como parámetro en la petición del enlace. **/
        public static string GetSortedByQuery(ComboBox sortByList)
        {
            string sortBy = "";

            /* LISTA DE ELEMENTOS DE LA LISTA
             *  CON SU EQUIVALENCIA EN LA QUERY
             *  DE LA PÁGINA WEB DE Best Buy:
             * - Mejor Coincidencia [Best-Match]
               - Precio de Bajo a Alto [Price-Low-To-High]
               - Precio de Alto a Bajo [Price-High-To-Low]
               - Marcas A-Z [Brand-A-Z]
               - Marcas Z-A [Brand-Z-A]
               - Fecha de lanzamiento [Release-Date]
               - Recién Llegados [New-Arrivals]
               - Título A-Z [Title-A-Z]
               - Título Z-A [Title-Z-A]
            **/

            switch (sortByList.SelectedItem)
            {
                case "Mejor Coincidencia": sortBy = "Best-Match"; break;
                case "Precio de Bajo a Alto": sortBy = "Price-Low-To-High"; break;
                case "Precio de Alto a Bajo": sortBy = "Price-High-To-Low"; break;
                case "Marcas A-Z": sortBy = "Brand-A-Z"; break;
                case "Marcas Z-A": sortBy = "Brand-Z-A"; break;
                case "Fecha de lanzamiento": sortBy = "Release-Date"; break;
                case "Recién Llegados": sortBy = "New-Arrivals"; break;
                case "Título A-Z": sortBy = "Title-A-Z"; break;
                case "Título Z-A": sortBy = "Title-Z-A"; break;
            }

            return sortBy;
        }
        /* Método para mostrar la información del Uri enviado
         *  por parámetro.**/
        static private void ShowURIInfo(Uri URI)
        {
            /* Mensajes en consola para comprobar que la URL se esté manejando
             *  correctamente.**/
            Console.WriteLine("\n - URI DE RESPUESTA: " + URI.AbsoluteUri);
            //Console.WriteLine("\n - URI DE RESPUESTA: " + URI.PathAndQuery);
            //Console.WriteLine("\n - URI DE RESPUESTA: " + URI.AbsolutePath);
            //Console.WriteLine("\n - URI DE RESPUESTA: " + URI.ToString());
        }
    }
}
