using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Razporejanje
{
    class Program
    {
        //TODO 21 deklarirajte spremenljivko z imenom casovnik, tipa System.Timers.Timer, ki se bo izvedla vsakih 3s
        //private, static
        private static System.Timers.Timer casovnik = new System.Timers.Timer(3000);

        static void Main(string[] args)
        {
            Random _random = new Random();

            // Razporejevalniku bomo ustvarili naloge, z različnim časom in obremenitvijo
            var razporediteljbremena = Razporeditelj.VrniRazporeditelj();



            //TODO 23
            //klicite metodo dolociCasovnik, ki kot parameter dobi casovnik ustvarjen v koraku 2.1
            //koda 1 vrstica
            dolociCasovnik(casovnik);  //kot parameter podamo časovnik, ki smo ga deklarirari na vrhu, deklrariramo ga znotraj Classa,
                                       //da je dostopen vsem metodam znotraj tega classa
                                       //ker smo v statični metodi, morajo biti metode, ki jih kličemo tudi statične!



            for (int i = 0; i < 100; i++)
            {
                int teza = _random.Next(10, 30);
                //zaženemo Task in nadaljujemo
                Task<int> naloga = ObremenitevAsync(_random.Next(2, 6), razporediteljbremena.NaslednjiStreznik(teza), teza);
            }



            // Počakamo pred koncem
            Console.ReadKey();
        }



        //VAJE 7
        private static void dolociCasovnik(System.Timers.Timer pcasovnik)
        {

            //TODO 24
            // Časovniku določite interval 2s (kličite konstuktor  System.Timers.Timer) 
            //določite interval na 2s
            pcasovnik.Interval = 2000;

            // na event Elapsed, dodajte metodo casovnikDogodek 
            pcasovnik.Elapsed += casovnikDogodek;  //!!!!!! tu se vse zgodi!: določimo metodo, ki bo klicana, ko se sproži naš Timer!!!
            //časovnik naj deluje ves čas, naj se izvaja nepreklicno
            pcasovnik.AutoReset = true;  //to velja za  System.Timers.Timer, za Sytem.Threading.Timer, že v konstuktorju povemo, kako bo deloval, 
                                         //imamo 2 parametra, kdaj se zažene prvič in kdaj se ponovi (infiniti), pomeni, da se naslednjič nikoli ne zažene



            //TODO
            //vkjucite/zaženite pcasovnik

            pcasovnik.Start();
            //koda 1 vrstica

        }
        //VAJE7
        private static void casovnikDogodek(Object source, ElapsedEventArgs e)
        {
            //TODO INFO čas klica 
            //izpišite, kdaj je bil časonvnik sprožen, izpišite v Consoli:
            //"Dogodek je bil sprožen 17:12:12.123"
            
            //Console.WriteLine("Dogodek je bil sprožen {0:HH:mm:ss.fff}", e.SignalTime);

            //TODO 2.5
            //uporabite razred singleton in izpišite trenutno stanje strežnikov
            //deklarijate spremenljivko tipa Razporeditelj s klicem Razporeditelj.VrniRazporeditelj
            var raz = Razporeditelj.VrniRazporeditelj();
            //kličite izpis stanja, ki ste ga kreirali v točki 2.2
            raz.izpisPorocilo();

        }



        //VAJE predhodne UPORABITE V NALOGI
        //metoda nam bo obremenila strezik za dolocen cas
        //metoda ja async (več je predvideno v nadaljevanju)
        public static async Task<int> ObremenitevAsync(double cas, Streznik pstreznik, int pteza)
        {
            //povečamo StKlicev
            pstreznik.StKlicev += 1;
            //dodamo obremenitev
            pstreznik.Obremenitev += pteza;

            //zapisemo si trenutno stevilo klicev (ker sicer bi pstreznik vrnil trenutno)
            int temp = pstreznik.StKlicev;

            // Console.WriteLine("Zacetek (" + temp + "/" + pstreznik.StKlicev + "): " + pstreznik.Name + " Cas: " + cas + "s  Zahtevnost:" + pteza + " " + pstreznik.Obremenitev + "/" + pstreznik.ObremenitevMax);

            //podatki moramo v milisekundah zato * 1000            
            int sek = (int)cas * 1000;
            //await pove, da async počaka, da se izvrši ukaz, sicer bi nadaljevalo takoj
            await Task.Delay(sek);
            //odstranimo obremenitev
            pstreznik.Obremenitev -= pteza;
            // Console.WriteLine("Konec (" + temp + "/" + pstreznik.StKlicev + "): " + pstreznik.Name + " Cas: " + cas + "s  Zahtevnost:" + pteza + " " + pstreznik.Obremenitev + "/" + pstreznik.ObremenitevMax);

            return 0;
        }
        public static void Obremenitev(double cas, Streznik pstreznik, int pteza)
        {
            //povečamo StKlicev
            pstreznik.StKlicev += 1;
            //dodamo obremenitev
            pstreznik.Obremenitev += pteza;

            //zapisemo si trenutno stevilo klicev (ker sicer bi pstreznik vrnil trenutno)
            int temp = pstreznik.StKlicev;

            Console.WriteLine("Zacetek (" + temp + "/" + pstreznik.StKlicev + "): " + pstreznik.Name + " Cas: " + cas + "s  Zahtevnost:" + pteza + " " + pstreznik.Obremenitev + "/" + pstreznik.ObremenitevMax);

            //podatki moramo v milisekundah zato * 1000            
            int sek = (int)cas * 1000;
            //await pove, da async počaka, da se izvrši ukaz, sicer bi nadaljevalo takoj
            Thread.Sleep(sek);
            //odstranimo obremenitev
            pstreznik.Obremenitev -= pteza;
            Console.WriteLine("Konec (" + temp + "/" + pstreznik.StKlicev + "): " + pstreznik.Name + " Cas: " + cas + "s  Zahtevnost:" + pteza + " " + pstreznik.Obremenitev + "/" + pstreznik.ObremenitevMax);

        }
    }
}
