using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float _maxHealth;
    [HideInInspector]public float _health;
    [Header("UI")]
    [SerializeField] private Slider _healthBar;
    [SerializeField] private GameObject _healthBarUI;
    [Header("Its Bool")]
    [SerializeField] private bool _isEnemy;
    [SerializeField] private bool _isBuildings;
    [SerializeField] private bool _itsPlayer;
    [SerializeField] private bool _itsAllies;
    [Header("Referenses")]
    private AlliesManager _alliesManager;
    private EnemyManager _enemyManager;
    [SerializeField]private Bot _bot;
    private DayNightManager _dayNightManager;
    private void Start()
    {
        _health = _maxHealth;
        _healthBar.value = _health;
        _healthBarUI.SetActive(false);
         
        _dayNightManager = DayNightManager.instance;
        if (_isEnemy)
        {
            _enemyManager = EnemyManager.instance;
        }
        if(_isBuildings)
        {
            _dayNightManager.onDayStart.AddListener(DayStart);
        }
        if(_itsPlayer)
        {
            StartCoroutine(Regeneration());
        }
        if (_itsAllies)
        {
            _alliesManager = AlliesManager.instance;
        }
    }

    public void MinusHp(int count)
    {
        Debug.Log(_health + " " + gameObject.name);
        _health = Mathf.Clamp(_health - count, 0, _maxHealth);
        if(_itsPlayer)
        {
            StartCoroutine(Regeneration());
        }

        if (_health == 0)
        {
            if (_isEnemy)
            {
                _bot._animatorController.dead = true;
                _bot.canAttack = false;
                _enemyManager.activeEnemy.Remove(this.gameObject);
                _enemyManager.Disable(this.gameObject);
                _dayNightManager.StartDay();
            }
            else if (_itsAllies)
            {
                _bot._animatorController.dead = true;
                _bot.canAttack = false;
                _alliesManager.activeAllies.Remove(this.gameObject);
                _alliesManager.Disable(this.gameObject);
            }
            else if(_isBuildings)
            {
                this.gameObject.SetActive(false);
            }
        }
        RefreshUI();
    }

    public void PlusHp(float count)
    {
        _health = Mathf.Clamp(_health + count, 0, _maxHealth);
        RefreshUI();
    }

    public float CheckHP(float count)
    {
        _health = Mathf.Clamp(_health - count, 0, _maxHealth);
        return _health;
    }

    private IEnumerator Regeneration()
    {
        while(_itsPlayer || _health < _maxHealth)
        {
            float plusHp = Mathf.Clamp((_maxHealth / 100) * 2, 0f, _maxHealth);
            PlusHp(plusHp);  // +2% 
            yield return new WaitForSeconds(1f);
            if(_health == _maxHealth)
            {
                _healthBarUI.SetActive(false);
                StopCoroutine(Regeneration());
            }
        }
    }

    public void RefreshUI()
    {
        _healthBarUI.SetActive(true);
        _healthBar.value = (float)_health / (float)_maxHealth;
    }

    private void DayStart()
    {
        _healthBarUI.SetActive(false);
        _health = _maxHealth;
    }
}
