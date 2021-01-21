using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PATH_F
{
    class Map
    {
        public static void SetParametsForLoadedList(List<Cell> lista_cell, int start,int end,String[] obstaclesNew,Form1 form)
        {
            lista_cell[start].start = true;
            lista_cell[start].klikniety = true;
            lista_cell[end].end = true;
            lista_cell[end].klikniety = true;
            lista_cell[0].isStart = start;
            lista_cell[0].isEnd = end;
            
            foreach (string obstacleNew in obstaclesNew)
            {
                if (obstacleNew != "")
                {
                    int nr = Int32.Parse(obstacleNew);
                    lista_cell[nr].przeszkoda = true;
                    lista_cell[nr].klikniety = true;
                }
            }
        }

        public static void DisplayLoadedMap(List<string> maps,Form1 form)
        {
            if (maps != null)
            {
                foreach (string map in maps)
                {
                    string[] data = map.Split(';');
                    form.comboBox1.Items.Add(data[0]);
                }
            }
        }

        public static void PaintMap(Form1 form,int number)
        {
            int maxNo = number;
                Random r = new Random();
                int h = form.mainPanel.Height;
                int w = form.mainPanel.Width;
                int rozmiar = h / maxNo;
                int x, y;
                for (int i = 0; i < maxNo; i++)
                {
                    for (int j = 0; j < maxNo; j++)
                    {
                        Panel nowy = new Panel();
                        x = j * rozmiar;
                        y = i * rozmiar;
                        Point point = new Point(x, y);
                        nowy.Location = point;
                        nowy.TabIndex = j + maxNo * i;
                        nowy.BackColor = Color.Honeydew;
                        nowy.Name = "panel" + nowy.TabIndex;
                        nowy.Size = new System.Drawing.Size(rozmiar, rozmiar);
                        form.mainPanel.Controls.Add(nowy);
                        nowy.Click += new System.EventHandler(form.panelAdd);
                    }
                }
        }

        public static List<Cell> lista(int rozmiar)
        {
            List<Cell> lista_cell = new List<Cell>();
            int p = 0;
            for (int i = 0; i < rozmiar; i++)
            {
                for (int y = 0; y < rozmiar; y++)
                {
                    lista_cell.Add(new Cell());
                    lista_cell[p].index = p;
                    lista_cell[p].x = y;
                    lista_cell[p].y = i;
                    lista_cell[p].start = false;
                    lista_cell[p].end = false;
                    lista_cell[p].przeszkoda = false;
                    lista_cell[p].klikniety = false;
                    lista_cell[p].main = false; 
                    p++;
                }
            }
            lista_cell[0].isStart = -1;
            lista_cell[0].isEnd = -1;

            return lista_cell;
        }
        public static void Paint(Form1 Form1, Panel clickedPanel, List<Cell> lista_cell)
        {
            int indexPanel = clickedPanel.TabIndex;
            if (lista_cell[indexPanel].klikniety == true)
                klikniety();
            else
                nieklikniety();

            void klikniety()
            {
                if (clickedPanel.BackColor == Color.Green)
                {
                    Form1.label1.Text = "-";
                    Form1.label2.Text = "-";
                    clickedPanel.BackColor = Color.Honeydew;
                    lista_cell[indexPanel].start = false;
                    lista_cell[0].isStart = -1;
                    lista_cell[indexPanel].klikniety = false;

                }
                if (clickedPanel.BackColor == Color.Red)
                {
                    Form1.label4.Text = "-";
                    Form1.label5.Text = "-";
                    lista_cell[0].isEnd = -1;
                    clickedPanel.BackColor = Color.Honeydew;
                    lista_cell[indexPanel].end = false;
                    lista_cell[indexPanel].klikniety = false;


                }
                if (clickedPanel.BackColor == Color.Black)
                {
                    lista_cell[indexPanel].przeszkoda = false;
                    clickedPanel.BackColor = Color.Honeydew;
                    lista_cell[indexPanel].klikniety = false;

                }
            }
            void nieklikniety()
            {
                if (clickedPanel.BackColor == Color.Honeydew)
                {
                    if ((lista_cell[0].isEnd != -1) && (lista_cell[0].isStart != -1))
                    {
                        //black
                        lista_cell[indexPanel].przeszkoda = true;
                        clickedPanel.BackColor = Color.Black;
                        lista_cell[indexPanel].klikniety = true;

                    }
                    if ((lista_cell[0].isEnd == -1) && (lista_cell[0].isStart != -1))
                    {
                        Form1.label4.Text = (lista_cell[indexPanel].x).ToString();
                        Form1.label5.Text = (lista_cell[indexPanel].y).ToString();
                        lista_cell[0].isEnd = indexPanel;
                        clickedPanel.BackColor = Color.Red;
                        lista_cell[indexPanel].end = true;
                        lista_cell[indexPanel].klikniety = true;
                    }
                    if (lista_cell[0].isStart == -1)
                    {
                        //green

                        Form1.label1.Text = (lista_cell[indexPanel].x).ToString();
                        Form1.label2.Text = (lista_cell[indexPanel].y).ToString();
                        lista_cell[0].isStart = indexPanel;
                        clickedPanel.BackColor = Color.Green;
                        lista_cell[indexPanel].start = true;
                        lista_cell[indexPanel].klikniety = true;
                    }
                }
            }
        }

        public static void Repaint(List<Cell> lista, Form1 form)
        {
            form.label9.Text = "-";
            foreach (var cell in lista)
            {
                cell.main = false;
                //cell.walkable = false;
                if (cell.przeszkoda == true)
                {
                    string panel_name = "panel" + cell.index;
                    Panel panel = form.Controls.Find(panel_name, true).First() as Panel;
                    panel.BackColor = Color.Black;
                }
                if (cell.start == true)
                {
                    //lista[0].isStart = cell.index;
                    string panel_name = "panel" + cell.index;
                    Panel panel = form.Controls.Find(panel_name, true).First() as Panel;
                    panel.BackColor = Color.Green;
                }
                if (cell.end == true)
                {
                    string panel_name = "panel" + cell.index;
                    Panel panel = form.Controls.Find(panel_name, true).First() as Panel;
                    panel.BackColor = Color.Red;
                }
                if (cell.przeszkoda == false && cell.start == false && cell.end == false)
                {
                    string panel_name = "panel" + cell.index;
                    Panel panel = form.Controls.Find(panel_name, true).First() as Panel;
                    panel.BackColor = Color.Honeydew;
                }
            }
        }
    }
}
