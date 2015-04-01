using System;

namespace AZ
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("\nUsage: input_file output_file\n");
                return;
            }

            var input = args[0];
            var output = args[1];

            var data = DataIO.Read(input);

            var result = Algorithm.Solve(data);

            DataIO.Write(output, result);

            var test = Tester.Test(result);

            Console.WriteLine("\nTest result: {0}\n", test ? "success" : "failure");
        }
    }
}
