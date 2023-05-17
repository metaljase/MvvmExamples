using System.Diagnostics;

namespace Metalhead.Examples.Mvvm.WpfSGDI
{
    public class DebugLogger : ILogger
    {
        public void Log(string message)
        {
            Debug.WriteLine(message);
        }
    }
}
