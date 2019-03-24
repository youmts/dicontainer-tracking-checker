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
            var checkers = new CheckerBase[] {
                new AutofacChecker(),
                new LightInjectChecker(),
            };
            var types = new Type[] {
                typeof(NormalModel),
                typeof(DisposableModel),
            };

            foreach (var checker in checkers)
            {
                Console.WriteLine($"{checker.GetType().Name}");

                foreach (var type in types)
                {
                    Console.WriteLine(
                        $"{type.Name} " +
                        $"finalized:{checker.Run(type)}");
                }
            }
        }
    }
}
