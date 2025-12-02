using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class EnemyTrigger : MonoBehaviour
{

    [SerializeField] private Bot _bot;

    public byte _priority = 0;
    public byte _notPriority = 0;

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
                    if (other.CompareTag("Wall"))
                    {
                        _bot.wals.Add(other.gameObject);
                        _bot.walsHealth.Add(other.GetComponent<HealthManager>());
                    }
                    break;
                case TriggerPriority.priority1:
                    if (other.CompareTag("Player") ||
                        other.CompareTag("PlayerAllies") ||
                        other.CompareTag("Buldings"))
                    {
                        _bot.builds.Insert(_priority, other.gameObject);
                        _bot.buildsHealth.Insert(_priority, other.GetComponent<HealthManager>());
                        _priority++;
                    }
                    break;
            }

        }
    }

    private void OnTriggerExit(Collider other)
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
                    _bot.canAttack = false;
                    if (other.CompareTag("Wall"))
                    {
                        _bot.wals.Remove(other.gameObject);
                        _bot.walsHealth.Remove(other.GetComponent<HealthManager>());
                    }
                    break;
                case TriggerPriority.priority1:
                    if (other.CompareTag("Player") ||
                        other.CompareTag("PlayerAllies") ||
                        other.CompareTag("Buldings"))
                    {
                        switch (currentTriggerPriority)
                        {
                            case TriggerPriority.priority0:
                                _bot.canAttack = false;
                                break;
                            case TriggerPriority.priority1:
                                _bot.builds.Remove(other.gameObject);
                                _bot.buildsHealth.Remove(other.GetComponent<HealthManager>());
                                _priority--;
                                break;
                        }
                    }
                    break;
            }
        }
    }
}
