using System;
using System.Threading;
using System.Threading.Tasks;

namespace PrimeriKlicevTask
{
    class Program
    {

        static void Main(string[] args)
        {

            //primer klicev
            //direktni klic
            Console.WriteLine("Direktni klice");

            Task.Factory.StartNew(() => { Console.WriteLine("Test klica Taska: "); 
                Thread.Sleep(8000); 
                Console.WriteLine("Konec Test klica Taska: "); });

            Console.WriteLine("Direktni klice");
            //Uporaba Action
            Task task1 = new Task(new Action (IzpisiSporocilo1));
            task1.Start();


            //Uporaba Delagatov, s parametri
            Console.WriteLine("Delegate");
            Task task2 = new Task(delegate { IzpisiSporocilo("DelegateTask2",6); });
            task2.Start();

            //lambda s parametri
            Console.WriteLine("Lambda");
            //Lambda and named method
            Task task3 = new Task(() => IzpisiSporocilo("LambdaTask3",5));
            task3.Start();

            Console.WriteLine("Lambda async");
            Task task4 = new Task(() => { IzpisiSporocilo("Task4",4); });
            task4.Start();

            //Using Task.Run in .NET4.5!!!
            //Task.Run and Task.FromResult 
            Console.WriteLine("Run priporočljivo");
            var task5 = Task.Run(IzpisiSporocilo1);
            

            Console.WriteLine("Konec zagona, čakamo zaključke. Za nadaljevanje pritisni enter");
            Console.ReadLine();


            Console.WriteLine("NADALJEVANJE");


            Console.WriteLine("Primer: Čakamo prekinitev izvajanja");
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;

            Task.Run(() => IzpisiSporociloPrekinitev(token));

            //var taskprekini = Task.Run(IzpisiSporociloPrekinitev, token);
            Console.WriteLine("Ob pritisku enter se prekine izvajanje");

            Console.ReadLine();


            // #1 create a token source with timeout
            //tokenSource = newCancellationTokenSource(TimeSpan.FromSeconds(2));

            // #2 request cancellation after timeout
            tokenSource.CancelAfter(TimeSpan.FromSeconds(2));

            // #3 directly request cancellation
            //tokenSource.Cancel();
            // or
            //tokenSource.Cancel(true);


            Console.WriteLine("Konec izvajanja, za naslednji primer Enter");
            Console.ReadLine();



            // primer klica Async s parametri
            Random _random = new Random();
            int teza = _random.Next(5, 10);

            Console.WriteLine($"Primer klica Async metode, ki se bo izvajala {teza} sekund?");

            //zaženemo Task in nadaljujemo
            Task<int> naloga = ObremenitevAsync(teza);
            Console.WriteLine("Čakamo na konec metode");
            

            //čakamo, dokler se ne zaključi
            naloga.Wait();
            Console.WriteLine("Konec Async");
            Console.ReadLine();


            Console.WriteLine("Nalaganje strani!");
            sync raz = new sync();
            raz.test_url("https://moodle.fis.unm.si");
            raz.test_url("https://www.google.com");
            raz.test_url("https://www.rtvslo.si");
            raz.test_url("https://www.siol.net");


             
            Console.WriteLine("Konec Klicev");
            Console.ReadLine();
        }

        public static void IzpisiSporocilo(string napis, int cas)
        {
            Console.WriteLine(napis + "Začetek!");
            Thread.Sleep(cas * 1000 );
            Console.WriteLine(napis + "Konec!");
        }
        public static void IzpisiSporocilo1( )
        {
            Console.WriteLine("1" + "Začetek!");
            Thread.Sleep(2000);
            Console.WriteLine("1" + "Konec!");
        }


        public static void IzpisiSporociloPrekinitev(CancellationToken token)
        {
            Console.WriteLine("Začetek, čakamo prekinitev!");

            while (true)
            {

                Thread.Sleep(500);
                Console.WriteLine("Delam!");
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("Dočakali smo prekinitev!");
                    return;
                };
            }

        }


        //primer async

        public static async Task<int> ObremenitevAsync(double cas)
        {
            //povečamo StKlicev

            //podatki moramo v milisekundah zato * 1000            
            int sek = (int)cas * 1000;
            //await pove, da async počaka, da se izvrši ukaz, sicer bi nadaljevalo takoj
            await Task.Delay(sek);
            //odstranimo obremenitev

            return 0;
        }
    }
}
