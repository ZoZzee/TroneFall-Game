using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseLevel : MonoBehaviour
{
    [Header("Timer")]
    public int indexLevel;
    [SerializeField] private float _maxTimer;
    [SerializeField] private float timer = 0f;

    private bool inTrigger;
    [Header("Components")]
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Material _standart;
    [SerializeField] private Material _yellow;
    [Header("UI")]
    public Image _roll;
    public GameObject _obgectUI;

    [Header("Level")]
    [SerializeField] private Level level;
    public List<MeshRenderer> _levelStars;

    private void Start()
    {
        inTrigger = false;
        for (int i = 0; i < level.completedTimes; i++)
        {
            _levelStars[i].material = _yellow;
        }
        _obgectUI.SetActive(false);
    }

    public void LoadScene(int level)
    {
        SceneManager.LoadScene(level);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inTrigger = true;
            _meshRenderer.material = _yellow;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inTrigger = false;

            _meshRenderer.material = _standart;
        }
    }
}
