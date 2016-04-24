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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {


            PictureBox[,] pctBox = new PictureBox[10, 10];
            int[,] smallTab = new int[10,10];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    smallTab[i,j] = 1;
                }
            }

            Image image = Image.FromFile(@"J:\Desktop\kappa.png");
            for (int i = 0; i < 9;i++)
                {
                for(int j = 0; j < 9; j++)
                    {
                    if (smallTab[i, j] == 1)
                    {
                        pctBox[i, j] = new PictureBox();
                        pctBox[i, j].Height = image.Height/2;
                        pctBox[i, j].Width = image.Width/2;
                        pctBox[i, j].Image = image;
                        //pctBox[i, j].BackColor = Color.Red;
                        pctBox[i, j].Location = new Point((i * image.Width / 2) + 10, (j * image.Height / 2)+10);
                            
                        this.Controls.Add(pctBox[i, j]);

                    }
                }
                }
        }
    }
}
