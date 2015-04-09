using System;
using Golfers;

namespace GolfersCommandline
{
    public class Program
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

            var validator = new Validator();
            if(!validator.IsDataValid(data))
            {
                Console.WriteLine("Invalid input data - points are collinear.");
                return;
            }

            var solver = new Algorithm();
            var result = solver.Solve(data);

            DataIO.Write(output, result);

            var test = validator.IsSolutionValid(result);

            Console.WriteLine("\nIsSolutionValid result: {0}\n", test ? "success" : "failure");
        }
    }
}
