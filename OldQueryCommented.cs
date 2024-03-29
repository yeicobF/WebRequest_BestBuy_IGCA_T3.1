﻿using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;

/* CLASE EN DONDE PONDRÉ EL ANTIGUO CÓDIGO
 *  DE LA CLASE Query, ya que hice grandes
 *  modificaciones y limpié código que ya
 *  no es necesario, pero quiero dejar
 *  constancia de los comentarios que hice
 *  porque fueron muchos y me serán
 *  útiles.
 *  
 *  Viernes, 27 de noviembre del 2020 [03:09AM]
 *  **/

namespace _T3._1__WebRequest_con_BestBuy
{
    static class OldQueryCommented
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
         *  esta manera se da a entender mejor el proceso. 
         * Recibimos un arreglo de objetos para tener el
         *  combobox y el ErrorProvider para mostrar un avis
         *  cuando se haya redirigido la búsqueda.**/
        public static void SearchQuery(TextBox _query, string sortBy, Object[] errorAndCombobox)
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
            /* currentIndex: Variable que guardará el índice actual de la cadena en la consulta
             *  de productos de cada página. 
             * lastIndex: Guardará el último índice de incidencia para así
             *  obtener el texto del elemento buscado con una
             *  subcadena que va desde currentIndex hasta el lastIndex**/
            int currentIndex, lastIndex;
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
            while (true)
            {
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
                if (addSortBy)
                    currentPageURL += "&sort=" + sortBy;
                if (addPageNumber)
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
                Console.WriteLine("\n - URL ACTUAL: " + currentPageURL);// + "\n -> Request\n");

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

                /* Así mostramos la información del URL actual.**/
                ShowUriInfo(uri);

                /* Si la URL anterior es igual a la actual, romper ciclo.
                 * De otra forma se estará accediendo a la misma una y otra vez
                 *  porque hay búsquedas que redirigen a otras URLs que no son
                 *  las que nosotros buscamos.**/
                if (lastURL.Equals(uri.AbsoluteUri) || uri.AbsoluteUri.Equals(onceRedirectedURL))
                    break;
                else /* Si la URL anterior no es igual a la actual, guardar esta como
                      * la nueva anterior.**/
                    lastURL = uri.AbsoluteUri;

                /* Si nos redirige, irnos moviéndonos de página para
                 *  ver si hay más, si no, nos seguirá redirigiendo a la misma.**/
                if (uri.AbsoluteUri.Contains("searchRedirect") && !searchRedirectFound)
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
                    if (uri.AbsoluteUri.IndexOf("searchRedirect=" + query) + ("searchRedirect=" + query).Length == uri.AbsoluteUri.Length)
                    {
                        if (uri.AbsoluteUri.Contains("&searchRedirect="))
                            /* Hay que tomar el nuevo URL pero sin el "&searchRedirect=".**/
                            baseURL = uri.AbsoluteUri.Substring(0, uri.AbsoluteUri.IndexOf("&searchRedirect="));
                        else
                            /* Hay URL que no tienen el ampersand "&" antes del parámetro searchRedirect.**/
                            if (uri.AbsoluteUri.Contains("searchRedirect="))
                            baseURL = uri.AbsoluteUri.Substring(0, uri.AbsoluteUri.IndexOf("searchRedirect="));
                        /* Si el enlace ya tiene algunos de los elementos de la consulta,
                         *  ya no agregarlos.**/
                        if (uri.AbsoluteUri.Contains("query="))
                            addQuery = false;
                        if (uri.AbsoluteUri.Contains("&sort="))
                            addSortBy = false;
                        if (uri.AbsoluteUri.Contains("&page="))
                            addPageNumber = false;
                        /* Continue para ya no seguir con los siguientes procesos.
                         * 
                         * - NO SE NECESITA EL CONTINUE PORQUE NECESITA REVISAR EL SITIO ACTUAL,
                         * luego ya cambiará a la página 2 (o la siguiente).**/
                        //continue;
                    }
                    else
                        /* Si vuelve a redirigir a la misma página, break.**/
                        if (searchRedirectFound)
                        break;
                    /* Reiniciar contador para buscar en las páginas hacia donde nos redirigieron.
                     * 
                     * - Ya vi que volverá a redirigir a la misma, así que lo mejor sería
                     * aumentar el contador.**/
                    // pageCounter = 1;
                    /* Aumentar el contador de página. Esto porque hará un continue y reiniciará
                     *  el ciclo sin avanzar al otro pageCounter.
                     * - ESTE YA SE AUMENTA AL FINAL DE LA ITERACIÓN GENERAL DEL WHILE PRINCIPAL.**/
                    // pageCounter++;
                    /* Hay que guardar esta URL por si nos vuelve a redirigir, ya no repetir proceso.**/
                    onceRedirectedURL = uri.AbsoluteUri;
                    //currentPageURL = uri.AbsoluteUri + "&page=" + pageCounter;

                    /* Hacemos la base el nuevo enlace al que se redirigió. Esto 
                     * encontrando el último índice de la subcadena que lo indica.
                     * 
                     * - Ya no es necesario hacer esto porque ya se hace arriba.**/
                    // baseURL = uri.AbsoluteUri.Substring(0, uri.AbsoluteUri.IndexOf("&searchRedirect=") + "&searchRedirect=".Length);

                    /* Indicar que ya se encontró el texto.**/
                    searchRedirectFound = true;
                    /*
                     * - NO SE NECESITA EL CONTINUE PORQUE NECESITA REVISAR EL SITIO ACTUAL,
                     * luego ya cambiará a la página 2 (o la siguiente).**/
                    //continue;
                }


