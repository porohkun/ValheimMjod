using System;
using System.Collections.Generic;
using System.Linq;

namespace ValheimMjod
{
    public class PropWithSelection<T> : Prop<T>
    {
        public Tuple<string, T>[] Variants { get; }

        public Tuple<string, T> SelectedVariant
        {
            get => Variants.First(v => v.Item2.Equals(Value));
            set => Value = value.Item2;
        }

        public PropWithSelection(string label, string template, Func<T> getValue, Action<T> setValue, IEnumerable<(string, T)> variants) : base(label, template, getValue, setValue)
        {
            Variants = variants.Select(v => v.ToTuple()).ToArray();
        }

        public PropWithSelection(string label, string template, Func<T> getValue, Action<T> setValue, params (string, T)[] variants)
            : this(label, template, getValue, setValue, (IEnumerable<(string, T)>)variants) { }
    }
}
