using System;
using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem instance;


    public event Action OnSaveRequested;

    public event Action OnLoadCompleted;

    public LevelsData levelsData;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F5))
        {
            SaveAll();
            Debug.Log("Збережено");
            Debug.Log(Application.persistentDataPath);
        }
        if (Input.GetKeyUp(KeyCode.F6))
        {
            LoadAll();

            Debug.Log("Завантажено");
        }
    }

    public void SaveAll()
    {
        OnSaveRequested?.Invoke();

        Save("LevelsData", levelsData);
    }
    public void LoadAll()
    {
        levelsData = Load<LevelsData>("LevelsData");
        OnLoadCompleted?.Invoke();
    }

    public void Save<T>( string fileName, T data)
    {
        string fullPath = Application.persistentDataPath + $"/{fileName}.json";
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(fullPath, json);
    }
    public T Load<T>(string fileName)
    {
        string fullPath = Application.persistentDataPath + $"/{fileName}.json";

        if (File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath); 
            return JsonUtility.FromJson<T>(json);
        }
        return default;
    }
}

[Serializable]
public class LevelsData
{
    public int[] numberLevel;
    public int[] completed;
}
