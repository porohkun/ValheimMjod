using System;

namespace ValheimMjod
{
    public abstract class Prop : BindableBase
    {
        public string Label { get; }
        public string Template { get; }

        public abstract object ObjectValue { get; set; }

        protected Prop(string label, string template)
        {
            Label = label;
            Template = template;
        }

        public abstract T GetValue<T>();
        public abstract void SetValue<T>(T value);
    }

    public class Prop<T> : Prop
    {
        public T Value
        {
            get => InvokeWithExtensions(_getValue);
            set
            {
                _setValue?.Invoke(value);
                RaisePropertyChanged();
            }
        }

        public override object ObjectValue
        {
            get => GetValue<T>();
            set => SetValue<T>((T)value);
        }

        private Func<T> _getValue;
        private Action<T> _setValue;

        public DelegateCommand<T> SetValueCommand { get; }

        public Prop(string label, string template, Func<T> getValue, Action<T> setValue = null) : base(label, template)
        {
            _getValue = getValue;
            _setValue = setValue;

            SetValueCommand = new DelegateCommand<T>(SetValue);
        }

        protected virtual T InvokeWithExtensions(Func<T> getValue)
        {
            return getValue.Invoke();
        }

        public override T2 GetValue<T2>()
        {
            if (typeof(T2) != typeof(T))
                throw new InvalidCastException();
            return (T2)(object)Value;
        }

        public override void SetValue<T2>(T2 value)
        {
            if (typeof(T2) != typeof(T))
                throw new InvalidCastException();
            Value = (T)(object)value;
        }

        protected void SetValue(T value)
        {
            Value = value;
        }
    }
}
