using System.IO;
using System.Text.Json;
namespace ProyectoEst1
{
    class GuardarLibros
    {
        public ArbolLibros admin = new ArbolLibros(4); // se instancia el arbolito
        public Admin_libros admin2 = new Admin_libros(); // se instancia el maxheap

        public void Guardar() // función para guardar los libros: solo usa el de ArbolLibros al ser la estructura principal y más fiable
        {
            try{
                List<Libro> libros = admin.recorrer();
                string json = JsonSerializer.Serialize(libros);
                File.WriteAllText("Libros.json", json);
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error al leer Jsons: {ex.Message}");
            }
        }

        public void Cargar() // función para cargar los libros
        {
            if (!File.Exists("Libros.json")) return; // si no existe el archivo aún (primera ejecución), no hac ena
            try
            {
                string json = File.ReadAllText("Libros.json");
                List<Libro> libros = JsonSerializer.Deserialize<List<Libro>>(json); // los deserializa
                foreach (Libro libro in libros)
                {
                    admin.Insertar(libro);
                    admin2.agregar(libro); // y los guarda tanto en el árbol B+ como en el MaxHeap
                }
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error al leer Jsons: {ex.Message}");
            }
            
        }
    }

    class GuardarPrestamos
    {
        public Admin_prestamos admin = new Admin_prestamos();

        public void Guardar() // lógica para guardar préstamos
        {
            try
            {
                List<Prestamo> prestamos = admin.recorrer(); // obtiene toda la lista de valores
                string json = JsonSerializer.Serialize(prestamos);
                File.WriteAllText("Prestamos.json", json);  // la serializa y la convierte a archivo JSON
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error al leer Jsons: {ex.Message}");
            }
        }
        public void Cargar()
        {
            if (!File.Exists("Prestamos.json")) return; // si no existe, se omite
            try
            {
                string json = File.ReadAllText("Prestamos.json"); // obtiene la carpeta completa
                List<Prestamo> prestamos = JsonSerializer.Deserialize<List<Prestamo>>(json);
                foreach (Prestamo prestamo in prestamos) admin.agregar(prestamo.libros_prestados, prestamo.cantidad_prestada); // deserializa y agrega cada préstamo al heap
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error al leer Jsons: {ex.Message}");
            }
        }
    }
}  
