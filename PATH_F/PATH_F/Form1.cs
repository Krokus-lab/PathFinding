using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PATH_F
{
    public partial class Form1 : Form
    {
        //current grid
        List<Cell> lista_cell;
        //list of the grids from the file
        List<string> maps= FileMap.Load("maps.txt");
        //Size of the current grid
        int rozmiar;
        //Program starts
        public Form1()
        {
            InitializeComponent();
            Map.DisplayLoadedMap(maps,this);
            ElementsManagment.ChangeStart(this);
        }
        //Coloring the grid
        public void panelAdd(object sender, EventArgs e)
        {
            Panel clickedPanel = sender as Panel;
            Map.Paint(this, clickedPanel, lista_cell);
        }
        //a*
        private void button1_Click(object sender, EventArgs e)
        {
            if (lista_cell[0].isStart != -1 && lista_cell[0].isEnd != -1)
            {
                Astar astar = new Astar();
                astar.Work(lista_cell, this, lista_cell[0].isEnd, lista_cell[0].isStart, rozmiar);
            }
            else
            {
                MessageBox.Show("Create correct grid! Start point or end point might be missing");
            }
        }
        //dijkstra
        private void button3_Click(object sender, EventArgs e)
        {
            if (lista_cell[0].isStart != -1 && lista_cell[0].isEnd != -1)
            {
                Dijkstra dijkstra = new Dijkstra();
                dijkstra.Work(lista_cell, this, lista_cell[0].isEnd, lista_cell[0].isStart, rozmiar);
            }
            else
            {
                MessageBox.Show("Create correct grid! Start point or end point might be missing");
            }
        }
        //reset
        private void reset_Click(object sender, EventArgs e)
        {
            Controls.Clear();
            InitializeComponent();
           
            Map.DisplayLoadedMap(maps, this);
            ElementsManagment.ChangeReset(this);
        }
        //refresh
        private void button4_Click(object sender, EventArgs e)
        { 
            Map.Repaint(lista_cell, this);
        }
        //Creating the grid
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() != string.Empty)
            {
                if (Int32.Parse(textBox1.Text) > 1 && Int32.Parse(textBox1.Text) <100)
                {
                    rozmiar = Int32.Parse(textBox1.Text);
                    lista_cell = Map.lista(rozmiar);
                    Map.PaintMap(this,rozmiar);
                    ElementsManagment.ChangeGrid(this);

                }
                else
                    MessageBox.Show("Please enter the correct grid size (2-99)");
            }
            else
            {
                MessageBox.Show("Please enter the grid size");
            }
        }
        //Saving the grid
        private void button5_Click(object sender, EventArgs e)
        {
            if (lista_cell[0].isStart != -1 && lista_cell[0].isEnd != -1)
            {
                if (textBox2.Text.Trim() != string.Empty)
                {
                    FileMap.Save(maps,lista_cell, rozmiar, textBox2.Text.Trim());
                    maps = FileMap.Load("maps.txt");
                    comboBox1.Items.Clear();
                    Map.DisplayLoadedMap(maps, this);
                }
                else
                {
                    MessageBox.Show("Please enter the name for the  new map to be saved");
                }
            }
            else
            {
                MessageBox.Show("Create correct grid before you save it");
            }
        }
        //Loading grid from the list
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (string map in maps)
            {
                string[] dataNew = map.Split(';');
                if (comboBox1.Text == dataNew[0])
                {
                    rozmiar = Int32.Parse(dataNew[1]);
                    int start = Int32.Parse(dataNew[2]);
                    int end = Int32.Parse(dataNew[3]); ;
                    lista_cell = Map.lista(rozmiar);
                    string[] obstaclesNew = dataNew[4].Split(',');
                    Map.SetParametsForLoadedList(lista_cell, start, end, obstaclesNew,this);
                    Controls.Clear();
                    InitializeComponent();
                    label1.Text = (lista_cell[start].x).ToString();
                    label2.Text = (lista_cell[start].y).ToString();
                    label4.Text = (lista_cell[end].x).ToString();
                    label5.Text = (lista_cell[end].y).ToString();
                    Map.PaintMap(this, rozmiar);
                    Map.Repaint(lista_cell, this);
                    Map.DisplayLoadedMap(maps, this);
                    ElementsManagment.ChangeList(this);
                }

            }
        }
    }
}


