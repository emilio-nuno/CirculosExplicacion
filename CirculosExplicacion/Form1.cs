﻿using System;
using System.Collections.Generic;
using System.Drawing;
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
        private Bitmap originalImage;
        private Dictionary<int, Tuple<int, int, int>> centros;
        private Dictionary<int, List<Dictionary<int, VerticeConectado>>> conexiones; //Does not have to be list of dicts
        private bool sobreescribir;
        private Circle primera;
        private Grafo g;
        private Dictionary<int, Dictionary<int, List<Tuple<int, int>>>> caminos;
        private List<Presa> presas;
        private bool finSim;
        private List<int> verticesActuales;

        public Form1()
        {
            this.presas = new List<Presa>();
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

        private void origenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            presas.Add(new Presa(Int32.Parse(nodosConectados.SelectedNode.Text), Int32.Parse(nodosConectados.SelectedNode.Text), Presa.ObjetivoGlobal,10, Color.Red)); //Agregar color manual
        }

        private void destinoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Presa.ObjetivoGlobal = Int32.Parse(nodosConectados.SelectedNode.Text);
        }

        private void DibujarPresa(Presa presa, Bitmap bmp, int radio, Color color)
        {
            using (var graphics = Graphics.FromImage(bmp))
            {
                graphics.Clear(Color.Transparent);
                graphics.FillEllipse(new SolidBrush(color), presa.X - (radio / 2), presa.Y - (radio / 2), radio, radio);
                graphics.DrawString(presa.Resistencia.ToString(), new Font("Arial", 16), new SolidBrush(Color.Black), presa.X + 10, presa.Y + 10);
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
                        Caminar(pTemporal, bmp);
                        selectedImage.Image = bmp;
                        selectedImage.Refresh();
                        Thread.Sleep(1);
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

        private void Caminar(Presa presa, Bitmap bmp)
        {
            if (presa.Actual == presa.ObjetivoLocal)
            {
                if(presa.ObjetivoLocal == Presa.ObjetivoGlobal)
                {
                    presa.GanarVida();
                    ObjetivoAleatorio();
                }
                presa.ObjetivoLocal = Presa.ObjetivoGlobal;
                presa.Recalcular();
                presa.NuevoDestino();
                return;
            }
            if (presa.Velocidad + presa.Pos < caminos[presa.Actual][presa.Siguiente].Count - 1)
            {
                presa.X = caminos[presa.Actual][presa.Siguiente][presa.Pos].Item1;
                presa.Y = caminos[presa.Actual][presa.Siguiente][presa.Pos].Item2;
                selectedImage.BackgroundImage = originalImage;
                selectedImage.BackgroundImageLayout = ImageLayout.Zoom; //Para que encuadre
                DibujarPresa(presa, bmp, 40, presa.ColorEntidad);
                presa.Pos += presa.Velocidad;
            }
            else
            {
                presa.X = caminos[presa.Actual][presa.Siguiente][caminos[presa.Actual][presa.Siguiente].Count - 1].Item1;
                presa.Y = caminos[presa.Actual][presa.Siguiente][caminos[presa.Actual][presa.Siguiente].Count - 1].Item2;
                DibujarPresa(presa, bmp, 40, presa.ColorEntidad);
                if(presa.ObjetivoLocal != Presa.ObjetivoGlobal)
                {
                    presa.Pos = 0;
                    presa.Actual = presa.Siguiente;
                    if(presa.Actual == Presa.ObjetivoGlobal)
                    {
                        presa.GanarVida();
                        ObjetivoAleatorio();
                        presa.ObjetivoLocal = Presa.ObjetivoGlobal;
                        presa.Recalcular();
                        presa.NuevoDestino();
                    }
                    if(presa.ObjetivoLocal != Presa.ObjetivoGlobal)
                    {

                    }
                  
                }
                presa.Pos = 0;
                presa.Actual = presa.Siguiente;
                presa.NuevoDestino();
            }
        }

        private void btnProbar_Click(object sender, EventArgs e)
        {
        }
    }
}