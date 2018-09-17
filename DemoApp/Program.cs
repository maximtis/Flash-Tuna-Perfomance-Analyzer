using FlashTuna.Attributes;
using System;
using System.Linq;

namespace DemoApp
{
    class Program
    {
        public Program()
        {
            var _perfomanceMethods = this.GetType().GetMethods().Where(x => x.GetCustomAttributes(typeof(PerfomanceMetricAttribute), true).Any());
            _perfomanceMethods.First().Name
        }
        static void Main(string[] args)
        {
            
        }
    }
}
