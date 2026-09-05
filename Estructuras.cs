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
    public int[] Claves { get; set; } // Arreglo de claves para el nodo
    public Libro[] Valores { get; set; } // Arreglo de valores para el nodo
    public bool esHoja { get; set; } // Indica si el nodo es una hoja

    public Nodo(int grado, bool esHoja)
    {
        this.Claves = new int[2 * grado - 1]; // Inicializa el arreglo de claves
        this.Valores = new Libro[2 * grado - 1]; // Inicializa el arreglo de valores
        this.siguiente = null; // Inicializa el puntero al siguiente nodo como null
        this.esHoja = esHoja; // Establece si el nodo es una hoja o no
    }
}

