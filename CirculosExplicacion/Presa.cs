using System;
using System.Collections.Generic;
using System.Drawing;

namespace CirculosExplicacion
{
    class Presa : EntidadBase
    {
        private int siguiente;

        private int resistencia;
        private int acechadaPor; //-1 Significa que no está siendo acechada
        private Dictionary<int, List<int>> caminosMinimos;
        private static double[,] matrizAdy;
        private static int objetivoGlobal;
        private int objetivoLocal;

        public int AcechadaPor { get => acechadaPor; set => acechadaPor = value; }
        public Dictionary<int, List<int>> CaminosMinimos { get => caminosMinimos; set => caminosMinimos = value; }
        public static int ObjetivoGlobal { get => objetivoGlobal; set => objetivoGlobal = value; } //Cambiar propiedad
        public int Siguiente { get => siguiente; set => siguiente = value; }
        public int Resistencia { get => resistencia; private set => resistencia = value; }
        public int ObjetivoLocal { get => objetivoLocal; set => objetivoLocal = value; }

        public static void Inicializar(double[,] matriz)
        {
            matrizAdy = matriz;
        }

        public Presa(int actual, int inicial, int global, int velocidad, Color color)
        {
            Actual = actual;
            Inicial = inicial;
            Velocidad = velocidad;
            ColorEntidad = color;

            objetivoLocal = global;
            Resistencia = 1;
            acechadaPor = -1;
            caminosMinimos = new Dictionary<int, List<int>>();
            Dijkstra();
            NuevoDestino(); //Inicializa el primer destino
        }

        public  void NuevoDestino()
        {
            for (int i = 0; i <= caminosMinimos[objetivoLocal].Count - 2; i++)
            {
                if(caminosMinimos[objetivoLocal][i] == Actual)
                {
                    siguiente = caminosMinimos[objetivoLocal][i + 1];
                }
            }
        }

        public void Recalcular()
        {
            Dijkstra();
        }

        private void ImprimirCamino(int[] padre, int origen, List<int> camino)
        {
            if (padre[origen] == -1)
            {
                return;
            }
            ImprimirCamino(padre, padre[origen], camino);
            camino.Add(origen);
        }

        void GenerarCaminos(int[] padre, int origen)
        {
            int V = (int)Math.Sqrt(matrizAdy.Length);
            for (int v = 0; v < V; v++)
            {
                if (v != origen && padre[v] != -2)
                {
                    List<int> listTemp = new List<int>();
                    listTemp.Add(origen);
                    ImprimirCamino(padre, v, listTemp);
                    caminosMinimos.Add(v, listTemp);
                }
            }
        }

        private int DistanciaMinima(double[] distancias, bool[] visitados)
        {
            int V = (int)Math.Sqrt(matrizAdy.Length);
            double min = double.MaxValue;
            int idxMin = 0; //Inicializamos

            for (int v = 0; v < V; v++)
            {
                if (!visitados[v] && distancias[v] <= min)
                {
                    min = distancias[v];
                    idxMin = v;
                }
            }
            return idxMin;
        }

        private void Dijkstra()
        {
            caminosMinimos.Clear();
            int V = (int)Math.Sqrt(matrizAdy.Length);
            double[] distancias = new double[V];
            bool[] visitados = new bool[V];
            int[] padre = new int[V];

            for (int v = 0; v < V; v++)
            {
                distancias[v] = double.MaxValue;
            }

            for (int v = 0; v < V; v++) //-2 va a representar sin padre
            {
                padre[v] = -2;
            }

            padre[Actual] = -1;
            distancias[Actual] = 0;

            for (int contador = 0; contador < V - 1; contador++)
            {
                int u = DistanciaMinima(distancias, visitados);
                visitados[u] = true;
                for (int v = 0; v < V; v++)
                {
                    if (!visitados[v] && matrizAdy[u, v] != 0 && distancias[u] + matrizAdy[u, v] < distancias[v])
                    {
                        distancias[v] = distancias[u] + matrizAdy[u, v];
                        padre[v] = u;
                    }
                }
            }
            GenerarCaminos(padre, Actual);
        }

        public void AccionEvasiva()
        {
            Console.WriteLine("He tomado una accion evasiva");
            Dijkstra();
        }

        private void Morir()
        {
            Console.WriteLine("He muerto...");
        }

        public void PerderVida()
        {
            if(Resistencia > 0)
            {
                Resistencia -= 1;
            }
            else
            {
                Morir();
            }
        }

        public void GanarVida()
        {
            Resistencia += 1;
        }
    }
}
