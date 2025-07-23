using DebugBehaviour.Runtime;
using UnityEngine;

public class Singleton<T> : VerboseMonoBehaviour where T : MonoBehaviour
{
    public static bool keepAlive = true;

    private static T instance = null;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindFirstObjectByType<T>();
                if (instance == null)
                {
                    var singletonObj = new GameObject
                    {
                        name = typeof(T).ToString()
                    };
                    instance = singletonObj.AddComponent<T>();
                }
            }
            return instance;
        }
    }

    static public bool IsInstanceAlive
    {
        get { return instance != null; }
    }

    public virtual void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = GetComponent<T>();

            if (keepAlive)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }

}