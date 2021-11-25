using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SpletneStrani
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> seznam = new List<string>
            {
                "http://vlado.fmf.uni-lj.si/vlado/vlado.htm",
                "https://www.kirupa.com/html5/loading_random_page_inline_pg1.htm",
                "https://moodle.fis.unm.si",
                "https://www.arnes.si",
                "https://www.rtvslo.si",
                "https://www.siol.net",
                "http://www.kosarka.si",
                "https://slo-tech.com/",
                "https://cnn.com/",
                "http://ddv.inetis.com/",
                "https://www.moodle.org/",
                "https://www.fmf.uni-lj.si/"

            };
            pajek paj = new pajek();

            paj.naloziPodatke(seznam);

            Console.WriteLine("Asasa");

        }


    }
}
