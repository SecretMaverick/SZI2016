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
            InitializeMap();
            EventInitializator();
            PathFinder pathFinder = new PathFinder(searchParameters);
            List<Point> path = pathFinder.FindPath();
            //ShowRoute("The algorithm should find a direct path without obstacles:", path);
            //Console.WriteLine();

            // Now add an obstacle
            InitializeMap();
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
                        Form1.pctBox[x, y].BackColor = Color.Blue;
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

        private void InitializeMap()
        {
            //  □ □ □ □ □ □ □
            //  □ □ □ □ □ □ □
            //  □ S □ □ □ F □
            //  □ □ □ □ □ □ □
            //  □ □ □ □ □ □ □

            this.map = new bool[14, 14];
            for (int y = 0; y < 14; y++)
                for (int x = 0; x < 14; x++)
                    map[x, y] = true;

            var startLocation = new Point(1, 1);
            var endLocation = new Point(9, 9);
            this.searchParameters = new SearchParameters(startLocation, endLocation, map);
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