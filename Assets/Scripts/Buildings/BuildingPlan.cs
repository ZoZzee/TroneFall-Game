using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlan : MonoBehaviour
{
    [Header("Values")]
    public byte goldAmount;

    private byte _levelEnhancement;

    [SerializeField]
    [Tooltip("На скільки рухаємо гравця коли побудували будівлю")]
    private float _playerMoveDistanceOnBuild;
    [SerializeField] private float _playerMoveSpeed;

    [Header("References")]
    public GameObject building;
    public List<GameObject> LevelEnhancement;
    
    [SerializeField] private GameObject _flag;

    public bool isBuilt = false;

    public bool _isPlayerNearby;

    private GoldManager goldManager;

    [Header("Components")]
    [SerializeField] private BuildingTrigger _buildingTrigger;

    private void Start()
    {
        goldManager = GoldManager.instance;
        _levelEnhancement = 0;
    }

    private void Update()
    {
        if (_levelEnhancement <= LevelEnhancement.Count &&
             _isPlayerNearby &&
             Input.GetKeyDown(KeyCode.E) &&
             goldManager.EnoughGold(goldAmount))
        {
            Build(true);
        }
    }

    public void Build(bool minusGold)
    {
        if (!isBuilt)
        {
            building.SetActive(true);
            isBuilt = true;
            _flag.SetActive(false);
        }
        


        if (minusGold)
        {
            goldManager.MinusGold(goldAmount);
        }

        if (_buildingTrigger.playerTransform)
        {
            Vector3 targetPosition = (_buildingTrigger.playerTransform.position - transform.position).normalized;
            targetPosition *= _playerMoveDistanceOnBuild;
            targetPosition += transform.position;

            StartCoroutine(PlayerMoveOnBuild(targetPosition));
        }


        NextlevlEnhancement();
        _levelEnhancement++;
    }

    private void NextlevlEnhancement()
    {
        if (LevelEnhancement.Count > 0)
        {
            goldAmount = 10;
            LevelEnhancement[_levelEnhancement].SetActive(true);
        }
    }

    private IEnumerator PlayerMoveOnBuild(Vector3 targetPosition)
    {
        while (_buildingTrigger.playerTransform != null && _buildingTrigger.playerTransform.position != targetPosition)
        {
            _buildingTrigger.playerTransform.position = Vector3.MoveTowards(_buildingTrigger.playerTransform.position, targetPosition, _playerMoveSpeed);
            yield return null;
        }
    }
}
