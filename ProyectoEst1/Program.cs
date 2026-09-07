using System.Linq.Expressions;
using System.Reflection;

namespace ProyectoEst1
{
    class Program
    {
        public static void Main(string[] args)
        {   // se instancias los administradores de persistencia
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
                                        Libro libro = new Libro(cod, titulo, autor, genero, copias);
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
                                            if (pre_mng.admin.Prestado(cod_search) != null)
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
                                    lib_mng.admin.mostrar2();
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