using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;


namespace Promet
{
    class Semafor
    {
        //koda, kot pomoč, ni nujna uporaba
        public Timer timRdeca = new System.Timers.Timer(500);
        public Timer timZelena = new System.Timers.Timer(1200);
        public Timer timOranzna = new System.Timers.Timer(1200);


        //uporabili bom timer iz System.Timers
        //using System.Timers

        //deklaracija timerjeve
        //public Timer timZ= new System.Timers.Timer(9999);
        public int stevec = 0;

        //deklaracija propery
        public bool zelena = false;
        public bool rdeca = false;
        public bool oranzna = false;
        public bool zadnjazelena = false;

        public Semafor()
        {

            //postavitev začetnega stanja
            //dodajanje eventov na timerje
            timRdeca.AutoReset = false;
            timZelena.AutoReset = false;
            timOranzna.AutoReset = false;


            timRdeca.Elapsed += goriRdeca;
            timZelena.Elapsed += goriZelena;
            timOranzna.Elapsed += goriOranzna;


            timZelena.Start();
            //timZ.Elapsed += goriZelena;
            //timZ.Start();
        }



        public void goriRdeca(Object source, ElapsedEventArgs e)
        {
            zelena = false;
            zadnjazelena = false;
            oranzna = false;
            rdeca = true;
            stevec += 1;
            izpis();

            timOranzna.Start();

        }
        public void goriOranzna(Object source, ElapsedEventArgs e)
        {
            zelena = false;
            oranzna = true;
            rdeca = false;
            stevec += 1;
            izpis();
            if (zadnjazelena)
            {

                timRdeca.Start();

            }
            else
            {

                timZelena.Start();
            }
        }

        public void goriZelena(Object source, ElapsedEventArgs e)
        {
            zelena = true;
            zadnjazelena = true;
            oranzna = false;
            rdeca = false;
            stevec += 1;
            izpis();
            timOranzna.Start();

        }
        public void izpis()
        {
            string stanje = "";
            stanje += (zelena ? "Z" : "z");
            stanje += (oranzna ? "O" : "o");
            stanje += (rdeca ? "R" : "r");
            Console.WriteLine($"{stanje}    {stevec}");
        }
    }

}
