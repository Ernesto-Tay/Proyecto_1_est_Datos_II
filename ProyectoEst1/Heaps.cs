using System;
using System.Collections.Generic;
namespace ProyectoEst1
{
    class Prestamo
    {
        public string destinatario { get; set; }
        public List<int> libros_prestados { get; set; } // aquí estarán los códigos
        public List<int> cantidad_prestada { get; set; } // y aquí las cantidades
        public int prioridad { get; set; }
        public Prestamo(List<int> Libros, List<int> cantidades)
        {
            this.destinatario = "";
            this.libros_prestados = Libros;
            this.cantidad_prestada = cantidades;
            this.prioridad = 0;
        }
        public void EstablecerPrioridad()
        {
            int total = 0;
            foreach (int cant in cantidad_prestada) total += cant;
            prioridad = total * libros_prestados.Count;
        }

        public void mostrar_datos()
        {
            Console.WriteLine($"Prestador: {destinatario}");
            for (int i = 0; i < libros_prestados.Count; i++)
            {
                Console.Write($"código: {libros_prestados[i]} - {cantidad_prestada[i]} und. | ");
            }
        }
    }

    class Admin_prestamos
    {
        private List<Prestamo> heap = new List<Prestamo>();

        // Funciones para obtener a los cercanos rápidamente
        private int padre(int indx) { return (indx - 1) / 2; }
        private int izq(int indx) { return 2 * indx + 1; }
        private int der(int indx) { return 2 * indx + 2; }

        private void heapify_up(int index) // función para 
        {
            do
            {
                int papa = padre(index);
                if (heap[index].prioridad < heap[papa].prioridad)
                {
                    Prestamo a = heap[index];
                    Prestamo b = heap[papa];
                    heap[index] = b;
                    heap[papa] = a;
                    index = papa;
                }
                else { break; }

            } while (index > 0);
        }

        private void heapify_down(int index)
        {
            int n = heap.Count;
            while (true)
            {
                int izqrd = izq(index);
                int derc = der(index);
                int min = index;

                if (izqrd < n && heap[izqrd].prioridad < heap[min].prioridad) min = izqrd;
                if (derc < n && heap[derc].prioridad < heap[min].prioridad) min = derc;

                if (min != index)
                {
                    Prestamo a = heap[index];
                    Prestamo b = heap[min];
                    heap[index] = b;
                    heap[min] = a;
                    index = min;
                }
                else { break; }
            }
        }

        public bool agregar(List<int> codigos, List<int> cantidades, string dest) // agrega un nuevo préstamo a la lista
        {
            Prestamo nuevo = new Prestamo(codigos, cantidades);
            nuevo.destinatario = dest;
            if (Buscar(nuevo))
            {
                return false;
            }
            nuevo.EstablecerPrioridad();
            heap.Add(nuevo);
            heapify_up(heap.Count - 1);
            return true;
        }

        public Prestamo? consultar() // Consulta el siguiente en atender
        {
            if (heap.Count == 0)
            {
                return null;
            }
            return heap[0];
        }

        public Prestamo? atender() // atiende el préstamo actual, y reordena con heapify_down
        {
            if (heap.Count == 0) return null;
            Prestamo raiz = heap[0];
            Prestamo ultimo = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);
            if (heap.Count > 0)
            {
                heap[0] = ultimo;
                heapify_down(0);
            }
            return raiz;
        }

        public void mostrar_cola()
        {
            if (heap.Count == 0)
            {
                Console.WriteLine("La cola está vacía");
                return;
            }
                            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("--------------- Cola actual ---------------");
            Console.ResetColor();
            int i = 0;
            foreach (Prestamo val in heap)
            {
                i += 1;
                Console.Write($"Índice {i}: ");
                val.mostrar_datos();
                Console.WriteLine();
            }
        }

