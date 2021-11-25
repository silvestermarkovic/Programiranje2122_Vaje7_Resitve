using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtikliLokacije
{

    //TODO 10
    //ustvarite razred Lokacija
    //  ima lastnost naziv in kolicina


    //TODO 20
    //ustvarite razred Artikel z lastnostima:
    //      public id int
    //      public lokacije tipa   List<Lokacija> 
    //ustvarite konstuktor Artikel(int id, List<Lokacija> lokacije)
    //ustvarite public metodo vrniZalogo, ki vrne zalogo na lokacijah

    class Artikel
    {

        public int id;
        public List<Lokacija> lokacije = new List<Lokacija>();

        public Artikel(int id, List<Lokacija> lokacije)
        {
            this.id = id;
            this.lokacije = lokacije;
        }

        public double vrniZalogo()
        {
            return lokacije.Sum(s => s.kolicina);

        }

    }
    class Lokacija
    {
        public string naziv { get; set; }
        public double kolicina { get; set; }

    }


}
