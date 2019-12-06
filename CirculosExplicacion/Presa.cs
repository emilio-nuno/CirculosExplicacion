using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CirculosExplicacion
{
    class Presa : EntidadBase
    {
        private int resistencia; //Actual contiene el vértice en el que se encuentra el agente luego de caminar

        public Presa(int x, int y, int r, int actual, int inicial, int velocidad, Color color)
        {
            X = x;
            Y = y;
            R = r;
            Actual = actual;
            Inicial = inicial;
            Velocidad = velocidad;
            ColorEntidad = color;
            resistencia = 1;
        }

        private void Morir()
        {
            Console.WriteLine("He muerto...");
        }

        private void PerderVida()
        {
            if(resistencia > 0)
            {
                resistencia -= 1;
            }
            else
            {
                Morir();
            }
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
