using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace CirculosExplicacion
{
    abstract class EntidadBase
    {
        private int inicial;
        private Color colorEntidad;
        private int x;
        private int y;
        private int r;
        private int actual;
        private int velocidad;
        private int pos;

        protected int X { get => x; set => x = value; }
        protected int Y { get => y; set => y = value; }
        protected int R { get => r; set => r = value; }
        protected int Actual { get => actual; set => actual = value; }
        protected int Velocidad { get => velocidad; set => velocidad = value; }
        protected int Pos { get => pos; set => pos = value; }
        protected int Inicial { get => inicial; set => inicial = value; }
        protected Color ColorEntidad { get => colorEntidad; set => colorEntidad = value; }
    }
}
