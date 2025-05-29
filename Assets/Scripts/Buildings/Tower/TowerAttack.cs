using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    [SerializeField]private TowerTrigger _towerTrigger;
    [SerializeField] private int _damage;
    [SerializeField] private float cooldown;
    [SerializeField] private float cooldownMax;

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        if(_towerTrigger.enemy.Count > 0)
        {
            cooldown++;

            if (cooldown >= cooldownMax)
            {
                Attack();
                cooldown = 0;
            }
        }
    }

    private void Attack()
    {
        _towerTrigger.enemyHealth[0].MinusHp(_damage);
        if(_towerTrigger.enemyHealth[0].EnoughHealth(_damage))
        {
            _towerTrigger.enemyHealth.RemoveAt(0);
            _towerTrigger.enemy.RemoveAt(0);
        }
    }

}
