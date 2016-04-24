
using System;
using IndianaFord.Properties;

namespace IndianaFord.IndianaFord
{
    class IndianaFord : Equipment
    {
        /// <summary>
        /// Energia agenta
        /// </summary>
        public int energia;
        /// <summary>
        /// Kierunek w jakim porusza sie agent(Indiana Ford)
        /// </summary>
        public string kierunek;
        /// <summary>
        /// Typ ruchu jakim porusza sie agent
        /// </summary>
        public string typ;
        /// <summary>
        /// Wartosc ruchu z jaka porusza sie agent
        /// </summary>
        public int wartosc_ruch;
        /// <summary>
        /// Wartosc energii jaka musi wykorzystac agent na dany ruch
        /// </summary>
        public int energia_ruch;
        /// <summary>
        /// Flaga informujace czy napotkana skrzynka jest otwarta
        /// </summary>
        public bool is_chest_open;

        /// <summary>
        /// Metoda zwracajaca aktualny stan energii agenta
        /// </summary>
        public int Aktualna_energia()
        {
            return energia;
        }

        /// <summary>
        /// Metoda ruchu agenta
        /// </summary>
        /// <param name="kierunek"></param>
        /// <param name="typ"></param>
        /// <param name="wartosc_ruch"></param>

        public void Move(string kierunek, string typ, int wartosc_ruch, int energia_ruch)
        {
            this.kierunek = "Przod";
            this.typ = "Bieg";
            this.wartosc_ruch = 10;
            this.energia_ruch = 5;

            energia = energia - energia_ruch;

            Console.WriteLine(Resources.Ruch+@" w kierunku: "+kierunek+@" o "+wartosc_ruch);
        }

        /// <summary>
        /// Metoda otwierania skrzyni przez agenta
        /// </summary>
        public void Open_chest()
        {
            foreach (var nazwa in ekwipunek)
            {
                if (nazwa == "Łom")
                {
                    is_chest_open = true;
                }
                    is_chest_open = false;

            }
        }

        /// <summary>
        /// Metoda wywoływana w przypadku kiedy natrafimy na skrzynie
        /// </summary>
        public void Chest_encounter()
        {
            Open_chest();
            Console.WriteLine(Resources.OtworzenieSkrzyni);
        }

        /// <summary>
        /// Metoda wywoływana w przypadku kiedy natrafimy na przeszkode
        /// </summary>
        public void Obstacle_encounter()
        {
            if (energia > 0)
            {
               Console.WriteLine(Resources.PrzejsciePrzeszkody); 
            }
            Console.WriteLine(Resources.NieudanePrzejsciePrzeszkody);
            EndGame();
        }

        /// <summary>
        /// Metoda wywoływana gry energia bedzie na 0
        /// </summary>
        public void EndGame()
        {
            Console.WriteLine(Resources.KoniecGry);
        }

    }
}
