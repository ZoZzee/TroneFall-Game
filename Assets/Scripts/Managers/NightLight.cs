using System.Collections;
using UnityEngine;

public class NightLight : MonoBehaviour
{
    [SerializeField] private float _lightIntensity;

    [SerializeField] private float _lightChangeSpeed;
    private float _targetLightIntensity;


    [SerializeField] private Light _nightLight;


    private DayNightManager _dayNightManager;



    void Start()
    {
        _dayNightManager = DayNightManager.instance;

        _dayNightManager.onDayStart.AddListener(DisableLight);
        _dayNightManager.onNightStart.AddListener(EnableLight);
        //_dayNightManager.onDayStart.AddListener(Coin coi);
    }

    private void EnableLight()
    {
        _nightLight.enabled = true;
        _targetLightIntensity = _lightIntensity;
        StartCoroutine(ChangeLightState());
    }

    private void DisableLight()
    {
        _targetLightIntensity = 0;
        StartCoroutine(ChangeLightState());
    }

    private IEnumerator ChangeLightState()
    {
        while(_nightLight.intensity != _targetLightIntensity)
        {
            _nightLight.intensity = Mathf.MoveTowards(_nightLight.intensity, _targetLightIntensity, _lightChangeSpeed);
            yield return null;
        }
        if(_lightChangeSpeed <= 0)
        {
            _nightLight.enabled = false;
        }
    }
}
