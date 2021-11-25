using System;
using System.Timers;

namespace PrimerKlicevTimer
{
    class Program
    {

        static void Main(string[] args)
        {
            demo aa = new demo();
            aa.zazeniCasovnike();
            Console.WriteLine("Deluje...., za konec pritisnite Enter");

            Console.ReadLine();
        }
    }
     
}
