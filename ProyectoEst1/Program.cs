using System.Linq.Expressions;
using System.Reflection;

namespace ProyectoEst1
{
    class Program
    {
        public static void Main(string[] args)
        {   // se instancian los administradores de persistencia
            string input1 = "0";
            GuardarLibros lib_mng = new GuardarLibros();
            GuardarPrestamos pre_mng = new GuardarPrestamos();
            lib_mng.Cargar(); // se cargan los datos de forma preliminar
            pre_mng.Cargar();
            Console.Out.Flush();
            while (input1 != "3")
            {
                                Console.ForegroundColor = ConsoleColor.Magenta;

                Console.WriteLine("\n-------------------SISTEMA-------------------\n1. Ir a menú de libros\n2. ir a menú de préstamos\n3. Salir del sistema");
                Console.ResetColor();
                Console.Write("Seleccione una opción: ");
                input1 = Console.ReadLine();
                switch (input1)
                {
                    case "1":
                        {   
                                            Console.ForegroundColor = ConsoleColor.Cyan;

                            Console.WriteLine("\n---------- MENÚ LIBROS ----------\n1. Agregar libro\n2. Ver catálogo\n3. Buscar libro\n4. Eliminar libro\n5. ver libros más prestados\n6. volver al menú");
                            Console.ResetColor();
                            Console.Write("Selecciona una opción: ");
                            string input2 = Console.ReadLine();

                            switch (input2)
                            {
                                case "1":
                                    {
                                        try
                                        {
                                            Console.Write("Ingrese código (enteros): ");
                                            int cod = Convert.ToInt32(Console.ReadLine());
                                            if (lib_mng.admin.buscar(cod))
                                            {
                                                
                                                throw new DuplicateWaitObjectException("Ya existe un libro con ese código");
                                            }

                                            Console.Write("Ingrese título: ");
                                            string titulo = Console.ReadLine();
                                            Console.Write("Ingrese Autor: ");
                                            string autor = Console.ReadLine();
                                            Console.Write("Ingrese género: ");
                                            string genero = Console.ReadLine();
                                            Console.Write("Ingrese cantidad inicial de copias: ");
                                            int copias = Convert.ToInt32(Console.ReadLine());
                                            if (copias <= 0)
                                            {
                                                throw new Exception("La cantidad de copias no puede ser negativa ni cero");
                                            }
                                            if (titulo == "" || autor == "" || genero == "")
                                            {
                                                throw new FormatException("Los campos no pueden estar vacíos");
                                            }
                                            Libro libro = new Libro(cod, titulo, autor, genero, copias, 0);
                                            lib_mng.admin.Insertar(libro);
                                            lib_mng.admin2.agregar(libro);
                                            Console.WriteLine("Libro insertado con éxito");
                                        }
                                        catch (FormatException ex)
                                        {
                                                            Console.ForegroundColor = ConsoleColor.DarkRed;

                                            Console.WriteLine("Error: no ingrese texto en campos numéricos y no deje campos vacíos");
                                            Console.ResetColor();
                                        }
                                        catch (DuplicateWaitObjectException x)
                                        {
                                                            Console.ForegroundColor = ConsoleColor.DarkRed;

                                            Console.WriteLine($"Error: {x}");
                                            Console.ResetColor();
                                        }
                                        catch (Exception x)
                                        {
                                                            Console.ForegroundColor = ConsoleColor.DarkRed;

                                            Console.WriteLine($"Error: {x}");
                                            Console.ResetColor();
                                        }
                                        break;
                                    }
                                case "2":
                                    {
                                        lib_mng.admin.mostrar2();
                                        break;
                                    }
                                case "3":
                                    {
                                        try
                                        {
                                            Console.Write("Ingrese el código del libro a buscar: ");
                                            int cod_buscar = Convert.ToInt32(Console.ReadLine());
                                            Libro lib = lib_mng.admin.BuscarLibro(cod_buscar);
                                            if (lib != null)
                                            {
                                                Console.WriteLine("Libro encontrado: ");
                                                lib.MostrarInfo();
                                            }
                                            else
                                            {
                                                Console.WriteLine("No se encontró un libro con ese código");
                                            }
                                        }
                                        catch (Exception x)
                                        {
                                                            Console.ForegroundColor = ConsoleColor.DarkRed;

                                            Console.WriteLine($"Error: {x}");
                                            Console.ResetColor();
                                        }
                                        break;
                                    }
                                case "4":
                                    {
                                        try
                                        {
                                            Console.Write("Ingrese el ID del libro a eliminar: ");
                                            int cod_search = Convert.ToInt32(Console.ReadLine());
                                            if (lib_mng.admin.BuscarLibro(cod_search) != null)
                                            {
                                                if (pre_mng.admin.Prestado(cod_search) == null)
                                                {
                                                    Console.Write($"El libro {lib_mng.admin.BuscarLibro(cod_search).titulo} fué eliminado exitosamente del sistema.");

                                                    lib_mng.admin.eliminar(cod_search);
                                                    lib_mng.admin2.bye_libro(cod_search);
                                                }
                                                else
                                                {
                                                                    Console.ForegroundColor = ConsoleColor.Yellow;

                                                    Console.WriteLine("Ese libro tiene préstamos activos, por lo que no se puede eliminar.");
                                                    Console.ResetColor();
                                                }
                                            }
                                            else
                                            {
                                                                Console.ForegroundColor = ConsoleColor.Yellow;

                                                Console.WriteLine("No se encontró un libro con ese código");
                                                Console.ResetColor();
                                            }
                                        }
                                        catch (Exception x)
                                        {
                                                            Console.ForegroundColor = ConsoleColor.DarkRed;

                                            Console.WriteLine($"Error: {x}");
                                            Console.ResetColor();
                                        }
                                        break;
                                    }
                                case "5":
                                    {
                                        lib_mng.admin2.mostrar_ranking();
                                        break;
                                    }
                                case "6":
                                    {
                                        Console.WriteLine("Volviendo al menú...");
                                        break;
                                    }
                                default:
                                    {
                                        Console.WriteLine("Opción inválida.");
                                        break;
                                    }
                            }
                            break;
                        }
                    case "2":
                        {
                                            Console.ForegroundColor = ConsoleColor.Blue;

                            Console.WriteLine("\n---------- MENÚ PRÉSTAMOS ----------\n1. Añadir préstamo\n2. Buscar préstamo\n3. Atender préstamo\n4. Mostrar cola de préstamos\n5. Mostrar siguiente préstamo en cola\n6. volver al menú");
                            Console.ResetColor();
                            Console.Write("Selecciona una opción: ");
                            string input3 = Console.ReadLine();
                            switch (input3)
                            {
                                case "1":
                                    {
                                        try
                                        {
                                            Console.Write("Ingrese nombre del solicitante: ");
                                            string solicitante = Console.ReadLine();
                                            lib_mng.admin.mostrar_pa_prestamo(); // muestra libros disponibles

                                            List<int> codigos = new List<int>();
                                            List<int> cantidades = new List<int>();
                                            Dictionary<int, int> apartado = new Dictionary<int, int>(); // para evitar apartar de más
                                            Console.WriteLine("Ingrese códigos a prestar (0 para terminar):");
                                            while (true)
                                            {
                                                Console.Write("Código: ");
                                                int cod = Convert.ToInt32(Console.ReadLine());
                                                if (cod == 0) break;

                                                Libro libro = lib_mng.admin.BuscarLibro(cod);
                                                if (libro == null) { Console.WriteLine("No existe ese código."); continue; } // si el libro no existe, entonces permite ingresar otra vez

                                                int ya_apartado = apartado.ContainsKey(cod) ? apartado[cod] : 0; // si se solicita el mismo libro varias veces, suma las cantidades
                                                int disp = libro.copias - ya_apartado;


                                                Console.Write("Cantidad a prestar: ");
                                                int cant = Convert.ToInt32(Console.ReadLine());
                                                if (cant <= 0 || cant > disp) { Console.WriteLine("Cantidad inválida."); continue; }

                                                codigos.Add(cod);
                                                cantidades.Add(cant);
                                                apartado[cod] = cant + ya_apartado;
                                            }

                                            if (codigos.Count == 0)
                                            {
                                                Console.WriteLine("No se agregaron libros al préstamo.");
                                            }
                                            else
                                            {
                                                bool fue_ingresado = pre_mng.admin.agregar(codigos, cantidades, solicitante);
                                                if (fue_ingresado)
                                                {
                                                    lib_mng.admin.actualizar_stock(codigos, cantidades, true);

                                                    foreach (int cod in codigos)
                                                    {
                                                        Libro lib = lib_mng.admin.BuscarLibro(cod);
                                                        if (lib != null)
                                                        {
                                                            lib.veces_prestado++;
                                                            lib_mng.admin2.upd_prioridad(lib);
                                                        }
                                                    }
                                                    Console.WriteLine("Préstamo agregado con éxito.");
                                                }
                                                else
                                                {
                                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                                    Console.WriteLine("Ya existe un préstamo igual");
                                                    Console.ResetColor();
                                                }
                                                
                                            }
                                        }
                                        catch (Exception x) 
                                        {
                                            Console.ForegroundColor = ConsoleColor.DarkRed;
                                            Console.WriteLine($"Error: {x}");
                                            Console.ResetColor();
                                            }
                                        break;
                                    }
                                case "2":
                                    {
                                        try
                                        {
                                            Console.Write("Código de libro a buscar en préstamos: ");
                                            int cod_buscar = Convert.ToInt32(Console.ReadLine());
                                            var encontrados = pre_mng.admin.Prestado(cod_buscar);
                                            if (encontrados != null)
                                            {
                                                Console.WriteLine("Préstamo encontrado: ");
                                                foreach(Prestamo encontrado in encontrados)
                                                encontrado.mostrar_datos();
                                                Console.WriteLine();
                                            }

                                            else {
                                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                                Console.WriteLine("No se encontró préstamo con ese código.");
                                                Console.ResetColor();
                                                 }
                                        }
                                        catch (Exception x)
                                        {
                                                            Console.ForegroundColor = ConsoleColor.DarkRed;

                                            Console.WriteLine($"Error: {x}");
                                            Console.ResetColor();
                                        }
                                        break;
                                    }
                                case "3":
                                    {
                                        Prestamo atendido = pre_mng.admin.atender();
                                        if (atendido != null)
                                        {
                                            Console.WriteLine("Préstamo atendido: ");
                                            atendido.mostrar_datos();
                                            Console.WriteLine();
                                            lib_mng.admin.actualizar_stock(atendido.libros_prestados, atendido.cantidad_prestada, false);

                                        }
                                        else Console.WriteLine("No hay préstamos pendientes.");
                                        break;
                                    }
                                case "4":
                                    {
                                        pre_mng.admin.mostrar_cola();
                                        break;
                                    }
                                case "5":
                                    {
                                        Prestamo siguiente = pre_mng.admin.consultar();
                                        if (siguiente != null)
                                        {
                                            Console.WriteLine("Siguiente en cola: ");
                                            siguiente.mostrar_datos();
                                            Console.WriteLine();
                                        }
                                        else Console.WriteLine("La cola está vacía.");
                                        break;
                                    }
                                case "6":
                                    {
                                        Console.WriteLine("Volviendo al menú...");
                                        break;
                                    }
                                default:
                                    {
                                                        Console.ForegroundColor = ConsoleColor.Yellow;

                                        Console.WriteLine("Opción inválida.");
                                        Console.ResetColor();
                                        break;
                                    }
                            }
                            break;
                        }
                    case "3":
                        {
                            lib_mng.Guardar();
                            pre_mng.Guardar();
                            Console.WriteLine("Saliendo del sistema...");
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
}