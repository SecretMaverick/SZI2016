using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DecisionTree1
{
    static class Program
    {
        /// <summary>
        /// G��wny punkt wej�cia dla aplikacji.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}