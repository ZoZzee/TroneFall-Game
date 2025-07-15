using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public bool alive = true;

    [SerializeField] private int _maxHealth;
    [HideInInspector]public int _health;
    
    [SerializeField] private Slider _healthBar;
    [SerializeField] private Canvas _canvas;

    [SerializeField] private bool _isEnemy;
    [SerializeField] private bool _isBuildings;
    [SerializeField] private bool _itsPlayer;
    [SerializeField] private bool _itsAllies;
    [SerializeField] private AlliesController _alliesController;
    private EnemyManager _enemyManager;
    [SerializeField]private EnemyController _enemyController;
    private DayNightManager _dayNightManager;
    private void Start()
    {
        _health = _maxHealth;
        _healthBar.value = _health;
        _canvas.enabled = !_canvas.enabled;

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
    }

    public void MinusHp(int count)
    {
        _health = Mathf.Clamp(_health - count, 0, _maxHealth);
        Debug.Log(_health + " " + gameObject.name);

        if (_health == 0)
        {
            if (_isEnemy)
            {
                _enemyController._animatorController.dead = true;
                _enemyManager.activeEnemy.Remove(this.gameObject);
                Destroy(gameObject,2f);
                _dayNightManager.StartDay();

                alive = false;
            }
            else if(_isBuildings)
            {
                Destroy(gameObject);
            }
            else if(_itsAllies)
            {
                _alliesController._animatorController.dead = true;
                _alliesController.spawnScript.activeAllies.Remove(gameObject);
                Destroy(gameObject, 2f);
            }
        }
        RefreshUI();
    }

    public void PlusHp(int count)
    {
        _health = Mathf.Clamp(_health + count, 0, _maxHealth);
        RefreshUI();
    }

    private IEnumerator Regeneration()
    {
        while(_itsPlayer || _health < _maxHealth)
        {
            PlusHp(_maxHealth / 100);  //добавляємо 1%
            yield return new WaitForSeconds(1f);
        }
        
    }

    public void RefreshUI()
    {
        _healthBar.value = (float)_health / (float)_maxHealth;
        if (_canvas.enabled == false)
            _canvas.enabled = true;
    }


    private void DayStart()
    {
        _canvas.enabled = false;
        _health = _maxHealth;
    }
}
