using System;
using System.Collections.Generic;
using System.Threading; 

namespace ApplePicker
{
  class Program
    {  
        // static LotteryPeriod p = new LotteryPeriod();
        static Orchard orchard = new Orchard();
        public static int mainThreads = 4; //VENDOR Thread Count
        public static int juiceThread = 3;
        public static int loadCapacity = 1000;
        public static int applesToPick = 100000;


        static void Main(string[] args)
        {

            //Stopwatch to measure how long N threads take to pick __ apples
            System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
            timer.Start();

            List<Thread> Workers = new List<Thread>();

            for (int i = 0; i < 4; i++)
            {
                Thread t = new Thread(new ThreadStart(tWorker));
                Workers.Add(t);
            }

            foreach (Thread t in Workers)
            {
                t.Start();
            }

            foreach (Thread t in Workers)
            {
                t.Join();
            }

            //Stopwatch stop
            timer.Stop();
            Console.WriteLine("Elapsed time for {0} threads is: {1}", Workers, timer.Elapsed);
            Console.WriteLine("There were " + orchard.applesPicked.Count + " apples picked ");

            //Phase 3 - process into apple juice
            Thread juicer1 = new Thread(Juicers);
            juicer1.Start();
            Thread juicer2 = new Thread(Juicers);
            juicer2.Start();
            Thread juicer3 = new Thread(Juicers);
            juicer3.Start();
            juicer1.Join(); juicer2.Join(); juicer3.Join();

            Console.WriteLine("There were " + orchard.applesJuiced.Count + " apples juiced with " + (orchard.applesJuiced.Count / 42) + " gallons of juice made from the apples");
        }

   static void tWorker(){
            newWorker w = new newWorker();

            w.pickApples(orchard);
            Console.WriteLine($"Thread finished with " + orchard.applesPicked.Count + " apples picked");
        }

        static void Juicers(){
            appleJuicer j = new appleJuicer();
            j.juiceApples(orchard);
            Console.WriteLine("Thread finished juicing");
        }
    }
}