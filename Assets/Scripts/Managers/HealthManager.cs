using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [Header("Parameters")]

    [SerializeField] private float _maxHealth;
    public float _health;

    [Header("UI")]
    [SerializeField] private Slider _healthBarSlider;
    [SerializeField] private GameObject _healthBarUI;

    [Header("Its Bool")]

    [SerializeField] private bool _isEnemy;
    [SerializeField] private bool _isBuildings;
    [SerializeField] private bool _isMainBuilding;
    [SerializeField] private bool _itsPlayer;
    [SerializeField] private bool _itsAllies;
    private bool _isRegeneration = false;

    [Header("Referenses")]
    private BuildingsManager _buildingsManager;
    private UIController _uIController;
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
        _dayNightManager.onDayStart.AddListener(DayStart);
        if (_isMainBuilding)
        {
            
            _uIController = UIController.instance;
        }
        else
        if(_isBuildings)
        {
            _buildingsManager = BuildingsManager.instance;
        }
        else
        if (_isEnemy)
        {
            _enemyManager = EnemyManager.instance;
        }
        else if (_itsPlayer)
        {
            StartCoroutine(Regeneration());
        }
        else if (_itsAllies)
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
        if ((_health - count) < 0)
        {
            _health = 0;
        }
        else
        {
            _health = _health - count;
        }
        
        if(_itsPlayer && !_isRegeneration)
        {
            StartCoroutine(Regeneration());
            _isRegeneration = true;
        }
        if (_health < 1)
        {
            if(_itsPlayer)
            {

            }
            if(_isMainBuilding)
            {
                _uIController.gameOver();
            }
            else if (_isEnemy)
            {
                _bot.SwitchState(new DeadState());
                _enemyManager.activeEnemy.Remove(this.gameObject);
                StartCoroutine(Dead());
            }
            else if (_itsAllies)
            {
                _bot.SwitchState(new DeadState());
                _alliesManager.activeAllies.Remove(this.gameObject);
                _alliesManager.Disable(gameObject);
            }
            else if (_isBuildings)
            {
                Debug.Log(this.gameObject);
                _buildingsManager.wallDestroyed.Add(this.gameObject);
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

    private IEnumerator Dead()
    {
        yield return new WaitForSeconds(2f);

        _dayNightManager.StartDay();
        _enemyManager.Disable(gameObject);

        StopCoroutine(Dead());
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
