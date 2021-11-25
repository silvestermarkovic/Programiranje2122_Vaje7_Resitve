using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
namespace PrimerKlicevTimer
{

    class demo
    {
        public Timer casovnik = new System.Timers.Timer(500);

        public Timer casovnik_izpis = new System.Timers.Timer(200);

        public int stevec = 0;
        public demo()
        {

            casovnik.AutoReset = false;

            casovnik.Elapsed += akcijaCasovnik;

            casovnik_izpis.AutoReset = false;
            casovnik_izpis.Elapsed += akcijaCasovnikIzpis;
        }
        public void zazeniCasovnike()
        {

            casovnik.AutoReset = true;
            casovnik_izpis.AutoReset = true;

            casovnik.Enabled = true;
            casovnik_izpis.Enabled = true;

        }

        public void akcijaCasovnik(Object source, ElapsedEventArgs e)
        {

            stevec += 1;
            if (stevec % 1000 == 0)
            {
                stevec = 0;
            }
        }
        public void akcijaCasovnikIzpis(Object source, ElapsedEventArgs e)
        {

            Console.WriteLine($"{stevec}");

        }
    }
}