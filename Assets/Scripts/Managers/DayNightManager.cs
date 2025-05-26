using System.Collections;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Events;

public class DayNightManager : MonoBehaviour
{
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


    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        dayStart = true;
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartDay();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StartNight();
        }
    }

    private void StartDay()
    {
        onDayStart.Invoke();

        dayStart = true;
        _targetAngle = _dayAngle;
        StartCoroutine(ChangeAngle());
    }

    private void StartNight()
    {
        onNightStart.Invoke();

        dayStart = false;
        _targetAngle = _nightAngle;
        StartCoroutine(ChangeAngle());
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
