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

    public void SaveAll(string fileName)
    {
        OnSaveRequested?.Invoke();

        Save(fileName, levelsData);

        Debug.Log("Збережено");
    }
    public void LoadAll(string fileName)
    {
        levelsData = Load<LevelsData>(fileName);
        OnLoadCompleted?.Invoke();

        Debug.Log("Завантажено");
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
    //________________________________________________________________________________

    public void LevelComplete(int numberOfLevel ,string fileName)
    {
        levelsData = Load<LevelsData>(fileName);
        for(int i = 0; i< levelsData.levelsNumber.Length; i++)
        {
            if(numberOfLevel == levelsData.levelsNumber[i])
            {
                levelsData.completedTimes[i]++;
                Debug.Log("Level = "+levelsData.levelsNumber[i] + "   CompletedTimes = " + levelsData.completedTimes[i]);
                Save(fileName, levelsData);
            }
        }
    }
}

[Serializable]
public class LevelsData
{
    public int [] levelsNumber;
    public int [] completedTimes;
}
