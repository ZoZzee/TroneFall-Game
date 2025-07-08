using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    public GameObject allies;
    private List<GameObject> activeAllies;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField]private float _spawnTimerMax;

    [SerializeField]private BuildingPlan _buildingPlan;
    private float _quantityAllies;
    private float _spawnTimer;

    private void FixedUpdate()
    {
        if(_spawnTimer >= _spawnTimerMax /*&& activeAllies.Count <= _quantityAllies*/)
        {
            Instantiate(allies, _spawnPoint);
            activeAllies.Add(allies);
            _spawnTimer = 0;
        }
        else
        {
            _spawnTimer++;
        }
    }
}
