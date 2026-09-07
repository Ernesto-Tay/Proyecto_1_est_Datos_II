namespace ProyectoEst1
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("\n-------------------SISTEMA-------------------\n1. Ir a menú de libros\n2. ir a menú de préstamos");
            Console.Write("Seleccione una opción: ");
            string input1 = Console.ReadLine();
            switch (input1)
            {
                case "1":
                    Console.WriteLine("\n---------- MENÚ LIBROS ----------\n1. Agregar libro\n2. Ver catálogo\n3. Buscar libro\n5. Eliminar libro");
                    break;
            }
        }
    }
    
}   

