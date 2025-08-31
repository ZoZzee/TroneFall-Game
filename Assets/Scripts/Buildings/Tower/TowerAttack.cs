using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    [SerializeField] private TowerTrigger _towerTrigger;
    [SerializeField] private int _damage = 10;
    [SerializeField] private float cooldown;
    [SerializeField] private float cooldownMax;

    public List<GameObject> target;
    public List<HealthManager> targetHealth;

    public AlliesManager _alliesManager;

    private void Start()
    {
        _alliesManager = AlliesManager.instance;
        _alliesManager.activeAllies.Add(this.gameObject);
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
            }
        }
    }

    private void Attack()
    {
        if (targetHealth[0].CheckHP(_damage) == 0)
        {
            _alliesManager.ClineDeadTarget(target[0], targetHealth[0]);
        }
        targetHealth[0].MinusHp(_damage);
    }
}
