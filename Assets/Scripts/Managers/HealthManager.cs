using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public bool alive = true;

    [SerializeField] private int _maxHealth;
    private int _health;

    [SerializeField] private Image _healthBar;

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
        _health = Mathf.Clamp(_health - count, 0, _maxHealth);
        Debug.Log(_health + " " + gameObject.name);

        if (_health == 0)
        {
            if (isEnemy)
            {
                _enemyManager.acriveEnemy.Remove(this.gameObject);
                Destroy(gameObject);
                _dayNightManager.StartDay();

                alive = false;
            }
        }

        if (_healthBar)
        {
            _healthBar.fillAmount = (float)_health / (float)_maxHealth;
        }
    }

    public void PlusHp(int count)
    {
        _health = Mathf.Clamp(_health + count, 0, _maxHealth);
    }
}
