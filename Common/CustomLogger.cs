using Godot;

namespace Common
{
    public enum ELogType
    {
        Info,
        Warning,
        Error,
    }

    public static class CustomLogger
    {
        public static void Log(ELogType logType, string message)
        {
            switch (logType)
            {
                case ELogType.Info:
                    GD.Print(message);
                    break;
                case ELogType.Warning:
                    GD.PushWarning(message);
                    break;
                case ELogType.Error:
                    GD.PushError(message);
                    break;
            }
        }

        public static void Log(string message)
        {
            GD.Print(message);
        }

        public static void LogWarning(string message)
        {
            GD.PushWarning(message);
        }

        public static void LogError(string message)
        {
            GD.PushError(message);
        }
    }
}