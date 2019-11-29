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
        private int señuelos = 1;
        private int contador = 0;
        private Bitmap originalImage;
        private Dictionary<int, Tuple<int, int, int>> centros;
        private Dictionary<int, List<Dictionary<int, Tuple<int, int, int>>>> conexiones;
        private bool sobreescribir;
        private Circle primera;
        private Grafo g;
        private Señuelo señuelo;
        private Dictionary<int, Dictionary<int, List<Tuple<int, int>>>> caminos;
        private List<Agente> agentes;
        private bool encontrado;

        public Form1()
        {
            this.encontrado = false;
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
                foreach (Dictionary<int, Tuple<int, int, int>> lista in conexiones[i])
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

        private bool Caminar(Agente agente, int destino, Bitmap bmp)
        {
            if(agente.Velocidad + agente.Pos < caminos[agente.Actual][destino].Count - 1)
            {
                selectedImage.BackgroundImage = originalImage;
                selectedImage.BackgroundImageLayout = ImageLayout.Zoom; //Para que encuadre
                DibujarCirculo(caminos[agente.Actual][destino][agente.Pos].Item1, caminos[agente.Actual][destino][agente.Pos].Item2, bmp, 10, agente.Color);
                DibujarRadar(caminos[agente.Actual][destino][agente.Pos].Item1, caminos[agente.Actual][destino][agente.Pos].Item2, bmp, 10, agente.Color);
                agente.Pos += agente.Velocidad;
                return true;
            }
            else
            {
                DibujarCirculo(caminos[agente.Actual][destino][caminos[agente.Actual][destino].Count-1].Item1, caminos[agente.Actual][destino][caminos[agente.Actual][destino].Count - 1].Item2, bmp, 10, agente.Color);
                DibujarRadar(caminos[agente.Actual][destino][agente.Pos].Item1, caminos[agente.Actual][destino][agente.Pos].Item2, bmp, 10, agente.Color);
                agente.Pos = 0;
                return false;
            }
        }

        private int Buscar_Camino(Agente a, Dictionary<int, Dictionary<int, Dictionary<int, int>>> visitados)
        {
            bool todosVisitados = true;
            if (a.Actual == señuelo.Actual)
            {
                encontrado = true;
                SeñueloAleatorio();
                a.Velocidad += 10;
            }

            Dictionary<int, double> prioridad = new Dictionary<int, double>();
            foreach (int id in caminos[a.Actual].Keys)
            {
                if(visitados[a.Inicial][a.Actual][id] == 0)
                {
                    todosVisitados = false;
                }

                double thetaBait = Math.Atan2((señuelo.Y - caminos[a.Actual][id][0].Item2), (señuelo.X - caminos[a.Actual][id][0].Item1));
                double thetaEdge = Math.Atan2((caminos[a.Actual][id][caminos[a.Actual][id].Count-1].Item2 - caminos[a.Actual][id][0].Item2), (caminos[a.Actual][id][caminos[a.Actual][id].Count - 1].Item1 - caminos[a.Actual][id][0].Item1));
                if((thetaEdge > Math.PI) && (thetaBait == 0))
                {
                    thetaBait = (2 * Math.PI);
                }
                double tempdiff = Math.Abs(thetaBait - thetaEdge);
                prioridad.Add(id, tempdiff);
            }

            List<double> ordenado = prioridad.Values.ToList();
            var destino = from entry in prioridad where entry.Value == ordenado.Min() select entry.Key;
            int destinoactual = destino.FirstOrDefault();

            var iguales = visitados[a.Inicial][a.Actual].GroupBy(x => x.Value).Where(x => x.Count() > 1);
            using(var secuencia = iguales.GetEnumerator())
            {
                while (secuencia.MoveNext())
                {
                    Console.WriteLine(secuencia.Current);
                }
            }

           if(todosVisitados)
            {
                int id = visitados[a.Inicial][a.Actual].Aggregate((l, r) => l.Value < r.Value ? l : r).Key;
                return id;
            }
            else
            {
                if(visitados[a.Inicial][a.Actual][destinoactual] == 0)
                {
                    return destinoactual;
                }
                else
                {
                    int id = visitados[a.Inicial][a.Actual].Aggregate((l, r) => l.Value < r.Value ? l : r).Key;
                    return id;
                }
            }
        }

        private void SeñueloAleatorio()
        {
            Random aleatorio = new Random();
            int index = aleatorio.Next(centros.Count);
            señuelo.Actual = index;
            //Next two lines probably useless
            señuelo.X = centros[index].Item1;
            señuelo.Y = centros[index].Item2;
        }

        private void DarPaso(Dictionary<int, Dictionary<int, int>> destinos) //a.Actual y destino
        {
            List<int> terminados = new List<int>();
            int contadorTerminado = 0;
            using (Bitmap bmp = new Bitmap(originalImage))
            {
                while (contadorTerminado < agentes.Count)
                {
                    foreach (Agente agente in agentes)
                    {
                        if (!terminados.Contains(agente.Inicial))
                        {
                            selectedImage.Image = bmp;
                    selectedImage.Refresh();
                    Thread.Sleep(1);
                            if (!Caminar(agente, destinos[agente.Inicial][agente.Actual], bmp))
                            {
                                terminados.Add(agente.Inicial);
                                contadorTerminado++;
                            }
                        }
                    }
                }
            }
        }

        private void botonAnimar_Click(object sender, EventArgs e) //Actualmente el primer agente llega al señuelo seleccionado, y los demas llegan a un señuelo random
        {
            //agentes[0].Color = Color.Red;
            //agentes[1].Color = Color.Green;
            //agentes[2].Color = Color.Pink;
            //agentes[3].Color = Color.Yellow;

            Dictionary<int, Dictionary<int, Dictionary<int, int>>> visitados = new Dictionary<int, Dictionary<int, Dictionary<int, int>>>();
            foreach (Agente agente in agentes)
            {
                Dictionary<int, Dictionary<int, int>> aux = new Dictionary<int, Dictionary<int, int>>();
                agente.Velocidad = 10;
                agente.Color = Color.Red;
                foreach (int id in caminos.Keys)
                {
                    Dictionary<int, int> temp = new Dictionary<int, int>();
                    foreach (int conectado in caminos[id].Keys)
                    {
                        temp.Add(conectado, 0);
                    }
                    aux.Add(id, temp);
                }
                visitados.Add(agente.Inicial, aux);
            }

            while (contador < señuelos)
            {
                encontrado = false;
                Dictionary<int, Dictionary<int, int>> destinos = new Dictionary<int, Dictionary<int, int>>();
                while (!encontrado)
                {
                    foreach (Agente agente in agentes)
                    {
                        if (Existe_Camino(agente.Actual, señuelo.Actual))
                        {
                            //Agregar velocidad a agente actual, solo aumentamos velocidad en el metodo de buscar
                            destinos.Add(agente.Inicial, new Dictionary<int, int>() { { agente.Actual, señuelo.Actual } });
                            SeñueloAleatorio();
                            encontrado = true;
                            agente.Velocidad += 10;
                        }
                        else
                        {
                            int temp = Buscar_Camino(agente, visitados);
                            destinos.Add(agente.Inicial, new Dictionary<int, int>() { { agente.Actual, temp } });
                            visitados[agente.Inicial][agente.Actual][temp] += 1; //La visita la haces dos veces
                            visitados[agente.Inicial][temp][agente.Actual] += 1;
                        }
                    }
                    DarPaso(destinos);
                    foreach(Agente agente in agentes)
                    {
                        foreach (int id in destinos.Keys)
                        {
                            foreach (int actual in destinos[id].Keys)
                            {
                                if(agente.Inicial == id)
                                {
                                    agente.Actual = destinos[agente.Inicial][agente.Actual];
                                }
                        }
                        }
                    }
                    destinos.Clear();
                }
                contador++;
            }
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

        private void DibujarSeñuelo(int x, int y, Bitmap bmp, int radio, Color color)
        {
            using (var graphics = Graphics.FromImage(bmp))
            {
                graphics.FillEllipse(new SolidBrush(color), x - (radio / 2), y - (radio / 2), radio, radio);
            }
        }

        private void DibujarRadar(int x, int y, Bitmap bmp, int largo, Color color)
        {
            using (var graphics = Graphics.FromImage(bmp))
            {
                Pen p = new Pen(color, largo);
                p.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                p.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;

                double theta = Math.Atan2(señuelo.Y - y, señuelo.X - x);
                int flechax = (int)Math.Round(40 * Math.Cos(theta) + x);
                int flechay = (int)Math.Round(40 * Math.Sin(theta) + y);

                graphics.Clear(Color.Transparent);
                graphics.DrawLine(p, x, y, flechax, flechay);
            }
        }

        private bool Existe_Camino(int origen, int destino) //Se trabaja con IDs de vértice
        {
            return caminos[origen].ContainsKey(destino) ? true : false;
        }
    }
}