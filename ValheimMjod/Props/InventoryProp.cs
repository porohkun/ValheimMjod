using System;
using System.Collections.Generic;

namespace ValheimMjod
{
    public class InventoryProp : Prop<Dictionary<string, Prop>>
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public string Durab
        {
            get => Value["durability"].GetValue<int>().ToString();
            set => Value["durability"].SetValue(int.TryParse(value, out var r) ? r : Value["durability"].GetValue<int>());
        }
        public string Count
        {
            get => Value["stack"].GetValue<int>().ToString();
            set => Value["stack"].SetValue(int.TryParse(value, out var r) ? r : Value["stack"].GetValue<int>());
        }

        public InventoryProp(string label, string template, Func<Dictionary<string, Prop>> getValue, Action<Dictionary<string, Prop>> setValue, int x, int y) : base(label, template, getValue, setValue)
        {
            X = x;
            Y = y;
        }

        protected override Dictionary<string, Prop> InvokeWithExtensions(Func<Dictionary<string, Prop>> getValue)
        {
            var dictionary = getValue.Invoke();

            foreach (var prop in dictionary.Values)
                prop.PropertyChanged += Prop_PropertyChanged;

            return dictionary;
        }

        private void Prop_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(nameof(Value));
            RaisePropertyChanged(nameof(Durab));
        }
    }
}
