using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.UI;

public class BuildingPlan : MonoBehaviour
{
    [Header("Values")]

    [SerializeField][Tooltip("На скільки рухаємо гравця коли побудували будівлю")]
    private float _playerMoveDistanceOnBuild;
    [SerializeField][Tooltip("На скільки швидко рухаєм гравця від будівлі")]
    private float _playerMoveSpeed;

    public bool isBuilt = false;

    [Header("References")]
    //Питання чи так можна робити
    public GameObject _nextLevel = null;
    
    public int costBuildings;
    [HideInInspector]public GoldManager goldManager;

    public Image _buildRol;

    [Header("Aydio")]
    public AudioClip itsBuild;

    [Header("Allies")]
    [SerializeField] private bool _isAllies;
    [SerializeField] private byte _maxAmountAllies;

    [Header("Components")]
    [SerializeField] private PlayerMove _playerMove;

    private void Start()
    {
        goldManager = GoldManager.instance;
        _playerMove = PlayerMove.instance;
        _buildRol.fillAmount = 0;
    }

    public void Build(bool minusGold)
    {
        if (minusGold && _nextLevel != null)
        {
            isBuilt = true;
            goldManager.MinusGold(costBuildings);
            _nextLevel.SetActive(true);
            SoundsManager.instance.PlayMusic(itsBuild, this.transform.position);
            if (_playerMove.transform)
            {
                Vector3 targetPosition = (_playerMove.transform.position - transform.position).normalized;
                targetPosition *= _playerMoveDistanceOnBuild;
                targetPosition += transform.position;
                StartCoroutine(PlayerMoveOnBuild(targetPosition));
            }
            _playerMove.StartCuldown();
            this.gameObject.SetActive(false);
        }
    }

    private IEnumerator PlayerMoveOnBuild(Vector3 targetPosition)
    {
        Vector3 startPosition = _playerMove.transform.position;
        while (_playerMove.transform != null && _playerMove.transform.position != targetPosition)
        {
            _playerMove.transform.position = Vector3.MoveTowards(_playerMove.transform.position,new Vector3( targetPosition.x, startPosition.y + 0.5f, targetPosition.z), _playerMoveSpeed);
            yield return null;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            Debug.Log("Гравець всередині");
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Гравець за межами");
        }
    }
}
