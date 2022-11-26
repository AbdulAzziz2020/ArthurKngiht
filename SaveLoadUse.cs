using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArthurKnight;
using System.IO;

public class Skills : MonoBehaviour
{
    public string heroName;
    public int heroLevel;
    public float heroSpeed;
    public Vector3 pos;

    private const string directory = "/Data/";
    private const string filename = "Heroes.miracle-gate";
    private static readonly string key = ";lk12~.q23";

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) SaveData();
        if (Input.GetKeyDown(KeyCode.Return)) LoadData();
    }

    public void SaveData()
    {
        string dir = Application.persistentDataPath + directory;
        if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

        Hero counter = new Hero();
        counter.name = heroName;
        counter.level = heroLevel;
        counter.speed = heroSpeed;
        counter.pos = pos;
        
        string json = Data.Encrypt(counter, key);
        File.WriteAllText(dir + filename, json);

        Debug.Log("Save!!");
    }

    public void LoadData()
    {
        string fullPath = Application.persistentDataPath + directory + filename;

        Hero loadPlayer = Data.Decrypt<Hero>(fullPath, key);

        if (loadPlayer != null)
        {
            heroName = loadPlayer.name;
            heroLevel = loadPlayer.level;
            heroSpeed = loadPlayer.speed;
            pos = loadPlayer.pos;
        }
    }
}

[System.Serializable]
public class Hero
{
    public string name;
    public int level;
    public float speed;
    public Vector3 pos;
}
