using System.Windows;

namespace Valheim
{
    public class ZLog
    {
        internal static void LogWarning(object v)
        {
            MessageBox.Show(v.ToString());
        }

        internal static void Log(object v)
        {

        }

        internal static void LogError(object v)
        {
            MessageBox.Show(v.ToString());
        }
    }
}
