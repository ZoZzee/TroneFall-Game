using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerTrigger : MonoBehaviour
{
    public List<HealthManager> enemyHealth;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemyHealth.Add(other.GetComponent<HealthManager>());
        }
        //if (other.CompareTag("Player"))
        //{

        //}
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemyHealth.Remove(other.GetComponent<HealthManager>());
        }
        //if (other.CompareTag("Player"))
        //{

        //}
    }
}
