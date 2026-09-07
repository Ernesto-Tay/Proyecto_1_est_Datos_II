namespace ProyectoEst1
{
    class Program
    {
        public static void Main(string[] args)
        {
            GuardarLibros lib_mng = new GuardarLibros();
            GuardarPrestamos pre_mng = new GuardarPrestamos();

            lib_mng.Cargar();
            pre_mng.Cargar();
            Console.WriteLine("\n-------------------SISTEMA-------------------\n1. Ir a menú de libros\n2. ir a menú de préstamos");
            Console.Write("Seleccione una opción: ");
            string input1 = Console.ReadLine();
            switch (input1)
            {
                case "1":
                    {
                        Console.WriteLine("\n---------- MENÚ LIBROS ----------\n1. Agregar libro\n2. Ver catálogo\n3. Buscar libro\n5. Eliminar libro");
                        Console.Write("Selecciona una opción: ");
                        string input2 = Console.ReadLine();

                        switch (input2)
                        {
                            case "1": break;
                            case "2": break;
                            case "3": break;
                            case "4": break;
                            case "5": break;
                            case "6": break;
                            case "7": break;
                            default:
                                {
                                    Console.WriteLine("Opción inválida.");
                                    break;
                                }
                                break;
                        }
                    }
                case "2":
                    {
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Opción inválida.");
                        break;
                    }
            }
    }
}
}