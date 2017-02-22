using System;

namespace SyslabControlAlgorithmFrameworkWpfGui.Middleware
{
    public class MiddlewareTypeHelper
    {
        public static MiddlewareType GetFromstring(string name)
        {
            MiddlewareType choice = MiddlewareType.YAMI4;
            foreach (MiddlewareType type in Enum.GetValues(typeof(MiddlewareType)))
            {
                if (name != null && name.ToLower().Contains(Enum.GetName(typeof(MiddlewareType), type).ToLower()))
                    choice = type;
            }
            return choice;
        }

        public static string GetName() => "Yami4";
        public static int GetDefaultPortNumber() => 20900;
        public static bool SupportsPubSub() => true;
        public static bool SupportsPrioritization() => true;
    }
}
