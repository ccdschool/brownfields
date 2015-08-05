using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace solverdyn
{
    class DynamicFwdSolver
    {
        private static DifferencesStore _differencesStore;


        public static Solution Solve(Problem problem)
        {
            using (_differencesStore = new DifferencesStore())
            {
                File.WriteAllText("dynamicfwdsolver.log", "progress...\r\n");
    
                var objectiveValue = Calc_objective_values(problem);
                var selection = Trace_item_selection(problem);

                var sum = 0;
                for (var i = 0; i < selection.Length; i++)
                    if (selection[i]) sum += problem.Items[i].Value;

                File.AppendAllText("dynamicfwdsolver.log",
                                   string.Format("Trace result: {0}, found: {1}\r\n", objectiveValue, sum));

                return new Solution
                    {
                        ObjectiveValue = objectiveValue,
                        ItemSelections = selection,
                        IsExact = sum == objectiveValue
                    };
            }
        }



        private static bool[] Trace_item_selection(Problem problem)
        {
            var selected_items = new bool[problem.Items.Length];

            var remaining_capacity = problem.Capacity;
            for (var i = problem.Items.Length - 1; i >= 0; i--)
            {
                var differences = _differencesStore.Load_differences(i);
                if (differences.Length > 0)
                {
                    if (differences.Contains(remaining_capacity))
                    {
                        selected_items[i] = true;
                        remaining_capacity -= problem.Items[i].Weight;
                    }
                }
            }

            return selected_items;
        }



        private static int Calc_objective_values(Problem problem)
        {
            File.Delete("dynamicfwdsolver_diffs.log");

            var total_diff_count = 0;

            var start = DateTime.Now;

            var curr_col = new int[problem.Capacity+1];
            for (var item_col = 1; item_col <= problem.Items.Length; item_col++)
            {
                var prev_col = curr_col;
                curr_col = new int[problem.Capacity+1];

                for (var capacity_of_row = 1; capacity_of_row <= problem.Capacity; capacity_of_row++)
                {
                    var i = item_col - 1;

                    if (problem.Items[i].Weight <= capacity_of_row)
                    {
                        var prevObj = prev_col[capacity_of_row];
                        var currObj = problem.Items[i].Value +
                                      prev_col[capacity_of_row - problem.Items[item_col - 1].Weight];
                        curr_col[capacity_of_row] = Math.Max(prevObj, currObj);
                    }
                    else
                        curr_col[capacity_of_row] = prev_col[capacity_of_row];
                }

                File.AppendAllText("dynamicfwdsolver.log", string.Format("Calc {0}, {1}\r\n", item_col, DateTime.Now.Subtract(start)));

                var differences = new List<int>();
                for (var i = 0; i < curr_col.Length; i++)
                    if (prev_col[i] != curr_col[i]) differences.Add(i);
                File.AppendAllText("dynamicfwdsolver_diffs.log", string.Format("item {0}: {1}\r\n", item_col - 1, differences.Count));

                total_diff_count += differences.Count;

                _differencesStore.Store_differences(item_col-1, differences.ToArray());
            }

            File.AppendAllText("dynamicfwdsolver_diffs.log", string.Format("total: {0}, avg: {1}\r\n", total_diff_count, total_diff_count / problem.Items.Length));

            return curr_col[problem.Capacity];
        }
    }
}
