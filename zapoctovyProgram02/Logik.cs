using System;

namespace zapoctovyProgram02
{
    /// <summary>
    /// Logic je hlavni trida programu, ktera ho cely ridi.
    /// Uzivatel si zde vybere, ktere nejvyssi cislo bude kod obsahovat.
    /// Nastavujeme zde typy hracu, podle toho, jaky typ hrace si uzivatel zvoli
    /// druheho hrace nastavime jako automat.
    /// </summary>
    internal class Logik
    {
        private HraciPlan hraciPlan;
        private HodnoticiHrac hodnoticiHrac;
        private HadajiciHrac hadajiciHrac;
        private int nejvyssiCislo;
        private int typHrace;

        /// <summary>
        /// Nastavi jednotlive hodnoty - typHrace a nejvyssiCislo podle toho, jake byly zadany. 
        /// Zbylé hodnoty si vyzada od uzivatele na konzoly. Pokud nektery argument byl chybne zadan, program skonci
        /// a vypise chybovou hlasku.
        /// </summary>
        /// <param name="argumenty">Argumenty, ktere zadal uzivatel pri spusteni programu.</param>
        /// <returns>Vraci bool, podle toho zda inicializace probehla nebo neprobehla v poradku.</returns>
        
        internal bool Inicializace(string[] argumenty)
        {
            bool spravneCislo = true;
            bool spravnyHrac = true;

            switch (argumenty.Length)
            {
                case 0:         //nemame zadne argumenty
                    NastavNejvyssiCislo(argumenty);
                    NastavJakyJeUzivatelHrac(argumenty);
                    break;
                case 1:             //v argumentech je zadane nejvyssi cislo
                    spravneCislo = NastavNejvyssiCislo(argumenty);
                    if (spravneCislo)
                    {
                        NastavJakyJeUzivatelHrac(argumenty);
                    }
                    break;
                case 2:         // v argumentech je nastaveno nejvyssi cislo a typ hrace
                    spravneCislo = NastavNejvyssiCislo(argumenty);
                    if (spravneCislo)
                    {
                        spravnyHrac = NastavJakyJeUzivatelHrac(argumenty);
                    }
                    break;
            }
            
            if(spravneCislo && spravnyHrac)     //pokud byly vsechny argumenty zadany spravne, inicializace probehla uspesne, vracime true
            {
                this.hraciPlan = new HraciPlan(nejvyssiCislo);
                return true;
            }
                
            return false;
        }

