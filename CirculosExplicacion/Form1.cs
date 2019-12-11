using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
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
     //Yellow for predator hunting and pink for hunted prey
        private Bitmap originalImage;
        private Dictionary<int, Tuple<int, int, int>> centros;
        private Dictionary<int, List<Dictionary<int, VerticeConectado>>> conexiones; //Does not have to be list of dicts
        private bool sobreescribir;
        private Circle primera;
        private Grafo g;
        private Dictionary<int, Dictionary<int, List<Tuple<int, int>>>> caminos;
        private List<Presa> presas;
        private List<Depredador> depredadores;
        private bool finSim;
        private List<int> verticesActuales;

        public Form1()
        {
            this.presas = new List<Presa>();
            this.depredadores = new List<Depredador>();
            InitializeComponent();
            this.sobreescribir = false;
            finSim = false;
            verticesActuales = new List<int>();
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
                foreach (Dictionary<int, VerticeConectado> lista in conexiones[i])
                {
                    foreach (int id in lista.Keys)
                    {
                        nodosConectados.Nodes[i].Nodes.Add(id.ToString());
                    }
                }
            }
            Presa.Inicializar(ConseguirMatriz());
            sobreescribir = true;
        }

        private void nodosConectados_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            ((TreeView)sender).SelectedNode = e.Node;
        }

        private void objetivoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Presa.ObjetivoGlobal = Int32.Parse(nodosConectados.SelectedNode.Text);
        }

        private void origenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            presas.Add(new Presa(Int32.Parse(nodosConectados.SelectedNode.Text), Int32.Parse(nodosConectados.SelectedNode.Text), Presa.ObjetivoGlobal, 10, Color.Red)); //Agregar color manual
        }

        private void destinoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Depredador dTemp = new Depredador(Int32.Parse(nodosConectados.SelectedNode.Text), Int32.Parse(nodosConectados.SelectedNode.Text), 200, 5, Color.Blue);
            dTemp.Siguiente = DestinoAleatorio(dTemp); //Le damos un primer destino, como a l
            depredadores.Add(dTemp);
        }

        private void DibujarPresa(Presa presa, Bitmap bmp, Color color)
        {
            if(presa.AcechadaPor != null)
            {
                using (var graphics = Graphics.FromImage(bmp)) //Checar manera más elegante de expresar esto
                {
                    graphics.Clear(Color.Transparent);
                    graphics.FillEllipse(new SolidBrush(Color.Pink), presa.X - (presa.Tamaño / 2), presa.Y - (presa.Tamaño / 2), presa.Tamaño, presa.Tamaño);
                    graphics.DrawString(presa.Resistencia.ToString(), new Font("Arial", 16), new SolidBrush(Color.Black), presa.X + 10, presa.Y + 10);
                }
            }
            else
            {
                using (var graphics = Graphics.FromImage(bmp))
                {
                    graphics.Clear(Color.Transparent);
                    graphics.FillEllipse(new SolidBrush(color), presa.X - (presa.Tamaño / 2), presa.Y - (presa.Tamaño / 2), presa.Tamaño, presa.Tamaño);
                    graphics.DrawString(presa.Resistencia.ToString(), new Font("Arial", 16), new SolidBrush(Color.Black), presa.X + 10, presa.Y + 10);
                }
            }
        }

        private void DibujarDepredador(Depredador depredador, Bitmap bmp, Color color)
        {
            if(depredador.PresaAcechada != null)
            {
                using (var graphics = Graphics.FromImage(bmp))
                {
                    graphics.Clear(Color.Transparent);
                    graphics.FillEllipse(new SolidBrush(Color.Yellow), depredador.X - (depredador.Tamaño / 2), depredador.Y - (depredador.Tamaño / 2), depredador.Tamaño, depredador.Tamaño);
                    graphics.DrawEllipse(new Pen(depredador.ColorRadio), depredador.X - ((depredador.RadioDepredador + depredador.Tamaño) / 2), depredador.Y - ((depredador.RadioDepredador + depredador.Tamaño) / 2), depredador.RadioDepredador + depredador.Tamaño, depredador.RadioDepredador + depredador.Tamaño); //Hacer esto opcional
                }
            }
            else
            {
                using (var graphics = Graphics.FromImage(bmp))
                {
                    graphics.Clear(Color.Transparent);
                    graphics.FillEllipse(new SolidBrush(color), depredador.X - (depredador.Tamaño / 2), depredador.Y - (depredador.Tamaño / 2), depredador.Tamaño, depredador.Tamaño);
                    graphics.DrawEllipse(new Pen(depredador.ColorRadio), depredador.X - ((depredador.RadioDepredador + depredador.Tamaño) / 2), depredador.Y - ((depredador.RadioDepredador + depredador.Tamaño) / 2), depredador.RadioDepredador + depredador.Tamaño, depredador.RadioDepredador + depredador.Tamaño); //Hacer esto opcional
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

        private void btnDijkstra_Click(object sender, EventArgs e)
        {
            using (Bitmap bmp = new Bitmap(originalImage))
            {
                while (!finSim){
                    foreach (Presa pTemporal in presas)
                    {
                        if (!pTemporal.Muerto)
                        {
                            CaminarPresa(pTemporal, bmp);
                            selectedImage.Image = bmp;
                            selectedImage.Refresh();
                            Thread.Sleep(1);
                        }
                    }
                    foreach (Depredador dTemporal in depredadores)
                    {
                        if(dTemporal.PresaAcechada != null)
                        {
                            if(dTemporal.Actual == dTemporal.PresaAcechada.Siguiente)
                            {
                                if(dTemporal.DecisionQuedarse(caminos[dTemporal.PresaAcechada.Actual][dTemporal.PresaAcechada.Siguiente].Count - 1))
                                {
                                    dTemporal.TiempoQuedarseTemp = Depredador.TiempoQuedarse;
                                }
                            }
                        }

                        if(dTemporal.TiempoQuedarseTemp > 0)
                        {
                            DibujarDepredador(dTemporal, bmp, dTemporal.ColorEntidad);
                            if (dTemporal.PresaAcechada != null)
                            {
                                if (dTemporal.Colision(dTemporal.PresaAcechada))
                                {
                                    dTemporal.PresaAcechada.PerderVida();
                                    if (dTemporal.PresaAcechada.Muerto)
                                    {
                                        dTemporal.PresaAcechada.AcechadaPor = null;
                                        dTemporal.PresaAcechada = null;
                                    }
                                }
                            }
                            dTemporal.TiempoQuedarseTemp -= 1;
                        }
                        else
                        {
                            Presa mejorPresa = null;
                            if (dTemporal.PresaAcechada != null)
                            {
                                CaminarDepredador(dTemporal, bmp, dTemporal.FactorVelocidad(dTemporal.PresaAcechada));
                            }
                            else
                            {
                                CaminarDepredador(dTemporal, bmp);
                            }
                            CaminarDepredador(dTemporal, bmp);
                            selectedImage.Image = bmp;
                            selectedImage.Refresh();
                            Thread.Sleep(1);

                            if (dTemporal.PresaAcechada != null)
                            {
                                if (dTemporal.Colision(dTemporal.PresaAcechada))
                                {
                                    dTemporal.PresaAcechada.PerderVida();
                                    if (dTemporal.PresaAcechada.Muerto)
                                    {
                                        dTemporal.PresaAcechada.AcechadaPor = null;
                                        dTemporal.PresaAcechada = null;
                                    }
                                }
                            }

                            if (dTemporal.PresaAcechada != null && !dTemporal.VerificarRango(dTemporal.PresaAcechada))
                            {
                                dTemporal.PresaAcechada.AcechadaPor = null;
                                dTemporal.PresaAcechada = null;
                            }

                            foreach (Presa presa in presas)
                            {
                                if (dTemporal.VerificarRango(presa) && presa.AcechadaPor == null) //Para verificar que no están siendo cazadas, hay error de referencias no eliminadas
                                {
                                    if (mejorPresa == null)
                                    {
                                        mejorPresa = presa;
                                    }
                                    else
                                    {
                                        if (dTemporal.DistanciaEuclideana(presa) < dTemporal.DistanciaEuclideana(mejorPresa))
                                        {
                                            mejorPresa = presa;
                                        }
                                    }
                                }
                            }

                            if (mejorPresa != null && !mejorPresa.Muerto)
                            {
                                if (dTemporal.PresaAcechada == null)
                                {
                                    mejorPresa.AcechadaPor = dTemporal;
                                    dTemporal.PresaAcechada = mejorPresa;
                                }
                                else
                                {
                                    dTemporal.PresaAcechada.AcechadaPor = null; //Removemos la referencia anterior
                                    dTemporal.PresaAcechada = null;

                                    mejorPresa.AcechadaPor = dTemporal;
                                    dTemporal.PresaAcechada = mejorPresa;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void ObjetivoAleatorio() //Actualizar a que el nuevo objetivo aparezca en un nodo aleatorio en el cual no se encuentre ningún vértice
        {
            verticesActuales.Clear();
            Random rnd = new Random();
            int intentoObjetivo = rnd.Next(0, centros.Count);
            foreach (Presa presa in presas)
            {
                verticesActuales.Add(presa.Actual);
            }
            while (verticesActuales.Contains(intentoObjetivo))
            {
                intentoObjetivo = rnd.Next(0, centros.Count);
            }
            Presa.ObjetivoGlobal = intentoObjetivo;
        }

        private int DestinoAngular(Depredador depredador)
        {
            Dictionary<int, double> prioridad = new Dictionary<int, double>();
            foreach (int id in caminos[depredador.Actual].Keys)
            {
                double thetaPresa = Math.Atan2((depredador.PresaAcechada.Y - caminos[depredador.Actual][id][0].Item2), (depredador.PresaAcechada.X - caminos[depredador.Actual][id][0].Item1));
                double thetaArista = Math.Atan2((caminos[depredador.Actual][id][caminos[depredador.Actual][id].Count - 1].Item2 - caminos[depredador.Actual][id][0].Item2), (caminos[depredador.Actual][id][caminos[depredador.Actual][id].Count - 1].Item1 - caminos[depredador.Actual][id][0].Item1));
                if ((thetaArista > Math.PI) && (thetaPresa == 0))
                {
                    thetaPresa = (2 * Math.PI);
                }
                double tempdiff = Math.Abs(thetaPresa - thetaArista);
                prioridad.Add(id, tempdiff);
            }

            List<double> angulosOrdenados = prioridad.Values.ToList();
            var destinoMejor = from entry in prioridad where entry.Value == angulosOrdenados.Min() select entry.Key;
            int destinoactual = destinoMejor.FirstOrDefault();


            return destinoactual;
        }

        private int DestinoAleatorio(Depredador depredador) //Actualizar a que el nuevo objetivo aparezca en un nodo aleatorio en el cual no se encuentre ningún vértice
        {
            List<int> conexionesTemp = new List<int>(caminos[depredador.Actual].Keys);
            Random rnd = new Random();
            int intentoDestino = rnd.Next(0, centros.Count);
            while (!conexionesTemp.Contains(intentoDestino))
            {
                intentoDestino = rnd.Next(0, centros.Count);
            }
            return intentoDestino;
        }

        private void CaminarDepredador(Depredador depredador, Bitmap bmp, int factorVelocidad = 0)
        {
            if((depredador.Velocidad + factorVelocidad) + depredador.Pos < caminos[depredador.Actual][depredador.Siguiente].Count - 1)
            {
                depredador.X = caminos[depredador.Actual][depredador.Siguiente][depredador.Pos].Item1;
                depredador.Y = caminos[depredador.Actual][depredador.Siguiente][depredador.Pos].Item2;
                selectedImage.BackgroundImage = originalImage;
                selectedImage.BackgroundImageLayout = ImageLayout.Zoom;
                DibujarDepredador(depredador, bmp, depredador.ColorEntidad);
                depredador.Pos += depredador.Velocidad;
            }
            else
            {
                depredador.X = caminos[depredador.Actual][depredador.Siguiente][caminos[depredador.Actual][depredador.Siguiente].Count - 1].Item1;
                depredador.Y = caminos[depredador.Actual][depredador.Siguiente][caminos[depredador.Actual][depredador.Siguiente].Count - 1].Item2;
                DibujarDepredador(depredador, bmp, depredador.ColorEntidad);
                depredador.Pos = 0;
                depredador.Actual = depredador.Siguiente;
                if(depredador.PresaAcechada == null)
                {
                    depredador.Siguiente = DestinoAleatorio(depredador); //Caminar aleatoriamente
                }
                else
                {
                    depredador.Siguiente = DestinoAngular(depredador); //Caminar hacia uana presa
                }
            }
        }

        private void CaminarPresa(Presa presa, Bitmap bmp)
        {
            if (presa.Velocidad + presa.Pos < caminos[presa.Actual][presa.Siguiente].Count - 1)
            {
                presa.X = caminos[presa.Actual][presa.Siguiente][presa.Pos].Item1;
                presa.Y = caminos[presa.Actual][presa.Siguiente][presa.Pos].Item2;
                selectedImage.BackgroundImage = originalImage;
                selectedImage.BackgroundImageLayout = ImageLayout.Zoom; //Para que encuadre
                DibujarPresa(presa, bmp, presa.ColorEntidad);
                presa.Pos += presa.Velocidad;
            }
            else
            {
                presa.X = caminos[presa.Actual][presa.Siguiente][caminos[presa.Actual][presa.Siguiente].Count - 1].Item1;
                presa.Y = caminos[presa.Actual][presa.Siguiente][caminos[presa.Actual][presa.Siguiente].Count - 1].Item2;
                DibujarPresa(presa, bmp, presa.ColorEntidad);
                presa.Pos = 0;
                presa.Actual = presa.Siguiente;

                if(presa.Actual == presa.ObjetivoLocal)
                {
                    if(presa.Actual == Presa.ObjetivoGlobal){
                        presa.GanarVida();
                        ObjetivoAleatorio();
                        presa.ObjetivoLocal = Presa.ObjetivoGlobal;
                        presa.Recalcular();
                        presa.NuevoDestino();
                        return;
                    }
                    else
                    {
                        presa.ObjetivoLocal = Presa.ObjetivoGlobal;
                        presa.Recalcular();
                        presa.NuevoDestino();
                        return;
                    }
                }

                if (presa.ObjetivoLocal != Presa.ObjetivoGlobal && Presa.ObjetivoGlobal != presa.Actual)
                {
                    presa.ObjetivoLocal = Presa.ObjetivoGlobal;
                    presa.Recalcular();
                    presa.NuevoDestino();
                    return;
                }

                presa.NuevoDestino();
            }
        }

        private void btnProbar_Click(object sender, EventArgs e)
        {
        }
    }
}