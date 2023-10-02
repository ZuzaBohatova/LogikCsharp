using System;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("ZapoctovyProgramTests")]

namespace zapoctovyProgram02
{
    internal class Program
    {
        /// <summary>
        /// Vypisou se pravidla hry a spusti se nova hra, po konci hry se nabidne jeji restart,
        /// pokud chce hrat uzivatel znovu stiskne 0, a hra se restartuje, po stisknuti jakekoliv jine klavesy hra skonci.
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            Console.WriteLine("Vitejte u hry LOGIC");
            Console.WriteLine($"{Environment.NewLine}Pravidla hry: {Environment.NewLine} " +  
                $"hrac 1 vybere petimistny kod skladajici se z cislic 0 az maximalne 9 a hrac 2 se ho snazi uhodnout,{Environment.NewLine} " +
                $"hrac 1 odhady hodnoti cernymi a bilymi body, {Environment.NewLine} cerny bod = spravne cislo na spravnem miste {Environment.NewLine} " +
                $"bily bod = odhad obsahuje spravne cislo, ale je chybne umisteno {Environment.NewLine}" +
                $"Hra ma dva mody: {Environment.NewLine} " +
                $"MOD1 = program vybere kod a vy ho hadate {Environment.NewLine} " +
                "MOD2 = vyberete si kod a program ho uhadne");

            string novaHra = "0";
            while (novaHra == "0")  
            {
                Logik logic = new Logik();
                if(!logic.Inicializace(args))       //pokud inicializace argumentu neprobehne v poradku, hra skonci a vypise chybovou hlasku
                {
                    break;
                }
                logic.ZacniHru();           //spusti se nova hra
                Console.WriteLine("Pokud si chcete zahrat znovu stisknete cislo 0, jinou klavesou hru ukoncite.");
                novaHra = Console.ReadLine();
            }
        }
    }
}
