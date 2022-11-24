using UnityEngine;
using System.IO;

namespace ArthurKnight
{
    public class Data
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
                var jsonData = File.ReadAllText(fullPath);
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
