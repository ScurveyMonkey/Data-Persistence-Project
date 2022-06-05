using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class PlayerDataManager : MonoBehaviour
{
    public string playerName;
    public string bestPlayer;
    public int highScore;
    public static PlayerDataManager instance;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        LoadName();
        Debug.Log(bestPlayer);
    }

    public void GetPlayerName(string name)
    {
        playerName = name;
        Debug.Log(playerName);
    }

    public void SetBestPlayer()
    {
        if (MainManager.instance.m_Points > highScore)
        {
            highScore = MainManager.instance.m_Points;
            SaveName();
            Debug.Log(playerName + bestPlayer);
        }


    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    [System.Serializable]
    class SaveData
    {
        public string playerName;
        public string bestPlayer;
        public int highScore;
    }
    

    public void SaveName()
    {
        SaveData data = new SaveData();
        data.playerName = playerName;
        data.highScore = highScore;
        data.bestPlayer = bestPlayer;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadName()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            playerName = data.playerName;
            highScore = data.highScore;
            bestPlayer = data.bestPlayer;
        }
    }

}
