using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T instance;

    protected void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = gameObject.GetComponent<T>();
        }

        AfterAwaik();
    }

    protected virtual void AfterAwaik() { }

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError(typeof(T) + "  не найден!!!");
            }

            return instance;
        }
    }
}
