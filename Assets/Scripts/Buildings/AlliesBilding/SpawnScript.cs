using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    public GameObject allies;
    public List<GameObject> activeAllies;
    
    public List<Transform> _pointsPosition;
    public Transform deadPosition = null;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField]private float _spawnTimerMax;

    [SerializeField]private BuildingPlan _buildingPlan;
    public float _quantityAllies;
    private float _spawnTimer;

    public byte num = 0;
    private void FixedUpdate()
    {
        if (_spawnTimer >= _spawnTimerMax && activeAllies.Count < _quantityAllies)
        {
            GameObject newAllies = Instantiate(allies, _spawnPoint);
            newAllies.GetComponent<AlliesController>().spawnScript = this;
            activeAllies.Add(newAllies);
            _spawnTimer = 0;
        }
        else
        {
            _spawnTimer++;
        }
    }
}
