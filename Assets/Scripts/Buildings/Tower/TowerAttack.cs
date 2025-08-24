using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    [SerializeField] private TowerTrigger _towerTrigger;
    [SerializeField] private int _damage;
    [SerializeField] private float cooldown;
    [SerializeField] private float cooldownMax;

    public List<GameObject> target;
    public List<HealthManager> targetHealth;

    public AlliesManager _alliesManager;

    private void Start()
    {
        _alliesManager = AlliesManager.instance;
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
        if (targetHealth[0].CheckHP(_damage) <= 0)
        {
            for (int i = 0; i < _alliesManager.activeAllies.Count; i++)
            {
                Debug.Log("Refresh");
                EnemyController allies = _alliesManager.activeAllies[i].GetComponent<EnemyController>();
                GameObject perevirka = target[0];
                HealthManager _perevirkaHp = targetHealth[0];
                target.Remove(perevirka);
                targetHealth.Remove(_perevirkaHp);
                allies.target.Remove(perevirka);
                allies.targetHealth.Remove(_perevirkaHp);
                
                    
                //_bot._enemyManager.Disable();
            }
        }
        targetHealth[0].MinusHp(_damage);
    }
}
