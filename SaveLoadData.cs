using System;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadData : MonoBehaviour
{
    private static string directory = "/SaveData/";
    private static string fileName = "SaveGame.sav";
    private static readonly string keyWord = "198329183";

    public string heroName;
    public string description;
    public int level;

    public Text nameText, descText, levelText;

    public void SaveData()
    {
        string dir = Application.persistentDataPath + directory;
        if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

        Hero newHero = new Hero(heroName, description, level);
        
        string json = JsonUtility.ToJson(newHero);
        var encryptData = EncryptDecrypt(json);
        
        File.WriteAllText(dir + fileName, encryptData);
        Debug.Log("Save!");
        
        UpdateHUD();
    }

    public void LoadData()
    {
        string fullPath = Application.persistentDataPath + directory + fileName;
        Debug.Log(fullPath);
        
        if (File.Exists(fullPath))
        {
            var jsonData = File.ReadAllText(fullPath);
            Hero data = JsonUtility.FromJson<Hero>(EncryptDecrypt(jsonData));

            heroName = data.name;
            description = data.description;
            level = data.level;
            
            UpdateHUD();
        }
        else
        {
            Debug.Log("Data don't exist!");
        }
    }

    static string EncryptDecrypt(string data)
    {
        string result = "";

        for (int i = 0; i < data.Length; i++) result += (char)(data[i] ^ keyWord[i % keyWord.Length]);
        
        return result;
    }


    void UpdateHUD()
    {
        nameText.text = heroName;
        descText.text = description;
        levelText.text = "Level " + level.ToString();
    }
}

public class Hero
{
    public string name;
    public string description;
    public int level;

    public Hero(string name, string description, int level)
    {
        this.name = name;
        this.description = description;
        this.level = level;
    }
}

