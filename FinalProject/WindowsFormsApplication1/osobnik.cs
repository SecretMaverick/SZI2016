using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class osobnik
    {
        public static Random random = new Random();

        public bool mutant { get; set; }
        public Items[] parametry { get; set; }


        public double ocena { get; set; }


        public osobnik()
        {
            this.parametry = new Items[3];
            this.generate();
            mutant = false;
            this.ocena = 0;
        }

        public void lvlUp()
        {
            this.ocena = this.ocena + 1;
        }

        public void generate()
        {
            this.parametry = RandomItems();
        }

        public Items[] RandomItems()
        {
            
            Items[] returntable = new Items[3];

            for (int i = 0; i < 3; i++)
            {
                int TwojSczescliwyNumerek = random.Next(7);
                returntable[i] = (Items)TwojSczescliwyNumerek;                
            }
            return returntable;
        }

        public Items RandomItem()
        {
            return (Items)random.Next(7);
        }

        public osobnik(osobnik tata, osobnik mama)
        {
            this.ocena = 0;
            this.parametry = new Items[3];
            this.parametry[0] = tata.parametry[0];
            this.parametry[1] = tata.parametry[1];
            this.parametry[2] = mama.parametry[2];
        }

        public void mutacja()
        {
            Random random = new Random();
            int test = random.Next(0, 100);
            if (test == 0)
            {
                mutant = true;
                int indeks = random.Next(0, 3);
                parametry[indeks] = RandomItem();
            }
        }
    }
}
