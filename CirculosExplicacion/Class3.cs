using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CirculosExplicacion
{
    class Presa
    {
        private int x, y, r, actual, velocidad, pos, inicial, resistencia; //Actual contiene el vértice en el que se encuentra el agente luego de caminar
        private Color color;

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public int R { get => r; set => r = value; }
        public int Actual { get => actual; set => actual = value; }
        public int Velocidad { get => velocidad; set => velocidad = value; }
        public int Pos { get => pos; set => pos = value; }
        public int Inicial { get => inicial; set => inicial = value; }
        public Color Color { get => color; set => color = value; }
        public int Resistencia { get => resistencia; set => resistencia = value; }

        public Presa(int x, int y, int r, int actual, int inicial, int velocidad, Color color)
        {
            this.x = x;
            this.y = y;
            this.r = r;
            this.actual = actual;
            this.velocidad = velocidad;
            this.inicial = inicial;
            this.color = color;
        }

        private void PerderVida()
        {
            resistencia -= 1;
        }

        private void GanarVida()
        {
            resistencia += 1;
        }

        private void Maniobrar()
        {
            Console.WriteLine("He maniobrado");
        }
    }
}
