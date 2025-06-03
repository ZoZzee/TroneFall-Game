using UnityEngine;

public class HealthManager : MonoBehaviour
{

    [SerializeField] private int _maxHealth;
    private int _health;

    [SerializeField] private bool isEnemy;
    private EnemyManager _enemyManager;
    private DayNightManager _dayNightManager;
    private void Start()
    {
        _health = _maxHealth;
        if (isEnemy)
        {
            _dayNightManager = DayNightManager.instance;
            _enemyManager = EnemyManager.instance;
        }
    }

    public void MinusHp(int count)
    {
        _health -= (_health - count) >= 0 ? count : 0;
        Debug.Log(_health + " " + gameObject.name);
        if(EnoughHealth(count))
        {
            if (isEnemy)
            {

                _enemyManager.acriveEnemy.Remove(this.gameObject);
                Destroy(gameObject);
                _dayNightManager.StartDay();
            }
        }
    }

    public bool EnoughHealth(int count)
    {
        if (_health >= count)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void PlusHp(int count)
    {
        _health += (_health + count) <= 100 ? count : 100;
    }
}
