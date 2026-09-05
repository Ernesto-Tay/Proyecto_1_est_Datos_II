int Aprox(double x)
{
    n = (int)x;
    if (x == n)
    {
        return x;
    }
    else
    {
        return n + 1;
    }
}


int ins_izq(int[] lista, int buscado) 
{
    int posc = 0;
    foreach (int i in lista)
    {
        posc += 1;
        if (i == buscado)
        {
            return posc;
        }
    }
    return -1;
}

int ins_der(int[] lista, int buscado)
{
    int posc = 0;
    int actual = 0;
    foreach (int i in lista)
    {
        posc += 1;
        if (i == buscado)
        {
            actual = posc;
        }
    }
    if (actual == 0)
    {
        return -1;
    }
    return actual;
}

public class Libro
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

public class Nodo
{
    public Libro Libro { get; set; }
    public Nodo siguiente { get; set; }
    public int[] Claves { get; set; }    
    public Libro[] Valores { get; set; }
    public bool esHoja { get; set; } 

    public Nodo(int grado, bool esHoja = true)
    {
        this.Claves = new int[2 * grado - 1]; // Inicializa el arreglo de claves
        this.Valores = new Libro[2 * grado - 1]; // Inicializa el arreglo de valores
        this.siguiente = null; // Inicializa el puntero al siguiente nodo como null
        this.esHoja = esHoja; // Establece si el nodo es una hoja o no
    }
}

public class ArbolLibros
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


    public void Insertar(Libro Libro)
    {
        if (raiz == null) // Si la raíz está vacía, se inserta ahí
        {
            raiz = new Nodo(orden);
            raiz.Claves[0] = Libro.codigo;
            raiz.Valores[0] = Libro;
        }
        else
        {
            if (raiz.Claves[max_claves - 1] != 0) // Si la raíz está llena, se divide
            {
                Nodo nuevaRaiz = new Nodo(orden, false);
                nuevaRaiz.siguiente = raiz;
                DividirNodo(nuevaRaiz, 0, raiz);
                raiz = nuevaRaiz;
            }
            InsertarNoLleno(raiz, Libro); // Inserta el libro en un nodo que no está lleno
        }

    }

    private void DividirNodo(Nodo nodoPadre, int posicion, Nodo nodoHijo)
    {
        
    }


    public bool Buscar(int codigo)
    {
        return BuscarRecursivo(raiz, codigo); // Llama a la función recursiva para buscar el libro
    }
    private bool BuscarRecursivo(Nodo nodo, int codigo)
    {
        if (nodo == null) // Si el nodo es null, retorna false
            return false;

        int i = 0;
        while (i < nodo.Claves.Length && nodo.Claves[i] != 0 && codigo > nodo.Claves[i]) 
            i++;

        if (i < nodo.Claves.Length && nodo.Claves[i] == codigo) // Si encuentra el código, retorna true
            return true;

        if (nodo.esHoja) // Si es una hoja y no encontró el código, retorna false
            return false;

        return BuscarRecursivo(nodo.siguiente, codigo); 
    }
}