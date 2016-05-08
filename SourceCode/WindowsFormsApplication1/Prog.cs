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
                        // Show the path in between
                        Form1.pctBox[x, y].BackColor = Color.Green;
                    //else
                        // Show nodes that aren't part of the path
                        //Console.Write('#');
                        
                }

                //Console.WriteLine();
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
