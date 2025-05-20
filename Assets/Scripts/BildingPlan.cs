using System.Collections;
using UnityEngine;

public class BildingPlan : MonoBehaviour
{
    [Header("Values")]
    public int goldAmount;

    [SerializeField]
    [Tooltip("На скільки рухаємо гравця коли побудували будівлю")]
    private float _playerMoveDistanceOnBuild;
    [SerializeField] private float _playerMoveSpeed;

    [Header("References")]

    [SerializeField] private GameObject _referens;
    [SerializeField] private GameObject _flag;

    private bool _isBuilt = false;

    public bool _isPlayerNearby;

    private GoldManager goldManager;

    [Header("Components")]
    [SerializeField] private BuildingTrigger _buildingTrigger;

    private void Start()
    {
        goldManager = GoldManager.instance;
    }

    private void Update()
    {
        if (!_isBuilt &&
             _isPlayerNearby &&
             Input.GetKeyDown(KeyCode.E) &&
             goldManager.EnoughGold(goldAmount))
        {
            Build();
        }
    }

    private void Build()
    {
        _referens.SetActive(true);
        _isBuilt = true;
        _flag.SetActive(false);
        goldManager.MinusGold(goldAmount);

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
        while (_buildingTrigger.playerTransform.position != targetPosition)
        {
            _buildingTrigger.playerTransform.position = Vector3.MoveTowards(_buildingTrigger.playerTransform.position, targetPosition, _playerMoveSpeed);
            yield return null;
        }
    }
}
