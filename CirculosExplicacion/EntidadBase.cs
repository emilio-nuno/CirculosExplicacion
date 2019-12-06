using System.Drawing;

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

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public int R { get => r; set => r = value; }
        public int Actual { get => actual; set => actual = value; }
        public int Velocidad { get => velocidad; set => velocidad = value; }
        public int Pos { get => pos; set => pos = value; }
        public int Inicial { get => inicial; set => inicial = value; }
        public Color ColorEntidad { get => colorEntidad; set => colorEntidad = value; }
    }
}
