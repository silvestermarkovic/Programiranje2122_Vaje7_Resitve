using System;

namespace Promet
{
    class Program
    {
        static void Main(string[] args)
        {
            //SEMAFOR
            //Začnite s poljubno barvo
            //Uredite razred semafor tako, da bosta
            //Zelena in Rdeča svetili 2 sekundi, Oranžna 0,5sekunde
            //nalogo rešite s pomočjo timerjev
            //stanje semaforja, naj se izpiše ob spremembi
            //izpiše naj se v 1 vrstici: Zor (luč, katera je prižgana, naj bo izpisana z veliko)

            //klic konstruktorja razreda
            //razred ustvarite poljubno
            Semafor mojsemafor = new Semafor();

            Console.ReadKey();

        }
    }
}
