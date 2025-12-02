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
    public bool inBuild = false;
    [SerializeField] private bool _isMainBuilding;

    [Header("References")]
    //Питання чи так можна робити
    public GameObject _nextLevel = null;
    
    public int costBuildings;
    [HideInInspector]public GoldManager goldManager;
    public GameObject rollFilling;
    public Image buildRol;

    [Header("Aydio")]
    public AudioClip itsBuild;


    [Header("Components")]
     private PlayerMove _playerMove;
    private BuildingsManager _buildingsManager;
    //[SerializeField] private NavMeshSurface _navMeshSurface;

    private void Start()
    {
        
        _buildingsManager = BuildingsManager.instance;
        goldManager = GoldManager.instance;
        _playerMove = PlayerMove.instance;
        rollFilling.SetActive(false);
        buildRol.fillAmount = 0;
        if(_isMainBuilding)
        {
            _buildingsManager.mainBuilding = this.gameObject;
        }
    }

    public void Build(bool minusGold)
    {
        if (minusGold && _nextLevel != null)
        {
            isBuilt = true;
            goldManager.MinusGold(costBuildings);
            _nextLevel.SetActive(true);
            SoundsManager.instance.PlayMusic(itsBuild, this.transform.position);
            

            this.gameObject.SetActive(false);
        }
    }

    private IEnumerator PlayerMoveOnBuild()
    {
        Vector3 targetPosition = (_playerMove.transform.position - transform.position).normalized;
        targetPosition *= _playerMoveDistanceOnBuild;
        targetPosition.y = _playerMove.transform.position.y + 0.5f;
        targetPosition += transform.position;
        Vector3 startPosition = _playerMove.transform.position;
        while (_playerMove.transform != null && _playerMove.transform.position != targetPosition)
        {
            _playerMove.transform.position = Vector3.MoveTowards(_playerMove.transform.position, targetPosition , _playerMoveSpeed);
            if (!inBuild)
            {
                StopAllCoroutines();
            }
            yield return null;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            inBuild = true;
            _playerMove.StartCuldown();
            StartCoroutine(PlayerMoveOnBuild());
            
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Вийшов з колізії");
            inBuild = false;
            _playerMove.StopCuldown();
        }
    }
}
