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
        private Dictionary<int, List<int>> caminosMinimos; //Mover a local para poder reiniciar correctamente
        private Bitmap originalImage;
        private Bitmap imagenEditar;
        private Dictionary<int, Tuple<int, int, int>> centros;
        private Dictionary<int, List<Dictionary<int, VerticeConectado>>> conexiones; //Does not have to be list of dicts
        private bool sobreescribir;
        private Circle primera;
        private Grafo g;
        private Dictionary<int, Dictionary<int, List<Tuple<int, int>>>> caminos;
        private List<Presa> agentes;
        private bool finSim;

        public Form1()
        {
            this.agentes = new List<Presa>();
            InitializeComponent();
            this.sobreescribir = false;
            finSim = false;
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
        }

        private void nodosConectados_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            ((TreeView)sender).SelectedNode = e.Node;
        }

        private void origenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //agentes.Add(new Presa(centros[Int32.Parse(nodosConectados.SelectedNode.Text)].Item1, centros[Int32.Parse(nodosConectados.SelectedNode.Text)].Item2, 50, Int32.Parse(nodosConectados.SelectedNode.Text), Int32.Parse(nodosConectados.SelectedNode.Text), Color.Transparent)); //Agregar color manual
        }

        private void destinoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //señuelo = new Objetivo(centros[Int32.Parse(nodosConectados.SelectedNode.Text)].Item1, centros[Int32.Parse(nodosConectados.SelectedNode.Text)].Item2, Int32.Parse(nodosConectados.SelectedNode.Text));
        }

        private void DibujarCirculo(int x, int y, Bitmap bmp, int radio, Color color)
        {
            using (var graphics = Graphics.FromImage(bmp))
            {
                graphics.Clear(Color.Transparent);
                graphics.FillEllipse(new SolidBrush(color), x - (radio / 2), y - (radio / 2), radio, radio);
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
            Presa p = new Presa(15, 0, 10, ConseguirMatriz(), Color.Red);
            using(Bitmap bmp = new Bitmap(originalImage))
            {
                    foreach (int paso in p.CaminosMinimos[0])
                    {
                        if (paso != p.Actual)
                        {
                            while(Caminar(p, paso, bmp))
                            {
                                selectedImage.Image = bmp;
                                selectedImage.Refresh();
                                Thread.Sleep(1);
                            }
                            p.Actual = paso;
                        }
                    }
            }
        }

        private bool Caminar(Presa presa, int destino, Bitmap bmp)
        {
            if (presa.Velocidad + presa.Pos < caminos[presa.Actual][destino].Count - 1)
            {
                selectedImage.BackgroundImage = originalImage;
                selectedImage.BackgroundImageLayout = ImageLayout.Zoom; //Para que encuadre
                DibujarCirculo(caminos[presa.Actual][destino][presa.Pos].Item1, caminos[presa.Actual][destino][presa.Pos].Item2, bmp, 40, presa.ColorEntidad);
                presa.Pos += presa.Velocidad;
                return true;
            }
            else
            {
                DibujarCirculo(caminos[presa.Actual][destino][caminos[presa.Actual][destino].Count - 1].Item1, caminos[presa.Actual][destino][caminos[presa.Actual][destino].Count - 1].Item2, bmp, 40, presa.ColorEntidad);
                presa.Pos = 0;
                return false;
            }
        }

        private void btnProbar_Click(object sender, EventArgs e)
        {
        }
    }
}