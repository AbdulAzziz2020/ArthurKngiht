using System;
using UnityEngine;
using ArthurKnight;

public class SaveLoad : Singleton<SaveLoad>
{
    public override void Awake()
    {
        base.Awake();
    }

    private static string directory = "/datas/";
    private static string fileName = "/hero.miracle-gate";
    private static readonly string key = "5664313331";

    // public Hero hero = new Hero();
    public Player player;
    
    public void Update()
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

    public void Save()
    {
        string dir = Application.persistentDataPath + directory;
        if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

        PlayerCounter counter = new PlayerCounter(player.pos);
        
        string json = Data.Encrypt<PlayerCounter>(counter, key);
        File.WriteAllText(dir + fileName, json);
    }

    public void Load()
    {
        string fullPath = Application.persistentDataPath + directory + fileName;
        
        PlayerCounter loadPlayer = Data.Decrypt<PlayerCounter>(fullPath, key);

        if (loadPlayer != null)
        {
            player.transform.position = loadPlayer.pos;
        }
    }
}
