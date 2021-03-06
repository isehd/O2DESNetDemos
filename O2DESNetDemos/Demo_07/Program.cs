﻿using System;

namespace O2DESNetDemos.Demo_07
{
    class Program
    {
        public static void Run()
        {
            var sim = new TandemQueue_Pull(new TandemQueue_Pull.Statics 
            { 
                Capacity = 1,
                HourlyArrivalRate = 4,
                HourlyServiceRate = 5,
            }, seed: 0);
            while (sim.Run(10)) Console.ReadKey();
        }
    }
}
