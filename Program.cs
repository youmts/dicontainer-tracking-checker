using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                new AbiocChecker(),
                new DryIocNoTrackChecker(),
                new DryIocTrackChecker(),
                new GraceChecker(),
            };
            var types = new Type[] {
                typeof(Normal),
                typeof(Disposable),
            };

            var results = DoCheck(checkers, types);

            OutputResult(checkers, types, results);
        }

        static bool[][] DoCheck(CheckerBase[] checkers, Type[] types)
        {
            return
                checkers.Select(
                    c => types.Select(t => c.Run(t)).ToArray()
                ).ToArray();
        }

        // Output in Markdown format
        static void OutputResult(
            CheckerBase[] checkers, Type[] types, bool[][] results)
        {
            // Output Containers infomations
            Console.WriteLine("Target DI Containers:");
            Console.WriteLine(string.Join("\n",
                checkers.Select(x => "* " + x.GetAssemblyVersionString())
            ));

            Console.WriteLine();

            var rowHeaders = checkers.Select(x => x.ContainerName).ToArray();
            var colHeaders = types.Select(x => x.Name).ToArray();
            var rowHeaderMaxLength = rowHeaders.Max(x => x.Length);
            var rowHeaderFormat = "|{0, -" + rowHeaderMaxLength + "}|";

            // Output column headers
            Console.Write(rowHeaderFormat, string.Empty);
            Console.Write(string.Join("|", colHeaders));
            Console.WriteLine("|");

            // Output markdown table separator
            Console.Write(rowHeaderFormat, ":--".PadRight(rowHeaderMaxLength, '-'));
            Console.Write(string.Join("|", colHeaders.Select(x => ":--".PadRight(x.Length, '-'))));
            Console.WriteLine("|");

            // Output cells
            for(int i = 0; i < results.Length; i++)
            {
                Console.Write(string.Format(
                    rowHeaderFormat, rowHeaders[i]));
                Console.Write(string.Join("|", 
                    colHeaders.Select((x, j) => 
                        string.Format(
                            "{0, " + colHeaders[j].Length + "}", 
                            results[i][j] ? "T" : "F")
                    )
                ));

                Console.WriteLine("|");
            }
        }
    }
}
