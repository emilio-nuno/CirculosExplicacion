using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CirculosExplicacion
{
    class Circle
    {
        private Bitmap sample;
        public Color pintar = Color.Yellow;
        private Color borrar = Color.Red;
        private int x, y, radiox, radioy, diametrox, diametroy, id;
        private bool circulo;
        public Dictionary<int, Tuple<int, int, int>> centros;

        public Circle(Bitmap sample)
        {
            centros = new Dictionary<int, Tuple<int, int, int>>();
            id = 0;
            this.circulo = true;
            this.sample = sample;
        }

        public int X { get => x; private set => x = value; }
        public int Y { get => y; private set => y = value; }
        public int Radiox { get => radiox; private set => radiox = value; }
        public int Radioy { get => radioy; private set => radioy = value; }

        public void Empezar()
        {
            DetectarCirculos();
        }

        public void DetectarCirculos()
        {
            for (int y = 0; y < sample.Height; y++)
            {
                for (int x = 0; x < sample.Width; x++)
                {
                    if (sample.GetPixel(x, y).ToArgb() == Color.Black.ToArgb())
                    {
                        this.circulo = true;
                        CalcularCentro(x, y);
                    }
                }
            }
        }

        public void CalcularCentro(int x, int y)
        {
            int p_der, p_izq, p_inf, p_sup, p_c_x, p_c_y;

            p_der = p_izq = x;
            p_inf = p_sup = y;

            while (sample.GetPixel(p_der, y).ToArgb() == Color.Black.ToArgb())
            {
                sample.GetPixel(p_der++, y);
            }
            p_der -= 1;
            p_c_x = (p_der + p_izq) / 2;

            while (sample.GetPixel(x, p_inf).ToArgb() == Color.Black.ToArgb())
            {
                sample.GetPixel(x, p_inf++);
            }
            p_inf -= 1;
            p_c_y = (p_inf + p_sup) / 2;

            this.x = p_c_x;
            this.y = p_c_y;
            ConseguirRadio();
            if (this.circulo)
            {
                Dibujar(pintar);
                centros.Add(id++, Tuple.Create(p_c_x, p_c_y, radiox)); //Mover a condicion if
            }
            else
            {
                Dibujar(borrar);
                Borrar();
            }
        }

        public void Borrar()
        {
            for (int y = 0; y < sample.Height; y++)
            {
                for (int x = 0; x < sample.Width; x++)
                {
                    if (sample.GetPixel(x, y).ToArgb() == borrar.ToArgb())
                    {
                        sample.SetPixel(x, y, Color.White);
                    }
                }
            }
        }

        public void ConseguirRadio()
        {
            int original = x;
            while (sample.GetPixel(original, y).ToArgb() != Color.White.ToArgb())
            {
                sample.GetPixel(original++, y);
            }
            original--;
            this.radiox = original - x;
            this.diametrox = this.radiox * 2;
            original = y;
            while (sample.GetPixel(x, original).ToArgb() != Color.White.ToArgb())
            {
                sample.GetPixel(x, original++);
            }
            original--;
            this.radioy = original - y;
            this.diametroy = this.radioy * 2;
            if (Math.Abs(this.diametroy - this.diametrox) > 10)
            {
                this.circulo = false;
            }
        }

        public void DemostrarRadio()
        {
            for (int i = y; i <= y + radioy; i++)
            {
                sample.SetPixel(x, i, Color.Green);
            }
            for (int i = x; i <= x + radiox; i++)
            {
                sample.SetPixel(i, y, Color.Green);
            }
            for (int i = y; i >= y - radioy; i--)
            {
                sample.SetPixel(x, i, Color.Green);
            }
            for (int i = x; i >= x - radiox; i--)
            {
                sample.SetPixel(i, y, Color.Green);
            }
        }
        public void Dibujar(Color c)
        {
            int tempx = x;
            while (sample.GetPixel(tempx, y).ToArgb() != Color.White.ToArgb()) //Dibuja cuarto cuadrante
            {
                sample.SetPixel(tempx, y, c);
                int tempy = y;
                while (sample.GetPixel(tempx, tempy).ToArgb() != Color.White.ToArgb())
                {
                    sample.SetPixel(tempx, tempy, c);
                    sample.GetPixel(tempx, tempy++);
                }
                sample.GetPixel(tempx++, y);
            }
            tempx = x;
            while (sample.GetPixel(tempx, y).ToArgb() != Color.White.ToArgb()) //Dibuja cuarto cuadrante
            {
                sample.SetPixel(tempx, y, c);
                int tempy = y;
                while (sample.GetPixel(tempx, tempy).ToArgb() != Color.White.ToArgb())
                {
                    sample.SetPixel(tempx, tempy, c);
                    sample.GetPixel(tempx, tempy--);
                }
                sample.GetPixel(tempx++, y);
            }
            tempx = x;
            while (sample.GetPixel(tempx, y).ToArgb() != Color.White.ToArgb()) //Dibuja cuarto cuadrante
            {
                sample.SetPixel(tempx, y, c);
                int tempy = y;
                while (sample.GetPixel(tempx, tempy).ToArgb() != Color.White.ToArgb())
                {
                    sample.SetPixel(tempx, tempy, c);
                    sample.GetPixel(tempx, tempy++);
                }
                sample.GetPixel(tempx--, y);
            }
            tempx = x;
            while (sample.GetPixel(tempx, y).ToArgb() != Color.White.ToArgb()) //Dibuja cuarto cuadrante
            {
                sample.SetPixel(tempx, y, c);
                int tempy = y;
                while (sample.GetPixel(tempx, tempy).ToArgb() != Color.White.ToArgb())
                {
                    sample.SetPixel(tempx, tempy, c);
                    sample.GetPixel(tempx, tempy--);
                }
                sample.GetPixel(tempx--, y);
            }
        }
    }
}
