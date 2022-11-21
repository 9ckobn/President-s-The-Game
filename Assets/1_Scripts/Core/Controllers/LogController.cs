using UI;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "LogController", menuName = "Controllers/Core/LogController")]
    public class LogController : BaseController
    {
        private bool isNeedLog = false;

        public bool SetIsNeedLog
        {
            set 
            { 
                isNeedLog = value;

                if (value)
                {
                    UIManager.ShowWindow<LogWindow>();
                }
            }
        }

        public void Log(string message)
        {
            if (isNeedLog)
            {
                Debug.Log(message);

                UIManager.GetWindow<LogWindow>().Log(message);
            }
        }

        public void LogError(string error)
        {
            if (isNeedLog)
            {
                UIManager.GetWindow<LogWindow>().LogError(error);
            }
        }
    }
}