using System;
using System.Collections;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class DayNightManager : MonoBehaviour
{
    [SerializeField] private float spaceHoldTime;
    [SerializeField] private float spaceHoldTimeMax;

    [SerializeField] private Vector3 _dayAngle;
    [SerializeField] private Vector3 _nightAngle;
    [SerializeField] private float _lightRotationSpeed;

    [Header("Referenses")]
    [SerializeField] private GameObject _victoryUI;
    [SerializeField] private Transform _globalLight;

    private Vector3 _targetAngle;
    public bool dayStart;

    [Header("Aydio")]
    public AudioClip onDayStartSound;
    public AudioClip onNightStartSound;

    [Header("Events")]
    public UnityEvent onDayStart;
    public UnityEvent onNightStart;

    public static DayNightManager instance;

    private EnemyManager _enemyManager;
    private SpawnManager _spawnManager;
    private BuildingsManager _buildingsManager;
    private SaveSystem _saveSystem;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        dayStart = true;
        _enemyManager = EnemyManager.instance;
        _spawnManager = SpawnManager.instance;
        _buildingsManager = BuildingsManager.instance;
        _saveSystem = SaveSystem.instance;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            StartDay();
        }
        if(Input.GetKeyDown(KeyCode.F2))
        {
            StartNight();
        }
    }
   
    public void StartDay()
    {
        if (_enemyManager.activeEnemy.Count == 0 
            && ChekWave() 
            && !dayStart)                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     
        {
            Debug.Log("Day start");
            onDayStart.Invoke();

            SoundsManager.instance.PlaySound(onDayStartSound, transform.position);
            dayStart = true;
            _targetAngle = _dayAngle;
            StartCoroutine(ChangeAngle());
            if (_spawnManager.spawnPoints[0].spawnPointData.waves.Length == _spawnManager.currentWave)
            {
                Debug.Log("You won");
                _victoryUI.SetActive(true);
                _saveSystem.LevelComplete(SceneManager.GetActiveScene().buildIndex, "Levels");
            }
        }
    }

    public void StartNight()
    {
        if (_buildingsManager.mainBuilding != null && dayStart)
        {

            SoundsManager.instance.PlaySound(onNightStartSound, transform.position);
            dayStart = false;
            _targetAngle = _nightAngle;
            StartCoroutine(ChangeAngle());

            SpawnManager.instance.Spawn();
            onNightStart.Invoke();
        }
    }

    private IEnumerator ChangeAngle()
    {
        Vector3 rotation = _globalLight.rotation.eulerAngles;
        while(_globalLight.rotation != Quaternion.Euler(_targetAngle) )
        {
            rotation = Vector3.MoveTowards(rotation, _targetAngle, _lightRotationSpeed);
            
            _globalLight.rotation = Quaternion.Euler(rotation);

            yield return null;
        }
    }
    private bool ChekWave()
    {
        int amount = 0;
        for (int i = 0; i < _spawnManager.spawnPoints.Length; i++)
        {
            if (_spawnManager.spawnPoints[i].wavefinich == true)
            {
                amount++;
            }
        }
        if (amount == _spawnManager.spawnPoints.Length)
            return true;
        else
            return false;
    }
}
