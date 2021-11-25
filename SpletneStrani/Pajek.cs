using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SpletneStrani
{

    class Strani
    {
        public string url = "";
        public string vsebina = "";
        public List<Povezave> seznam_povezav = new List<Povezave>();

        public Strani(string purl, string pvsebina)
        {
            //konstruktor
            url = purl;
            vsebina = pvsebina;
        }
    }
    class Povezave
    {
        public string url;
        public string vsebina;
        public Povezave(string purl)
        {
            //konstruktor
            url = purl;
            vsebina = "";
        }

        public void dolociVsebino(string vsebina)
        {
            this.vsebina = vsebina;
        }
    }
    class pajek
    {
        //napolni vhodne podatke
        public List<Strani> seznamStrani = new List<Strani>();

        
        public void naloziPodatke(List<string> seznam)
        {
            //spraznimo/kličemo konsturktor, da vedno naložimo podatke znova 
            seznamStrani = new List<Strani>();
            
            //3A
            //uporabite lahko metode (pomagajte si z metodami, ki so že pripravljene
            //za vsako stran naložite vsebino HTML v lastnost vsebina v seznamStrani 

            Stopwatch sw = Stopwatch.StartNew();
            sw = Stopwatch.StartNew();
            foreach (string p in seznam)
            {
                seznamStrani.Add(new Strani(p, vrniVsebino(p)));
                //string result = await vrniStranAsync("https://moodle.fis.unm.si");
            }
            Console.WriteLine("Čas izvedbe zaporedni klici: " + sw.Elapsed.TotalSeconds   + " za nadaljevcanje Enter");

            Console.ReadKey();
              sw = Stopwatch.StartNew();
            // AsyncTestSeveral(keyword, seznamStrani, seznam);

            //z kreiranim seznamom, zaporedno, paralelno obdelaj sezname, tako da najdeš spletne povezave na strani
            //in jih shraiš v zapis Strani, Povezave
            sw = Stopwatch.StartNew();

            //.WithDegreeOfParallelism(i)  lahko določimo s koliko jedri delamo; max število jeder najdemo v Environment.ProcessorCount

            seznam.AsParallel().ForAll(i => 
                                        {
                                            Strani temp = new Strani(i, vrniVsebino(i));
                                            obdelajSeznam(temp); //iščemo povezave
                                            seznamStrani.Add(temp); 
                                        } 
                                        );
            Console.WriteLine("Čas izvedbe as Parallels: " + sw.Elapsed.TotalSeconds + " za nadaljevcanje Enter");

            Console.ReadKey();

            //drugi način
            sw = Stopwatch.StartNew();
            Parallel.ForEach(seznam, i =>
            {
                Strani temp = new Strani(i, vrniVsebino(i));
                obdelajSeznam(temp);  //iščemo povezave
                seznamStrani.Add(temp);
            });
            Console.WriteLine("Čas izvedbe as 2Parallels: " + sw.Elapsed.TotalSeconds + " za nadaljevcanje Enter");

            Console.ReadKey();

            sw = Stopwatch.StartNew();
            foreach (string p in seznam)
            {
                seznamStrani.Add(new Strani(p, vrniVsebino(p)));
               
            }
            Console.WriteLine("Čas izvedbe zaporedni klici: " + sw.Elapsed.TotalSeconds + " za nadaljevcanje Enter");


            Console.ReadKey();
             
            seznamStrani.ForEach(i => i.seznam_povezav.ForEach(j => Console.WriteLine($"{j.url} ")));
            Console.WriteLine("Seznam Povezav Enter");


            Console.ReadKey();
            sw = Stopwatch.StartNew();
            seznamStrani.Take(5).AsParallel().ForAll(stan => stan.seznam_povezav.AsParallel().ForAll(pov => pov.dolociVsebino(vrniVsebino(pov.url))));
            Console.WriteLine("Podstrani: " + sw.Elapsed.TotalSeconds + " za nadaljevcanje Enter");

            seznamStrani.ForEach(i => i.seznam_povezav.ForEach(j => Console.WriteLine($"{j.url} + {j.vsebina}  ")));

        }
       
        public void obdelajSeznam(Strani pstran)
        {

            //koda
            //z uporabo paralelnih klicev, poiščite HTML povezave v dokumentih
            //izpišite jih v konzolo
            //ustvarite razred  Povezave, in jih dodajte seznamStrani.seznam_povezav (kreirajte metodo za dodajanje)
            //iščite <a href=" 

            pstran.seznam_povezav = vrniSeznamMedStringi("<a href=\"", "\"", pstran.vsebina, pstran.url );

            
        }
        public static List<Povezave> vrniSeznamMedStringi(string zacetniDel,string koncniDel, string vsebina, string osnovna_stran)
        {
            List<Povezave> templist = new List<Povezave>();

             
            int iIndexOfBegin = vsebina.IndexOf(zacetniDel);
            while (iIndexOfBegin != -1 )
            {

                int iIndexOfEnding = vsebina.IndexOf(koncniDel, iIndexOfBegin + zacetniDel.Length);
                int iEnd = iIndexOfEnding - iIndexOfBegin - zacetniDel.Length;

                string tempurl = vsebina.Substring(iIndexOfBegin + zacetniDel.Length, (iEnd > 200 ? 200 : iEnd));

                iIndexOfBegin = vsebina.IndexOf(zacetniDel, iIndexOfEnding);
                if (tempurl.IndexOf("#") != -1)
                {
                    continue;
                } 
                if (tempurl.Length == 0)
                {
                    continue;
                }

                if (tempurl.First() == "/".First())
                {
                    templist.Add(new Povezave(osnovna_stran + tempurl));
                }
                else
                {
                    templist.Add(new Povezave(tempurl));
                }
           
            }
   
            return templist;
        }

        //Rešitev sošolca Primoža z Uporabo HtmlAgilityPacka
        public static void NajdiLinke(Strani s, string url)
        {
            HtmlWeb hw = new HtmlWeb();
            HtmlDocument doc = hw.Load(url);
            foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
            {
                string hrefValue = link.GetAttributeValue("href", string.Empty);
                s.seznam_povezav.Add(new Povezave(hrefValue));
            }
        }

        public async Task<string> vrniStranAsync(string p_url)
        {
            HttpClient _httpClient = new HttpClient();
            string url = p_url;

            // The actual Get method
            using (var result = await _httpClient.GetAsync($"{url}"))
            {
                string content = await result.Content.ReadAsStringAsync();
                return content;
            }
        }
        //NALOGA 2C uporabi kodo iz prejšnje strani
        public static string vrniVsebino(string url)
        {

            try
            {
                WebRequest request = WebRequest.Create(url);
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                
               
                string vsebina = reader.ReadToEnd();
                Console.WriteLine("Prebrano: " + url );
                return (vsebina );

            } catch
            {
                return "Napaka";
            }

        }
    }
}
