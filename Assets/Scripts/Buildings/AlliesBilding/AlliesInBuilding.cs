using System.Collections.Generic;
using UnityEngine;

public class AlliesInBuilding : MonoBehaviour
{
    [Header("Allies")]
    [SerializeField] private byte _countLevel = 0;
    [SerializeField] private List<byte> _pointOnLevel;
    public byte _points;

    [Header("References")]
    [SerializeField] public List<GameObject> _pointsActive;

    public void NextLevel()
    {
        
        _points += _pointOnLevel[_countLevel];
        
        for (int i = 0; i < _points;i++)
        {
            _pointsActive[i].SetActive(true);
        }
        _countLevel++;
    }
}
