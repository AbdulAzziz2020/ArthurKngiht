using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace EncryptSaveSystem
{
    [System.Serializable]
    public class Hero
    {
        public string name;
        public string nickName;
        public int level;
        public long defense;
        public float speed;
    }

    [System.Serializable]
    public class HeroList
    {
        public List<Hero> heroes = new List<Hero>();
    }
    
    public class SaveLoadSystem : MonoBehaviour
    {
        public static string directory = "/HeroesData/";
        public static string fileName = "Heroes.enc";
        public static string noEncrypt = "NoEncrypt.file";
        private static readonly string codeKey = "3543546254";
        
        public HeroList heroList = new HeroList();


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) Save();
            if (Input.GetKeyDown(KeyCode.Return)) Load();
        }

        public void Save()
        {
            string dir = Application.persistentDataPath + directory;
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

            string json = JsonUtility.ToJson(heroList);
            string encryptJson = EncryptDecrypt(json);
            
            File.WriteAllText(dir + noEncrypt, json);
            File.WriteAllText(dir + fileName, encryptJson);
            
            Debug.Log("Save!!");
        }

        public void Load()
        {
            string fullPath = Application.persistentDataPath + directory + fileName;

            if (File.Exists(fullPath))
            {
                string json = File.ReadAllText(fullPath);
                heroList = JsonUtility.FromJson<HeroList>(EncryptDecrypt(json));
                
                //Update in here!
            }
            else
            {
                Debug.Log("Data don't exist!!");
            }
        }
        
        string EncryptDecrypt(string data)
        {
            string result = "";

            for (int i = 0; i < data.Length; i++)
            {
                result += (char)(data[i] ^ codeKey[i % codeKey.Length]);
            }

            return result;
        }
    }
}