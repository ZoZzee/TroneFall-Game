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
            Debug.Log("Спавн точки і лучника");
            GameObject newPoint = _alliesManager.GetPoint(_pointsPosition[num].position, Quaternion.identity);
            num++;
            Debug.Log("Zaspavleno new target" + num);
            GameObject newAllies = _alliesManager.GetArchers(_spawnPoint.position,Quaternion.identity);
            newAllies.GetComponent<Bot>().spawnScript = this;

            //newAllies.GetComponent<Bot>().target.Add( newPoint);
            newAllies.GetComponent<Bot>()._targetPoint = newPoint;

            Debug.Log("Dodano new target");
            myAllies.Add(newAllies);
            
            _spawnTimer = 0;
        }
        else
        {
            _spawnTimer++;
        }
    }
}
