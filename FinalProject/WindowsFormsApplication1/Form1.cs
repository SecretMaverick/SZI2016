using DecisionTree1;
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
        public static TextBox TextBoxCEA;
        public static TextBox TextBoxDT;
        public bool generated = false;
        public int[,] smallTab = new int[15, 15];
        Thing[,] specific = new Thing[10, 10]; //  
        Thing[,] general = new Thing[10, 10]; // 
        Thing Hipothesis = new Thing("nieostry", null, null, "mały");
        Thing pattern = new Thing();
        public static Point startpoint;







        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dodajtextbox();
            rysuj();
            this.ActiveControl = pctBox[1, 1];
            startpoint = new Point(0, 0);
        }

        public void dodajtextbox()
        {
            text = new TextBox();
            TextBox1 = new TextBox();
            TextBoxCEA = new TextBox();
            TextBoxDT = new TextBox();
            TextBox1.ScrollBars = ScrollBars.Vertical;
            TextBox1.Multiline = true;
            
            TextBox1.Location = new System.Drawing.Point(480, 71);
            TextBox1.Multiline = true;
            TextBox1.Name = "textBox1";
            TextBox1.Size = new System.Drawing.Size(155, 430);
            TextBox1.TabIndex = 10;
            TextBox1.Width = 200;


            TextBoxCEA.ScrollBars = ScrollBars.Vertical;
            TextBoxCEA.Location = new Point(690, 70);
            TextBoxCEA.Height = 200;
            TextBoxCEA.Multiline = true;
            TextBoxCEA.Width = 300;

            TextBoxDT.ScrollBars = ScrollBars.Vertical;
            TextBoxDT.Location = new Point(690, 300);
            TextBoxDT.Height = 200;
            TextBoxDT.Multiline = true;
            TextBoxDT.Width = 300;




            text.ScrollBars = ScrollBars.Vertical;
            text.Location = new Point(10, (14*30) + 10);
            text.Height = 100;
            text.Multiline = true;
            text.Width = 14*30;
            this.Controls.Add(text);
            this.Controls.Add(TextBox1);
            this.Controls.Add(TextBoxCEA);
            this.Controls.Add(TextBoxDT);
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
           // startpoint = new Point(0, 0);
            Prog prog = new Prog();
            this.Text = string.Empty;
            TextBox1.Text = string.Empty;

            prog.Run();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CEA cea = new CEA();
            cea.Generate();
            pattern = specific[2, 0];

            DecisionTreeImplementation sam = new DecisionTreeImplementation();
            TextBoxDT.Text = sam.GetTree("J:\\Desktop\\SZI2016-Marge\\SZI2016-master\\FinalProject\\testdata.txt");




            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1.TextBoxCEA.Text = "";
            Form1.TextBoxDT.Text = "";
            Form1.TextBox1.Text = "";
        }
    }
}
