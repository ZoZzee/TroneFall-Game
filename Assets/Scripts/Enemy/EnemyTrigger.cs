using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class EnemyTrigger : MonoBehaviour
{

    [SerializeField] private Bot _bot;

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
                    if (other.CompareTag("Wall"))
                    {
                        _bot.wals.Add(other.gameObject);
                        _bot.walsHealth.Add(other.GetComponent<HealthManager>());
                        Debug.Log("Wall");
                    }
                    _bot.canAttack = true;
                    
                    break;
                case TriggerPriority.priority1:
                    if (other.CompareTag("Player") ||
                        other.CompareTag("PlayerAllies") ||
                        other.CompareTag("Buldings")||
                        other.CompareTag("Bulding"))
                    {
                        _bot.builds.Add(other.gameObject);
                        _bot.buildsHealth.Add(other.GetComponent<HealthManager>());
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
            other.CompareTag("MainBild") ||
            other.CompareTag("Bulding"))
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
                        other.CompareTag("Buldings") ||
                        other.CompareTag("Bulding"))
                    {
                        _bot.builds.Remove(other.gameObject);
                        _bot.buildsHealth.Remove(other.GetComponent<HealthManager>());
                    }
                    break;
            }
        }
    }
}
