using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    [SerializeField] private bool isBarbarian = false;
    public List<GameObject> myAllies;                   //Лучники цієї будівлі
    public List<Transform> _pointsPosition;             //Точки позицій для цих лучників


    [SerializeField] private Transform _spawnPoint;     //Точка спавна лучників
     public GameObject _dedAlliesSpawnPoint = null;
    [SerializeField] private float _spawnTimerMax;
    private float _spawnTimer;

    [SerializeField]private BuildingPlan _buildingPlan;
    public float _quantityAllies;
    
    private AlliesManager _alliesManager;

    [HideInInspector]public byte num = 0;

    private void Start()
    {
        _alliesManager = AlliesManager.instance;
    }

    private void FixedUpdate()
    {
        if (myAllies.Count < _quantityAllies)
        {
            if (_spawnTimer >= _spawnTimerMax && myAllies.Count < _quantityAllies)
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
        if (num < _quantityAllies)
        {
            newPoint = _alliesManager.GetPoint(_pointsPosition[num].position, Quaternion.identity);
            num++;
        }
        else if (_dedAlliesSpawnPoint != null)
        {
            newPoint = _dedAlliesSpawnPoint;
            _dedAlliesSpawnPoint = null;
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
        Debug.Log(isBarbarian);
        myAllies.Add(newAllies);
        newAllies.GetComponent<Bot>().spawnScript = this;
        newAllies.GetComponent<Bot>()._targetPoint = newPoint;
        newAllies.GetComponent<Bot>().AddTarget();
    }
}
