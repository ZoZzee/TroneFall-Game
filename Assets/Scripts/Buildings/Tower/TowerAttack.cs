using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    [SerializeField] private TowerTrigger _towerTrigger;
    [SerializeField] private List<int> _damage;
    [SerializeField] private float cooldown;
    [SerializeField] private float cooldownMax;
    [SerializeField] private BuildingPlan _buildingPlan = null;

    public List<GameObject> target;
    public List<HealthManager> targetHealth;

    public AlliesManager _alliesManager;


    [Header("Aydio")]
    public AudioClip attack;
    public float audioDistance;

    private void Start()
    {
        _alliesManager = AlliesManager.instance;
        _alliesManager.bildingAllies.Add(this.gameObject);
    }
    private void FixedUpdate()
    {
        if(targetHealth.Count > 0)
        {
            cooldown++;

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
        if (_buildingPlan != null)
        { }
        //targetHealth[0].MinusHp(_damage[_buildingPlan._levelEnhancement]);
        else
        {
            targetHealth[0].MinusHp(_damage[0]);
        }
        if (targetHealth[0]._health <= 0)
        {
            _alliesManager.ClineDeadTarget(target[0], targetHealth[0]);
        }
        
    }
}
