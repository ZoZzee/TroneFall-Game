using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    [SerializeField] private Level[] level;

    private SaveSystem saveSystem;

    private void OnEnable()
    {
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
        LevelsData levels = new LevelsData();

        levels.levelsNumber = new int[level.Length];
        levels.completedTimes = new int[level.Length];

        for (int i = 0; i < level.Length; i++)
        {
            Debug.Log(level[i]);
            levels.levelsNumber[i] = level[i].levelsNumber;
            levels.completedTimes[i] = level[i].completedTimes;
        }
        saveSystem.levelsData = levels;
    }
    public void Load()
    {
        for (int i = 0; i < level.Length ; i++)
        {
            level[i].completedTimes = saveSystem.levelsData.completedTimes[i];
            level[i].levelsNumber = saveSystem.levelsData.levelsNumber[i];
        }

        Debug.Log(saveSystem.levelsData.completedTimes[0]);
    }
}
