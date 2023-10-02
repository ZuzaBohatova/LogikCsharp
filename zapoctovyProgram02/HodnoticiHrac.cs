using System;
using System.Linq;

namespace zapoctovyProgram02
{
    internal class HodnoticiHrac : Hrac
    {
        internal HodnoticiHrac(bool automat) : base(automat) { }

        /// <summary>
        /// Vraci a nastavuje hadany kod.
        /// </summary>
        internal int[] HadanyKod { get; set; }

        /// <summary>
        /// Vyhodnoti dany odhad vuci hadanemu kodu. Bud automat pomoci funkce VyhodnotPocitacem viz class Hrac,
        /// nebo pozada o vyhodnoceni uzivatele a prekontroluje jeho vstup, zda byl spravne zadan.
        /// </summary>
        /// <param name="odhad">Odhad druheho hrace. </param>
        /// <returns>Vraci reakci na dany odhad.</returns>
        internal int[] Vyhodnot(int[] odhad)      
        {
            if (automat)
            {
                int[] reakce = VyhodnotPocitacem(odhad.ToList<int>(), HadanyKod);
                Console.WriteLine($" Pocet cernych bodu: {reakce[0]} {Environment.NewLine} Pocet bilych bodu: {reakce[1]} {Environment.NewLine}");
                return reakce;
            }
            else
            {
                return VyhodnotUzivatelem();
            }
        }

        /// <summary>
        /// Zada po uzivateli ohodnoceni predchoziho odhadu, a kontroluje zda bylo hodnoceni zadano spravne, 
        /// napr. zda jsou to cisla, zda jsou to spravne velka cisla a zda davaji smysl.
        /// </summary>
        /// <returns>Vraci hodnoceni uzivatele.</returns>
        private static int[] VyhodnotUzivatelem()
        {
            int[] reakce = new int[2];  //hodnoceni tahu
            bool spravnaReakce = false;
            string vstup;

            while (!spravnaReakce)
            {
                Console.Write(" Pocet cernych bodu: ");
                vstup = Console.ReadLine();
                bool uspechParsovani = Int32.TryParse(vstup, out reakce[0]);  //zda je reakce cislo

                while (!uspechParsovani) //uzivatel nezadal cislo
                {
                    Console.WriteLine($"Nezadali jste cislo, zkuste to znovu:{Environment.NewLine}");
                    Console.Write(" Pocet cernych bodu: ");
                    vstup = Console.ReadLine();
                    uspechParsovani = Int32.TryParse(vstup, out reakce[0]);
                }

                Console.Write(" Pocet bilych bodu: ");
                vstup = Console.ReadLine();
                uspechParsovani = Int32.TryParse(vstup, out reakce[1]);   //zda je reakce cislo

                while (!uspechParsovani) //uzivatel nezadal cislo
                {
                    Console.WriteLine($"Nezadali jste cislo, zkuste to znovu:{Environment.NewLine}");
                    Console.Write($" Pocet cernych bodu: {reakce[0]}{Environment.NewLine} Pocet bilych bodu: ");
                    vstup = Console.ReadLine();
                    uspechParsovani = Int32.TryParse(vstup, out reakce[1]);
                }

                Console.WriteLine();
                spravnaReakce = true;

                if (reakce[0] + reakce[1] > 5 || reakce[0] + reakce[1] < 0)  //soucet reakci nemuze byt vetsi nez pet nebo zaporny, jelikoz mame pouze pet mist kodu
                {
                    Console.WriteLine("Soucet bilych a cernych nemuze byt vetsi nez 5 nebo mensi nez 0");
                    spravnaReakce = false;
                }
                if (reakce[0] > 5 || reakce[0] < 0 || reakce[1] > 5 || reakce[1] < 0) //zadna z reakci nemuze byt zaporna nebo vetsi nez pet - pet mist kodu
                {
                    Console.WriteLine("Pocet cernych nebo bilych nemuze byt vetsi nez 5 nebo mensi nez 0");
                    spravnaReakce = false;
                }
                if (reakce[0] == 4 && reakce[1] == 1)  //nemuze jen jedni cislo byt na spatnem miste a ostatni na spravnem
                {
                    Console.WriteLine("Tato kombinace bilych a cernych neni mozna!");
                    spravnaReakce = false;
                }
                if (!spravnaReakce)  //nekde v reakci je chyba, zadame o nove zadani
                {
                    Console.WriteLine($"Ve vasi reakci na muj odhad byla chyba, zkuste to prosim znovu {Environment.NewLine}");
                }
            }
            return reakce;
        }
    }
}
