using System;
using System.Collections.Generic;
using System.Linq;

namespace zapoctovyProgram02
{
    internal class HadajiciHrac : Hrac
    {
        /// <summary>
        /// Zde se ulozi gracuv odhad kodu.
        /// </summary>
        private int[] odhad = new int[5]; 

        /// <summary>
        /// Uklademe sem, ktera policka kodu uz zname z napovedy.
        /// </summary>
        private List<int> napoveda = new List<int>();     
        
        /// <summary>
        /// Pocet kol, ktera uz jsme odehrali.
        /// </summary>
        
        internal HadajiciHrac(bool automat) : base(automat) { }   //konstruktor HadajicihoHrace

        internal bool KonecHry { get; set;}
             
        internal int PocetKol { get; set;}

        /// <summary>
        /// Vraci delku napovedy.
        /// </summary>
        /// <returns></returns>
        internal int GetDelkaNapovedy()       
        {
            return napoveda.Count;
        }

        /// <summary>
        /// Vybere jeden mozny odhad podle toho, jestli je hadajici hrac uživatel zavola metodu ZadejSvujOdhad()
        /// nebo pocitac (JsouJesteNejakeMoznosti()) a vrati dany odhad.
        /// </summary>
        /// <param name="reakce"> Posledni reakce hodnoticiho hrace.</param>
        /// <param name="hadanyKod"> Kod, ktery vybral hodnotici uzivatel.</param>
        /// <param name="nejvyssiCislo"> Nejvyssi cislo, ktere muze hadany kod obsahovat.</param>
        /// <param name="zbyvaMoznosti"> Kolik nam zbyva moznosti. </param>
        /// <param name="moznosti"> Vsechny moznosti kodu.</param>
        /// <returns>Vraci dany odhad.</returns>
        internal int[] SehrajTah(int[] reakce, int[] hadanyKod, int nejvyssiCislo, ref int zbyvaMoznosti, ref int[,] moznosti)
        {
            if (automat)
            {
                                if (PocetKol == 1)      //pokud hrajeme teprve kolo jedna, hodnotici hrac automat musi nejprve vygenerovat hadany kod vuci kteremu pak bude hodnotit
                {
                    odhad = VygenerujKod(nejvyssiCislo);
                    Console.WriteLine($"Můj {PocetKol}. odhad je {String.Join("", odhad)}");
                }
                else
                {
                    KonecHry = JsouJesteNejakeMoznosti(reakce, ref zbyvaMoznosti, ref moznosti);
                    if (!KonecHry)     //pokud hra jeste neskoncila vypiseme nas odhad
                    {
                        Console.WriteLine($"Můj {PocetKol}. odhad je {String.Join("", odhad)}");
                    }
                }
            }
            else
            {
                odhad = ZadejSvujOdhad(nejvyssiCislo, hadanyKod);       //hada uzivatel
            }
            return odhad;
        }

        /// <summary>
        /// Metoda pracuje s polem moznosti a promennou zbyvaMoznosti z objektu hraciPlan(odkazujeme na ne referenci) 
        /// a porovnava vsechny moznosti s reakci, kterou mame a pokud by reakce nesedela,
        /// tak snizuje promennou zbyvaMoznosti a prepisuje pole moznosti, tak aby cislo zbyvaMoznosti indexovalo na posledni moznou variantu v poli moznosti.
        /// Ve chvili, kdy dojdou moznosti vraci true(je konec hry),pokud nedojdou zavola metodu VygenerujDalsiOdhad(), 
        /// ktera nahodne vybere jeden z jeste moznych odhadu.
        /// </summary>
        /// <param name="reakce">Posledni reakce odruheho hrace s kterou porovnavame potencionalni kody zda by sedeli.</param>
        /// <param name="zbyvaMoznosti"> Pocet moznosti ktere nam zbyvaji, pomoci nich indexujeme do pole moznosti.</param>
        /// <param name="moznosti"> Pole vsech moznosti, zadne se nemazou, jen se dopredu posunuji ty jeste mozne, posledni mozna variace je vzdy na indexu zbyvaMoznosti.</param>
        /// <returns>Vraci true/false podle toho zda uz skoncila hra - true = skoncila. </returns>
        private bool JsouJesteNejakeMoznosti(int[] reakce, ref int zbyvaMoznosti, ref int[,] moznosti) 
        {
            int kam = 0;  //kam piseme v poli moznosti

            if (zbyvaMoznosti == 0) //zadne moznosti - neni reseni, uzivatel nekde udelal chybu ve vyhodnoceni
            {
                Console.WriteLine("Nejsou zadne dalsi moznosti, ve vasich reakcich je nekde chyba");
                return true;  //konec hry
            }
            else //jinak vyhodnotit
            {
                for (int i = 0; i < zbyvaMoznosti; i++)
                {
                    int[] kod = new int[5];

                    for (int k = 0; k < 5; k++)
                    {
                        kod[k] = moznosti[k, i];
                    }

                    int[] vyhodnoceni = VyhodnotPocitacem(odhad.ToList<int>(), kod);   //vyhodnotime odhad oproti moznemu kodu

                    if (reakce[1] == vyhodnoceni[1] && reakce[0] == vyhodnoceni[0]) //pokud sedi ohodnoceni - muze to byt hledany kod
                    {
                        for (int j = 0; j < 5; j++) //prepis - realne moznosti posouvame nahoru
                        {
                           moznosti[j, kam] = moznosti[j, i];
                        }

                        kam++; //kam piseme se posune o radek dal
                    }
                }

                zbyvaMoznosti = kam; // zmensuje se pocet moznosti

                if (zbyvaMoznosti == 0)  //neni zadna dalsi moznost
                {
                    Console.WriteLine("Nejsou zadne dalsi moznosti, nekde ve vasich reakcich je chyba");
                    return true;    //konec hry
                }
                else
                {
                    VygenerujDalsiOdhad(zbyvaMoznosti, moznosti); //vybereme dalsi odhad ze zbylych moznych odhadu
                }
            }
            return false;
        }
        
