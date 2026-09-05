// FUNCIONES PARA EL ARBOL B+
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
    int max = lista.Length;
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
    int max = lista.Length;
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
    public int[] claves { get; set; }
    public Nodo[] hijos { get; set; }
    public Nodo padre { get; set; }
    public bool esHoja { get; set; }

    public Nodo(int grado, bool esHoja = true)
    {
        this.Claves = new int[2 * grado - 1]; // Inicializa el arreglo de claves
        this.Valores = new Nodo[2 * grado - 1]; // Inicializa el arreglo de valores
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
        this.max_claves = 2 * orden - 1; // Calcula el número máximo de claves por nodo
        this.raiz = new Nodo(orden); // Inicializa la raíz del árbol
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
        return posc < hoja.claves.Length && hoja.claves[posc] == clave; // si la posición no sobrepasa el tamaño de matriz Y la posición actual apunta a un nodo, retorna True
    }

    // ----- INSERCIÓN -----
    public void Insertar(Libro Libro)
    {
        if (raiz == null) // Si la raíz está vacía, se inserta ahí
        {
            raiz = new Nodo(orden);
            raiz.Claves[0] = Libro.codigo;
            raiz.Valores[0] = Libro;
            return;
        }

        if (buscar(Libro.codigo)) // Si el libro ya existe, no se inserta
        {
            return;
        }

        if (raiz.Claves[max_claves - 1] != 0) // Si la raíz está llena, se divide
        {
            Nodo nuevaRaiz = new Nodo(orden, false);
            nuevaRaiz.siguiente = raiz;
            DividirNodo(nuevaRaiz, 0, raiz);
            raiz = nuevaRaiz;
        }
        InsertarNoLleno(raiz, Libro); // Inserta el libro en un nodo que no está lleno


    }

    private void DividirNodo(Nodo nodoPadre, int posicion, Nodo nodoHijo) // proceso de división
    {

    }

}