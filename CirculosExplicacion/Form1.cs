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
        private Bitmap originalImage;
        private Dictionary<int, Tuple<int, int, int>> centros;
        private Dictionary<int, List<Dictionary<int, VerticeConectado>>> conexiones; //Does not have to be list of dicts
        private bool sobreescribir;
        private Circle primera;
        private Grafo g;
        private Dictionary<int, Dictionary<int, List<Tuple<int, int>>>> caminos;
        private List<Agente> agentes;
        private Señuelo señuelo;

        public Form1()
        {
            this.agentes = new List<Agente>();
            InitializeComponent();
            this.sobreescribir = false;
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
            originalImage.Save("C:\\Users\\super\\Pictures\\new.png", System.Drawing.Imaging.ImageFormat.Png);

            if (sobreescribir)
            {
                nodosConectados.Nodes.Clear();
            }

            for (int i = 0; i < conexiones.Count; i++)
            {
                nodosConectados.Nodes.Add(i.ToString());
                foreach(Dictionary<int, VerticeConectado> lista in conexiones[i])
                {
                    foreach (int id in lista.Keys)
                    {
                        nodosConectados.Nodes[i].Nodes.Add(id.ToString());
                    }
                }
            }
            sobreescribir = true;
        }

        private void Ordenar()
        {
            dynamic temp;
            for (int i = 0; i < centros.Count - 1; i++)
            {
                for (int j = 0; j < centros.Count - i - 1; j++)
                {
                    if (centros[j].Item3 < centros[j + 1].Item3)
                    {
                        temp = centros[j + 1];
                        centros[j + 1] = centros[j];
                        centros[j] = temp;
                    }
                }
            }
        }

        private void Mostrar()
        {
            for (int i = 0; i < centros.Count; i++)
            {
                Console.WriteLine(centros[i]);
            }
        }

        private void botonOrdenar_Click(object sender, EventArgs e)
        {
            listaAntes.DataSource = new BindingSource(centros, null);
            Ordenar();
            listaDespues.DataSource = new BindingSource(centros, null);
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

        private void botonARM_Click(object sender, EventArgs e)
        {
            //double[,] matriz = conseguirMatriz();
            double[,] matriz = new double[5, 5] {{0, 2, 0, 6, 0},
                      {2, 0, 3, 8, 5},
                      {0, 3, 0, 0, 7},
                      {6, 8, 0, 0, 9},
                      {0, 5, 7, 9, 0},
                     };

            int V = (int)Math.Sqrt(matriz.Length); //Sacamos el número de nodos que hay
            int[] padre = new int[V];
            double[] llave = new double[V];
            bool[] conjuntoARM = new bool[V];

            for(int i = 0; i < V; i++)
            {
                llave[i] = double.MaxValue;
                conjuntoARM[i] = false;
            }

            llave[0] = 0;
            padre[0] = -1;

            for(int contador = 0; contador < V - 1; contador++)
            {
                int u = LLaveMin(llave, conjuntoARM);
                conjuntoARM[u] = true;

                for(int v = 0; v < V; v++)
                {
                    if(matriz[u, v] != 0 && conjuntoARM[v] == false && matriz[u, v] < llave[v])
                    {
                        padre[v] = u;
                        llave[v] = matriz[u, v];
                    }
                }
            }
            ImprimirARM(padre, V, matriz);
        }

        private int LLaveMin(double[] llave, bool[] conjuntoARM)
        {
            /*Retorna el índice mínimo
             * Argumentos: 
             * llave: Valores llave usados para conseguir el arista de menor tamaño
             * conjuntoARM: Representa los vértices que no se han incluido en el ARM
             */
            int V = llave.Length;
            double min = double.MaxValue;
            int indiceMin = 0;

            for(int v = 0; v < V; v++)
            {
                if(conjuntoARM[v] == false && llave[v] < min)
                {
                    min = llave[v];
                    indiceMin = v;
                }
            }

            return indiceMin;
        }

        private void ImprimirARM(int[] padre, int n, double[,] matriz)
        {
            int V = padre.Length;
            Console.WriteLine("Edge   Weight");
            for (int i = 1; i < V; i++)
                Console.WriteLine(string.Format("{0} - {1}    {2} ", padre[i], i, matriz[i, padre[i]]));
        }

        private double[,] conseguirMatriz()
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
    }
}