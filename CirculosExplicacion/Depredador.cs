using System;
using System.Drawing;

namespace CirculosExplicacion
{
    class Depredador : EntidadBase
    {
        private int radioDepredador;
        private Presa presaAcechada;
        private int siguiente;
        private Color colorRadio;

        public Depredador(int inicial, int actual, int radio, int velocidad, Color color, int tamaño = 40)
        {
            Tamaño = tamaño;
            ColorRadio = Color.Black;
            RadioDepredador = radio;
            Inicial = inicial;
            Actual = actual;
            Velocidad = velocidad;
            ColorEntidad = color;
            presaAcechada = null;
        }

        public bool Colision(Presa presa)
        {
            return DistanciaEuclideana(presa) < Tamaño;
        }

        public double DistanciaEuclideana(Presa presa)
        {
            return Math.Sqrt(Math.Pow(presa.X - X, 2) + Math.Pow(presa.Y - Y, 2));
        }

        public int FactorVelocidad(Presa presa)
        {
            return RadioDepredador - Convert.ToInt32(DistanciaEuclideana(presa));
        }

        public bool VerificarRango(Presa presa)
        {
            if(DistanciaEuclideana(presa) < RadioDepredador)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Presa PresaAcechada { get => presaAcechada; set => presaAcechada = value; }
        public int RadioDepredador { get => radioDepredador; set => radioDepredador = value; }
        public int Siguiente { get => siguiente; set => siguiente = value; }
        public Color ColorRadio { get => colorRadio; set => colorRadio = value; }
    }
}
