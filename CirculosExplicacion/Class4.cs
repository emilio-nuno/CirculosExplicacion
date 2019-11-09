using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CirculosExplicacion
{
    class Señuelo
    {
        private int x, y, actual;

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public int Actual { get => actual; set => actual = value; }

        public Señuelo(int x, int y, int actual)
        {
            this.x = x;
            this.y = y;
            this.Actual = actual;
        }
    }
}
