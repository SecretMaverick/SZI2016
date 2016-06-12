using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DecisionTree1
{
    public partial class HowToUseForm : Form
    {
        public HowToUseForm()
        {
            InitializeComponent();
        }

        private void HowToUseForm_Load(object sender, EventArgs e)
        {
            _txtExample.Text = "temperature,cloudiness,weatherreport,result" + Environment.NewLine +
                                "90,very,0,true" + Environment.NewLine +
                                "90,somewhat,0,false" + Environment.NewLine +
                                "85,none,0,false" + Environment.NewLine +
                                "85,very,1,true" + Environment.NewLine +
                                "70,very,1,true" + Environment.NewLine +
                                "85,somewhat,1,true" +
                                "90,none,0,false";
        }
    }
}