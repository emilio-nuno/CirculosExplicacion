using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CirculosExplicacion
{
    class Agente
    {
        private int x, y, r, actual, velocidad, pos, inicial; //Actual contiene el vértice en el que se encuentra el agente luego de caminar
        private Color color;

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public int R { get => r; set => r = value; }
        public int Actual { get => actual; set => actual = value; }
        public int Velocidad { get => velocidad; set => velocidad = value; }
        public int Pos { get => pos; set => pos = value; }
        public int Inicial { get => inicial; set => inicial = value; }
        public Color Color { get => color; set => color = value; }

        public Agente(int x, int y, int r, int actual, int inicial, Color color)
        {
            this.x = x;
            this.y = y;
            this.r = r;
            this.actual = actual;
            this.velocidad = 1;
            this.Inicial = inicial;
            this.Color = color;
        }
    }
}
