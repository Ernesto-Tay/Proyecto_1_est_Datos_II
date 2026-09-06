namespace Estructuras
{
    // FUNCIONES PARA EL ARBOL B+
    public static class Utilidades
    {
        public static int Aprox(double x) // Utilidades.Aproximador
        {
            int n = (int)x;
            if (x == n)
            {
                return n; // si son iguales, retorna el número convertido
            }
            else
            {
                return n + 1; // si no lo son, le suma 1 par Utilidades.Aproximar y no quedar truncado
            }
        }
        public static int ins_izq(List<int> lista, int buscado)  // obtiene la posición más a la izquierda de todas las istnacias encotradas del valor buscado
        {
            int max = lista.Count;
            int min = 0;
            while (min < max)
            {
                int medio = (max + min) / 2; // busca la mitad de la lista actual
                if (lista[medio] < buscado) // siempre que el medio sea menor al buscado, se va al bloque derecho
                {
                    min = medio + 1;
                }
                else max = medio;
            }
            return min;
        }

        public static int ins_der(List<int> lista, int buscado) // obtiene la posición más a la derecha de todas las instancias aparecidas en el valor buscado
        {
            int max = lista.Count - 1;
            int min = 0;
            while (min < max)
            {
                int medio = (max + min) / 2;
                if (lista[medio] <= buscado) // si el medio es menor O IGUAL al buscado, se toma el bloque derecho (para ir al valor más a la derecha que exista)
                {
                    min = medio + 1;
                }
                else max = medio;
            }
            return min;
        }
    }



    public class Libro // la clase del libro (así todo queda bien organizado)
    {
        // Atributos de los libros
        public int codigo { get; set; }
        public string titulo { get; set; }
        public string autor { get; set; }
        public string genero { get; set; }
        public int copias { get; set; }
        public int veces_prestado { get; set; }

        //Inicializador de clase
        public Libro(int codigo, string titulo, string autor, string genero, int copias, int veces_prestado)
        {
            this.codigo = codigo;
            this.titulo = titulo;
            this.autor = autor;
            this.genero = genero;
            this.copias = copias;
            this.veces_prestado = veces_prestado;
        }

        // Mostrar info del libro
        public void MostrarInfo()
        {
            Console.WriteLine($"Código:\t\t{codigo}\nTítulo:\t\t{titulo}\nAutor:\t\t{autor}\nGénero:\t\t{genero}\nCopias disponibles:\t{copias}\nVeces prestado:\t{veces_prestado}");
        }
    }



    public class Nodo // nodo para el árbol B+
    {
        public Nodo siguiente { get; set; }
        public List<int> claves { get; set; }
        public List<Libro> libros { get; set; }
        public List<Nodo> hijos { get; set; }
        public Nodo padre { get; set; }
        public bool esHoja { get; set; }

        public Nodo(bool esHoja = true)
        {
            this.claves = new List<int>(); // Inicializa el arreglo de claves
            this.hijos = new List<Nodo>(); // Inicializa el arreglo de hijos
            this.libros = new List<Libro>();
            this.siguiente = null; // Inicializa el puntero al siguiente nodo como null
            this.padre = null; // Inicializar el padre del nodo actual como null
            this.esHoja = esHoja; // Establece si el nodo es una hoja o no
        }
    }



    public class ArbolLibros //el mero Árbol B+
    {
        private Nodo raiz; // Raíz del árbol
        private int orden;
        private int max_claves;
        private int min_claves;
        private int min_hijos_interno;

        public ArbolLibros(int orden)
        {
            this.orden = orden;
            this.max_claves = Utilidades.Aprox(orden - 1); // Calcula el número máximo de claves por nodo
            this.raiz = new Nodo(); // Inicializa la raíz del árbol
            this.min_claves = Utilidades.Aprox((orden - 1) / 2);
            this.min_hijos_interno = Utilidades.Aprox(orden / 2);
        }


        public bool Vacio()
        {
            return raiz == null; // Indica si el arbol está vacío
        }

        //  ----- BÚSQUEDA -----
        private Nodo buscar_hoja(int codigo) // Función para llevar la posición actual al nodo hoja óptimo
        {
            Nodo actual = raiz;
            while (!actual.esHoja) // siempre y cuando el nodo actual no sea una hoja...
            {
                int posicion = Utilidades.ins_der(actual.claves, codigo);
                actual = actual.hijos[posicion]; // actualiza la posición para el último nodo con clave coincidente, y busca en ese nodo tmb
            }
            return actual;
        }

        public bool buscar(int clave) // busca si existe un nodo con definida clave
        {
            Nodo hoja = buscar_hoja(clave);
            int posc = Utilidades.ins_izq(hoja.claves, clave); // utliza la función para llevar a nodos hoja
            return posc < hoja.claves.Count && hoja.claves[posc] == clave; // si la posición no sobrepasa el tamaño de matriz Y la posición actual apunta a un nodo, retorna True
        }

        public Libro BuscarLibro(int clave) // para buscar los libros (pues trabajamos con esas mmds, PEERO se me olvidó x,D)
        {
            Nodo hoja = buscar_hoja(clave);
            int posc = Utilidades.ins_izq(hoja.claves, clave);

            if (posc < hoja.claves.Count && hoja.claves[posc] == clave)
            {
                return hoja.libros[posc]; 
            }
            return null; // no se encontró
        }

        // ----- INSERCIÓN -----
        public void Insertar(Libro libro)
        {
            int clave = libro.codigo;
            if (buscar(clave)) // Si el libro ya existe, no se inserta
            {
                return;
            }
            // se obtiene la hoja onde insertar, y se inserta en su posición respectiva
            Nodo hoja = buscar_hoja(clave);
            int posc = Utilidades.ins_der(hoja.claves, clave);

            List<int> lista = hoja.claves; // se toma la lista
            List<int> izq = lista.GetRange(0, posc); // parte izquierda
            List<int> der = lista.GetRange(posc, lista.Count - posc); // parte derecha
            izq.Add(clave); // se pone la clave donde debe quedar
            izq.AddRange(der); // y se concatena todo
            hoja.claves = izq;

            // Literalmente lo mismo, pero ahora con los LiBriToS
            List<Libro> listaLibros = hoja.libros;
            List<Libro> izqLibros = listaLibros.GetRange(0, posc);
            List<Libro> derLibros = listaLibros.GetRange(posc, listaLibros.Count - posc);
            izqLibros.Add(libro);
            izqLibros.AddRange(derLibros);
            hoja.libros = izqLibros;

            if (hoja.claves.Count > max_claves) DividirNodo(hoja);
            recalcular(raiz);
        }

        private void DividirNodo(Nodo nodo) // proceso de división
        {
            int p_div = (nodo.claves.Count + 1) / 2;

            Nodo n_hoja = new Nodo();
            n_hoja.padre = nodo.padre; // comparten papi
            n_hoja.claves = nodo.claves.GetRange(0, p_div);
            nodo.claves = nodo.claves.GetRange(p_div, nodo.claves.Count - p_div); // se dividen las claves  
            n_hoja.libros = nodo.libros.GetRange(0, p_div);
            nodo.libros = nodo.libros.GetRange(p_div, nodo.claves.Count - p_div);
            

            n_hoja.siguiente = nodo.siguiente;
            nodo.siguiente = n_hoja; // nodo -> nuevo_nodo -> siguiente

            int c_guia = n_hoja.claves[0]; //clave guía para siguientes operatorias
            ins_padre(nodo, c_guia, n_hoja);
        }

        private void ins_padre(Nodo izq, int guia, Nodo der)
        {
            if (izq == raiz) // caso 1 - la raíz se divide
            {
                Nodo n_raiz = new Nodo(false); //nueva raíz
                n_raiz.claves.Add(guia);
                n_raiz.hijos.Add(izq);
                n_raiz.hijos.Add(der); // se actualizan guías y hojas

                // se actualizan referencias
                izq.padre = n_raiz;
                der.padre = n_raiz;
                raiz = n_raiz;
                return;
            }

            // recupera padre y localiza hijo dividido
            Nodo padre = izq.padre;
            int posicion = padre.hijos.IndexOf(izq);

            // completa información del nuevo padre
            padre.claves.Insert(posicion, guia);
            padre.hijos.Insert(posicion + 1, der);
            der.padre = padre;

            // salvaguarda: si se desborda el papi, se divide
            if (padre.claves.Count > max_claves) division_interna(padre);
        }

        private void division_interna(Nodo nodo)
        {
            int p_div = nodo.claves.Count / 2;
            int ascendido = nodo.claves[p_div];

            Nodo n_interno = new Nodo(false);
            n_interno.padre = nodo.padre;

            n_interno.claves = nodo.claves.GetRange(p_div + 1, nodo.claves.Count - p_div - 1);
            n_interno.hijos = nodo.hijos.GetRange(p_div + 1, nodo.hijos.Count - p_div - 1);

            foreach (Nodo hijo in n_interno.hijos)
            {
                hijo.padre = n_interno;
            }

            nodo.claves = nodo.claves.GetRange(0, p_div);
            nodo.hijos = nodo.hijos.GetRange(0, p_div + 1);

            ins_padre(nodo, ascendido, n_interno);
        }

        public bool eliminar(int clave)
        {
            // busca hoja y posición
            Nodo hoja = buscar_hoja(clave);
            int posc = Utilidades.ins_izq(hoja.claves, clave);

            //caso 1: clave inexistente
            if (posc > hoja.claves.Count || hoja.claves[posc] != clave) return false;

            hoja.claves.RemoveAt(posc);
            hoja.libros.RemoveAt(posc);

            if (hoja == raiz) return true;

            if (hoja.claves.Count < min_hijos_interno) reparar_hoja(hoja);
            recalcular(raiz);
            return true;
        }

        private void reparar_hoja(Nodo hoja)
        {
            // OBTIENE papá y hoja
            Nodo padre = hoja.padre;
            int posc = padre.hijos.IndexOf(hoja);
            Nodo h_izq = null;
            Nodo h_der = null;

            // define hermanos si están en rango
            if (posc > 0) h_izq = padre.hijos[posc - 1];
            if (posc + 1 < padre.hijos.Count) h_der = padre.hijos[posc + 1];

            if (h_izq != null && h_izq.claves.Count > min_hijos_interno)
            {
                int c_prestada = h_izq.claves[-1];
                h_izq.claves.RemoveAt(-1);
                hoja.claves.Insert(0, c_prestada);

                Libro l_prestado = h_izq.libros[h_izq.libros.Count - 1];
                h_izq.libros.RemoveAt(h_izq.libros.Count - 1);
                hoja.libros.Insert(0, l_prestado);
                return;
            }

            if (h_der != null && h_der.claves.Count > min_hijos_interno)
            {
                int c_prestada = h_der.claves[0];
                h_der.claves.RemoveAt(0);
                hoja.claves.Add(c_prestada);
                Libro l_prestado = h_der.libros[0];
                h_der.libros.RemoveAt(0);
                hoja.libros.Add(l_prestado);
                return;
            }

            if (h_izq != null)
            {
                h_izq.claves.AddRange(hoja.claves);
                h_izq.libros.AddRange(hoja.libros);
                h_izq.siguiente = hoja.siguiente;

                padre.hijos.RemoveAt(posc);
                padre.claves.RemoveAt(posc - 1);
            }
            else if (h_der != null)
            {
                hoja.claves.AddRange(h_der.claves);
                hoja.libros.AddRange(h_der.libros);
                hoja.siguiente = h_der.siguiente;

                padre.hijos.RemoveAt(posc + 1);
                padre.claves.RemoveAt(posc);
            }
            reparar_interno(padre);
        }

        private void reparar_interno(Nodo nodo)
        {
            //en caso el nodo sea la raiz, si se queda sin claves el hijo ahora es raiz (sin padres)
            if (nodo == raiz)
            {
                if (nodo.claves.Count == 0)
                {
                    raiz = nodo.hijos[0];
                    raiz.padre = null;
                }
                return;
            }

            //de haber suficientes hijos, no ocupa repararse
            if (nodo.hijos.Count >= min_hijos_interno) return;

            // Obtiene padre y localiza nodos
            Nodo padre = nodo.padre;
            int posc = padre.hijos.IndexOf(nodo);
            Nodo izq = null;
            Nodo der = null;

            // si la posición está en rango, se definen izquierdo o derecho
            if (posc > 0) izq = padre.hijos[posc - 1];
            if (posc + 1 < padre.hijos.Count) der = padre.hijos[posc + 1];

            // si el izq puede, baja de nivel
            if (izq != null && izq.hijos.Count > min_hijos_interno)
            {
                Nodo h_movido = izq.hijos[izq.hijos.Count - 1];
                izq.hijos.RemoveAt(izq.hijos.Count-1);
                h_movido.padre = nodo;

                int n_guia = izq.claves[izq.claves.Count - 1];
                izq.claves.RemoveAt(izq.claves.Count-1);

                nodo.hijos.Insert(0, h_movido);
                nodo.claves.Insert(0, padre.claves[posc - 1]);
                padre.claves[posc - 1] = n_guia;
                return;
            }

            // si el der puede, baja de nivel
            if (der != null && der.hijos.Count > min_hijos_interno)
            {
                Nodo h_movido = der.hijos[0];
                der.hijos.RemoveAt(0);
                h_movido.padre = nodo;

                nodo.hijos.Add(h_movido);
                nodo.claves.Add(padre.claves[posc]);
                padre.claves[posc] = der.claves[der.claves.Count - 1];
                der.claves.RemoveAt(der.claves.Count-1);
                return;
            }


            if (izq != null)
            {
                izq.claves.Add(padre.claves[posc - 1]);
                padre.claves.RemoveAt(posc - 1);

                izq.claves.AddRange(nodo.claves);
                foreach (Nodo hijo in nodo.hijos) hijo.padre = izq;

                izq.hijos.AddRange(nodo.hijos);
                padre.hijos.RemoveAt(posc);
            }
            else if (der != null)
            {
                nodo.claves.Add(padre.claves[posc]);
                padre.claves.RemoveAt(posc);

                nodo.claves.AddRange(der.claves);
                foreach (Nodo hijo in der.hijos) hijo.padre = nodo;

                nodo.hijos.AddRange(der.hijos);
                padre.hijos.RemoveAt(posc + 1);
            }
            reparar_interno(padre);
        }

        private int min_subarbol(Nodo nodo)
        {
            while (!nodo.esHoja) nodo = nodo.hijos[0]; // va a la hoja más a la izquierda
            return nodo.claves[0]; // devuelve la clave más pequeña del nodo más pequeño
        }

        private void recalcular(Nodo nodo)
        {
            if (nodo.esHoja) return;
            foreach (Nodo hijo in nodo.hijos) recalcular(hijo);  // niveles inferiores

            foreach (Nodo hijo in nodo.hijos.GetRange(1, nodo.hijos.Count - 1)) min_subarbol(hijo); // hijos derechos
        }

        public List<Libro> recorrer()
        {
            Nodo nodo = raiz;
            while (!nodo.esHoja) nodo = nodo.hijos[0];

            List<Libro> res = new List<Libro>();

            while (nodo != null)
            {
                res.AddRange(nodo.libros);
                nodo = nodo.siguiente;
            }
            return res;
        }

        public void mostrar()
        {
            _mostrar(raiz, 0);
        }

        private void _mostrar(Nodo nodo, int nivel) // muestra los nodos de forma tabulada
        {
            string sangria = new string('\t', nivel);
            string tipo = nodo.esHoja? "Hoja" : "Interno"; // si aprendí esta sintaxis para algo la voy a usar
            Console.WriteLine($"{sangria}{tipo}: {string.Join(",",nodo.claves)}");

            if (!nodo.esHoja) // Expande la impresión cada que no se acceda a un nodo hoja
            {
                foreach (Nodo hijo in nodo.hijos) _mostrar(hijo, nivel + 1);
            }
        }
    }
}