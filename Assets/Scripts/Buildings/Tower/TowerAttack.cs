using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private int _damage;
    [SerializeField] private float cooldown;
    [SerializeField] private float cooldownMax;
    [SerializeField] private BuildingPlan _buildingPlan = null;

    public List<GameObject> target;
    public List<HealthManager> targetHealth;

    public AlliesManager _alliesManager;

    private bool playerControllerCheck;

    [Header("Aydio")]
    public AudioClip attack;
    public float audioDistance;

    private void Start()
    {
        _alliesManager = AlliesManager.instance;
        _alliesManager.bildingAllies.Add(this.gameObject);
        if (_playerController != null)
        {
            playerControllerCheck = true;
        }
        
    }

    private void FixedUpdate()
    {

        if (targetHealth.Count > 0 && (playerControllerCheck && !_playerController.playerIsDead))
        {
            cooldown += Time.deltaTime;
            if (cooldown >= cooldownMax)
            {
                cooldown = 0;
                Attack();
                SoundsManager.instance.audioDistance = audioDistance;
                SoundsManager.instance.PlaySound(attack, transform.position);
            }
        }
    }

    private void Attack()
    {
        targetHealth[0].MinusHp(_damage);
        Debug.Log("HP = " + targetHealth[0]._health);
        if (targetHealth[0]._health <= 0)
        {
            _alliesManager.ClineDeadTarget(target[0], targetHealth[0]);
        }
    }
}
