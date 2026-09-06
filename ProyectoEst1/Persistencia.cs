using System.Text.Json;
using System.IO;
namespace ProyectoEst1
{
    class GuardarLibros
    {
        public ArbolLibros admin = new ArbolLibros();
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
                foreach (Libro libro in libros)admin.insertar(libro);
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
                foreach (Prestamo prestamo in prestamos) admin.agregar(prestamo);
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error al leer Jsons: {ex.Message}");                
            }
        }
    }
}