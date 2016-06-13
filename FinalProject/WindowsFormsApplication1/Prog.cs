using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Prog
    {
        private bool[,] map;
        public static Events[,] EventMap = new Events[14, 14];
        private SearchParameters searchParameters;
        public List<Point> WayPoints;
        public bool CEApattern;
        public bool DTpattern;

        public Prog()
        {
            WayPoints = new List<Point>();
            WayPoints.Add(new Point(3, 6));
            WayPoints.Add(new Point(7, 4));
            WayPoints.Add(new Point(9, 4));
            WayPoints.Add(new Point(8, 7));
            WayPoints.Add(new Point(13, 2));
            WayPoints.Add(new Point(10, 0));
            WayPoints.Add(new Point(7, 12));
            WayPoints.Add(new Point(2, 10));
            WayPoints.Add(new Point(12, 10));
            WayPoints.Add(new Point(13, 13));
        }

        
    private static double[,] EventValue = 
         {
            { 0, 0, 0.8, 0, 0.5 },
            { 0, 1, 1, 0, 0 },
            { 0, 0.8, 0.8, 0, 0.5 },
            { 1, 0, 0, 0, 1 },
            { 0, 0, 0, 1, 0 },
            { 0, 0, 0, 0, 1 },
            { 0.8, 0, 0, 0, 1 }
           };

        Populacja test = new Populacja();

        public string Direct(int tempX, int tempY)
        {
            if (tempX == -1 && tempY == -1) return "North West";
            if (tempX == 0 && tempY == -1) return "North";
            if (tempX == 1 && tempY == -1) return "North East";
            if (tempX == -1 && tempY == 0) return "West";
            if (tempX == 1 && tempY == 0) return "East";
            if (tempX == -1 && tempY == 1) return "South West";
            if (tempX == 0 && tempY == 1) return "South";
            if (tempX == 1 && tempY == 1) return "South East";
            else return "error";
        }
        public void EventInitializator()
        {
            for(int i=0; i<14; i++)
            {
                for (int j = 0; j < 14; j++) EventMap[i, j] = Events.Pusto;
            }

            for (int i=0; i<14; i++)
            {
                int RandX = Populacja.random.Next(14);
                int RandY = Populacja.random.Next(14);
                EventMap[RandX, RandY] = (Events)Populacja.random.Next(5);
            }
        }

        public void Run()
        {
            // Start with a clear map (don't add any obstacles)
            InitializeMap(Form1.startpoint);
            EventInitializator();
            PathFinder pathFinder = new PathFinder(searchParameters);
            List<Point> path = pathFinder.FindPath();
            //ShowRoute("The algorithm should find a direct path without obstacles:", path);
            //Console.WriteLine();

            // Now add an obstacle
            //InitializeMap();
            AddWallWithGap();
            pathFinder = new PathFinder(searchParameters);
            path = pathFinder.FindPath();
            ShowRoute("The algorithm should find a route around the obstacle:", path);
            // Console.WriteLine();

            // Finally, create a barrier between the start and end points
            /*InitializeMap();
            AddWallWithoutGap();
            pathFinder = new PathFinder(searchParameters);
            path = pathFinder.FindPath();
            ShowRoute("The algorithm should not be able to find a route around the barrier:", path);
           // Console.WriteLine();
           */

            // Console.WriteLine("Press any key to exit...");
            // Console.ReadKey();
        }

        private void ShowRoute(string title, IEnumerable<Point> path)
        {
            //Console.WriteLine("{0}\r\n", title);
            //for (int y = this.map.GetLength(1) - 1; y >= 0; y--) // Invert the Y-axis so that coordinate 0,0 is shown in the bottom-left
            for (int y = 0; y < this.map.GetLength(1); y++)
            {
                for (int x = 0; x < this.map.GetLength(0); x++)
                {
                    if (this.searchParameters.StartLocation.Equals(new Point(x, y)))
                    {
                        // Show the start position
                        // Console.Write('S');
                        Form1.pctBox[x, y].BackColor = Color.Blue;
                    }
                    else if (this.searchParameters.EndLocation.Equals(new Point(x, y)))
                        // Show the end position
                        Form1.pctBox[x, y].BackColor = Color.Gold;
                    else if (this.map[x, y] == false)
                        // Show any barriers
                        Form1.pctBox[x, y].BackColor = Color.Black;
                    else if (path.Where(p => p.X == x && p.Y == y).Any())
                    {
                        // Show the path in between
                        Form1.pctBox[x, y].BackColor = Color.Green;
                    }

                    //else
                    // Show nodes that aren't part of the path
                    //Console.Write('#');
                }

                //Console.WriteLine();
            }
            for (int i = 0; i < path.Count(); i++)
            {
                Form1.text.AppendText("Step: " + (path.ElementAt(i).X + 1).ToString() + " " +
                                      (path.ElementAt(i).Y + 1).ToString() + " ");


                int tempX;
                int tempY;
                if (i == 0)
                {
                    tempX = path.ElementAt(i).X - this.searchParameters.StartLocation.X;
                    tempY = path.ElementAt(i).Y - this.searchParameters.StartLocation.Y;
                }
                else
                {
                    tempX = path.ElementAt(i).X - path.ElementAt(i - 1).X;
                    tempY = path.ElementAt(i).Y - path.ElementAt(i - 1).Y;
                }
                Form1.text.AppendText(this.Direct(tempX, tempY) + " \r\n");
            }
            Form1.text.AppendText("Dotarłeś do celu. \r\n");
            List<Events> EventList = new List<Events>();
            for(int i=0; i<path.Count(); i++)
            {
                if(EventMap[(path.ElementAt(i).X), (path.ElementAt(i).Y)] != Events.Pusto)
                {
                  EventList.Add(EventMap[path.ElementAt(i).X, path.ElementAt(i).Y]);
                }
            }
            Form1.TextBox1.AppendText("Liczba eventów na trasie: " + EventList.Count().ToString() + "\r\n");
            for (int i = 0; i < EventList.Count(); i++) Form1.TextBox1.AppendText(EventList.ElementAt(i).ToString() + ", ");
            Form1.TextBox1.AppendText("\r\n");
            for (int k = 0; k < 10; k++)
            {
                Form1.TextBox1.AppendText("\r\n");
                Form1.TextBox1.AppendText((k + 1).ToString() + " Pokolenie" + "Średnia ocena: " + test.srednia_ocena() +
                                          " \r\n");
                
                Form1.TextBox1.AppendText("\r\n");
                for (int j = 0; j < test.osobnicy.Count(); j++)
                {
                    test.osobnicy.ElementAt(j).ocena = 0;

                    for (int l = 0; l < EventList.Count(); l++) 
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            double bestgrade = 0;
                            if (EventValue[(int)test.osobnicy.ElementAt(j).parametry[i], (int)EventList.ElementAt(l)] > 0)
                            {
                                if (EventValue[(int)test.osobnicy.ElementAt(j).parametry[i], (int)EventList.ElementAt(l)] > bestgrade) bestgrade = EventValue[(int)test.osobnicy.ElementAt(j).parametry[i], (int)EventList.ElementAt(l)];
                            }
                            test.osobnicy.ElementAt(j).ocena = test.osobnicy.ElementAt(j).ocena + bestgrade;
                            //test.osobnicy.ElementAt(j).ocena= test.osobnicy.ElementAt(j).ocena+ EventValue[(int)test.osobnicy.ElementAt(j).parametry[i], (int)EventList.ElementAt(l)]
                            //(int)EventList.ElementAt(l)
                            //(int)test.osobnicy.ElementAt(j).parametry[i]

                        }
                    }

                    Form1.TextBox1.AppendText(test.osobnicy.ElementAt(j).ocena.ToString() + " " +
                                              test.osobnicy.ElementAt(j).mutant.ToString() + " "+test.osobnicy.ElementAt(j).parametry[0].ToString()+ " "+test.osobnicy.ElementAt(j).parametry[1].ToString()+" "+ test.osobnicy.ElementAt(j).parametry[2].ToString() +" \r\n");
                }
                test.selekcja();
                test.reproduce();
            }
        }


        private void clearMap()
        {
            for(int i=0;i<14; i++)
            {
                for(int j=0; j<14; j++)
                {
                    Form1.pctBox[i, j].BackColor = Color.Red;
                }
            }
        }


        private void InitializeMap(Point startLocation)
        {
            clearMap();
            //  □ □ □ □ □ □ □
            //  □ □ □ □ □ □ □
            //  □ S □ □ □ F □
            //  □ □ □ □ □ □ □
            //  □ □ □ □ □ □ □

            this.map = new bool[14, 14];
            for (int y = 0; y < 14; y++)
                for (int x = 0; x < 14; x++)
                    map[x, y] = true;





            //var startLocation = new Point(1, 1);
            //var endLocation = new Point(9, 9);
            //for(int i=0; i<2; i++)
            //{

                Point endLocation = startLocation;
                int indeks = (osobnik.random.Next(WayPoints.Count()));
                endLocation = WayPoints.ElementAt(indeks);
                

                this.searchParameters = new SearchParameters(startLocation, endLocation, map);
                 //startLocation = endLocation;
                Form1.startpoint.Equals(endLocation);

                WayPoints.RemoveAt(indeks);
                Thing Diamond = new Thing();
                System.Threading.Thread.Sleep(200);

            /*andom rshape = new Random();
            Random rcolor = new Random();
            Random rweight = new Random();
            Random rsize = new Random();*/

            /*hape.Next(0, 1);
            rcolor.Next(0, 3);
            rweight.Next(0, 5);
            rsize.Next(0, 2);*/
                int rshape = osobnik.random.Next(2);

                if (rshape.Equals(0)) Diamond.SetShape("Nieostry");
                else Diamond.SetShape("Ostry");


                int rcolor = osobnik.random.Next(4);
                if (rcolor.Equals(0)) Diamond.SetColor("Biały");
                else if (rcolor.Equals(1)) Diamond.SetColor("Niebieski");
                else if (rcolor.Equals(2)) Diamond.SetColor("Czerwony");
                else if (rcolor.Equals(3)) Diamond.SetColor("Zielony");


                int rweight = osobnik.random.Next(6);
                if (rweight.Equals(0)) Diamond.SetWeight("10g");
                else if (rweight.Equals(1)) Diamond.SetWeight("20g");
                else if (rweight.Equals(2)) Diamond.SetWeight("30g");
                else if (rweight.Equals(3)) Diamond.SetWeight("40g");
                else if (rweight.Equals(4)) Diamond.SetWeight("50g");
                else if (rweight.Equals(5)) Diamond.SetWeight("60g");



                int rsize = osobnik.random.Next(3);
                if (rsize.Equals(0)) Diamond.SetSize("maly");
                else if (rsize.Equals(1)) Diamond.SetSize("sredni");
                else if (rsize.Equals(2)) Diamond.SetSize("duzy");

            //////////    Diament z atrybutami

            Form1.text.AppendText("(" + Diamond.GetColor() + Diamond.GetShape() + Diamond.GetSize() + Diamond.GetWeight() + ")\n\r");

            if (Diamond.GetShape() == "Nieostry" && Diamond.GetSize() == "maly") CEApattern = true;
            else CEApattern = false;
            if (Diamond.GetShape() == "Nieostry" && (Diamond.GetSize() == "maly" || Diamond.GetSize() == "sredni")) DTpattern = true;
            else DTpattern = false;


            Form1.text.AppendText("Decyzja CEA : " + CEApattern + "\r\n" + "Decyzja DT : " + DTpattern + "\r\n");
            Form1.text.AppendText(NN.Verdict(CEApattern, DTpattern));


            /*if (WayPoints.Count() > 8)
            {
                //clearMap();
                InitializeMap(startLocation);

            }
            else
            {

            }*/


            // }

        }

        private void AddWallWithGap()
        {
            //  □ □ □ ■ □ □ □
            //  □ □ □ ■ □ □ □
            //  □ S □ ■ □ F □
            //  □ □ □ ■ ■ □ □
            //  □ □ □ □ □ □ □

            // Path: 1,2 ; 2,1 ; 3,0 ; 4,0 ; 5,1 ; 5,2

            this.map[2, 1] = false;
            this.map[1, 2] = false;
            this.map[2, 2] = false;
            this.map[4, 3] = false;
            this.map[4, 4] = false;
            this.map[4, 5] = false;
            this.map[4, 6] = false;
            this.map[4, 7] = false;
            this.map[4, 8] = false;
            this.map[4, 9] = false;
            this.map[4, 10] = false;
            this.map[4, 11] = false;

            //
           


            //
        }

        private void AddWallWithoutGap()
        {
            //  □ □ □ ■ □ □ □
            //  □ □ □ ■ □ □ □
            //  □ S □ ■ □ F □
            //  □ □ □ ■ □ □ □
            //  □ □ □ ■ □ □ □

            // No path

            this.map[3, 4] = false;
            this.map[3, 3] = false;
            this.map[3, 2] = false;
            this.map[3, 1] = false;
            this.map[3, 0] = false;
        }
    }
}