using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PATH_F
{
    public class Astar
    {
        public Astar()
        {
           
        }
       async public void Work(List<Cell> lista_cell, Form1 Form1,int end,int start,int rozmiar)
        {
            
            var watch = System.Diagnostics.Stopwatch.StartNew();
         

            var aktywne_cell = new List<Cell>();
            var odwiedzone_cell = new List<Cell>();

            //set distance
            lista_cell[start].SetDistance(lista_cell[end].x, lista_cell[end].y);

            aktywne_cell.Add(lista_cell[start]);
            aktywne_cell[0].Koszt = 0;
            //aktywne_cell[0].index = aktywne_cell[0].index;
            aktywne_cell[0].KosztDystansu = aktywne_cell[0].Dystans + aktywne_cell[0].Koszt;
            //petla do szukania sciezki,jesli dobiegnie konca oznacza to ze sciezka nie istnieje
            while (aktywne_cell.Any())
            {
                //za kazdym razem wykonania petli biorę tą z aktywnych,która ma najniższy koszt przebycia dystansu
                var main = aktywne_cell.OrderBy(cell => cell.KosztDystansu).First();             

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


                        if (cell.start == false&& cell.end == false)
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
                    Form1.label8.Text = elapsedMs.ToString();
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
                odwiedzone_cell.Add(main);
                aktywne_cell.Remove(main);

                //poruszam sie w8 kierunkach i dla uzyskanych pozycji sprawdzam dalej
                var dostepne = Dostepne_cell(rozmiar, main,lista_cell[end],lista_cell);

                //zaznaczam jakie dostepne komorki algorytm bral pod uwage
                foreach (var dostepna in dostepne)
                {

                    if (dostepna.end == false && dostepna.main == false)
                    {
                      
                        string panel_name = "panel" + dostepna.index;
                        Panel panel = Form1.Controls.Find(panel_name, true).First() as Panel;
                        panel.BackColor = Color.Silver;

                    }


                    //Sprawdzam po współrzędnych czy dostępna nie była już odwiedzona,
                    //jeśli tak,przechodzę do nastęj iteracji foreach i spradzam nastepna
                    if (odwiedzone_cell.Any(cell => cell.x == dostepna.x && cell.y == dostepna.y))
                        continue;
                    //Sprawdzam czy nie ma takiej aktywnej jak dostepnej,i porownuje ich koszt dystansu

                    if (aktywne_cell.Any(cell => cell.x == dostepna.x && cell.y == dostepna.y))
                    {

                        var dostepna_jak_aktywna = aktywne_cell.First(cell => cell.x == dostepna.x && cell.y == dostepna.y);
                        if (dostepna_jak_aktywna.KosztDystansu > dostepna.KosztDystansu)
                        {
                            aktywne_cell.Remove(dostepna_jak_aktywna);
                            aktywne_cell.Add(dostepna);
                        }


                    }
                    else
                    {
                        aktywne_cell.Add(dostepna);

                    }
                }
                if (Form1.checkBox1.Checked)
                    await Task.Delay(150);
            }
            Form1.label9.Text = "NO PATH";
        }

        private static List<Cell> Dostepne_cell(int rozmiar,Cell aktualna, Cell szukana,List<Cell> lista_cell)
        {
            //tworze potencjalne obiekty
            var potencjalnaCell = new List<Cell>()
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
            potencjalnaCell.ForEach(cell =>
            {
                if (cell.y >= 0 && cell.y <= rozmiar-1 && cell.x >= 0 && cell.x <= rozmiar-1)
                {
                    cell.start = lista_cell[cell.x + cell.y * rozmiar].start;
                    cell.end = lista_cell[cell.x + cell.y * rozmiar].end;
                    cell.przeszkoda = lista_cell[cell.x + cell.y * rozmiar].przeszkoda;
                    cell.main = lista_cell[cell.x + cell.y * rozmiar].main;
                    cell.SetDistance(szukana.x, szukana.y);
                    cell.KosztDystansu = cell.Dystans + cell.Koszt;
                    cell.index = cell.x + cell.y * rozmiar;



                    if (cell.start == false && cell.przeszkoda == false)
                        cell.walkable = true;
                    else
                        cell.walkable = false;

                    if(cell.x==aktualna.x-1&&cell.y==aktualna.y-1)//UL
                    {
                        if(potencjalnaCell[1].przeszkoda==true&&potencjalnaCell[3].przeszkoda==true)
                        {
                            cell.walkable = false;
                        }
                    }

                    if (cell.x == aktualna.x - 1 && cell.y == aktualna.y +1)//DL
                    {
                        if (potencjalnaCell[1].przeszkoda == true && potencjalnaCell[2].przeszkoda == true)
                        {
                            cell.walkable = false;
                        }
                    }

                    if (cell.x == aktualna.x + 1 && cell.y == aktualna.y - 1)//UR
                    {
                        if (potencjalnaCell[0].przeszkoda == true && potencjalnaCell[3].przeszkoda == true)
                        {
                            cell.walkable = false;
                        }
                    }

                    if (cell.x == aktualna.x + 1 && cell.y == aktualna.y + 1)//DR
                    {
                        if (potencjalnaCell[0].przeszkoda == true && potencjalnaCell[2].przeszkoda == true)
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
            return potencjalnaCell.Where(cell => cell.walkable == true).ToList();
        }
    }
}
