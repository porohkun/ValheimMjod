using System.Diagnostics;

namespace ValheimMjod.Commands
{
    public class HyperlinkCommand : InjectableCommand<HyperlinkCommand>
    {
        protected override bool CanExecuteInternal(object parameter)
        {
            return true;
        }

        protected override void ExecuteInternal(object parameter)
        {
            if (parameter is string url)
                Process.Start(new ProcessStartInfo(url));
        }
    }
}
