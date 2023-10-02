using System;
using System.Collections.Generic;

namespace zapoctovyProgram02
{
    /// <summary>
    /// Trida Hrac je rodic pro hadajiciho a hodnoticiho hrace.
    /// </summary>
    internal class Hrac      
    {
        /// <summary>
        /// Urcuje zda je hrac automat nebo uzivatel.
        /// </summary>
        protected bool automat;

        /// <summary>
        /// Konstruktor hrace.
        /// </summary>
        /// <param name="automat">Zda je hrac automat nebo uzivatel.</param>
        internal Hrac(bool automat)   
        {
            this.automat = automat;
        }

        internal bool Automat
        {
            get 
            { 
                return automat; 
            }
        }

        /// <summary>
        /// Vraci hodnotu promenne automat.
        /// </summary>
        /// <returns></returns>

        /// <summary>
        /// Vygeneruje nahodny kod.
        /// </summary>
        /// <param name="nejvyssiCislo"> Nejvyssi cislo, ktere muze kod obsahovat.</param>
        /// <returns>Vraci vygenerovany kod.</returns>
        internal int[] VygenerujKod(int nejvyssiCislo)  
        {
            int[] kod = new int[5];
            Random r = new Random();
            for (byte i = 0; i < 5; ++i)
            {
                kod[i] = r.Next(nejvyssiCislo + 1);     
            }
            return kod;
        }

        /// <summary>
        /// Vyhodnoti a vypise reakci na dany odhad vuci kodu.
        /// </summary>
        /// <param name="odhad"></param>
        /// <param name="kod"></param>
        /// <returns>Vraci vyhodnoceni odhadu.</returns>
        protected static int[] VyhodnotPocitacem(List<int> odhad, int[] kod)
        {
            int[] kopieKodu = new int[5];
            kod.CopyTo(kopieKodu, 0);
            int[] reakce = new int[2];

            for (byte i = 0; i < 5; ++i)  //pripocitava cerne body - spravne cislo na spravne pozici
            {
                if (kopieKodu[i] == odhad[i])
                {
                    ++reakce[0];
                    kopieKodu[i] = odhad[i] = -1;
                }
            }

            odhad.RemoveAll(prvek => prvek == -1);

            for (byte i = 0; i < 5; ++i)  //pripocitava bile body - spatne cislo na spravne pozici
            {
                if (odhad.IndexOf(kopieKodu[i]) != -1)
                {
                    odhad.Remove(kopieKodu[i]);
                    kopieKodu[i] = -1;
                    ++reakce[1];
                }
            }
            return reakce;  //vraci pole obsahujici pocet cernych a pocet bilych bodu
        }
    }
}
   