        public List<Prestamo> recorrer()
        {
            return heap;
        }
        public List<Prestamo>? Prestado(int codigo)
        {
            List<Prestamo> prestados = new List<Prestamo>();
            foreach (Prestamo prestamo in heap)
            {
                if (prestamo.libros_prestados.Contains(codigo))
                {

                    prestados.Add(prestamo);
                }
            }
            if (prestados.Count > 0) return prestados;
            return null;
        }
        public bool Buscar(Prestamo p)
        {
            foreach (Prestamo existente in heap)
            {
                if (existente.destinatario == p.destinatario && ListasIguales(existente.libros_prestados, p.libros_prestados) && ListasIguales(existente.cantidad_prestada, p.cantidad_prestada)) {return true;}
            }
            return false;
        }

        private bool ListasIguales(List<int> a, List<int> b)
        {
            if (a.Count != b.Count) return false;
            for (int i = 0; i < a.Count; i++)
            {
                if (a[i] != b[i]) return false;
            }
            return true;
        }
    }

    class Admin_libros
    {
        private List<Libro> heap = new List<Libro>();

        // Funciones para obtener a los cercanos rápidamente
        private int padre(int indx) { return (indx - 1) / 2; }
        private int izq(int indx) { return 2 * indx + 1; }
        private int der(int indx) { return 2 * indx + 2; }

        private void heapify_up(int index) // función para 
        {
            do
            {
                int papa = padre(index);
                if (heap[index].veces_prestado > heap[papa].veces_prestado)
                {
                    Libro a = heap[index];
                    Libro b = heap[papa];
                    heap[index] = b;
                    heap[papa] = a;
                    index = papa;
                }
                else { break; }

            } while (index > 0);
        }

        private void heapify_down(int index)
        {
            int n = heap.Count;
            while (true)
            {
                int izqrd = izq(index);
                int derc = der(index);
                int max = index;

                if (izqrd < n && heap[izqrd].veces_prestado > heap[max].veces_prestado) max = izqrd;
                if (derc < n && heap[derc].veces_prestado > heap[max].veces_prestado) max = derc;

                if (max != index)
                {
                    Libro a = heap[index];
                    Libro b = heap[max];
                    heap[index] = b;
                    heap[max] = a;
                    index = max;
                }
                else { break; }
            }
        }

        public void agregar(Libro nuevo) // agrega un nuevo libro a la lista
        {
            heap.Add(nuevo);
            heapify_up(heap.Count - 1);
        }

        public Libro? consultar() // Consulta el siguiente en el ranking (top 2)
        {
            if (heap.Count == 0)
            {
                return null;
            }
            return heap[0];
        }


        public void mostrar_ranking()
        {
            List<Libro> copia = new List<Libro>(heap);
            copia.Sort((a, b) => -a.veces_prestado.CompareTo(b.veces_prestado));
            int i = 0;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"No.\t{"TITULO".PadRight(30, ' ')}{"AUTOR".PadRight(15, ' ')}{"PRESTACIONES".PadRight(5, ' ')}");
            Console.ResetColor();
            foreach (Libro libro in copia)
            {
                i += 1;
                Console.WriteLine($"{i}\t{libro.titulo.PadRight(30, ' ')}{libro.autor.PadRight(15, ' ')}{Convert.ToString(libro.veces_prestado).PadRight(5, ' ')}");
            }
        }


        public void mostrar_cola() // muestra la cola de libros
        {
            if (heap.Count == 0)
            {
                Console.WriteLine("La cola está vacía");
                return;
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("--------------- Cola actual ---------------");
            Console.ResetColor();
            int i = 0;
            foreach (Libro val in heap)
            {
                i += 1;
                Console.Write($"Índice {i}: ");
                val.MostrarInfo();
                Console.WriteLine();
            }
        }

        public List<Libro> recorrer() // devuelve el heap actual de libros
        {
            return heap;
        }
        public void upd_prioridad(Libro libro)
        {
            int index = heap.IndexOf(libro);
            if (index == -1) return;
            heapify_up(index);
        }
        public void bye_libro(int codigo) // FUNCIÓN para eliminar un libro elimiando en el arbol, y estar en sintonía
        {

            int index = heap.IndexOf // 2. luego encontramos su índice
                (
                    heap.Find(lib => lib.codigo == codigo) // 1. primero encontramos el valor con Lambda
                );
            heap.RemoveAt(index); // 3. se elimina el valor
            heapify_down(index); // 4. y se reordena el arbolito
        }
    }
}   