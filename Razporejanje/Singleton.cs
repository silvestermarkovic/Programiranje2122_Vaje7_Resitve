using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Razporejanje
{

    public class Streznik
    {
        // Gets or sets server name
        public string Name { get; set; }
        // Gets or sets server IP address
        public string Ip { get; set; }
        public int StKlicev { get; set; }
        public double Obremenitev { get; set; }
        public double ObremenitevMax { get; set; }

        public bool VServisu { get; set; }
    }

    //ustvarite sealed class Razporeditelj
    public sealed class Razporeditelj

    {

        private static readonly Razporeditelj _instance = new Razporeditelj();
        private List<Streznik> _strezniki { get; set; }

        public bool sprozenAlarm = false;

        public bool cakamProstiStreznik = false;

        // Pazite: konstruktor je 'private'
        private Razporeditelj()
        {

            // Load list of available servers
            _strezniki = new List<Streznik>
                    {
                        new Streznik{ Name = "Server1", Ip = "10.10.10.1", StKlicev = 0, ObremenitevMax = 100, VServisu = false},
                        new Streznik{ Name = "Server2", Ip = "10.10.10.2", StKlicev = 0, ObremenitevMax = 100, VServisu = false},
                        new Streznik{ Name = "Server3", Ip = "10.10.10.3", StKlicev = 0, ObremenitevMax = 200, VServisu = false},
                };
        }

        //vrne _instance, ki ob incalizaciji kliče konstruktor, ki je private
        public static Razporeditelj VrniRazporeditelj()
        {
            return _instance;
        }

        public Streznik NaslednjiStreznik(int pteza)
        {
            while (true)
            {
                for (int i = 0; i < _strezniki.Count; i++)
                {

                    //na prejšnjih vajah smo uredili poizvedbo, ki vrne najmanj obremenjeni strežnik
                    var poizv5 = (from str in _strezniki
                                  where str.VServisu == false && str.ObremenitevMax >= (pteza + str.Obremenitev)
                                  orderby str.Obremenitev / str.ObremenitevMax
                                  select str).Take(1);

                    if (poizv5.Count() > 0)
                    {
                        cakamProstiStreznik = false;
                        return poizv5.First();
                    }

                }
                //če ni kapacitet počakamo 0.5s, če se sporstijo kapacitete
                Console.WriteLine("Vse kapacitete zasedene, čakamo!");
                cakamProstiStreznik = true;
                Thread.Sleep(500);
            }
        }

        //TODO 2.2
        //ustvarite public metodo izpisPorocilo, ki izpiše stanje obremenitve strežnika
        public void izpisPorocilo()
        {
            //Število opravljenih klicev:  XXX  (seštevek klicev
            //Trenutna skupna obremenitev strežnikov: seštevek obremenitev  / maxobremenitev
            //Obremenitev po strežnikih:
            //naziv strežnika, IP naslov, obremenitev/obremenitev max

            Console.WriteLine("Število opravljenih klicev: {0}", _strezniki.Sum(x => x.StKlicev));
            Console.WriteLine("Trenutna skupna obremenitev strežnikov: {0}/{0}", _strezniki.Sum(x => x.Obremenitev), _strezniki.Sum(x => x.ObremenitevMax));
            Console.WriteLine("Obremenitev po strežnikih:");
            _strezniki.ForEach(x => Console.WriteLine($"{x.Name}, {x.Ip}, {x.Obremenitev} / {x.ObremenitevMax}"));

        }
    }


}