        /// <summary>
        /// Vybere nahodne jeden z dalsich moznych tahu.
        /// </summary>
        /// <param name="zbyvaMoznosti">Maximalni index do pole moznosti.</param>
        /// <param name="moznosti">Pole vsech moznosti.</param>
        private void VygenerujDalsiOdhad(int zbyvaMoznosti, int[,] moznosti) 
        {
            Random r = new Random();
            int index = r.Next(zbyvaMoznosti);

            for (int i = 0; i < 5; i++) //5 pozic
            {
                odhad[i] = moznosti[i, index]; //vyber
            }
        }

        /// <summary>
        /// Zada po uzivateli, aby zadal svuj odhad a  kontroluje jeho syntaktickou spravnost (delku, zda obsahuje spravna cisla, …), 
        /// pokud uzivatel misto odhadu napise cislo 1, spusti metodu napoved. 
        /// </summary>
        /// <param name="nejvyssiCislo"> Nejvyssi mozne cislo, ktere muze odhad obsahovat.</param>
        /// <param name="hadanyKod"> Kod, ktery hadame, zde uzivan pri moznosti napovedy.</param>
        /// <returns>Vraci odhad.</returns>
       private int[] ZadejSvujOdhad(int nejvyssiCislo, int[] hadanyKod)  
        {
            bool spravneCislo = false;

            while (!spravneCislo)
            {
                Console.Write($"Vas {PocetKol}. odhad je: ");
                string nacteno = Console.ReadLine();
                if (nacteno.Length == 5)        //zda ma odhad spravnou delku
                {
                    if (Int32.TryParse(nacteno, out _))     //zda je odhad cislo
                    {
                        spravneCislo = true;
                        for (int i = 0; i < nacteno.Length; ++i)  //kod musi obsahovat jen 5 mist
                        {
                            if ((int)char.GetNumericValue(nacteno[i]) > nejvyssiCislo)      //zda zadne cislo v kodu neni vyssi nez nejvyssi mozne cislo
                            {
                                spravneCislo = false;
                                Console.WriteLine($"Kombinace obsahovala cislo vyssi nez {nejvyssiCislo}, zkuste to znovu:{Environment.NewLine}"); //kod musi obsahovat jen cisla od 0 do zadaneho cisla
                                odhad = new int[5];
                                break;
                            }
                            else
                            {
                                odhad[i] = (int)char.GetNumericValue(nacteno[i]);
                            }
                        }
                    }
                    else  //uzivatel nezadal cislo
                    {
                        Console.WriteLine($"Kod muze obsahovat jen cisla, zkuste to znovu:{Environment.NewLine}");
                    }
                }
                else
                {
                    if (nacteno == "1")  //uzivatel potrebuje napovedu
                    {
                        if (napoveda.Count == 5)  //uz zna cely kod, vyuzil vsechnu napovedu, ukonci danou hru a nabidne uzivateli moznost zahrat si znovu
                        {
                            Console.WriteLine($"{Environment.NewLine}Dosla vam napoveda, uz znate cely kod");
                            KonecHry = true;
                            return hadanyKod;
                        }
                        else
                        {
                            Napoveda(hadanyKod);        //spusti dalsi kork napovedy
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Kombinace nemá spravnou delku{Environment.NewLine}");
                    }
                }
            }
            return odhad;
        }

        /// <summary>
        /// Snazi se napovedet uzivateli dalsi cislo, napoveda se vypise ve forme napr. 15---. 
        /// Cisla, ktera uzivatel stale nezna se nahradi pomlckou. Postupne ukazuje vzdy jedno dalsi cislo.
        /// </summary>
        /// <param name="hadanyKod"></param>
        private void Napoveda(int[] hadanyKod)  
        {
            Console.Write($"{Environment.NewLine}Napoveda: ");
            napoveda.Add(hadanyKod[napoveda.Count]);
            foreach (int cislo in napoveda)  //pridava dalsi cisla do napovedy
            {
                Console.Write(cislo);
            }
            for (int i = 5; i > napoveda.Count; --i)
            {
                Console.Write('-');         //cisla, ktera uzivateli jeste nebyla prozrazena nahradi pomlckou
            }
            Console.WriteLine(Environment.NewLine);
        }
    }
}
