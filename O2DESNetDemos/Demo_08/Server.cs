﻿using O2DESNet;
using O2DESNet.Distributions;
using System;
using System.Collections.Generic;
using System.Text;

namespace O2DESNetDemos.Demo_08
{
    public class Server : Sandbox
    {
        #region Statics
        public int Capacity { get; private set; }
        public double HourlyServiceRate { get; private set; }
        #endregion

        #region Dynamics
        public int NumberInService { get; private set; } = 0;
        #endregion

        public Server(int capacity, double hourlyServiceRate, int seed = 0) : base(seed)
        {
            Capacity = capacity;
            HourlyServiceRate = hourlyServiceRate;
        }

        public void Start()
        {
            if (NumberInService >= Capacity) throw new Exception("Insufficient vacancy.");
            NumberInService++;
            Schedule(Finish, TimeSpan.FromHours(Exponential.Sample(DefaultRS, 1 / HourlyServiceRate)));
            Console.WriteLine($"{ClockTime}\t{GetType().Name}#{Index}\tStart. #In-Service: {NumberInService}");
            if (NumberInService == Capacity) Schedule(ChangeAccessibility);
        }

        void Finish()
        {   
            NumberInService--;
            Console.WriteLine($"{ClockTime}\t{GetType().Name}#{Index}\tFinish. #In-Service: {NumberInService}");
            if (NumberInService == Capacity - 1) Schedule(ChangeAccessibility);
            OnFinish.Invoke();
        }

        void ChangeAccessibility()
        {
            Console.WriteLine($"{ClockTime}\t{GetType().Name}#{Index}\tChangeAssessibility. #In-Service: {NumberInService}");
            OnChangeAccessibility.Invoke(NumberInService < Capacity);
        }

        public event Action<bool> OnChangeAccessibility = (accessible) => { };
        public event Action OnFinish = () => { };
    }
}
