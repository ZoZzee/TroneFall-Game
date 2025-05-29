using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerTrigger : MonoBehaviour
{
    public List<Transform> enemy;
    public bool enemyDetected;
    public List<HealthManager> enemyHealth;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemy.Add(other.transform);
            enemyDetected = true;
            enemyHealth.Add(other.GetComponent<HealthManager>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.CompareTag("Enemy"))
            {
                enemy.Remove(other.transform);
                enemyHealth.Remove(other.GetComponent<HealthManager>());
            }
        }
    }
}
