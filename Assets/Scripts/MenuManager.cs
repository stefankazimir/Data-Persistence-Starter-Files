using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    
    public string Name;
    public string BestName;
    public int BestScore;
    public Text Best;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        LoadYouData();

        Best.text = $"Best Score : {BestName} : {BestScore.ToString()}"; 
    }

    [System.Serializable]

    class SaveData
    {
        public string BestName;
        public int BestScore;
    }

    public void SaveYouData()
    {
        SaveData data = new SaveData();
        data.BestName = BestName;
        data.BestScore = BestScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadYouData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            BestName = data.BestName;
            BestScore = data.BestScore;
        }
    }
}
