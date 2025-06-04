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
        if(_towerTrigger.enemyHealth.Count > 0)
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
        if (_towerTrigger.enemyHealth[0] != null)
        {
            _towerTrigger.enemyHealth[0].MinusHp(_damage);

            if (_towerTrigger.enemyHealth[0].alive == false)
            {
                _towerTrigger.enemyHealth.RemoveAt(0);
            }
        }
        else
        {
            _towerTrigger.enemyHealth.RemoveAt(0);
        }
    }
}
