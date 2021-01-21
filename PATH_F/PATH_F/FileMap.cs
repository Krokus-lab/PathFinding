using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace PATH_F
{
    public class FileMap
    {
        public static void Save(List<String> maps,List<Cell> lista_cell,int rozmiar,string name)
        {
            bool same = false;
            foreach (string map in maps)
            {
               string[] unknown = map.Split(';');
                if (name == unknown[0])
                    same=true;
            }
            if (same == false)
            {
                string FileName = "maps.txt";
                string start = "";
                string end = "";
                string obstacle = "";
                string size = rozmiar.ToString();

                foreach (var cell in lista_cell)
                {
                    if (cell.przeszkoda == true)
                    {
                        obstacle = obstacle + cell.index.ToString() + ",";
                    }
                    if (cell.start == true)
                    {
                        start = cell.index.ToString();
                    }
                    if (cell.end == true)
                    {
                        end = cell.index.ToString();
                    }
                }
                string Map = name + ";" + size + ";" + start + ";" + end + ";" + obstacle + ";" + "\n";
                File.AppendAllText(FileName, Map);
            }
            else
            {
                MessageBox.Show("There is already a map with this name");
            }
        }

        public static List<string> Load(string nameFile)
        {
            List<string> maps = new List<string>();
            using (var reader = new StreamReader(nameFile))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    maps.Add(line);
                }
            }
            return maps;
        }
    }
}

