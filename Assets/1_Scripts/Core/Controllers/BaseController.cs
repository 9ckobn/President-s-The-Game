using UnityEngine;

namespace Core
{
    public class BaseController : ScriptableObject, IManager
    {
        public bool NeedInstantiate = false;

        public virtual void OnInitialize() { }

        public virtual void OnStart() { }
    }
}