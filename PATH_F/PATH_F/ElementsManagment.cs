using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PATH_F
{
    class ElementsManagment
    {
        public static void ChangeStart(Form1 form)
        {
            form.button1.Enabled = false;
            form.button3.Enabled = false;
            form.button5.Enabled = false;
            form.button1.Visible = false;
            form.button3.Visible = false;
            form.button5.Visible = false;
            form.textBox2.Enabled = false;
            form.textBox2.Visible = false;
            form.label9.Visible = false;
            form.label8.Visible = false;
            form.label10.Visible = false;
            form.label11.Visible = false;
            form.label12.Visible = false;
            form.button4.Enabled = false;
            form.button4.Visible = false;
        }

        public static void ChangeReset(Form1 form)
        {
            form.button1.Enabled = false;
            form.button3.Enabled = false;
            form.button5.Enabled = false;
            form.button1.Visible = false;
            form.button3.Visible = false;
            form.button5.Visible = false;
            form.textBox2.Enabled = false;
            form.textBox2.Visible = false;
            form.label9.Visible = false;
            form.button2.Enabled = true;
            form.button2.Visible = true;
            form.textBox1.Enabled = true;
            form.textBox1.Visible = true;
            form.label8.Visible = false;
            form.label10.Visible = false;
            form.label11.Visible = false;
            form.label12.Visible = false;
            form.button4.Enabled = false;
            form.button4.Visible = false;
        }

        public static void ChangeGrid(Form1 form)
        {
            form.button2.Enabled = false;
            form.button2.Visible = false;
            form.textBox1.Enabled = false;
            form.textBox1.Visible = false;
            form.button1.Enabled = true;
            form.button3.Enabled = true;
            form.button5.Enabled = true;
            form.button1.Visible = true;
            form.button3.Visible = true;
            form.button5.Visible = true;
            form.textBox2.Enabled = true;
            form.textBox2.Visible = true;
            form.label9.Visible = true;
            form.label8.Visible = true;
            form.label10.Visible = true;
            form.label11.Visible = true;
            form.label12.Visible = true;
            form.button4.Enabled = true;
            form.button4.Visible = true;
        }

        public static void ChangeList(Form1 form)
        {
            form.button1.Enabled = true;
            form.button3.Enabled = true;
            form.button5.Enabled = true;
            form.button1.Visible = true;
            form.button3.Visible = true;
            form.button5.Visible = true;
            form.textBox2.Enabled = true;
            form.textBox2.Visible = true;
            form.button2.Enabled = false;
            form.button2.Visible = false;
            form.textBox1.Enabled = false;
            form.textBox1.Visible = false;
            form.button4.Enabled = true;
            form.button4.Visible = true;
        }
    }
}
