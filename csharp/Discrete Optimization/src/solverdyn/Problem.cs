using System;
using System.Collections.Generic;

namespace solverdyn
{
    class Problem
    {
        public class Item
        {
            public int Value { get; private set; }
            public int Weight { get; private set; }

            public static Item Parse(string text)
            {
                return new Item
                    {
                        Value = int.Parse(text.Split(new[]{' '}, StringSplitOptions.RemoveEmptyEntries)[0].Trim()),
                        Weight = int.Parse(text.Split(new[]{' '}, StringSplitOptions.RemoveEmptyEntries)[1].Trim()),
                    };
            }
        }


        public int Capacity { get; private set; }
        public Item[] Items { get; private set; }

        public static Problem Parse(string[] text)
        {
            var p = new Problem();

            p.Capacity = int.Parse(text[0].Split(new[]{' '}, StringSplitOptions.RemoveEmptyEntries)[1].Trim());
            var n = int.Parse(text[0].Split(new[]{' '}, StringSplitOptions.RemoveEmptyEntries)[0].Trim());
            p.Items = new Item[n];
            var items = new List<Item>();
            for (var i = 0; i < n; i++)
                p.Items[i] = Item.Parse(text[i + 1]);
            return p;
        }
    }
}