                /* - AQUÍ ESTABLECERÍAMOS LOS HEADER Y ESO DE SER NECESARIO.*/
                //ShowUriInfo(uri);


                // Display the content.
                //Console.WriteLine(webpageSourceCode);


                //Console.WriteLine(" -> Response\n");
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


                /* Ya vi que el totalCount no indica el número de elementos de la página actual,
                 *  sino el número de elementos en todas las páginas de esa búsqueda.
                 * - Mejor haré una condición diferente de si ya no existen objetos o algo así.**/
                //currentIndex = substrInCurrIndex.IndexOf(@"\""totalCount\"":") + @"\""totalCount\"":".Length;
                //currentIndex = substrInCurrIndex.IndexOf(@"\""totalCount\"":");

                /* No puedo hacer la búsqueda en la cadena original porque el índice es de
                 *  la subcadena creada, el cual es diferente porque es más pequeña
                 *  que la original. A partir de ahora se debe manejar la original.**/
                //productsInPage = webpageSourceCode.Substring(currentIndex, 1);
                /* Reiniciar la cadena.**/
                // productsInPage = "";
                //productsInPage = substrInCurrIndex.Substring(currentIndex, 1);
                //currentIndex++;
                /* Revisamos si el siguiente caracter también es un número.
                 * Si sí es un número, concatenarlo a la cadena del número
                 *  de elementos en la página. Luego se hará un cast a entero
                 *  para buscar todos los productos.
                 * 
                 * - FUENTE: [StackOverflow] Identify if a string is a number 
                 *    https://stackoverflow.com/questions/894263/identify-if-a-string-is-a-number#:~:text=If%20you%20want%20to%20know%20if%20a%20string%20is%20a,check%20if%20your%20parsing%20succeeded.
                 * **/
                //if (int.TryParse(substrInCurrIndex.Substring(currentIndex, 1), out _))
                //    productsInPage += substrInCurrIndex.Substring(currentIndex, 1);
                /* Ahora hacemos un ciclo para crear cada objeto con su nombre
                 *  y URL.**/
                //for(int i = 0; i < int.Parse(productsInPage); i++)

