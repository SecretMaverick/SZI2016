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
        private SearchParameters searchParameters;

        public string Direct(int tempX, int tempY)
        {
            
                if(tempX==-1 && tempY==-1) return "North West";
            if (tempX == 0 && tempY == -1) return "North";
            if (tempX == 1 && tempY == -1) return "North East";
            if (tempX == -1 && tempY == 0) return "West";
            if (tempX == 1 && tempY == 0) return "East";
            if (tempX == -1 && tempY == 1) return "South West";
            if (tempX == 0 && tempY == 1) return "South";
            if (tempX == 1 && tempY == 1) return "South East";
            else return "error";
                
            

        }

        public void Run()
        {
            // Start with a clear map (don't add any obstacles)
            InitializeMap();
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
            for(int y=0; y< this.map.GetLength(1); y++)
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
                        //Form1.text.AppendText("Step: " + (x + 1).ToString() + " " + (y + 1).ToString() + "\r\n");
                    }
                    
                    //else
                        // Show nodes that aren't part of the path
                        //Console.Write('#');
                        
                }

                //Console.WriteLine();
            }
            for (int i = 0; i < path.Count(); i++)
            {
                Form1.text.AppendText("Step: " + (path.ElementAt(i).X+1).ToString() + " " + (path.ElementAt(i).Y+1).ToString() + " ");
                
                
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
                    Form1.text.AppendText(this.Direct(tempX, tempY)+ " \r\n");


                    
                
            }
            Form1.text.AppendText("Dotarłeś do celu. \r\n");
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
            this.map[4,11] = false;
            
           
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
