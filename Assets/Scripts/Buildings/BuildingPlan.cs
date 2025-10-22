using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.UI;

public class BuildingPlan : MonoBehaviour
{
    [Header("Values")]

    [HideInInspector]public byte _levelEnhancement;

    [SerializeField][Tooltip("На скільки рухаємо гравця коли побудували будівлю")]
    private float _playerMoveDistanceOnBuild;

    [SerializeField][Tooltip("На скільки швидко рухаєм гравця від будівлі")]
    private float _playerMoveSpeed;

    [Tooltip("Чи видно попередні рівні будівлі")] 
    public bool _setillActive;

    public bool isBuilt = false;
    [SerializeField] private float spaceHoldTime;
    [SerializeField] private float spaceHoldTimeMax;

    [Header("References")]
    [SerializeField] private GameObject _flag;
    [SerializeField] private GameObject _isParent = null;
    public List<GameObject> LevelEnhancement;
    public List<byte> costEnhancement;
    public bool _isPlayerNearby;
    private GoldManager goldManager;
    [SerializeField] private Image _buildRol;

    [Header("Aydio")]
    public AudioClip itsBuild;

    [Header("Allies")]
    [SerializeField] private bool _isAllies;
    [SerializeField] private byte _maxAmountAllies;

    [Header("Components")]

    [SerializeField] private BuildingTrigger _buildingTrigger;

    private void Start()
    {
        goldManager = GoldManager.instance;
        _levelEnhancement = 0;
        spaceHoldTime = 0;
        _buildRol.fillAmount = 0;
    }

    private void Update()
    {
        var condition = _levelEnhancement < LevelEnhancement.Count &&
            _isPlayerNearby && goldManager.EnoughGold(costEnhancement[_levelEnhancement]);
        if (!condition)
        {
            return;
        }

        if (Input.GetKey(KeyCode.E))
        {
            spaceHoldTime++;

            if (spaceHoldTime >= spaceHoldTimeMax)
            {
                spaceHoldTime = 0;
                Build(true);
                SoundsManager.instance.PlayMusic(itsBuild, this.transform.position);
            }
        }
        else
        {
            if (spaceHoldTime > 0)
            {
                spaceHoldTime--;
            }
        }
        _buildRol.fillAmount = (spaceHoldTime / (spaceHoldTimeMax / 100)) / 100;

    }

    public void Build(bool minusGold)
    {
        LevelEnhancement[_levelEnhancement].SetActive(true);
        isBuilt = true;
        _flag.SetActive(false);
        goldManager.MinusGold(costEnhancement[_levelEnhancement]);
        _levelEnhancement++;
        if (_isParent != null)
        {
            _isParent.SetActive(true);                 //Чи перший об'єкт батьківський
        }
        if (_setillActive == false && _levelEnhancement > 1)
        {
            for(int i = 0; i < _levelEnhancement - 1; i ++)
            {
                    LevelEnhancement[i].SetActive(false);
            }
        }

        if (_isAllies)
        {
             _maxAmountAllies += _maxAmountAllies;
        }
        
        if (_buildingTrigger.playerTransform)
        {
            Vector3 targetPosition = (_buildingTrigger.playerTransform.position - transform.position).normalized;
            targetPosition *= _playerMoveDistanceOnBuild;
            targetPosition += transform.position;
            StartCoroutine(PlayerMoveOnBuild(targetPosition));
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
