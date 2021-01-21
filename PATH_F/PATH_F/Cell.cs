using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PATH_F
{
    public class Cell
    {
        public int index { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public bool start{ get; set; }
        public bool end { get; set; }
        public bool przeszkoda { get; set; }
        public bool klikniety { get; set; }
        public bool walkable { get; set; }
        public bool main { get; set; }
        public int Koszt { get; set; }
        public double Dystans { get; set; }
        public double KosztDystansu { get; set; }
        public Cell Rodzic { get; set; }
        public int isStart { get; set; }
        public int isEnd { get; set; }

        public void SetDistance(int targetX, int targetY)
        {
            int xd = Math.Abs(targetX - x);
            int xy = Math.Abs(targetY - y);
            this.Dystans = 10 * (xd + xy) + (14 - 2 * 10) * Math.Min(xd, xy);
        }
    }
}
