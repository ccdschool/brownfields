using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solverdyn
{
    class Program
    {
        static void Main(string[] args)
        {
            var problemText = File.ReadAllLines(args[0]);
            var problem = Problem.Parse(problemText);

            var start = DateTime.Now;
            File.AppendAllText("solverdyn.log", string.Format("{0}, {1}, {2}", DateTime.Now, problem.Items.Length, problem.Capacity));


            var solution = DynamicFwdSolver.Solve(problem);

            Console.WriteLine("{0} {1}", solution.ObjectiveValue, solution.IsExact ? 1 : 0);
            Console.Write(String.Join(" ", solution.ItemSelections.Select(s => s ? "1" : "0")));


            //Console.WriteLine();
            //var sum = 0;
            //for (var i = 0; i < solution.ItemSelections.Length; i++)
            //{
            //    if (solution.ItemSelections[i])
            //    {
            //        Console.WriteLine(problem.Items[i].Value);
            //        sum += problem.Items[i].Value;
            //    }
            //}
            //Console.WriteLine("=== {0}", sum);

            //solution = DynamicSolver.Solve(problem);
            //Console.WriteLine("{0} 1", solution.ObjectiveValue);
            //Console.Write(String.Join(" ", solution.ItemSelections.Select(s => s ? "1" : "0")));

            File.AppendAllText("solverdyn.log", string.Format(": {0} {1}, {2}\r\n", solution.ObjectiveValue, solution.IsExact, DateTime.Now.Subtract(start)));
        }
    }
}
