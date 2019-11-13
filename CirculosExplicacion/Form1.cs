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

        private void DibujarArista(int idOrigen, int idDestino)
        {
            using (var graphics = Graphics.FromImage(selectedImage.Image))
            {
                graphics.DrawLine(Pens.Red, centros[idOrigen].Item1, centros[idOrigen].Item2, centros[idDestino].Item1, centros[idDestino].Item2);
            }
        }

        private void GenerarARM(double[,] matriz, int inicial, bool[] seleccionados)
        {
            int V = (int)Math.Sqrt(matriz.Length);
            int numeroArista;
            numeroArista = 0; //Contador arista

            seleccionados[inicial] = true; //Seleccionamos el nodo raíz

            int x, y;
            Console.WriteLine("Edge : Weight");

            //Un ARM siempre tendrá V-1 aristas, por el nodo raíz
            while (numeroArista < V - 1)
            {
                double min = double.MaxValue;
                x = y = -1;

                for (int i = 0; i < V; i++)
                {
                    if (seleccionados[i])
                    {
                        for (int j = 0; j < V; j++)
                        {
                            if (!seleccionados[j] && matriz[i, j] != 0)
                            {
                                if (min > matriz[i, j])
                                {
                                    min = matriz[i, j];
                                    x = i;
                                    y = j;
                                }
                            }
                        }
                    }
                }
                if (x != -1 && y != -1) //Cuando x y y sean el valor inicial dado (-1), ya no se encuentran minimos en el componente
                {
                    Console.WriteLine("{0} - {1} :  {2}", x, y, matriz[x, y]);
                    DibujarArista(x, y);
                    selectedImage.Refresh();
                    seleccionados[y] = true;
                    numeroArista++;
                }
                else
                {
                    return;
                }
            }
        }

        private void botonARM_Click(object sender, EventArgs e)
        {
            double[,] matriz = ConseguirMatriz();
            int V = (int)Math.Sqrt(matriz.Length); //Sacamos el número de nodos que hay
            bool[] completados = new bool[V];
            GenerarARM(matriz, 5, completados); //Si hay componentes disjuntos, crea el bosque
            for(int i = 0; i < V; i++)
            {
                if(completados[i] == false)
                {
                    GenerarARM(matriz, i, completados);
                }
            }
            Console.WriteLine("TERMINADO");
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
    }
}