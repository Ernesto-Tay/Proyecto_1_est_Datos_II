using System.IO;
using System.Text.Json;
namespace ProyectoEst1
{
    class GuardarLibros
    {
        public ArbolLibros admin = new ArbolLibros(4);

        public void Guardar()
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

        public void Cargar()
        {
            try
            {
                string json = File.ReadAllText("Libros.json");
                List<Libro> libros = JsonSerializer.Deserialize<List<Libro>>(json);
                foreach (Libro libro in libros)admin.Insertar(libro);
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

        public void Guardar()
        {
            try
            {
                List<Prestamo> prestamos = admin.recorrer();
                string json = JsonSerializer.Serialize(prestamos);
                File.WriteAllText("Prestamos.json", json); 
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error al leer Jsons: {ex.Message}");                
            }
        }
        public void Cargar()
        {
            try
            {
                string json = File.ReadAllText("Prestamos.json");
                List<Prestamo> prestamos = JsonSerializer.Deserialize<List<Prestamo>>(json);
                foreach (Prestamo prestamo in prestamos) admin.agregar(prestamo.libros_prestados, prestamo.cantidad_prestada);
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error al leer Jsons: {ex.Message}");                
            }
        }
    }
}  
