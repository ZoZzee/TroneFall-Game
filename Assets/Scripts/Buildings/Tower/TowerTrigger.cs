using System.Collections.Generic;
using UnityEngine;

public class TowerTrigger : MonoBehaviour
{
    public List<EnemyController> enemy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {

        }
    }
}
