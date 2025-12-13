using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    [Header("Allies")]
    [SerializeField] private bool isBarbarian = false;
    public List<GameObject> myAllies;                   //Лучники цієї будівлі
    public float _sizeAllies;

    [Header("Position")]            
    [SerializeField] private Transform _spawnPoint;     //Точка спавна лучників
    public List <GameObject> _dedAlliesSpawnPoint = null;   //Точки позицій мертвих союзників

    [Header("Timer")]
    [SerializeField] private float _spawnTimerMax;
    [SerializeField] private float _spawnTimer;

    
    [Header("References")]
    [SerializeField] private AlliesInBuilding _alliesInBuilding;
    private AlliesManager _alliesManager;

    [HideInInspector]public byte num = 0;

    private void Start()
    {
        _alliesManager = AlliesManager.instance;
        _alliesInBuilding.NextLevel();
    }

    private void FixedUpdate()
    {
        if (myAllies.Count < _alliesInBuilding._points)
        {
            if (_spawnTimer >= _spawnTimerMax)
            {
                SpawnAllies();
                _spawnTimer = 0;
            }
            else
            {
                _spawnTimer++;
            }
        }
    }

    private void SpawnAllies()
    {
        GameObject newPoint = null;
        if (_dedAlliesSpawnPoint.Count > 0)
        {
            newPoint = _dedAlliesSpawnPoint[0];
            _dedAlliesSpawnPoint.Remove(newPoint);
        }
        else
        {
            newPoint = _alliesManager.GetPoint(_alliesInBuilding._pointsActive[num].transform.position, Quaternion.identity);
            num++;
        }

        GameObject newAllies;

        if (isBarbarian == true)
        {
             newAllies = _alliesManager.GetBarbarian(_spawnPoint.position, Quaternion.identity);
        }
        else
        {
             newAllies = _alliesManager.GetArchers(_spawnPoint.position, Quaternion.identity);
        }
        myAllies.Add(newAllies);
        newAllies.GetComponent<Bot>().spawnScript = this;
        newAllies.GetComponent<Bot>()._targetPoint = newPoint;
        newAllies.GetComponent<Bot>().AddTarget();
    }
}
