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
        private Bitmap originalImage;
        private Dictionary<int, Tuple<int, int, int>> centros;
        private Dictionary<int, List<Dictionary<int, VerticeConectado>>> conexiones; //Does not have to be list of dicts
        private bool sobreescribir;
        private Circle primera;
        private Grafo g;
        private Dictionary<int, Dictionary<int, List<Tuple<int, int>>>> caminos;
        private List<Agente> agentes;
        private Señuelo señuelo;
        private double[,] ARM;
        private bool encontradoDFS;
        private double pesoPrim, pesoKruskal;
        private List<string> pasosPrim, pasosKruskal;

        public Form1()
        {
            this.agentes = new List<Agente>();
            InitializeComponent();
            this.sobreescribir = false;
            this.encontradoDFS = false;
            this.pasosPrim = new List<string>();
            this.pasosKruskal = new List<string>();
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
            ARM = ConseguirMatrizARM();
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

        private void DibujarArista(int idOrigen, int idDestino, Bitmap imagen)
        {
            using (var graphics = Graphics.FromImage(imagen))
            {
                graphics.DrawLine(Pens.Red, centros[idOrigen].Item1, centros[idOrigen].Item2, centros[idDestino].Item1, centros[idDestino].Item2);
            }
        }

        private void GenerarARMPrim(double[,] matriz, int inicial, bool[] seleccionados, Bitmap image)
        {
            int V = (int)Math.Sqrt(matriz.Length);
            int numeroArista;
            numeroArista = 0; //Contador arista

            seleccionados[inicial] = true; //Seleccionamos el nodo raíz

            int x, y;

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
                    string aux = "{0} - {1} :  {2}";
                    pasosPrim.Add(string.Format(aux, x, y, matriz[x, y]));
                    ARM[x, y] = matriz[x, y]; //Lo hacemos no dirigido
                    ARM[y, x] = matriz[x, y];
                    DibujarArista(x, y, image);
                    selectedImage.Refresh();
                    seleccionados[y] = true;
                    numeroArista++;
                    pesoPrim += matriz[x, y];
                }
                else
                {
                    return;
                }
            }
        }

        private void botonPrim_Click(object sender, EventArgs e)
        {
            int raiz = Int32.Parse(Interaction.InputBox("Por favor elija nodo para que sea raíz", "Valor de Raíz", "0", -1, -1));
            double[,] matriz = ConseguirMatriz();

            int V = (int)Math.Sqrt(matriz.Length); //Sacamos el número de nodos que hay
            bool[] completados = new bool[V];
            GenerarARMPrim(matriz, raiz, completados, originalImage); //Si hay componentes disjuntos, crea el bosque
            for(int i = 0; i < V; i++)
            {
                if(completados[i] == false) //Agregamos esto para crear bosque
                {
                    GenerarARMPrim(matriz, i, completados, originalImage);
                }
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

        private void botonKruskal_Click(object sender, EventArgs e)
        {
            double[,] matriz = ConseguirMatriz();

            int V = (int)Math.Sqrt(matriz.Length);
            int[] padre = new int[V];

            for(int i = 0; i < V; i++) //Convertimos matriz a apropiada para Kruskal
            {
                for(int j = 0; j < V; j++)
                {
                    if(matriz[i, j] == 0)
                    {
                        matriz[i, j] = double.MaxValue;
                    }
                }
            }
            GenerarARMKruskal(padre, matriz, originalImage);
        }

        private  int Encontrar(int i, int[] padre)
        {
            while (padre[i] != i)
            {
                i = padre[i];
            }
            return i;
        }

        private void Union(int i, int j, int[] padre)
        {
            int a = Encontrar(i, padre);
            int b = Encontrar(j, padre);
            padre[a] = b;
        }

        private void GenerarARMKruskal(int[] padre, double[,] matriz, Bitmap imagen)
        {
            int V = (int)Math.Sqrt(matriz.Length);

            double costoMin = 0;
            for (int i = 0; i < V; i++)
                padre[i] = i;

            int numArista = 0;
            while (numArista < V - 1)
            {
                double min = double.MaxValue;
                int a = -1, b = -1;

                for (int i = 0; i < V; i++)
                {
                    for (int j = 0; j < V; j++)
                    {
                        if (Encontrar(i, padre) != Encontrar(j, padre) && matriz[i, j] < min)
                        {
                            min = matriz[i, j];
                            a = i;
                             b = j;
                        }
                    }
                }
                if(a != -1 && b != -1) //Solo calcula peso para grafos con componentes disjuntos
                {
                    Union(a, b, padre);
                    string aux = "Arista {0}: {1} - {2} con coste {3}";
                    pasosKruskal.Add(string.Format(aux, numArista++, a, b, min));
                    ARM[a, b] = min;
                    ARM[b, a] = min;
                    DibujarArista(a, b, imagen);
                    selectedImage.Refresh();
                    costoMin += min;
                }
                else
                {
                    Console.WriteLine("Costo Minimo: {0}", costoMin); //Costo del ARM
                    pesoKruskal = costoMin;
                    return;
                }
            }
            Console.WriteLine("Costo Minimo: {0}", costoMin); //Costo del ARM
            pesoKruskal = costoMin;
        }

        private void DFS(int i, bool[] visitados, int buscado, Bitmap bmp, int caller)
        {
            if (i == buscado)
            {
                encontradoDFS = true;
            }

            int V = (int)Math.Sqrt(ARM.Length);
            int j;
            visitados[i] = true;
            for (j = 0; j < V; j++)
            {
                if (!visitados[j] && ARM[i, j] != 0 && !encontradoDFS)
                {
                    Console.WriteLine("Vamos del nodo {0} al nodo {1}", i, j);
                    Caminar(i, j, bmp);
                    DFS(j, visitados, buscado, bmp, i);
                }
            }
            Console.WriteLine("Se terminó el nodo {0}", i);
            if (!encontradoDFS && i != caller)
            {
                Caminar(i, caller, bmp);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            int V = (int)Math.Sqrt(ARM.Length);
            bool[] visitados = new bool[V];
            int origen = Int32.Parse(txtOrigen.Text);
            int destino = Int32.Parse(txtDestino.Text);
            using (Bitmap bmp = new Bitmap(originalImage))
            {
                DFS(origen, visitados, destino, bmp, origen);
            }
            if(encontradoDFS)
            {
                Console.WriteLine("Se encuentra");
            }
            else
            {
                Console.WriteLine("No se encuentra");
            }
        }

        private void btnGenerarAmbos_Click(object sender, EventArgs e)
        {
            int raiz = Int32.Parse(Interaction.InputBox("Por favor elija nodo para que sea raíz", "Valor de Raíz", "0", -1, -1));
            Bitmap prim = new Bitmap(originalImage);
            Bitmap kruskal = new Bitmap(originalImage);

            double[,] matrizKruskal = ConseguirMatriz();

            int V = (int)Math.Sqrt(matrizKruskal.Length);
            int[] padre = new int[V];

            for (int i = 0; i < V; i++) //Convertimos matriz a apropiada para Kruskal
            {
                for (int j = 0; j < V; j++)
                {
                    if (matrizKruskal[i, j] == 0)
                    {
                        matrizKruskal[i, j] = double.MaxValue;
                    }
                }
            }
            GenerarARMKruskal(padre, matrizKruskal, kruskal);

            double[,] matrizPrim = ConseguirMatriz();

            V = (int)Math.Sqrt(matrizPrim.Length); //Sacamos el número de nodos que hay
            bool[] completados = new bool[V];
            GenerarARMPrim(matrizPrim, raiz, completados, prim); //Si hay componentes disjuntos, crea el bosque
            for (int i = 0; i < V; i++)
            {
                if (completados[i] == false) //Agregamos esto para crear bosque
                {
                    GenerarARMPrim(matrizPrim, i, completados, prim);
                }
            }

            using (var graphics = Graphics.FromImage(prim))
            {
                graphics.DrawString("Peso: " + pesoPrim.ToString(), new Font("Arial", 16), new SolidBrush(Color.Black), 0, 0);
            }
            using (var graphics = Graphics.FromImage(kruskal))
            {
                graphics.DrawString("Peso: " + pesoKruskal.ToString(), new Font("Arial", 16), new SolidBrush(Color.Black), 0, 0);
            }

            Form2 arboles = new Form2(prim, kruskal, pasosPrim, pasosKruskal);
            arboles.Show();
        }

        private void Caminar(int origen, int destino, Bitmap bmp)
        {
            int pos = 0;
            while(pos + 10 < caminos[origen][destino].Count - 1)
            {
                selectedImage.BackgroundImage = originalImage;
                selectedImage.BackgroundImageLayout = ImageLayout.Zoom; //Para que encuadre
                DibujarCirculo(caminos[origen][destino][pos].Item1, caminos[origen][destino][pos].Item2, bmp, 10, Color.Red);
                selectedImage.Image = bmp;
                selectedImage.Refresh();
                Thread.Sleep(1);
                pos += 10;
            }
        }
    }
}