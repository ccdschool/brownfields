using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace solverdyn
{
    class HybridSolver
    {
        public static Solution Solve(Problem problem)
        {
            File.WriteAllText("hybridsolver.log", "progress...\r\n");

            var objectiveValue = Calc_objective_values(problem);
            var selection = Decompose_objective_value(problem, objectiveValue);

            var sum = 0;
            for (var i = 0; i < selection.Length; i++)
                if (selection[i]) sum += problem.Items[i].Value;

            File.AppendAllText("hybridsolver.log", string.Format("B&B result: {0}, found: {1}\r\n", objectiveValue, sum));

            return new Solution
            {
                ObjectiveValue = objectiveValue,
                ItemSelections = selection,
                IsExact = sum == objectiveValue
            };
        }


        class SortedItem
        {
            public int Value;
            public int Weight;
            public int MaxTotal;

            public int Index;
        }


        private static int nodes_visited;
        private static int _errorMargin;

        private bool[] _best_result_so_far;


        // ergebnis ks_19_0: 4461 knoten besucht
        private static bool[] Decompose_objective_value(Problem problem, int objectiveValue)
        {
            const double ERROR_MARGIN_PCT = 0.00002;
            _errorMargin = (int)(objectiveValue * ERROR_MARGIN_PCT);

            var sorted_items = problem.Items.Select((item, i) => new SortedItem{Value=item.Value, Weight=item.Weight, Index=i})
                                            .OrderByDescending(item => item.Value).ToArray();
            for (var i = sorted_items.Length - 2; i >= 0; i--)
                sorted_items[i].MaxTotal = sorted_items[i + 1].Value + sorted_items[i + 1].MaxTotal;

            var selected_item_indexes = new Stack<int>();

            nodes_visited = 0;
            Decompose_by_branching_and_bounding(objectiveValue, problem.Capacity, sorted_items, -1, selected_item_indexes);
            File.AppendAllText("hybridsolver.log", string.Format("B&B: {0}, {1}\r\n", nodes_visited,  DateTime.Now));

            var selection = new bool[sorted_items.Length];
            selected_item_indexes.ToList().ForEach(i => selection[i]=true);

            return selection;
        }


        private static bool Decompose_by_branching_and_bounding(int objectiveValue, int capacity, SortedItem[] sortedItems, int i, Stack<int> selectedItemIndexes)
        {
            for (var k = i + 1; k < sortedItems.Length; k++)
            {
                nodes_visited++;
                if (nodes_visited % 10000 == 0) File.AppendAllText("hybridsolver.log", string.Format("B&B: {0}, {1}\r\n", nodes_visited, DateTime.Now));


                var rest_value = objectiveValue - sortedItems[k].Value;
                var rest_capacity = capacity - sortedItems[k].Weight;

                if (rest_value < 0) continue; // current val too large; try next one which will be smaller
                if (rest_capacity < 0) continue; // no more capacity left in knapsack
                if (rest_value > sortedItems[k].MaxTotal) return false; // the rest cannot possibly be covered by the remaining items

                selectedItemIndexes.Push(sortedItems[k].Index); // remember candidate value index

                if (rest_value <= _errorMargin && rest_capacity >= 0) return true; // goal reached!

                // continue with next value
                if (Decompose_by_branching_and_bounding(rest_value, rest_capacity, sortedItems, k, selectedItemIndexes))
                    return true;

                selectedItemIndexes.Pop(); // this candidate did not help
            }
            return false;
        }


        private static int Calc_objective_values(Problem problem)
        {
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

                File.AppendAllText("hybridsolver.log", string.Format("Calc: {0}, {1}\r\n", item_col, DateTime.Now));
            }

            return curr_col[problem.Capacity];
        }
    }
}

