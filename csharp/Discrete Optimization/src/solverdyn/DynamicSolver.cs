using System;
using System.IO;

namespace solverdyn
{
    internal class DynamicSolver
    {
        public static Solution Solve(Problem problem)
        {
            File.WriteAllText("dynamicsolver.log", "progress...\r\n");

            using (var table = new VirtualTable(problem.Items.Length + 1, problem.Capacity + 1))
            {

                Calc_objective_values(problem, table);
                var selection = Trace_selected_items(problem, table);

                return new Solution
                    {
                        ObjectiveValue = table[problem.Items.Length, problem.Capacity],
                        ItemSelections = selection
                    };
            }
        }


        private static void Calc_objective_values(Problem problem, VirtualTable table)
        {
            var start = DateTime.Now;

            for (var item_col = 1; item_col <= problem.Items.Length; item_col++)
            {
                for (var capacity_of_row = 1; capacity_of_row <= problem.Capacity; capacity_of_row++)
                {
                    var i = item_col - 1;

                    if (problem.Items[i].Weight <= capacity_of_row)
                    {
                        var prevObj = table[item_col - 1, capacity_of_row];
                        var currObj = problem.Items[i].Value +
                                      table[item_col - 1, capacity_of_row - problem.Items[item_col - 1].Weight];
                        table[item_col, capacity_of_row] = Math.Max(prevObj, currObj);
                    }
                    else
                        table[item_col, capacity_of_row] = table[item_col - 1, capacity_of_row];
                }

                File.AppendAllText("dynamicsolver.log", string.Format("Calc: {0}, {1}\r\n", item_col, DateTime.Now));
            }
        }


        private static bool[] Trace_selected_items(Problem problem, VirtualTable table)
        {
            var selections = new bool[problem.Items.Length];

            var item_col = problem.Items.Length;
            var capacity_of_row = problem.Capacity;
            while (item_col > 0 && capacity_of_row > 0)
            {
                var i = item_col - 1;

                var currObj = table[item_col, capacity_of_row];
                var prevObj = table[item_col - 1, capacity_of_row];

                if (currObj != prevObj)
                {
                    selections[i] = true;
                    capacity_of_row -= problem.Items[i].Weight;
                }

                File.AppendAllText("dynamicsolver.log", string.Format("Trace: {0}\r\n", item_col));
                item_col--;
            }

            return selections;
        }
    }
}