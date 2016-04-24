using System;
using System.Collections.Generic;
using IndianaFord.Properties;

namespace IndianaFord
{
    class Treasure : IndianaFord.IndianaFord
    {
        /// <summary>
        /// Wartosc zlota jakie posiada agent
        /// </summary>
        public int zloto;
        /// <summary>
        /// Tablica aktualnie posiadanych skarbow przez agenta
        /// </summary>
        public List<string> skarby;
        /// <summary>
        /// Zrodlo jakie chce zebrac agent
        /// </summary>
        public string zrodlo;
        /// <summary>
        /// Przedmiot jakim chce zebrac dane zrodlo
        /// </summary>
        public string przedmiot;
        /// <summary>
        /// Wartosc zrodla ktory chce zebrac agent
        /// </summary>
        public int wartosc_zrodla;
        /// <summary>
        /// Typ skarbu jaki wydobyl agent
        /// </summary>
        public string typ_skarbu;
        /// <summary>
        /// Ilosc skarbu wydobytego przez agenta
        /// </summary>
        public int ilosc_skarbu;
        /// <summary>
        /// Wartosc zlota skarbu ktory wydobyl agent
        /// </summary>
        public int zloto_skarbu;
        /// <summary>
        /// wartosc energii jaka potrzebuje agent do wydobycia
        /// </summary>
        public int energia_wydobycie;
        /// <summary>
        /// Wartosc zlota zrodla
        /// </summary>
        public int zloto_zrodla;
        /// <summary>
        /// Wartosc calkowita zlota
        /// </summary>
        public int calkowite_zloto;
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>zloto</returns>
        public int Aktualne_zloto()
        {
            Console.WriteLine(Resources.AktualneZłoto);
            return zloto;
        }
        
        /// <summary>
        /// Metoda zwracajaca tablice skarbow
        /// </summary>
        /// <returns>skarby</returns>
        public List<string> Aktualne_skarby()
        {
            Console.WriteLine(Resources.AktualneSkarby);
            return skarby;
        }
         
        /// <summary>
        /// Metoda zbierania przez agenta
        /// </summary>
        /// <param name="zrodlo"></param>
        /// <param name="przedmiot"></param>
        /// <param name="wartosc_zrodla"></param>
        public void Gather(string zrodlo, string przedmiot, int wartosc_zrodla, int zloto_zrodla, int energia_wydobycie)
        {
            foreach (var nazwa in ekwipunek)
            {
                if (nazwa != "Łopata") continue;
                this.zrodlo = "Skarb";
                this.przedmiot = "Łopata";
                this.wartosc_zrodla = 2;
                this.zloto_zrodla = 10;
                this.energia_wydobycie = 15;

                Treasure_data(zrodlo, wartosc_zrodla, zloto_zrodla);
                Add_treasure(typ_skarbu, zloto_zrodla, wartosc_zrodla);
                energia = energia - energia_wydobycie;

                Console.WriteLine(Resources.ZdobycieSkarbu + skarby.Count + @"skarbów oraz" + calkowite_zloto+@"złota");
            }
            Console.WriteLine(Resources.NieudaneZdobycieSkarbu);
        }

        public void Gather_from_chest(string zrodlo, string przedmiot, int wartosc_zrodla, int zloto_zrodla, int energia_wydobycie)
        {
            if (is_chest_open)
            {
                this.zrodlo = "Kryształowa Czaszka";
                this.przedmiot = "Ręka";
                this.wartosc_zrodla = 1;
                this.zloto_zrodla = 100;
                this.energia_wydobycie = 5;

                Treasure_data(zrodlo, wartosc_zrodla, zloto_zrodla);
                Add_treasure(typ_skarbu,zloto_zrodla,wartosc_zrodla);

                Console.WriteLine(Resources.ZdobycieKryształowejCzaszki);
            }
            Console.WriteLine(Resources.SkrzyniaZamknieta);
        }

        /// <summary>
        /// Metoda oblugujaca skarb
        /// </summary>
        public void Treasure_data(string typ_skarbu, int ilosc_skarbu, int zloto_skarbu)
        {
            this.typ_skarbu = "Klejnoty";
            this.ilosc_skarbu = 10;
            this.zloto_skarbu = 100;

            zloto_zrodla = ilosc_skarbu*zloto_skarbu;
        }

        /// <summary>
        /// Metoda dodajaca skarb ktory zebral agent to listy skarbow
        /// </summary>
        public void Add_treasure(string typ, int wartosc, int ilosc)
        {
            skarby.Add(typ_skarbu);
            calkowite_zloto = wartosc_zrodla * zloto_zrodla;
            zloto = zloto + calkowite_zloto;
            Console.WriteLine(Resources.DodanieSkarbu);
        }

        /// <summary>
        /// Metoda usuwajaca skarb z listy skarbow
        /// </summary>
        public void Delete_treasure(string typ, int wartosc, int ilosc)
        {
            skarby.RemoveAt(1);
            calkowite_zloto = wartosc_zrodla * zloto_zrodla;
            zloto = zloto - calkowite_zloto;
            Console.WriteLine(Resources.UsuniecieSkarbu);
        }
    }
}
