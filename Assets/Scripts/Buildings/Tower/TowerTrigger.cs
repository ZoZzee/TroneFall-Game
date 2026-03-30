using UnityEngine;

public class TowerTrigger : MonoBehaviour
{
    [SerializeField] private TowerAttack _towerAttack;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _towerAttack.targetHealth.Add(other.GetComponent<HealthManager>());
            _towerAttack.target.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _towerAttack.targetHealth.Remove(other.GetComponent<HealthManager>());
            _towerAttack.target.Remove(other.gameObject);

        }
    }
}


