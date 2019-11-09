using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public struct VerticeConectado //Clase para representar vertice conectado 
{
    public Tuple<int, int, int> informacion;
    public double distanciaEuclideana;

    public VerticeConectado(Tuple<int, int, int> informacionVertice, double distanciaPuntos)
    {
        informacion = informacionVertice;
        distanciaEuclideana = distanciaPuntos;
    }
}

namespace CirculosExplicacion
{
    class Grafo
    {
        private Pen lapiz;
        private Pen lapizCercanos;
        private Bitmap sample;
        private Color esperado;
        private Color temporal = Color.Magenta;
        private Color pintar = Color.SteelBlue;
        private Dictionary<int, Tuple<int, int, int>> vertices; //La lista de vertices a conectar
        public Dictionary<int, List<Dictionary<int, VerticeConectado>>> conexiones; //La lista de vertices y conexiones validas
        private List<Dictionary<int, VerticeConectado>> aristas; //Lista que contiene tuplas de vertices, se meten a conexiones
        private int valorCercano;
        private List<Tuple<int, int>> cercanos; //Contiene los dos vértices más cercanos
        public Dictionary<int, Dictionary<int, List<Tuple<int, int>>>> caminos; //Se trata de un diccionario para almacenar los puntos entre vertices

        public Grafo(Bitmap sample, Dictionary<int, Tuple<int, int, int>> vertices, Color esperado)
        {
            this.caminos = new Dictionary<int, Dictionary<int, List<Tuple<int, int>>>>();
            this.cercanos = new List<Tuple<int, int>>();
            this.esperado = esperado;
            this.conexiones = new Dictionary<int, List<Dictionary<int, VerticeConectado>>>();
            this.lapiz = new Pen(pintar, 1);
            this.lapizCercanos = new Pen(Color.Pink, 1);
            this.sample = sample;
            this.valorCercano = sample.Height * sample.Width;
            this.vertices = vertices;
            AnalizarImagen();
            DibujarLineas();
            EtiquetarVertices();
        }

        private bool Bresenham(int x, int y, int x2, int y2)
        {
            int bandera = 0;

            int xoriginal = x;
            int yoriginal = y;

            int ancho = x2 - x;
            int alto = y2 - y;

            int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
            dx1 = (ancho < 0) ? -1 : 1;
            dy1 = (alto < 0) ? -1 : 1;
            dx2 = (ancho < 0) ? -1 : 1;

            int largo = Math.Abs(ancho);
            int corto = Math.Abs(alto);

            if (!(largo > corto))
            {
                largo = Math.Abs(alto);
                corto = Math.Abs(ancho);
                dy2 = (alto < 0) ? -1 : 1;
                dx2 = 0;
            }

            if(largo < valorCercano) //Validamos la cercanía
            {
                cercanos.Clear();
                valorCercano = largo;
                cercanos.Add(Tuple.Create(x, y));
                cercanos.Add(Tuple.Create(x2, y2));
            }

            int numerador = largo >> 1;
            for (int i = 0; i <= largo; i++)
            {
               if(sample.GetPixel(x, y).ToArgb() == Color.White.ToArgb() && bandera == 0)
                {
                    bandera = 1;
                }
                if(sample.GetPixel(x, y).ToArgb() != Color.White.ToArgb() && bandera == 1)
                {
                    bandera = 2;
                }
                if(sample.GetPixel(x, y).ToArgb() == Color.White.ToArgb() && bandera == 2)
                {
                    return false;
                }
                numerador += corto;
                if (!(numerador < largo))
                {
                    numerador -= largo;
                    x += dx1;
                    y += dy1;
                }
                else
                {
                    x += dx2;
                    y += dy2;
                }
            }
            return true;
        }

        private List<Tuple<int, int>> Conseguir_Puntos(int x, int y, int x2, int y2)
        {
            List<Tuple<int, int>> puntos_temp = new List<Tuple<int, int>>();
            int ancho = x2 - x;
            int alto = y2 - y;

            int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
            dx1 = (ancho < 0) ? -1 : 1;
            dy1 = (alto < 0) ? -1 : 1;
            dx2 = (ancho < 0) ? -1 : 1;

            int largo = Math.Abs(ancho);
            int corto = Math.Abs(alto);

            if (!(largo > corto))
            {
                largo = Math.Abs(alto);
                corto = Math.Abs(ancho);
                dy2 = (alto < 0) ? -1 : 1;
                dx2 = 0;
            }

            int numerador = largo >> 1;
            for (int i = 0; i <= largo; i++)
            {
                puntos_temp.Add(Tuple.Create(x, y));
                numerador += corto;
                if (!(numerador < largo))
                {
                    numerador -= largo;
                    x += dx1;
                    y += dy1;
                }
                else
                {
                    x += dx2;
                    y += dy2;
                }
            }
            return puntos_temp; //NUNCA SE LLEGA ACA, VERIFICAR DISEÑO
        }

        private void AnalizarImagen()
        {
            for(int i = 0; i < vertices.Count; i++)
            {
                Dictionary<int, List<Tuple<int, int>>> caminos_temp = new Dictionary<int, List<Tuple<int, int>>>();
                aristas = new List<Dictionary<int, VerticeConectado>>();
                for (int j = 0; j < vertices.Count; j++)
                {
                    if (i != j)
                    {
                        if(Bresenham(vertices[i].Item1, vertices[i].Item2, vertices[j].Item1, vertices[j].Item2))
                        {
                            VerticeConectado verticeTemporal = new VerticeConectado(vertices[j], DistanciaEuclideana(vertices[i].Item1, vertices[i].Item2, vertices[j].Item1, vertices[j].Item2));
                            aristas.Add(new Dictionary<int, VerticeConectado>(){ { j, verticeTemporal } });
                            caminos_temp.Add(j, Conseguir_Puntos(vertices[i].Item1, vertices[i].Item2, vertices[j].Item1, vertices[j].Item2));
                        }
                    }
                }
                caminos.Add(i, caminos_temp);
                conexiones.Add(i, aristas);
            }
        }

        private void DibujarLineas()
        {
            for(int i = 0; i < conexiones.Count; i++)
            {
                foreach(Dictionary<int, VerticeConectado> vertice in conexiones[i])
                {
                    foreach(VerticeConectado informacion in vertice.Values)
                    {
                        using (var graphics = Graphics.FromImage(sample))
                        {
                            graphics.DrawLine(lapiz, vertices[i].Item1, vertices[i].Item2, informacion.informacion.Item1, informacion.informacion.Item2);
                        }
                    }
                }
            }
            if(cercanos.Count == 2)
            {
                using (var graphics = Graphics.FromImage(sample))
                {
                    graphics.DrawLine(lapizCercanos, cercanos[0].Item1, cercanos[0].Item2, cercanos[1].Item1, cercanos[1].Item2);
                }
            }
        }

        private void EtiquetarVertices()
        {
            for(int i = 0; i < vertices.Count; i++)
            {
                using(var graphics = Graphics.FromImage(sample))
                {
                    graphics.DrawString(i.ToString(), new Font("Arial", 16), new SolidBrush(Color.Black), vertices[i].Item1, vertices[i].Item2);
                }
            }
        }

        private double DistanciaEuclideana(int x, int y, int x2, int y2)
        {
            return Math.Sqrt(Math.Pow(x - x2, 2) + Math.Pow(y - y2, 2));
        }
    }
}
