using System;
using System.Collections;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Events;

public class DayNightManager : MonoBehaviour
{
    [SerializeField] private float spaceHoldTime;
    [SerializeField] private float spaceHoldTimeMax;

    [SerializeField] private Vector3 _dayAngle;
    [SerializeField] private Vector3 _nightAngle;
    [SerializeField] private float _lightRotationSpeed;

    [Header("Referenses")]

    [SerializeField] private Transform _globalLight;

    private Vector3 _targetAngle;
    public bool dayStart;
    [Header("Events")]

    public UnityEvent onDayStart;
    public UnityEvent onNightStart;


    public static DayNightManager instance;


    private EnemyManager _enemyManager;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        dayStart = true;
        _enemyManager = EnemyManager.instance;
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartDay();
        }
    }

    private void FixedUpdate()
    {
        if (dayStart == true)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                spaceHoldTime++;

                if (spaceHoldTime >= spaceHoldTimeMax)
                {
                    StartNight();
                    spaceHoldTime = 0;
                }
            }
            else
            {
                if (spaceHoldTime > 0)
                {
                    spaceHoldTime--;
                }   
            }
        }
    }

    public void StartDay()
    {
        if (_enemyManager.activeEnemy.Count == 0)
        {
            Debug.Log("Day start");
            onDayStart.Invoke();

            dayStart = true;
            _targetAngle = _dayAngle;
            StartCoroutine(ChangeAngle());
        }
    }

    private void StartNight()
    {
        onNightStart.Invoke();

        dayStart = false;
        _targetAngle = _nightAngle;
        StartCoroutine(ChangeAngle());

        SpawnManager.instance.Spawn();
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

}
