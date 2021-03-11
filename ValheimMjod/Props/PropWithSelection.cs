using System;
using System.Collections.Generic;
using System.Linq;

namespace ValheimMjod
{
    public class PropWithSelection : Prop
    {
        public Tuple<string, object>[] Variants { get; }

        public Tuple<string, object> SelectedVariant
        {
            get => Variants.First(v => v.Item2.Equals(Value));
            set => Value = value.Item2;
        }

        public PropWithSelection(string label, string template, Func<object> getValue, Action<object> setValue, IEnumerable<(string, object)> variants) : base(label, template, getValue, setValue)
        {
            Variants = variants.Select(v => v.ToTuple()).ToArray();
        }

        public PropWithSelection(string label, string template, Func<object> getValue, Action<object> setValue, params (string, object)[] variants)
            : this(label, template, getValue, setValue, (IEnumerable<(string, object)>)variants) { }
    }
}
