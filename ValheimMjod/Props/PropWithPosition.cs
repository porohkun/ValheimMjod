using System;

namespace ValheimMjod
{
    public class PropWithPosition : Prop
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public PropWithPosition(string label, string template, Func<object> getValue, Action<object> setValue, int x, int y) : base(label, template, getValue, setValue)
        {
            X = x;
            Y = y;
        }

    }
}
