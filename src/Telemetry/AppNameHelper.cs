using System.Reflection;

namespace Telemetry;

public class AppNameHelper
{
    public static string GetExecutingAssembly()
    {


        Assembly entryAssembly = Assembly.GetEntryAssembly();

        if (entryAssembly != null)
        {
            // Get the name of the entry assembly
            string executableName = entryAssembly.GetName().Name;
            return executableName;
        }

        return "Undeterminable";
    }
}