using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class EnemyTrigger : MonoBehaviour
{

    [SerializeField] private Bot _bot;

    private byte _priority = 0;
    private byte _notPriority = 0;

    public TriggerPriority currentTriggerPriority;


    public enum TriggerPriority
    {
        priority0,
        priority1
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") ||
            other.CompareTag("PlayerAllies") ||
            other.CompareTag("Buldings") ||
            other.CompareTag("Wall") ||
            other.CompareTag("MainBild"))
        {
            switch (currentTriggerPriority)
            {
                case TriggerPriority.priority0:
                    _bot.canAttack = true;
                    break;
                case TriggerPriority.priority1:
                    if (other.CompareTag("Player") ||
                        other.CompareTag("PlayerAllies"))
                    {
                        _bot.target.Insert(_priority, other.gameObject);
                        _bot.targetHealth.Insert(_priority, other.GetComponent<HealthManager>());
                        _priority++;
                        _notPriority++;
                    }
                    if (other.CompareTag("Buldings") ||
                        other.CompareTag("Wall"))
                    {
                        _bot.target.Insert(_notPriority, other.gameObject);
                        _bot.targetHealth.Insert(_notPriority, other.GetComponent<HealthManager>());
                        _notPriority++;
                    }
                    break;
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainBild"))
        {
            switch (currentTriggerPriority)
            {
                case TriggerPriority.priority0:
                    _bot.canAttack = false;
                    break;
            }
        }
        if (other.CompareTag("Player") ||
            other.CompareTag("PlayerAllies") ||
            other.CompareTag("Buldings") ||
            other.CompareTag("Wall"))
        {
            switch (currentTriggerPriority)
            {
                case TriggerPriority.priority0:
                    _bot.canAttack = false;
                    break;
                case TriggerPriority.priority1:
                    _bot.target.Remove(other.gameObject);
                    _bot.targetHealth.Remove(other.GetComponent<HealthManager>());
                    _notPriority--;
                    Debug.Log("видалдив Target з Enemy");
                    break;
            }
        }
        if (other.CompareTag("Player") ||
            other.CompareTag("PlayerAllies"))
        {
            _priority--;
        }


    }

}
