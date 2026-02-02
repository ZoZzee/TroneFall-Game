using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadingFromSaved : MonoBehaviour
{
    private SaveSystem _saveSystem;
    [SerializeField] private List<ChooseLevel> _chooseLevel;

    private string _fileName = "LevelData";
    private void Start()
    {
        _saveSystem = SaveSystem.instance;
        string fullPath = Application.persistentDataPath + $"/{_fileName}.json";

        if (File.Exists(fullPath))
        {
            _saveSystem.LoadAll(_fileName);
            for(int i = 0; i < _chooseLevel.Count; i++)
                _chooseLevel[i].RefreshStars();
        }
        else
        {
            _saveSystem.SaveAll(_fileName);
        }

    }
    private void OnEnable()
    {
        
    }
}
