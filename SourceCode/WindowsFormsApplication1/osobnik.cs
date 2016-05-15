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
        public string[] parametry { get; set; }


        public double ocena { get; set; }


        public osobnik()
        {
            this.parametry = new string[3];
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

        public string[] RandomItems()
        {
            List<string> itemy = new List<string>();
            itemy.Add("Łopata");
            itemy.Add("Łom");
            itemy.Add("Maczeta");
            itemy.Add("Lina");
            itemy.Add("Wytrych");
            itemy.Add("Ciężarek");
            string[] returntable = new string[3];

            for (int i = 0; i < 3; i++)
            {
                int TwojSczescliwyNumerek = random.Next(itemy.Count());
                returntable[i] = itemy.ElementAt(TwojSczescliwyNumerek);
                itemy.RemoveAt(TwojSczescliwyNumerek);
            }
            return returntable;
        }

        public string RandomItem()
        {
            List<string> itemy = new List<string>();
            itemy.Add("Łopata");
            itemy.Add("Łom");
            itemy.Add("Maczeta");
            itemy.Add("Lina");
            itemy.Add("Wytrych");
            itemy.Add("Ciężarek");

            return itemy.ElementAt(random.Next(itemy.Count()));
        }

        public osobnik(osobnik tata, osobnik mama)
        {
            this.ocena = 0;
            this.parametry = new string[3];
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
