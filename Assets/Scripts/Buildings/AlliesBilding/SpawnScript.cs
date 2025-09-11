using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    public GameObject allies;
    public List<GameObject> myAllies;                   //Лучники цієї будівлі
    public List<Transform> _pointsPosition;             //Точки позицій для цих лучників


    [SerializeField] private Transform _spawnPoint;     //Точка спавна лучників
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
        if (_spawnTimer >= _spawnTimerMax && myAllies.Count < _quantityAllies)
        {
            _spawnTimer = 0;
            GameObject newPoint = _alliesManager.GetPoint(_pointsPosition[num].position, Quaternion.identity);
            num++;
            GameObject newAllies = _alliesManager.GetArchers(_spawnPoint.position,Quaternion.identity);
            myAllies.Add(newAllies);
            newAllies.GetComponent<Bot>().spawnScript = this;
            newAllies.GetComponent<Bot>()._targetPoint = newPoint;
            newAllies.GetComponent<Bot>().AddTarget();
        }
        else
        {
            _spawnTimer++;
        }
    }
}
