using System.Linq.Expressions;
using System.Reflection;

namespace ProyectoEst1
{
    class Program
    {
        public static void Main(string[] args)
        {   // se instancias los administradores de persistencia
            string input1 = "0";
            GuardarLibros lib_mng = new GuardarLibros();
            GuardarPrestamos pre_mng = new GuardarPrestamos();
            while (input1 != "3")
            {
                lib_mng.Cargar();
                pre_mng.Cargar();
                Console.WriteLine("\n-------------------SISTEMA-------------------\n1. Ir a menú de libros\n2. ir a menú de préstamos\n3. Salir del sistema");
                Console.Write("Seleccione una opción: ");
                input1 = Console.ReadLine();
                switch (input1)
                {
                    case "1":
                        {
                            Console.WriteLine("\n---------- MENÚ LIBROS ----------\n1. Agregar libro\n2. Ver catálogo\n3. Buscar libro\n4. Eliminar libro\n5. ver libros más prestados\n6. volver al menú");
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
                                                throw new DuplicateWaitObjectException("Ya existe un valor con ese nombre");
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
                                            Libro libro = new Libro(cod, titulo, autor, genero, copias, 0);
                                            lib_mng.admin.Insertar(libro);
                                            Console.WriteLine("Libro insertado con éxito");
                                        }
                                        catch (ArgumentNullException ex)
                                        {
                                            Console.WriteLine("ingrese los tipos de valor correspondientes");
                                        }
                                        catch (DuplicateWaitObjectException x)
                                        {
                                            Console.WriteLine($"Error: {x}");
                                        }
                                        catch (Exception x)
                                        {
                                            Console.WriteLine($"Error: {x}");
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
                                            Console.WriteLine($"Error: {x}");
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
                                                    lib_mng.admin.eliminar(cod_search);
                                                    lib_mng.admin2.bye_libro(cod_search);
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Ese libro tiene préstamos activos, por lo que no se puede eliminar.");
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("No se encontró un libro con ese código");
                                            }
                                        }
                                        catch (Exception x)
                                        {
                                            Console.WriteLine($"Error: {x}");
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
                            Console.WriteLine("\n---------- MENÚ PRÉSTAMOS ----------\n1. Añadir préstamo\n2. Buscar préstamo\n3. Atender préstamo\n4. Mostrar cola de préstamos\n5. Mostrar siguiente préstamo en cola\n6. volver al menú");
                            Console.Write("Selecciona una opción: ");
                            string input3 = Console.ReadLine();
                            switch (input3)
                            {
                                case "1":
                                    {
                                        try
                                        {
                                            lib_mng.admin.mostrar_pa_prestamo(); // muestra libros disponibles

                                            List<int> codigos = new List<int>();
                                            List<int> cantidades = new List<int>();
                                            Console.WriteLine("Ingrese códigos a prestar (0 para terminar):");
                                            while (true)
                                            {
                                                Console.Write("Código: ");
                                                int cod = Convert.ToInt32(Console.ReadLine());
                                                if (cod == 0) break;

                                                Libro libro = lib_mng.admin.BuscarLibro(cod);
                                                if (libro == null) { Console.WriteLine("No existe ese código."); continue; }

                                                Console.Write("Cantidad a prestar: ");
                                                int cant = Convert.ToInt32(Console.ReadLine());
                                                if (cant <= 0 || cant > libro.copias) { Console.WriteLine("Cantidad inválida."); continue; }

                                                codigos.Add(cod);
                                                cantidades.Add(cant);
                                            }

                                            if (codigos.Count == 0)
                                            {
                                                Console.WriteLine("No se agregaron libros al préstamo.");
                                            }
                                            else
                                            {
                                                pre_mng.admin.agregar(codigos, cantidades);
                                                lib_mng.admin.actualizar_stock(codigos, cantidades);

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
                                        }
                                        catch (Exception x) { Console.WriteLine($"Error: {x}"); }
                                        break;
                                    }
                                case "2":
                                    {
                                        try
                                        {
                                            Console.Write("Código de libro a buscar en préstamos: ");
                                            int cod_buscar = Convert.ToInt32(Console.ReadLine());
                                            Prestamo encontrado = pre_mng.admin.Prestado(cod_buscar);
                                            if (encontrado != null)
                                            {
                                                Console.WriteLine("Préstamo encontrado: ");
                                                encontrado.mostrar_datos();
                                                Console.WriteLine();
                                            }
                                            else Console.WriteLine("No se encontró préstamo con ese código.");
                                        }
                                        catch (Exception x) { Console.WriteLine($"Error: {x}"); }
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
                                        Console.WriteLine("Opción inválida.");
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