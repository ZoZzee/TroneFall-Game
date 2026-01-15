
using System.Collections.Generic;
using UnityEngine;

public class InteractionTrigger : MonoBehaviour
{

    public List<GameObject> buildings;
    public List<GameObject> levels;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Building")||
            other.CompareTag("Construction"))
        {
            if (other.GetComponent<BuildingPlan>().haveNewBuild)
            {
                buildings.Add(other.gameObject);
            }
        }
        if (other.CompareTag("Level"))
        {
            levels.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Building")||
            other.CompareTag("Construction")
            ||other.CompareTag("Level"))
        {
            buildings.Remove(other.gameObject);
            levels.Remove(other.gameObject);
        }
    }
}