using System;
using System.Collections.Generic;
using System.Linq;

namespace ArtikliLokacije
{
    class Program
    {
        static void Main(string[] args)
        {




            //TODO 30
            //ustvarite seznam artiklov in v seznam dodajte 5 artiklov, vsak naj ima 2 do 3 lokacije
            //pri ustvarjanju si pomagajte s konstruktorjem 
            List<Artikel> seznam = new List<Artikel>();


            seznam.Add(new Artikel(1, new List<Lokacija> {
                new Lokacija { naziv = "Lok1", kolicina = 5 },
                new Lokacija { naziv = "Lok2", kolicina = 8 }
                }));
            seznam.Add(new Artikel(2, new List<Lokacija> {
                new Lokacija { naziv = "Lok8", kolicina = 2 },
                new Lokacija { naziv = "Lok5", kolicina = 3 }
                }));
            seznam.Add(new Artikel(3, new List<Lokacija> {
                new Lokacija { naziv = "Lok5", kolicina = 5 },
                new Lokacija { naziv = "Lok2", kolicina = 8 },
                }));
            seznam.Add(new Artikel(4, new List<Lokacija> {
                new Lokacija { naziv = "Lok1", kolicina = 5 },
                new Lokacija { naziv = "Lok2", kolicina = 8 }
                }));

            seznam.Add(new Artikel(5, new List<Lokacija> {
                new Lokacija { naziv = "Lok1", kolicina = 5 },
                new Lokacija { naziv = "Lok21", kolicina = 8 },
                  new Lokacija { naziv = "Lok1", kolicina = 5 },
                new Lokacija { naziv = "Lok8", kolicina = 8 }
                }));


            //TODO 40
            //ustvarite seznam artiklov Artiklov, ki imajo zalogo večjo od 20 in ga izpišite
            var result = from sez in seznam
                         where sez.vrniZalogo() > 20
                         select sez;

            foreach (var element in result)
            {
                Console.WriteLine(element.id);
            }


            //TODO 50
            Console.WriteLine("Primer2");
            //Izpišite vse lokacije, kjer se nahaja kakšen artikel in količina (brez ID artikla)
            var result1 = seznam.SelectMany(w => w.lokacije);
            foreach (var elt in result1)
            {
                Console.WriteLine($"Lokacija: {elt.naziv} Količina {elt.kolicina}");
            }

            //TODO 60
            Console.WriteLine("Primer2 A");
            //Izpišite vse lokacije, kjer se nahaja kakšen artikel in količina (brez ID artikla)
            var result2a = seznam.SelectMany(art => art.lokacije, (Artikel, Lokacija) => new { Artikel, Lokacija });
            foreach (var item in result2a)
            {
                Console.WriteLine($"Artikel: {item.Artikel.id} Lokacija: {item.Lokacija.naziv } Količina {item.Lokacija.kolicina }");
            }

        }
    }
}
