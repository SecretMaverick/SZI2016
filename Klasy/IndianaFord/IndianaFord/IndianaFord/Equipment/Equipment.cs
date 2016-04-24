using System;
using System.Collections.Generic;
using IndianaFord.Properties;

namespace IndianaFord
{
    class Equipment
    {
        /// <summary>
        /// Lista ekwipunku posiadanego przez agenta
        /// </summary>
        public List<string> ekwipunek;
        /// <summary>
        /// Typ przedmiotu jaki posiada agent
        /// </summary>
        public string typ_ekwipunek;


        /// <summary>
        /// Metoda zwracajaca liste aktualnie posiadanego ekwipunku przez agenta
        /// </summary>
         public List<string> Aktualny_ekwipunek()
        {
            Console.WriteLine(Resources.AktualnyEkwipunek);
            return ekwipunek;
        }

        public void Equipment_Data(string typ_ekwipunek)
        {
            this.typ_ekwipunek = "Łopata";
        }

         /// <summary>
         /// Metoda dodajaca ekwipunek ktory zebral agent to listy ekwipunku
         /// </summary>
        public void Add_equipment(string typ)
         {
             ekwipunek.Add(typ_ekwipunek);
             Console.WriteLine(Resources.DodanieEkwipunku);
         }

         /// <summary>
         /// Metoda usuwajaca skarb z listy skarbow
         /// </summary>
        public void Delete_equipment(string typ)
         {
             ekwipunek.RemoveAt(1);
             Console.WriteLine(Resources.UsuniecieEkwipunku);
         }

    }
}
