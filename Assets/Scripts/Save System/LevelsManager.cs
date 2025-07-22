using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    private Level[] levels;

    private SaveSystem saveSystem;

    private void Start()
    {
        levels = new Level[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            levels[i] = transform.GetChild(i).GetComponent<Level>();
        }

        saveSystem = SaveSystem.instance;
        saveSystem.OnSaveRequested += Save;
        saveSystem.OnLoadCompleted += Load;
    }

    private void OnDisable()
    {
        saveSystem.OnSaveRequested -= Save;
        saveSystem.OnLoadCompleted -= Load;
    }

    public void Save()
    {
        LevelsData newLevelsData = new LevelsData();
        newLevelsData.numberLevel = new int[levels.Length];
        newLevelsData.completed = new int[levels.Length];
        for(int i = 0; i < levels.Length; i++ )
        {
            newLevelsData.numberLevel[i] = levels[i].numberOfLevl;
            newLevelsData.completed[i] = levels[i].completedTimes;
        }

        saveSystem.levelsData = newLevelsData;
    }
    public void Load()
    {
        LevelsData newLevelsData = saveSystem.levelsData;
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].numberOfLevl = newLevelsData.numberLevel[i] ;
            levels[i].completedTimes = newLevelsData.completed[i] ;
        }
    }
}
