using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseLevel : MonoBehaviour
{
    
    [SerializeField] private int indexLevel;
    [SerializeField] private float _maxTimer;
    [SerializeField] private float timer = 0f;

    private bool inTrigger;

    [SerializeField] private GameObject _levlPoint;

    [SerializeField] private Level _levl;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Material _standart;
    [SerializeField] private Material _yellow;

    [SerializeField] private Level level;
    public List<MeshRenderer> _levels;

    private void Start()
    {
        inTrigger = false;
        _levl.numberOfLevl = indexLevel;
        for (int i = 0; i < level.completedTimes; i++)
        {
            _levels[i].material = _yellow;
        }
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
            //_levlPoint.
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
