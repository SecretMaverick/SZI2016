using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public static PictureBox[,] pctBox = new PictureBox[15, 15];
        public static TextBox text;
        public static TextBox TextBox1;
        public bool generated = false;
        public int[,] smallTab = new int[15, 15];

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dodajtextbox();
            rysuj();
            this.ActiveControl = pctBox[1, 1];
        }

        public void dodajtextbox()
        {
            text = new TextBox();
            TextBox1 = new TextBox();
            TextBox1.ScrollBars = ScrollBars.Vertical;
            TextBox1.Multiline = true;

            TextBox1.Location = new System.Drawing.Point(480, 71);
            TextBox1.Multiline = true;
            TextBox1.Name = "textBox1";
            TextBox1.Size = new System.Drawing.Size(155, 351);
            TextBox1.TabIndex = 10;
            TextBox1.Width = 200;
            
            text.ScrollBars = ScrollBars.Vertical;
            text.Location = new Point(10, (14*30) + 10);
            text.Height = 100;
            text.Multiline = true;
            text.Width = 14*30;
            this.Controls.Add(text);
            this.Controls.Add(TextBox1);
        }

        public void gentable()
        {
            for (int i = 0; i < 14; i++)
            {
                for (int j = 0; j < 14; j++)
                {
                    smallTab[i, j] = 1;
                }
            }

            generated = true;
        }

        public void rysuj()
        {
            if (generated == false)
            {
                gentable();
            }
            for (int i = 0; i < 14; i++)
            {
                for (int j = 0; j < 14; j++)
                {
                    pctBox[i, j] = new PictureBox();
                    pctBox[i, j].Height = 25;
                    pctBox[i, j].Width = 25;
                    if (smallTab[i, j] == 2)
                    {
                        pctBox[i, j].BackColor = Color.Gray;
                    }
                    else
                    {
                        pctBox[i, j].BackColor = Color.Brown;
                    }
                    pctBox[i, j].Location = new Point((i*30) + 10, (j*30) + 10);

                    this.Controls.Add(pctBox[i, j]);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Prog prog = new Prog();
            this.Text = string.Empty;
            TextBox1.Text = string.Empty;
            prog.Run();
        }

        

        
    }
}
