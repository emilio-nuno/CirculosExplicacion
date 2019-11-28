﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CirculosExplicacion
{
    public partial class Form2 : Form
    {
        public Form2(Bitmap prim, Bitmap kruskal, List<string> pasosPrim, List<string> pasosKruskal)
        {
            InitializeComponent();
            pcPrim.Image = prim;
            pcKruskal.Image = kruskal;
            lstPrim.DataSource = pasosPrim;
            lstKruskal.DataSource = pasosKruskal;
        }
    }
}