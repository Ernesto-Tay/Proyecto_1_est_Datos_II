// FUNCIONES PARA EL ARBOL B+
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Swift;

int Aprox(double x) // aproximador
{
    n = (int)x;
    if (x == n)
    {
        return n; // si son iguales, retorna el número convertido
    }
    else
    {
        return n + 1; // si no lo son, le suma 1 par aproximar y no quedar truncado
    }
}

// estas madres usan búsqueda binaria y yo ni en cuenta ;-;
// buscar el valor más a la derecha 
int ins_izq(int[] lista, int buscado)  // obtiene la posición más a la izquierda de todas las istnacias encotradas del valor buscado
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

int ins_der(int[] lista, int buscado) // obtiene la posición más a la derecha de todas las instancias aparecidas en el valor buscado
{
    int max = lista.Count;
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
    public Libro libro { get; set; }
    public Nodo siguiente { get; set; }
    public List<int> claves { get; set; }
    public List<Nodo> hijos { get; set; }
    public Nodo padre { get; set; }
    public bool esHoja { get; set; }

    public Nodo(bool esHoja = true)
    {
        this.Claves = new List<int>(); // Inicializa el arreglo de claves
        this.Valores = new List<Nodo>(); // Inicializa el arreglo de valores
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

    public ArbolLibros(int orden)
    {
        this.orden = orden;
        this.max_claves = Aprox(2 * orden - 1); // Calcula el número máximo de claves por nodo
        this.raiz = new Nodo(); // Inicializa la raíz del árbol
    }


    public bool Vacio()
    {
        return raiz == null; // Indica si el arbol está vacío
    }

    private Nodo buscar_hoja(int codigo) // Función para llevar la posición actual al nodo hoja óptimo
    {
        Nodo actual = raiz;
        while (!actual.esHoja) // siempre y cuando el nodo actual no sea una hoja...
        {
            int posicion = ins_der(actual.claves);
            Nodo actual = actual.hijos[posicion]; // actualiza la posición para el último nodo con clave coincidente, y busca en ese nodo tmb
        }
        return actual;
    }

    public bool buscar(int clave) // busca si existe un nodo con definida clave
    {
        Nodo hoja = buscar_hoja(clave);
        int posc = ins_izq(hoja.claves, clave); // utliza la función para llevar a nodos hoja
        return posc < hoja.claves.Count && hoja.claves[posc] == clave; // si la posición no sobrepasa el tamaño de matriz Y la posición actual apunta a un nodo, retorna True
    }

    // ----- INSERCIÓN -----
    public void Insertar(int clave)
    {
        if (buscar(clave)) // Si el libro ya existe, no se inserta
        {
            return;
        }
        // se obtiene la hoja onde insertar, y se inserta en su posición respectiva
        Nodo hoja = buscar_hoja(clave);
        int posc = ins_der(hoja.claves, clave);
        List<int> lista = hoja.claves; // se toma la lista
        List<int> izq = lista.GetRange(0, posc); // parte izquierda
        List<int> der = lista.GetRange(posc, lista.Count - posc); // parte derecha
        izq.Insert(clave); // se pone la clave donde debe quedar
        hoja.claves = izq.AddRange(der); // y se concatena todo

        if (hoja.claves.Count > max_claves) DividirNodo(hoja);
        recalcular(raiz);
    }

    private void DividirNodo(Nodo nodo) // proceso de división
    {
        p_div = (nodo.claves.count + 1) / 2;
        p_div = Convert.ToInt32(p_div); // calcula punto de división y lo convierte a entero

        Nodo n_hoja = Nodo();
        n_hoja.padre = nodo.padre; // comparten papi
        n_hoja.claves = nodo.claves.GetRange(0, p_div);
        nodo.claves = nodo.claves.GetRange(p_div, nodo.claves.Count - p_div); // se dividen las claves

        n_hoja.siguiente = nodo.siguiente;
        nodo.siguiente = n_hoja; // nodo -> nuevo_nodo -> siguiente

        int c_guia = n_hoja.claves[0]; //clave guía para siguientes operatorias
        ins_padre(nodo, c_guia, n_hoja);
    }

    private void ins_padre(Nodo izq, int guia, Nodo der)
    {
        if (izq == raiz) // caso 1 - la raíz se divide
        {
            n_raiz = Nodo(false); //nueva raíz
            n_raiz.claves.Insert(guia);
            n_raiz.hijos.Insert(izq);
            n_raiz.hijos.Insert(der); // se actualizan guías y hojas

            // se actualizan referencias
            izq.padre = n_raiz;
            der.padre = n_raiz;
            raiz = n_raiz;
            return;
        }

        // recupera padre y localiza hijo dividido
        Nodo padre = izq.padre;
        int posicion = padre.hijos.Find(izq);

        // completa información del nuevo padre
        padre.claves.Insert(posicion, guia);
        padre.hijos.Insert(posicion + 1, der);
        der.padre = padre;

        // salvaguarda: si se desborda el papi, se divide
        if (padre.claves.Count > max_claves) division_interna(padre);
    }

    private void division_interna(Nodo nodo)
    {
        p_div = nodo.claves.count / 2;
        ascendido = nodo.claves[p_div];

        n_interno = Nodo();
        n_interno.padre = nodo.padre;

        n_interno.claves = nodo.claves.GetRange(p_div + 1, nodo.claves.Count - p_div - 1);
        n_interno.hijos = nodo.hijos.GetRange(p_div + 1, nodo.hijos.Count - p_div - 1);

        foreach (Nodo hijo in n_interno.hijos)
        {
            hijo.padre = n_interno;
        }

        nodo.claves = nodo.claves.GetRange(0, p_div);
        nodo.hijos = nodo.claves.GetRange(0, p_div + 1);

        ins_padre(nodo, ascendido, n_interno);
    }
    

}