        /// <summary>
        /// Spusti se v pripade, ze nejvyssi cislo zadal uzivatel argumentem, kontroluje zda ma
        /// tento argument spravny format. Pokud ne vypise, ze se stala chyba a vrati false.
        /// </summary>
        /// <returns>Vraci bool podle uspechu inicializace argumentu 0.</returns>
        private bool NastavNejvyssiCislo(string[] arg)
        {
            string vstupNejvyssiCislo;
            bool spravneNacteniZKonzole = false;

            if (arg.Length == 0)
            {
                Console.WriteLine($"{Environment.NewLine}Nejprve si prosim vyberte, jaka vsechny cisla se mohou v kodu vyskytovat" +
                    $"{Environment.NewLine}napr. pro cisla od 0 do 8, zadejte 8 - pro cisla od 0 do 5, " +
                    "zadejte 5 - maximalni cislo, ktere muzete zadat je 9");
                vstupNejvyssiCislo = Console.ReadLine();
            }
            else
            {
                vstupNejvyssiCislo = arg[0];
            }

            while (!spravneNacteniZKonzole)
            {
                bool uspechParsovani = Int32.TryParse(vstupNejvyssiCislo, out nejvyssiCislo);

                if (uspechParsovani && nejvyssiCislo <= 9 && nejvyssiCislo >= 0)
                {
                    Console.WriteLine($"{Environment.NewLine}KOD bude OBSAHOVAT cisla od 0 do {nejvyssiCislo}");
                    return true;
                }
                else
                {
                    if(arg.Length == 0)
                    {
                        Console.WriteLine("Nezadali jste spravne cislo, zkuste to znovu:");
                        vstupNejvyssiCislo = Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine($"{Environment.NewLine}Prvni argument byl chybne zadany, zadejte prosim cislo od 0 do 9");
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Uzivatel si vybere, zda chce hadat nebo vybirat kod a hodnotit, podle toho zadá buď číslo 1 nebo 2.
        /// Tato hodnota se zapise do typu hrace. Uzivatel take muze zadat tuto hodnotu v argumentech programu, 
        /// pote probiha totozna kontrola jako pri zadani z konzole
        /// </summary>
        private bool NastavJakyJeUzivatelHrac(string[] arg)
        {
            Console.WriteLine($"{Environment.NewLine}Vyberte si mod hry - 1 = MOD1 = hadate nebo 2 = MOD2 = vybirate kod");

            string vstup;
            bool probehloVseVPoradku = false;

            if (arg.Length == 2)
            {
                vstup = arg[1];
            }
            else
            {
                vstup = Console.ReadLine();
            }

            while (!probehloVseVPoradku)
            {
                bool uspechParsovani = Int32.TryParse(vstup, out typHrace);
                if (uspechParsovani && (typHrace == 1 || typHrace == 2))
                {
                    InicializaceHracu();
                    return true;
                }
                else
                {
                    if (arg.Length == 2)
                    {
                        Console.WriteLine($"{Environment.NewLine}Druhy argument byl zadan chybne, druhy argument musi byt cislo 1 nebo 2");
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Zadali jste spatne cislo, zkuste to znovu");
                        vstup = Console.ReadLine();
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Inicializuje hrace podle typu hrace, ktery byl prijat bud od uzivatele z konzole nebo z argumentu programu.
        /// Vytvari podle typu hrace dane objekty s vlastnosti, zda jsou nebo nejsou automat.
        /// </summary>
        private void InicializaceHracu()
        {
            if (typHrace == 1)          //uzivatel hada, hodnoticiho hrace nastavime na automat
            {
                Console.WriteLine("HADATE KOD");
                this.hadajiciHrac = new HadajiciHrac(false);
                this.hodnoticiHrac = new HodnoticiHrac(true);
            }
            else                //uzivatel vybira kod a hodnoti odhady, hadajiciho hrace nastavime na automat 
            {
                Console.WriteLine("VYBIRATE KOD a HODNOTITE odhady protihrace");
                this.hadajiciHrac = new HadajiciHrac(true);
                this.hodnoticiHrac = new HodnoticiHrac(false);
            }
        }
        
        /// <summary>
        /// Ridi cely prubeh hry.
        /// Dokud posledniReakce neobsahuje pet cernych bodu, tak hadame dal.
        /// Spoustime metodu hadajiciHrac.SehrajTah(), pokud nam tato metoda prepise promennou konecHry na true,
        /// znamena to ze nam dosly moznosti pro hadani (v nejakem ohodnoceni uzivatele byla chyba), tak hra konci.
        /// Pokud promenna konecHry zustava false, hadame a hodnotime dal, dokud hodnoceni neobsahuje 5 na indexu 0.
        /// Pokud cyklus skonci, tak se zkontroluje, zda vyhral pocitac nebo uzivatel a vypise viteznou hlasku, 
        /// kolik kol a kolik nápovědy potřeboval na výhru.
        /// </summary>
        internal void ZacniHru()      
        {
            int[] posledniReakce = new int[2];  //ulozi se sem posledni reakce hodnoticiho hrace

            while (posledniReakce[0] != 5)         //cyklus probiha dokud posledni reakce neobsahuje 5 cernych bodu
            {
                int[] posledniOdhad;   //ulozi se sem posledni odhad hadajicicho hrace
                ++hadajiciHrac.PocetKol;     //zvysi pocet kol od 1
                if (hodnoticiHrac.Automat && hadajiciHrac.PocetKol == 1)   //pokud je hodnotici hrac automat a hrajeme 1. kolo program vygeneruje nahodny kod
                {
                    Console.WriteLine($"{Environment.NewLine}Vybral jsem si kod, nyni muzete zacit hadat{Environment.NewLine}" +
                        $"Pokud budete potrebovat NAPOVEDU, zadejte misto vaseho odhadu CISLO 1{Environment.NewLine}");
                    hodnoticiHrac.HadanyKod = hodnoticiHrac.VygenerujKod(nejvyssiCislo);
                }
                else if(hadajiciHrac.PocetKol == 1)      //pokud hrajeme 1. kolo a hodnotici hrac je uzivatel, musi si zapamatovat svuj kod
                {
                    Console.WriteLine($"{Environment.NewLine}Zapamatujte si prosim svuj kod a sledujte jak ho uhodnu{Environment.NewLine}");
                }
                
                posledniOdhad = hadajiciHrac.SehrajTah(posledniReakce, hodnoticiHrac.HadanyKod, nejvyssiCislo, ref hraciPlan.zbyvaMoznosti, ref hraciPlan.moznosti);

                if(hadajiciHrac.KonecHry)
                {
                    break;          //pokud je konec hry true, nejsou zadne dalsi moznosti, hra konci
                }

                posledniReakce = hodnoticiHrac.Vyhodnot(posledniOdhad);
            }

            if(posledniReakce[0] == 5)          //pokud jsme vyhrali vypise se vitezna hlaska podle toho zda vyhral pocitac nebo uzivatel
            {
                if (hadajiciHrac.Automat)
                {
                    Console.WriteLine($"VYHRALA JSEM na {hadajiciHrac.PocetKol}. pokus!");
                }
                else
                {
                    Console.WriteLine($"Gratuluji, VYHRALI JSTE na {hadajiciHrac.PocetKol}. pokus a pouzili jste {hadajiciHrac.GetDelkaNapovedy()}. napovedu!");
                }
            }
        }
    }
}
