using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PATH_F
{
    public class Dijkstra
    {
        public Dijkstra()
        {
        }

        async public void Work(List<Cell> lista_cell, Form1 Form1, int end, int start,int rozmiar)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var activeD = new List<Cell>();
            var visitedD = new List<Cell>();


            activeD.Add(lista_cell[start]);
            activeD[0].Koszt = 0;     
            //petla do szukania sciezki,jesli dobiegnie konca oznacza to ze sciezka nie istnieje
            while (activeD.Any())
            {
                //za kazdym razem wykonania petli biorę tą z aktywnych,która ma najniższy koszt przebycia dystansu
                var main = activeD.OrderBy(cell => cell.Koszt).First();

                if (main.end == false && main.start == false)
                {
                    string panel_name = "panel" + main.index;
                    Panel panel = Form1.Controls.Find(panel_name, true).First() as Panel;
                    panel.BackColor = Color.Gray;
                    lista_cell[main.index].main = true;

                }

                // sprawdzam czy aktualne miejsce nie jestem koncem
                if (main.x == lista_cell[end].x && main.y == lista_cell[end].y)
                {
                    Form1.label9.Text = "PATH";
                    var cell = main;

                    //w celu uzyskania sciezki cofam się po rodzicach komórki,która dotarała do celu,az dotre do startu ktory nie ma rodzica
                    while (cell != null)
                    {


                        if (cell.start == false && cell.end == false)
                        {
                            string sciezka = "panel" + cell.index;
                            Panel panel = Form1.Controls.Find(sciezka, true).First() as Panel;

                            panel.BackColor = Color.YellowGreen;
                        }

                        cell = cell.Rodzic;
                        if (Form1.checkBox1.Checked)
                            await Task.Delay(150);
                    }
                    watch.Stop();
                    var elapsedMs = watch.ElapsedMilliseconds;
                    Form1.label10.Text = elapsedMs.ToString();
                    return;

                }
                //droga
                var cell0 = main;
                if (lista_cell[main.index].main == true)
                    while (cell0 != null)
                    {


                        if (cell0.start == false && cell0.end == false)
                        {
                            string sciezka = "panel" + cell0.index;
                            Panel panel = Form1.Controls.Find(sciezka, true).First() as Panel;

                            panel.BackColor = Color.YellowGreen;
                        }

                        cell0 = cell0.Rodzic;
                    }
                if (Form1.checkBox1.Checked)
                    await Task.Delay(600);
                cell0 = main;
                if (lista_cell[main.index].main == true)
                    while (cell0 != null)
                    {


                        if (cell0.start == false && cell0.end == false)
                        {
                            string sciezka = "panel" + cell0.index;
                            Panel panel = Form1.Controls.Find(sciezka, true).First() as Panel;

                            panel.BackColor = Color.Gray;
                        }

                        cell0 = cell0.Rodzic;

                    }


                //aktualna nie jest koncem,wiec dodaje ja do odwiedzonych i usuwam z aktywnych
                visitedD.Add(main);
                activeD.Remove(main);

                //poruszam sie w8 kierunkach i dla uzyskanych pozycji sprawdzam dalej
                var available = Dostepne_cellD(rozmiar, main, lista_cell[end], lista_cell);

                //zaznaczam jakie dostepne komorki algorytm bral pod uwage
                foreach (var n in available)
                {

                    if (n.end == false && n.main == false)
                    {

                        string panel_name = "panel" + n.index;
                        Panel panel = Form1.Controls.Find(panel_name, true).First() as Panel;
                        panel.BackColor = Color.Silver;

                    }


                    //Sprawdzam po współrzędnych czy dostępna nie była już odwiedzona,
                    //jeśli tak,przechodzę do nastęj iteracji foreach i spradzam nastepna
                    if (visitedD.Any(cell => cell.x == n.x && cell.y == n.y))
                        continue;
                    //Sprawdzam czy nie ma takiej aktywnej jak dostepnej,i porownuje ich koszt dystansu

                    if (activeD.Any(cell => cell.x == n.x && cell.y == n.y))
                    {

                        var dostepna_jak_aktywna = activeD.First(cell => cell.x == n.x && cell.y == n.y);
                        if (dostepna_jak_aktywna.Koszt > n.Koszt)
                        {
                            activeD.Remove(dostepna_jak_aktywna);
                            activeD.Add(n);
                        }


                    }
                    else
                    {
                        activeD.Add(n);

                    }
                }
                if (Form1.checkBox1.Checked)
                    await Task.Delay(150);
            }
            Form1.label9.Text = "NO PATH";
        }

        private static List<Cell> Dostepne_cellD(int rozmiar, Cell aktualna, Cell szukana, List<Cell> lista_cell)
        {
            //tworze potencjalne obiekty
            var potencjalnaCellD = new List<Cell>()
            {
                new Cell {x=aktualna.x+1,y=aktualna.y,Rodzic=aktualna,Koszt=aktualna.Koszt+10},//RIGHT
                new Cell {x=aktualna.x-1,y=aktualna.y,Rodzic=aktualna,Koszt=aktualna.Koszt+10},//LEFT
                new Cell {x=aktualna.x,y=aktualna.y+1,Rodzic=aktualna,Koszt=aktualna.Koszt+10},//DOWN
                new Cell {x=aktualna.x,y=aktualna.y-1,Rodzic=aktualna,Koszt=aktualna.Koszt+10},//UP

                new Cell {x=aktualna.x-1,y=aktualna.y-1,Rodzic=aktualna,Koszt=aktualna.Koszt+14},//UL
                new Cell {x=aktualna.x-1,y=aktualna.y+1,Rodzic=aktualna,Koszt=aktualna.Koszt+14},//DL
                new Cell {x=aktualna.x+1,y=aktualna.y-1,Rodzic=aktualna,Koszt=aktualna.Koszt+14},//UR
                new Cell {x=aktualna.x+1,y=aktualna.y+1,Rodzic=aktualna,Koszt=aktualna.Koszt+14},//DR
            };
            //sprawdzam czy moge po nich chodzic
            potencjalnaCellD.ForEach(cell =>
            {
                if (cell.y >= 0 && cell.y <= rozmiar - 1 && cell.x >= 0 && cell.x <= rozmiar - 1)
                {
                    cell.start = lista_cell[cell.x + cell.y * rozmiar].start;
                    cell.end = lista_cell[cell.x + cell.y * rozmiar].end;
                    cell.przeszkoda = lista_cell[cell.x + cell.y * rozmiar].przeszkoda;
                    cell.main = lista_cell[cell.x + cell.y * rozmiar].main; 
                    cell.index = cell.x + cell.y * rozmiar;



                    if (cell.start == false && cell.przeszkoda == false)
                        cell.walkable = true;
                    else
                        cell.walkable = false;

                    if (cell.x == aktualna.x - 1 && cell.y == aktualna.y - 1)//UL
                    {
                        if (potencjalnaCellD[1].przeszkoda == true && potencjalnaCellD[3].przeszkoda == true)
                        {
                            cell.walkable = false;
                        }
                    }

                    if (cell.x == aktualna.x - 1 && cell.y == aktualna.y + 1)//DL
                    {
                        if (potencjalnaCellD[1].przeszkoda == true && potencjalnaCellD[2].przeszkoda == true)
                        {
                            cell.walkable = false;
                        }
                    }

                    if (cell.x == aktualna.x + 1 && cell.y == aktualna.y - 1)//UR
                    {
                        if (potencjalnaCellD[0].przeszkoda == true && potencjalnaCellD[3].przeszkoda == true)
                        {
                            cell.walkable = false;
                        }
                    }

                    if (cell.x == aktualna.x + 1 && cell.y == aktualna.y + 1)//DR
                    {
                        if (potencjalnaCellD[0].przeszkoda == true && potencjalnaCellD[2].przeszkoda == true)
                        {
                            cell.walkable = false;
                        }
                    }

                }
                else
                {
                    cell.walkable = false;
                }
            });

            //zwracan takie potencjalne po ktorych mozna chodzic
            return potencjalnaCellD.Where(cell => cell.walkable == true).ToList();
        }

    }
}
