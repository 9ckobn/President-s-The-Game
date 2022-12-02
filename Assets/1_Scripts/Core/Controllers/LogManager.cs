using UI;
using UnityEngine;

namespace Core
{
    public class LogManager : Singleton<LogManager>
    {
        private LogWindow logWindow;
        private bool isNeedLog = false;

        protected override void AfterAwaik()
        {
            logWindow = GetComponentInChildren<LogWindow>();
        }

        public static bool SetIsNeedLog
        {
            set
            {
                instance.isNeedLog = value;
            }
        }

        public static void Log(string message)
        {
            if (instance.isNeedLog)
            {
                Debug.Log(message);

                instance.logWindow.Log(message);
            }
        }

        public static void LogError(string error)
        {
            if (instance.isNeedLog)
            {
                Debug.Log($"<color=red>ERROR {error}</color>");

                instance.logWindow.LogError(error);
            }
        }
    }
}