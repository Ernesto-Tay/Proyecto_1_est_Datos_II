using System.ComponentModel;

namespace ProyectoEst1
{
    class MaxHeapNodo
    {
        public int prioridad { get; set; }
        public Libro libro { get; set; }

        public MaxHeapNodo(int prior)
        {
            this.prioridad = prioridad;
        }
    }

    class Prestamo
    {
        public List<int> libros_prestados = new List<int>(); // aquí estarán los códigos
        public List<int> cantidad_prestada = new List<int>(); // y aquí las cantidades
        public int prioridad { get; set; }

        public void EstablecerPrioridad()
        {
            int total = 0;
            foreach (int cant in cantidad_prestada) total += cant;
            prioridad = total * libros_prestados.Count;
        }
    }

    class Admin_prestamos
    {
        private List<Prestamo> heap = new List<Prestamo>();

        // Funciones para obtener a los cercanos rápidamente
        private int padre(int indx){return (indx-1)/2;}
        private int izq(int indx){return 2*indx + 1;}
        private int der(int indx) { return 2 * indx + 2; }

        private void heapify_up(int index)
        {
            do
            {
                int papa = padre(index);
                if (heap[index].prioridad > heap[papa].prioridad)
                {
                    Prestamo a = heap[index];
                    Prestamo b = heap[papa];
                    heap[index] = b;
                    heap[papa] = a;
                    index = papa;
                }
                else {break;}

            } while (index > 0);
        }

        
    }
} 