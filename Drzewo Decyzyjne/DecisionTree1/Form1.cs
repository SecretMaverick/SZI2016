using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DecisionTree1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void onBuildTreeButtonClicked(object sender, EventArgs e)
        {
            if (_txtSourceFile.Text == String.Empty)
            {
                MessageBox.Show("Prosze wybra� odpowiedni plik");
                return;
            }

            try
            {
                DecisionTreeImplementation sam = new DecisionTreeImplementation();
                _rtxtOutput.Text = sam.GetTree(_txtSourceFile.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("B�ad pliku");
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void _btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                _txtSourceFile.Text = fileDialog.FileName;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HowToUseForm howToUseForm = new HowToUseForm();
            howToUseForm.ShowDialog(this);
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AboutBox1 ab1 = new AboutBox1();
            ab1.ShowDialog(this);
        }
    }
}