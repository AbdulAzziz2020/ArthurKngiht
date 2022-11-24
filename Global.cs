using UnityEngine;
using System.Collections.Generic;
using System.IO;

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
    public abstract class ObjectPool : MonoBehaviour
    {
        public static void Init<T>(T prefab, int size, List<T> list, Transform parent) where T : Component
        {
            for (int i = 0; i < size; i++)
            {
                T obj = Instantiate(prefab, parent);
                obj.gameObject.SetActive(false);
                list.Add(obj);
            }
        }

        public static T GetObjectInPool<T>(List<T> list) where T : Component
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (!list[i].gameObject.activeInHierarchy) return list[i];
            }

            return null;
        }

        public static void SetActiveGameObject<T>(List<T> list, Transform origin) where T : Component
        {
            T obj = GetObjectInPool(list);

            if (obj != null)
            {
                Debug.Log("Active!!");
                obj.transform.position = origin.transform.position;
                obj.gameObject.SetActive(true);
            }
        }
    }
    public abstract class Data
    {
        public static string Encrypt<T>(T data, string key)
         {
            string json = JsonUtility.ToJson(data);
            string result = "";
    
             for (int i = 0; i < json.Length; i++) result += (char)(json[i] ^ key[i % key.Length]);
                
            return result;
        }
    
         public static T Decrypt<T>(string fullPath, string key) where T : class
        {
            if (File.Exists(fullPath))
            {
                string jsonData = File.ReadAllText(fullPath);
                string result = "";
    
                for (int i = 0; i < jsonData.Length; i++)
                {
                    result += (char)(jsonData[i] ^ key[i % key.Length]);
                }
                    
                T data = JsonUtility.FromJson<T>(result);
    
                return data;
            }
            else
            {
                Debug.Log($"file = <color=red>Don't Exist!!</color>");
                return null;
            }
        }
    }     
}