                /* Ciclo que seguirá iterando si encuentra desde el índice actual el 
                 *  delimitador de nombre de objeto, lo que significa que aún
                 *  hay más elementos.**/
                //while(IsElementInWebPage(substrInCurrIndex, @"\""title\"":\"""))
                while (substrInCurrIndex.Contains(@"\""title\"":\"""))
                {
                    ///* Inicializamos 0, ya que es el primer elemento de la subcadena actual.**/
                    //lastIndex = currentIndex = 0;
                    ///* Obtenemos el nombre del producto.**/
                    //productName = GetElementFromWebPage(ref substrInCurrIndex, @"\""title\"":\""", ref currentIndex, ref lastIndex);
                    //productURL = GetElementFromWebPage(ref substrInCurrIndex, @"\""seoPdpUrl\"":\""", ref currentIndex, ref lastIndex);

                    /* - CÓDIGO ANTERIOR: **/
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
                    lastIndex = substrInCurrIndex.IndexOf(@"\"",");
                    /* Ahora obtener el nombre desde el índice actual hasta el último.
                     * Como la nueva subcadena ya comienza desde el índice actual entonces
                     *  el primer parámetro de la subcadena será 0.
                     *  - Esto quiere decir que la cadena empieza desde el inicio
                     *  del nombre del producto hasta su fin.**/
                    productName = substrInCurrIndex.Substring(0, lastIndex);

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
                    /* Al igual que el nombre, el enlace se guarda desde el 0 hasta el último
                     *  índice, ya que la cadena actualmente comienza desde el inicio
                     *  del enlace.**/
                    productURL = substrInCurrIndex.Substring(0, lastIndex);

                    /* Llevar la subcadena al último índice de incidencia
                     *  para que el while verifique correctamente si
                     *  se encuentra algún producto restante o no.**/
                    substrInCurrIndex.Substring(lastIndex, substrInCurrIndex.Length - lastIndex);
                    /* Si llegó hasta aquí significa que no ha pasado la última
                     *  página y podemos instanciar un elemento de producto.**/
                    // productList.Add(new Product(productName, productURL));
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

            /* CÓDIGO BASE PARA HACER ESTE MÉTODO:**/

            //substrInCurrIndex = substrInCurrIndex.Substring(currentIndex, substrInCurrIndex.Length - currentIndex);
            ///* NOMBRE DEL PRODUCTO: \"title\":\"
            // * - Este se busca de la siguiente manera:
            // *      @"\""title\"":\"""
            // * -> Se pone el @ para no utilizar secuencias de escape,
            // * pero para las comillas no funciona, por lo que se tienen
            // * que poner unas comillas más para que cuenten. Por ejemplo,
            // * si queremos poner [ " hola " ] (sin contar los corchetes),
            // * tendremos que poner [ "" hola ""].
            // * 
            // * FUENTE: [StackOverflow] How to use “\” in a string without making it an escape sequence - C#?
            // *  https://stackoverflow.com/questions/1768023/how-to-use-in-a-string-without-making-it-an-escape-sequence-c
            // * **/
            //currentIndex = substrInCurrIndex.IndexOf(@"\""title\"":\""") + @"\""title\"":\""".Length;
            ///* Aquí tenemos la cadena del índice en donde comienza el título del producto
            // *  hasta el final de todo el código fuente.**/
            //substrInCurrIndex = substrInCurrIndex.Substring(currentIndex, substrInCurrIndex.Length - currentIndex);
            ///* Ahora hay que obtener el nombre del producto.
            // * Después del nombre del producto vienen estos caracteres:
            // *      [ \", ] por lo cual serán los delimitadores para la subcadena.**/
            //int lastIndex = substrInCurrIndex.IndexOf(@"\"",");
            ///* Ahora obtener el nombre desde el índice actual hasta el último.
            // * Como la nueva subcadena ya comienza desde el índice actual entonces
            // *  el primer parámetro de la subcadena será 0.
            // *  - Esto quiere decir que la cadena empieza desde el inicio
            // *  del nombre del producto hasta su fin.**/
            //productName = substrInCurrIndex.Substring(0, lastIndex);

            ///* Ahora hay que OBTENER EL URL DEL PRODUCTO. 
            // * Las incidencias son:
            // *      INICIO: \"seoPdpUrl\":\"
            // *      FINAL: \",
            // * **/
            ///* Ahora hacemos que la subcadena comience del último caracter
            // *  del nombre del producto para buscar desde ahí.**/
            //substrInCurrIndex.Substring(lastIndex, substrInCurrIndex.Length - lastIndex);
            ///* Ahora buscar la incidencia del URL.**/
            //currentIndex = substrInCurrIndex.IndexOf(@"\""seoPdpUrl\"":\""") + @"\""seoPdpUrl\"":\""".Length;
            ///* Aquí tenemos la cadena del índice en donde comienza el título del producto
            // *  hasta el final de todo el código fuente.**/
            //substrInCurrIndex = substrInCurrIndex.Substring(currentIndex, substrInCurrIndex.Length - currentIndex);
            ///* Ahora hay que obtener el nombre del producto.
            // * Después del nombre del producto vienen estos caracteres:
            // *      [ \", ] por lo cual serán los delimitadores para la subcadena.**/
            //lastIndex = substrInCurrIndex.IndexOf(@"\"",");
            ///* Al igual que el nombre, el enlace se guarda desde el 0 hasta el último
            // *  índice, ya que la cadena actualmente comienza desde el inicio
            // *  del enlace.**/
            //productURL = substrInCurrIndex.Substring(0, lastIndex);

            ///* Llevar la subcadena al último índice de incidencia
            // *  para que el while verifique correctamente si
            // *  se encuentra algún producto restante o no.**/
            //substrInCurrIndex.Substring(lastIndex, substrInCurrIndex.Length - lastIndex);
            ///* Si llegó hasta aquí significa que no ha pasado la última
            // *  página y podemos instanciar un elemento de producto.**/
            //productList.Add(new Product(productName, productURL));
            ///* Imprimir para ver si los objetos se crearon correctamente.**/
            //Console.WriteLine("\n\n - PRODUCTO CREADO: Nombre: " + productList.ElementAt(productList.Count - 1).Name + ", URL: " + productList.ElementAt(productList.Count - 1).URL);

        }
        /* Método que revisa si el elemento deseado se encuentra en el
         *  código fuente de la página web dada su cadena actual.**/
        public static bool IsElementInWebPage(string _webpageSourceCoude, string element)
        {
            return _webpageSourceCoude.Contains(element);
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
            catch (System.NullReferenceException) { } /* No hace nada en el catch porque quiero que
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
        static private void ShowUriInfo(Uri uri)
        {
            /* Mensajes en consola para comprobar que la URL se esté manejando
             *  correctamente.**/
            Console.WriteLine("\n - URI DE RESPUESTA: " + uri.AbsoluteUri);
            //Console.WriteLine("\n - URI DE RESPUESTA: " + uri.PathAndQuery);
            //Console.WriteLine("\n - URI DE RESPUESTA: " + uri.AbsolutePath);
            //Console.WriteLine("\n - URI DE RESPUESTA: " + uri.ToString());
        }
    }
}
