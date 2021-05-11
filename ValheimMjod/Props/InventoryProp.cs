using System;
using System.Collections.Generic;

namespace ValheimMjod
{
    public class InventoryProp : Prop<Dictionary<string, Prop>>
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public InventoryProp(string label, string template, Func<Dictionary<string, Prop>> getValue, Action<Dictionary<string, Prop>> setValue, int x, int y) : base(label, template, getValue, setValue)
        {
            X = x;
            Y = y;
        }

    }
}
