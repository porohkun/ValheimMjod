using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ValheimCharacterTrainer
{
    public class PropTemplateSelector : DataTemplateSelector
    {
        public Dictionary<string, DataTemplate> Templates { get; set; } = new Dictionary<string, DataTemplate>();

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is Prop viewModel)
                if (Templates.TryGetValue(viewModel.Template, out var template))
                    return template;
                else throw new Exception($"For prop '{viewModel.Label}' not found template '{viewModel.Template}'.");
            else throw new Exception($"Cant cast '{item.GetType()}' into '{typeof(Prop)}'.");
        }
    }
}
