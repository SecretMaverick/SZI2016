using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class CEA
    {


        Thing[,] specific = new Thing[4, 4]; //  
        Thing[,] general = new Thing[4, 4]; // 
        //Thing Hipothesis = new Thing("nieostry", null, null, "mały");
        public Thing Pattern;
        


        Thing Stop = new Thing("STOP", "", "", "");
        public String Result;
        Thing Pos1 = new Thing("nieostry", "biały", "40g", "mały");
        Thing Pos2 = new Thing("nieostry", "biały", "70g", "mały"); 
        Thing Pos3 = new Thing("nieostry", "zielony", "200g", "średni");
        Thing Neg1 = new Thing("nieostry", "niebieski", "300g", "duży");
        Thing Neg2 = new Thing("ostry", "czerwony", "200g", "mały");


        public void Generate()
        {
            Form1.TextBoxCEA.Text = "";
            
            for (int z = 0; z < 4; z++)
            {
                for(int x = 0; x < 4; x++)
                {
                    general[z, x] = new Thing();
                    specific[z, x] = new Thing();
                }
            }
            general[0, 0] = new Thing("START", "", "", "");
            /////////////////////////////
            ////  Pierwszy podział
            /////////////////////////////
            int i = 0;

            specific[i, 0] = Pos1;
            i++;
            Thing tmp = new Thing();
            Thing tmp2 = new Thing();

            if (Neg1.GetShape() != Pos1.GetShape())
                general[i, 0].SetShape(Pos1.GetShape());
            else
            {
                tmp2.SetShape(Pos1.GetShape());
                general[i, 0].SetShape("STOP"); 
            }
            if (Neg1.GetColor() != Pos1.GetColor())
                general[i, 1].SetColor(Pos1.GetColor());
            else
            {
                tmp2.SetColor(Pos1.GetColor());
                general[i, 1].SetShape("STOP");
            }
            if (Neg1.GetWeight() != Pos1.GetWeight())
                general[i, 2].SetWeight(Pos1.GetWeight());
            else
            {
                tmp2.SetWeight(Pos1.GetWeight());
                general[i, 2].SetShape("STOP");
            }
            if (Neg1.GetSize() != Pos1.GetSize())
                general[i, 3].SetSize(Pos1.GetSize());
            else
            {
                tmp2.SetSize(Pos1.GetSize());
                general[i, 3].SetShape("STOP");
            }

            ///////////////////////////////////
            ////        Pierwsza iteracja - pozytywny generalizuje special i obcina general
            ///////////////////////////////////

            if (Pos2.GetShape() == general[i,0].GetShape())
            {
                tmp.SetShape(Pos2.GetShape());
            }
            else general[i + 1, 0].SetShape("STOP");

            if (Pos2.GetColor() == general[i, 1].GetColor())
            {
                tmp.SetColor(Pos2.GetColor());
            }
            else general[i + 1, 1].SetShape("STOP");

            if (Pos2.GetWeight() == general[i, 2].GetWeight())
            {
                tmp.SetWeight(Pos2.GetWeight());
            }
            else general[i + 1, 2].SetShape("STOP");

            if (Pos2.GetSize() == general[i, 3].GetSize())
            {
                tmp.SetSize(Pos2.GetSize());
            }
            else general[i + 1, 3].SetShape("STOP");

            tmp.SetShape(Pos2.GetShape());
            specific[1, 0] = tmp;

            i++;


            //////////////////
            //  Druga iteracja -negatywny sepcjalizuje generala 
            //////////////////
            

            if(general[i,0].GetShape() != "STOP" && general[i-1, 0].GetShape() != "")
            {
                tmp = general[i - 1, 0];
                if (tmp.GetShape() == Neg2.GetShape()) tmp2.SetShape(Neg2.GetShape());
                if (tmp.GetColor() == Neg2.GetColor()) tmp2.SetColor(Neg2.GetColor());
                if (tmp.GetWeight() == Neg2.GetWeight()) tmp2.SetWeight(Neg2.GetWeight());
                if (tmp.GetSize() == Neg2.GetSize()) tmp2.SetSize(Neg2.GetSize());
                //if ((general[i - 1, 0].GetShape() == "" && tmp2.GetShape() != "") || (general[i - 1, 0].GetColor() == "" && tmp2.GetColor() != "") || (general[i - 1, 0].GetWeight() == "" && tmp2.GetWeight() != "") || (general[i - 1, 0].GetSize() == "" && tmp2.GetSize() != "")) specific[2, 0] = Stop;
                general[i, 0] = tmp2;
            }

            if (general[i, 1].GetShape() != "STOP" && general[i - 1, 1].GetColor() != "")
            {
                tmp = general[i - 1, 1];
                if (tmp.GetShape() == Neg2.GetShape()) tmp2.SetShape(Neg2.GetShape());
                if (tmp.GetColor() == Neg2.GetColor()) tmp2.SetColor(Neg2.GetColor());
                if (tmp.GetWeight() == Neg2.GetWeight()) tmp2.SetWeight(Neg2.GetWeight());
                if (tmp.GetSize() == Neg2.GetSize()) tmp2.SetSize(Neg2.GetSize());
                //if ((general[i - 1, 1].GetShape() == "" && tmp2.GetShape() != "") || (general[i - 1, 1].GetColor() == "" && tmp2.GetColor() != "") || (general[i - 1, 1].GetWeight() == "" && tmp2.GetWeight() != "") || (general[i - 1, 1].GetSize() == "" && tmp2.GetSize() != "")) specific[2, 0] = Stop;
                
                general[i, 1] = tmp2;
                //general[i, 1] = Stop;


            }
            if (general[i, 2].GetShape() != "STOP" && general[i-1, 2].GetWeight() != "")
            {
                tmp = general[i - 1, 2];
                if (tmp.GetShape() == Neg2.GetShape()) tmp2.SetShape(Neg2.GetShape());
                if (tmp.GetColor() == Neg2.GetColor()) tmp2.SetColor(Neg2.GetColor());
                if (tmp.GetWeight() == Neg2.GetWeight()) tmp2.SetWeight(Neg2.GetWeight());
                if (tmp.GetSize() == Neg2.GetSize()) tmp2.SetSize(Neg2.GetSize());
                //if ((general[i - 1, 2].GetShape() == "" && tmp2.GetShape() != "") || (general[i - 1, 2].GetColor() == "" && tmp2.GetColor() != "") || (general[i - 1, 2].GetWeight() == "" && tmp2.GetWeight() != "") || (general[i - 1, 2].GetSize() == "" && tmp2.GetSize() != "")) specific[2, 0] = Stop;
                general[i, 2] = tmp2;
            }
            if (general[i, 3].GetShape() != "STOP" && general[i-1, 3].GetSize() != "")
            {
                tmp = general[i - 1, 3];
                if (tmp.GetShape() == Neg2.GetShape()) tmp2.SetShape(Neg2.GetShape());
                if (tmp.GetColor() == Neg2.GetColor()) tmp2.SetColor(Neg2.GetColor());
                if (tmp.GetWeight() == Neg2.GetWeight()) tmp2.SetWeight(Neg2.GetWeight());
                if (tmp.GetSize() == Neg2.GetSize()) tmp2.SetSize(Neg2.GetSize());
                //if ((general[i - 1, 3].GetShape() == "" && tmp2.GetShape() != "") || (general[i - 1, 3].GetColor() == "" && tmp2.GetColor() != "") || (general[i - 1, 3].GetWeight() == "" && tmp2.GetWeight() != "") || (general[i - 1, 3].GetSize() == "" && tmp2.GetSize() != "")) specific[2, 0] = Stop;
                general[i, 3] = tmp2;
            }
            specific[2, 0] = tmp2;
            if ((specific[1, 0].GetShape() == "" && specific[2, 0].GetShape() != "") || (specific[1, 0].GetColor() == "" && specific[2, 0].GetColor() != "") || (specific[1, 0].GetWeight() == "" && specific[2, 0].GetWeight() != "") || (specific[1, 0].GetSize() == "" && specific[2, 0].GetSize() != "")) specific[2, 0] = Stop;




            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k<4; k++)
                {
                    Form1.TextBoxCEA.AppendText("(" + general[j,k].GetShape() + "," + general[j, k].GetColor() + "," + general[j, k].GetWeight() + "," + general[j, k].GetSize() + ")");
                }
                Form1.TextBoxCEA.AppendText(Environment.NewLine);
            }
            Form1.TextBoxCEA.AppendText(Environment.NewLine);
            Form1.TextBoxCEA.AppendText(Environment.NewLine);
            Form1.TextBoxCEA.AppendText(Environment.NewLine);

            for (int j = 2; j >=0 ; j--)
            {

                Form1.TextBoxCEA.AppendText("(" + specific[j, 0].GetShape() + "," + specific[j, 0].GetColor() + "," + specific[j, 0].GetWeight() + "," + specific[j, 0].GetSize() + ")");

                Form1.TextBoxCEA.AppendText(Environment.NewLine);
            }

            Result = "Failed";

            for (int b = 0; b < 4; b++)
            {
                if (specific[2, 0].GetShape() == general[2, b].GetShape() && specific[2, 0].GetColor() == general[2, b].GetColor() && specific[2, 0].GetWeight() == general[2, b].GetWeight() && specific[2, 0].GetSize() == general[2, b].GetSize())
                    Result = "Success";

            }

            Form1.TextBoxCEA.AppendText(Environment.NewLine);
            Form1.TextBoxCEA.AppendText(Result);
            Pattern = specific[2, 0];
        }


    }


}
