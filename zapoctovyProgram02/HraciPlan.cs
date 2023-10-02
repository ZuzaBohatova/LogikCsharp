using System;

namespace zapoctovyProgram02
{
    internal class HraciPlan
    {
        /// <summary>
        /// Pole vsech moznych variaci.
        /// </summary>
        internal int[,] moznosti;

        /// <summary>
        /// Kolik z moznosti muze byt hledanym kodem podle dosavadnich ohodnoceni.
        /// </summary>
        internal int zbyvaMoznosti;

        /// <summary>
        ///  Konstruktor hraciho planu nastavi pocet moznosti (zbyvaMoznosti) podle zadaneho nejvyssiho cisla 
        ///  a sestavi pole vsech moznosti metodou NastavMoznosti().
        /// </summary>
        /// <param name="nejvyssiCislo">nejvyssi cislo, ktere muze kod obshaovat</param>
        internal HraciPlan(int nejvyssiCislo)
        { 
            this.zbyvaMoznosti = (int)Math.Pow(Convert.ToDouble(nejvyssiCislo + 1), 5.00);
            this.moznosti = new int[5, zbyvaMoznosti];
            NastavMoznosti(nejvyssiCislo + 1);
        }

        /// <summary>
        /// Nastavi dvou rozmerne pole, ktere obsahuje vsechny mozne kombinace cisel.
        /// </summary>
        private void NastavMoznosti(int nejvyssiCislo)   
        {
            for (int i = 0, pozice = 0; i < nejvyssiCislo; i++) //5 for cyklu, jelikoz kod obsahuje 5 mist
            {
                for (int j = 0; j < nejvyssiCislo; j++)
                { 
                    for (int k = 0; k < nejvyssiCislo; k++)
                    {
                        for (int l = 0; l < nejvyssiCislo; l++)
                        {
                            for (int m = 0; m < nejvyssiCislo; m++)
                            {
                                moznosti[0, pozice] = i;
                                moznosti[1, pozice] = j;
                                moznosti[2, pozice] = k;
                                moznosti[3, pozice] = l;
                                moznosti[4, pozice] = m;
                                pozice++;
                            }
                        }
                    }
                }
            }
        }
    }
}
