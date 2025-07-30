using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class levelsPoints : MonoBehaviour
{
    [SerializeField] private List<Transform> _pointsPosition;

    [SerializeField] private SpawnScript _spawnScript;

    private void Start()
    {
        for (int i = 0; i < _pointsPosition.Count; i++)
        {
            _spawnScript._pointsPosition.Add(_pointsPosition[i]);
        }
        _spawnScript._quantityAllies = _spawnScript._pointsPosition.Count;
    }
}
