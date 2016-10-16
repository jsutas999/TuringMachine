using System;

namespace Turingas
{
    class Program
    {
        static void Main(string[] args)
        {

            int machines = Int32.Parse(Console.ReadLine());


            Turing[] turings = new Turing[machines];

            for (int i = 0; i < turings.Length; i++) turings[i] = new Turing();


            Console.WriteLine("Failo pavadinmas");
            string name = Console.ReadLine();


            foreach (Turing turing in turings) {

                if (!turing.ReadFromFile(name + ".txt"))
                {
                    Console.WriteLine("Nepavyko nuskaityti failo");
                    return;
                }

            turing.Start();
        }

            int c = 1;
            while(c != 0)
            {
                c = 0;
                foreach(Turing turing in turings )
                {
                    if (!turing.isRunning) return;
                    c++;
                    turing.Step();
                }
            }

            Console.ReadKey();

        }
    }
}
