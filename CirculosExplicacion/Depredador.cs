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

        private double DistanciaEuclideana(Presa objetivo)
        {
            return Math.Sqrt(Math.Pow(objetivo.X - X, 2) + Math.Pow(objetivo.Y - Y, 2));
        }

        public int FactorVelocidad(Presa objetivo)
        {
            return radioDepredador - Convert.ToInt32(DistanciaEuclideana(objetivo));
        }

        public bool VerificarRango(Presa objetivo)
        {
            if(DistanciaEuclideana(objetivo) <= radioDepredador)
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
