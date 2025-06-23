using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyTrigger : MonoBehaviour
{

    [SerializeField] private EnemyController enemyController;
    private byte _priority = 0;
    private byte _notPriority = 0;

    public TriggerPriority currentTriggerPriority;

    public enum TriggerPriority
    {
        priority0,
        priority1,
        priority2
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") ||
           other.CompareTag("PlayerAllies"))
        {
            switch (currentTriggerPriority)
            {
                case TriggerPriority.priority0:
                    enemyController.enemyAttack = true;
                    break;
                case TriggerPriority.priority1:
                    enemyController.target.Insert(_priority, other.transform);
                    enemyController.targetHealth.Insert(_priority, other.GetComponent<HealthManager>());
                    _priority++;
                    _notPriority++;
                    break;
                case TriggerPriority.priority2:

                    break;
            }
        }
        if (other.CompareTag("Buldings") ||
            other.CompareTag("Wall"))
        {
            switch (currentTriggerPriority)
            {
                case TriggerPriority.priority0:
                    enemyController.enemyAttack = true;
                    //if(enemyController.target[0] != )
                    //{

                    //}
                    break;
                case TriggerPriority.priority1:
                    
                    enemyController.target.Insert(_notPriority, other.transform);
                    enemyController.targetHealth.Insert(_notPriority, other.GetComponent<HealthManager>());
                    _notPriority++;
                    break;
                case TriggerPriority.priority2:

                    break;
            }
        }

            //switch (other.tag)
            //{
            //    case ("Player"):
            //        AddTargetPriority(other);
            //        Debug.Log(" Добавив " + other);
            //        break;
            //    case ("PlayerAllies"):
            //        AddTargetPriority(other);

            //        Debug.Log(" Добавив " + other);
            //        break;
            //    case ("Buldings"):
            //        AddTarget(other);

            //        Debug.Log(" Добавив " + other);
            //        break;
            //    case ("Wall"):
            //        AddTarget(other);

            //        Debug.Log(" Добавив " + other);
            //        break;
            //}
        }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") ||
            other.CompareTag("PlayerAllies") ||
            other.CompareTag("Buldings") ||
            other.CompareTag("Wall"))
        {
            switch (currentTriggerPriority)
            {
                case TriggerPriority.priority0:
                    
                    enemyController.enemyAttack = false;
                    break;
                case TriggerPriority.priority2:
                    enemyController.target.Remove(other.transform);
                    enemyController.targetHealth.Remove(other.GetComponent<HealthManager>());
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

    //private void AddTargetPriority(Collider other)
    //{
    //    switch (currentTriggerPriority)
    //    {
    //        case TriggerPriority.priority0:
    //            enemyController.enemyAttack = true;
    //            break;
    //        case TriggerPriority.priority1:
    //            _priority.Add(other.transform);
    //            enemyController.target.Insert(_priority.Count, other.transform);
    //            enemyController.targetHealth.Insert(_priority.Count, other.GetComponent<HealthManager>());
    //            break;
    //        case TriggerPriority.priority2:

    //            break;
    //    }
    //}

    //private void AddTarget(Collider other)
    //{
    //    switch (currentTriggerPriority)
    //    {
    //        case TriggerPriority.priority0:
    //            enemyController.enemyAttack = true;
    //            break;
    //        case TriggerPriority.priority1:
    //            enemyController.target.Add(other.transform);
    //            enemyController.targetHealth.Add(other.GetComponent<HealthManager>());
    //            break;
    //        case TriggerPriority.priority2:

    //            break;
    //    }
    //}
}
