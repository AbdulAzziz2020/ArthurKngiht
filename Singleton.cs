using UnityEngine;

namespace ArthurKnight
{
    public abstract class Singleton<T> : MonoBehaviour where T : Component
    {
        public static T Instance { get; private set; }

        public virtual void Awake()
        {
            if (Instance != null && Instance != this) Destroy(this.gameObject);
            else Instance = this as T;
        }
    }

    public abstract class SingletonPersitant<T> : MonoBehaviour where T : Component
    {
        public static T Instance { get; private set; }

        public virtual void Awake()
        {
            if (Instance != null && Instance != this) Destroy(this.gameObject);
            else
            {
                Instance = this as T;
                DontDestroyOnLoad(this.gameObject);
            }
        }
    }
}