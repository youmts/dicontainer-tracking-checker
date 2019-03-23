using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using TrackingCheck.Model;
using TrackingCheck.Checker;

namespace TrackingCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"{nameof(AutofacChecker)}");
            var checker = new AutofacChecker();
            Console.WriteLine(
                $"{nameof(NormalModel)} " +
                $"finalized:{checker.Run<NormalModel>()}");
            Console.WriteLine(
                $"{nameof(DisposableModel)} " +
                $"finalized:{checker.Run<DisposableModel>()}");
        }
    }
}
