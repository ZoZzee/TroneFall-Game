using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public UnityEvent OnEnter;
    public UnityEvent OnExit;

    public int repeatCount;
    public int repeatCountMax;

    public float startFromZeroDeley;

    public bool oneTime = true;
    private bool played;

    public string objectName;

    public void OnEnterVoid(float delay)
    {
        if (repeatCountMax == 0 || repeatCount == repeatCountMax) return;
        repeatCount++;
        if(delay == 0)
        {
            OnEnter.Invoke();
        }
        else
        {
            StartCoroutine(OnEnterDelay(delay));
        }
    }

    public void OnExitVoid(float delay)
    {
        if (repeatCountMax == 0 || repeatCount == repeatCountMax) return;
        repeatCount++;

        if (delay == 0)
        {
            OnExit.Invoke();
        }
        else
        {
            StartCoroutine(OnExitDelay(delay));
        }
    }

    IEnumerator OnEnterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (repeatCountMax != 0 || repeatCount != repeatCountMax) OnEnter.Invoke();
    }

    IEnumerator OnExitDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (repeatCountMax != 0 || repeatCount != repeatCountMax) OnExit.Invoke();
    }

    public void StartFromZero()
    {
        repeatCount = 0;
        OnEnterVoid(startFromZeroDeley);
    }

    public void OnTriggerEnter(Collider other)
    {
        if((other.CompareTag("Player") && objectName == "") || (objectName == other.name))
        {
            if(oneTime && !played)
            {
                played = true;
            }
            else if (oneTime && played)
            {
                return;
            }
            OnEnter.Invoke();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if ((other.CompareTag("Player") && objectName == "") || (objectName == other.name))
        {
            if (oneTime && !played)
            {
                played = true;
            }
            else if (oneTime && played)
            {
                return;
            }
            OnExit.Invoke();
        }
    }
}
