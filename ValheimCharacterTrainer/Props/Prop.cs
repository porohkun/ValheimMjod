using System;

namespace ValheimCharacterTrainer
{
    public class Prop
    {
        public string Label { get; }
        public string Template { get; }
        public object Value
        {
            get => _getValue?.Invoke();
            set => _setValue?.Invoke(value);
        }
        private Func<object> _getValue;
        private Action<object> _setValue;

        public Prop(string label, string template, Func<object> getValue, Action<object> setValue)
        {
            Label = label;
            Template = template;
            _getValue = getValue;
            _setValue = setValue;
        }
    }
}
