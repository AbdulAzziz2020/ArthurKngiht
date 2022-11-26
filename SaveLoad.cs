using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace EncryptSaveSystem
{
    [System.Serializable]
    public class Yggdrasil
    {
        public List<GodAndGodness> _gods = new List<GodAndGodness>();
        public List<Location> _locations = new List<Location>();
        public List<RaceAndGroup> _raceAndGroups = new List<RaceAndGroup>();
        public List<Another> _anothers = new List<Another>();
    }

    [System.Serializable]
    public class GodAndGodness
    {
        public string name;
    }

    [System.Serializable]
    public class Location
    {
        public string place;
    }

    [System.Serializable]
    public class RaceAndGroup
    {
        public string race;
    }

    [System.Serializable]
    public class Another
    {
        public string another;
    }
    public class SaveLoad : MonoBehaviour
    {
        public static string directory = "/SAVE_FOLDER/";
        public static string fileName = "/Char.miracles";
        private static readonly string key = "231563467";

        public Yggdrasil yggdrasil = new Yggdrasil();

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Save();
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                Load();
            }
        }

        public string EncryptDecrypt(string data)
        {
            string result = "";

            for (int i = 0; i < data.Length; i++)
            {
                result += (char)(data[i] ^ key[i % key.Length]);
            }

            return result;
        }
        
        public void Save()
        {
            string path = Application.persistentDataPath + directory;
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            string json = JsonUtility.ToJson(yggdrasil);
            string fullPath = EncryptDecrypt(json);
            
            File.WriteAllText(path + fileName, fullPath);
            
            Debug.Log("Save!!");
        }

        public void Load()
        {
            string fullPath = Application.persistentDataPath + directory + fileName;

            if (File.Exists(fullPath))
            {
                string path = File.ReadAllText(fullPath);
                string json = EncryptDecrypt(path);

                yggdrasil = JsonUtility.FromJson<Yggdrasil>(json);
                Debug.Log("Load!!");
            }
            else
            {
                Debug.Log("File don't exist!!");
            }
        }
    }
}