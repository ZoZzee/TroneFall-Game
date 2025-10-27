using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float _maxHealth;
    //[HideInInspector]
    public float _health;
    [Header("UI")]
    [SerializeField] private Slider _healthBarSlider;
    [SerializeField] private GameObject _healthBarUI;
    [Header("Its Bool")]
    [SerializeField] private bool _isEnemy;
    [SerializeField] private bool _isBuildings;
    [SerializeField] private bool _itsPlayer;
    [SerializeField] private bool _itsAllies;
    private bool _isRegeneration = false;
    [Header("Referenses")]
    private AlliesManager _alliesManager;
    private EnemyManager _enemyManager;
    [SerializeField]private Bot _bot;
    private DayNightManager _dayNightManager;
    private void Start()
    {
        _health = _maxHealth;
        _healthBarSlider.value = _health;
        _healthBarUI.SetActive(false);
         
        _dayNightManager = DayNightManager.instance;
        if (_isEnemy)
        {
            _enemyManager = EnemyManager.instance;
        }
            _dayNightManager.onDayStart.AddListener(DayStart);
        
        if(_itsPlayer)
        {
            StartCoroutine(Regeneration());
        }
        if (_itsAllies)
        {
            _alliesManager = AlliesManager.instance;
        }
    }
    private void OnEnable()
    {
        _health = _maxHealth;
        RefreshUI();
        _healthBarUI.SetActive(false);
    }

    public void MinusHp(int count)
    {
        _health = _health - count;
        if(_itsPlayer && !_isRegeneration)
        {
            StartCoroutine(Regeneration());
            _isRegeneration = true;
        }
        if (_health < 1)
        {
            if (_isEnemy)
            {
                _bot.SwitchState(new DeadState());
                _enemyManager.activeEnemy.Remove(this.gameObject);
                _dayNightManager.StartDay();
                _enemyManager.Disable(gameObject);
            }
            else if (_itsAllies)
            {
                _bot.SwitchState(new DeadState());
                _alliesManager.activeAllies.Remove(this.gameObject);
                _alliesManager.Disable(gameObject);
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
        float _health_check = Mathf.Clamp(_health - count, 0, _maxHealth);
        return _health_check;
    }

    private IEnumerator Regeneration()
    {
        while(_itsPlayer || _health <= _maxHealth)
        {
            float plusHp = Mathf.Clamp((_maxHealth / 100) * 1, 0f, _maxHealth);
            PlusHp(plusHp);  // +2% 
            yield return new WaitForSeconds(1f);
            if(_health == _maxHealth)
            {
                _healthBarUI.SetActive(false);
                _isRegeneration = false;
                StopCoroutine(Regeneration());
            }
        }
    }

    public void RefreshUI()
    {
        _healthBarUI.SetActive(true);
        _healthBarSlider.value = (float)_health / (float)_maxHealth;
    }

    private void DayStart()
    {
        _health = _maxHealth;
        RefreshUI();
        _healthBarUI.SetActive(false);
    }
}
