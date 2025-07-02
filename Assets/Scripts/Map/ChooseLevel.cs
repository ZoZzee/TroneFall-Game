using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseLevel : MonoBehaviour
{

    [SerializeField] private int indexLevel;
    [SerializeField] private float _maxTimer;
    [SerializeField] private float timer = 0f;
    private bool inTrigger;

    private void Start()
    {
        inTrigger = false;
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.E) && inTrigger)
        {
            if (timer >= _maxTimer)
            {
                SceneManager.LoadScene(indexLevel);
            }
            else
            {
                timer++;
            }
        }
        else if (timer > 1)
        {
            timer--;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inTrigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inTrigger = false;
        }
    }
}
