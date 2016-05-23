using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{

    
    class Populacja
    {
        public static Random random = new Random();
        public List<osobnik> osobnicy { get; set; }

        public Populacja()
        {
            osobnicy = new List<osobnik>();
            for (int i = 0; i < 20; i++) osobnicy.Add(new osobnik());
        }


        public void selekcja()
        {
           
                osobnicy = osobnicy.OrderBy(o=>o.ocena).ToList();
                for(int i=0; i<(osobnicy.Count()-5); i++)
                {
                    osobnicy.RemoveAt(i);
                }

            
        }

        public string srednia_ocena()
        {
            double temp = 0;
            for (int i = 0; i < osobnicy.Count(); i++)
            {
                temp = temp + osobnicy.ElementAt(i).ocena;
            }
            return (temp/osobnicy.Count()).ToString();
        }

        /*public void wypisz()
        {
            for(int i=0; i<osobnicy.Count(); i++)
            {
                Console.WriteLine(osobnicy.ElementAt(i).parametry[0].ToString()+ osobnicy.ElementAt(i).parametry[1].ToString() + osobnicy.ElementAt(i).parametry[2].ToString()+ osobnicy.ElementAt(i).parametry[3].ToString() + osobnicy.ElementAt(i).parametry[4].ToString()+ " "+osobnicy.ElementAt(i).mutant.ToString());
                
            }
            Console.WriteLine(srednia_ocena());
        }*/

        public void reproduce()
        {
            //Random random = new Random();
            List<osobnik> pokolenie2 = osobnicy;
            int temp = pokolenie2.Count();
            for (int i = 0; i < (20 - temp); i++)
            {
                osobnik mama = osobnicy.ElementAt(random.Next(0, osobnicy.Count()));
                osobnik tata = osobnicy.ElementAt(random.Next(0, osobnicy.Count()));
                osobnik potomek;
                if (i%2 == 0)
                {
                    potomek = new osobnik(tata, mama);
                }
                else
                {
                    potomek = new osobnik(mama, tata);
                }
                potomek.mutacja();
                pokolenie2.Add(potomek);
            }
            //for (int i = 0; i < pokolenie2.Count(); i++) pokolenie2.ElementAt(i).ocena = 0;
            osobnicy = pokolenie2;
        }
    }
}

