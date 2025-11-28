using System.IO;
using Unity.AI.Navigation.Samples;
using UnityEngine;

public class LoadingFromSaved : MonoBehaviour
{
    private SaveSystem _saveSystem;

    private string _fileName = "Levels";
    private void Start()
    {
        _saveSystem = SaveSystem.instance;
        string fullPath = Application.persistentDataPath + $"/{_fileName}.json";

        if (File.Exists(fullPath))
        {
            _saveSystem.LoadAll(_fileName);
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
