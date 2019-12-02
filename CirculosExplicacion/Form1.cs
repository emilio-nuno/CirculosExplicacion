using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace CirculosExplicacion //TODO: CAMBIAR EL SORT A EL MAYOR DE LOS DOS RADIOS
{
    /*Developer Changelog 3rd Assignment (Animation):
     * 13/10/2019: Added dictionary (camino) that associates a given vertex with all other vertices it is connected to, as well as storing a list of all points between them
     * 13/10/2019: Added method to check if there is a connection between two vertices
     * caminos dict usage: To check all points between vertex 0 and vertex 1 (connection is assumed) do this: caminos[0][1]
     * Existe_Camino method usage: To check if there is a connection between vertex 0 and 1 do this: Existe_Camino(0, 1)
     */

    public partial class Form1 : Form
    {//Usar listbox
        private Dictionary<int, List<int>> caminosMinimos; //Mover a local para poder reiniciar correctamente
        private Bitmap originalImage;
        private Bitmap imagenEditar;
        private Dictionary<int, Tuple<int, int, int>> centros;
        private Dictionary<int, List<Dictionary<int, VerticeConectado>>> conexiones; //Does not have to be list of dicts
        private bool sobreescribir;
        private Circle primera;
        private Grafo g;
        private Dictionary<int, Dictionary<int, List<Tuple<int, int>>>> caminos;
        private List<Agente> agentes;
        private Señuelo señuelo;
        private double[,] ARM;

        public Form1()
        {
            this.agentes = new List<Agente>();
            InitializeComponent();
            this.sobreescribir = false;
            this.caminosMinimos = new Dictionary<int, List<int>>();
        }

        private void botonSelect_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.ShowDialog();
            originalImage = new Bitmap(openFileDialog1.FileName);
            this.selectedImage.Image = originalImage; //validar null por si se cancela
        }

        private void botonFindCenter_Click(object sender, EventArgs e)
        {
            primera = new Circle(originalImage);
            primera.Empezar();
            centros = primera.centros;
            listBox1.DataSource = new BindingSource(centros, null);
            selectedImage.Refresh(); //Refleja los cambios de la clase de circulo
        }

        private void botonConectar_Click(object sender, EventArgs e)
        {
            g = new Grafo(originalImage, centros, primera.pintar);
            conexiones = g.conexiones;
            caminos = g.caminos;
            selectedImage.Refresh();
            imagenEditar = new Bitmap(originalImage);
            originalImage.Save("C:\\Users\\super\\Pictures\\new.png", System.Drawing.Imaging.ImageFormat.Png);
            if (sobreescribir)
            {
                nodosConectados.Nodes.Clear();
            }
            sobreescribir = true;
            ARM = ConseguirMatrizARM();
        }

        private void nodosConectados_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            ((TreeView)sender).SelectedNode = e.Node;
        }

        private void origenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            agentes.Add(new Agente(centros[Int32.Parse(nodosConectados.SelectedNode.Text)].Item1, centros[Int32.Parse(nodosConectados.SelectedNode.Text)].Item2, 50, Int32.Parse(nodosConectados.SelectedNode.Text), Int32.Parse(nodosConectados.SelectedNode.Text), Color.Transparent)); //Agregar color manual
        }

        private void destinoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            señuelo = new Señuelo(centros[Int32.Parse(nodosConectados.SelectedNode.Text)].Item1, centros[Int32.Parse(nodosConectados.SelectedNode.Text)].Item2, Int32.Parse(nodosConectados.SelectedNode.Text));
        }

        private void DibujarCirculo(int x, int y, Bitmap bmp, int radio, Color color)
        {
            using (var graphics = Graphics.FromImage(bmp))
            {
                graphics.Clear(Color.Transparent);
                graphics.FillEllipse(new SolidBrush(color), x - (radio / 2), y - (radio / 2), radio, radio);
            }
        }

        private void DibujarArista(int idOrigen, int idDestino, Bitmap imagen, Color color)
        {
            using (var graphics = Graphics.FromImage(imagen))
            {
                graphics.DrawLine(new Pen(color, 5), centros[idOrigen].Item1, centros[idOrigen].Item2, centros[idDestino].Item1, centros[idDestino].Item2);
            }
        }

        private double[,] ConseguirMatriz()
        {
            double[,] matriz = new double[conexiones.Count, conexiones.Count];
            foreach (int id in conexiones.Keys)
            {
                foreach (Dictionary<int, VerticeConectado> conexion in conexiones[id])
                {
                    foreach (int idConectado in conexion.Keys)
                    {
                        matriz[id, idConectado] = conexion[idConectado].distanciaEuclideana;
                    }
                }
            }
            return matriz;
        }

        private double[,] ConseguirMatrizARM()
        {
            double[,] matriz = new double[conexiones.Count, conexiones.Count];
            foreach (int id in conexiones.Keys)
            {
                foreach (Dictionary<int, VerticeConectado> conexion in conexiones[id])
                {
                    foreach (int idConectado in conexion.Keys)
                    {
                        matriz[id, idConectado] = 0;
                    }
                }
            }
            return matriz;
        }

        private int DistanciaMinima(double[,] matriz, double[] distancias, bool[] visitados)
        {
            int V = (int)Math.Sqrt(matriz.Length);
            double min = double.MaxValue;
            int idxMin = 0; //Inicializamos

            for (int v = 0; v < V; v++)
            {
                if(!visitados[v] && distancias[v] <= min)
                {
                    min = distancias[v];
                    idxMin = v;
                }
            }
            return idxMin;
        }

        private void ImprimirCamino(int[] padre, int origen, List<int> camino)
        {
            if(padre[origen] == -1)
            {
                return;
            }
            ImprimirCamino(padre, padre[origen], camino);
            camino.Add(origen);
        }

        void GenerarCaminos(int[] padre, int numNodos, int origen)
        {
            for (int v = 0; v < numNodos; v++)
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

        private Color ColorAleatorio(Random rnd)
        {
            Color colorRandom = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
            return colorRandom;
        }

        private void DibujarCaminos(Random rnd)
        {
            foreach (int vertice in caminosMinimos.Keys)
            {
                for (int paso = 0; paso <= caminosMinimos[vertice].Count - 2; paso++) //vamos de n a n-1
                {
                    Color colorSeleccionado = ColorAleatorio(rnd);
                    DibujarArista(caminosMinimos[vertice][paso], caminosMinimos[vertice][paso + 1], imagenEditar, colorSeleccionado);
                }
            }
        }

        private void Dijkstra(int origen)
        {
            nodosConectados.Nodes.Clear();
            imagenEditar = (Bitmap)originalImage.Clone();
            caminosMinimos.Clear();
            selectedImage.Image = imagenEditar;
            selectedImage.Refresh();

            double[,] matriz = ConseguirMatriz();
            int V = (int)Math.Sqrt(matriz.Length); //Como construir ARM
            double[] distancias = new double[V];
            bool[] visitados = new bool[V]; //Bool se inicializa en falso
            int[] padre = new int[V]; //Contiene los caminos

            for (int v = 0; v < V; v++)
            {
                distancias[v] = double.MaxValue;
            }

            for (int v = 0; v < V; v++) //-2 va a representar sin padre
            {
                padre[v] = -2;
            }

            padre[origen] = -1;

            distancias[origen] = 0;

            for (int contador = 0; contador < V - 1; contador++)
            {
                int u = DistanciaMinima(matriz, distancias, visitados);
                visitados[u] = true;
                for (int v = 0; v < V; v++)
                {
                    if (!visitados[v] && matriz[u, v] != 0 && distancias[u] + matriz[u, v] < distancias[v])
                    {
                        distancias[v] = distancias[u] + matriz[u, v];
                        padre[v] = u;
                    }
                }
            }
            GenerarCaminos(padre, V, origen);
            Random rnd = new Random();
            DibujarCaminos(rnd);
            selectedImage.Image = imagenEditar;
            selectedImage.Refresh();

            int i = 0;
            foreach (int vertice in caminosMinimos.Keys)
            {
                nodosConectados.Nodes.Add(vertice.ToString());
                foreach (int paso in caminosMinimos[vertice])
                {
                    nodosConectados.Nodes[i].Nodes.Add(paso.ToString());
                }
                i++;
            }
        }

        private void btnDijkstra_Click(object sender, EventArgs e)
        {
            int origen = Int32.Parse(Interaction.InputBox("Por favor elija el origen", "Vértce Origen", "0", -1, -1));
            Dijkstra(origen);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            int destino = Int32.Parse(txtDestino.Text);
            if (!caminosMinimos.ContainsKey(destino))
            {
                MessageBox.Show("No se puede llegar a el vértice deseado desde el origen actual");
                return;
            }
            using (Bitmap bmp = new Bitmap(imagenEditar))
            {
                for (int paso = 0; paso <= caminosMinimos[destino].Count - 2; paso++)
                {
                    Caminar(caminosMinimos[destino][paso], caminosMinimos[destino][paso+1], bmp);
                }
            }
            int actual = caminosMinimos[destino][caminosMinimos[destino].Count - 1];
            Dijkstra(actual);
        }

        private void Caminar(int origen, int destino, Bitmap bmp)
        {
            int pos = 0;
            while (pos + 10 < caminos[origen][destino].Count - 1)
            {
                selectedImage.BackgroundImage = originalImage;
                selectedImage.BackgroundImageLayout = ImageLayout.Zoom; //Para que encuadre
                DibujarCirculo(caminos[origen][destino][pos].Item1, caminos[origen][destino][pos].Item2, bmp, 40, Color.Blue);
                selectedImage.Image = bmp;
                selectedImage.Refresh();
                Thread.Sleep(1);
                pos += 10;
            }
        }
    }
}