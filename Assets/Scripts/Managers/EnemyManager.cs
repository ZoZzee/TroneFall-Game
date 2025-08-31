using System.Collections.Generic;
using UnityEditor.Searcher;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> activeEnemy;
    public int numberOfEnemies;

    [SerializeField] private GameObject _archers;
    [SerializeField] private GameObject _barbarians;
    [SerializeField] private byte _initialCount = 10;

    private List<GameObject> _poolArchers;
    private List<GameObject> _poolBarbarians;


    public static EnemyManager instance;

    private void Awake()
    {
        instance = this;
        _poolArchers = new List<GameObject>();
        _poolBarbarians = new List<GameObject>();

        for (int i = 0; i < _initialCount; i++)
        {
            CreateBarbarians();
            CreateArchers();
        }
    }

    private GameObject CreateBarbarians()
    {
        GameObject barbarian = Instantiate(_barbarians, transform);
        barbarian.SetActive(false);
        _poolBarbarians.Add(barbarian);
        return barbarian;
    }
    private GameObject CreateArchers()
    {
        GameObject archer = Instantiate(_archers, transform);
        archer.SetActive(false);
        _poolArchers.Add(archer);
        return archer;
    }

    public GameObject GetBarbarian(Vector3 position, Quaternion rotation)
    {
        foreach (GameObject barbarian in _poolBarbarians)
        {
            if (!barbarian.activeInHierarchy)
            {
                barbarian.transform.SetPositionAndRotation(position, rotation);
                barbarian.SetActive(true);
                barbarian.GetComponent<Bot>().AddTarget();

                return barbarian;
            }
        }

        GameObject newBarbarian = CreateBarbarians();
        newBarbarian.transform.SetPositionAndRotation(position, rotation);
        newBarbarian.SetActive(true);
        return newBarbarian;
    }
    public GameObject GetArchers(Vector3 position, Quaternion rotation)
    {
        foreach (GameObject archer in _poolArchers)
        {
            if (!archer.activeInHierarchy)
            { 
                archer.transform.SetPositionAndRotation(position, rotation);
                archer.SetActive(true);
                archer.GetComponent<Bot>().AddTarget();

                return archer;
            }
        }

        GameObject newArcher = CreateArchers();
        newArcher.transform.SetPositionAndRotation(position, rotation);
        newArcher.SetActive(true);
        return newArcher;
    }
    public void Disable(GameObject something)
    {
        something.SetActive(false);
    }

    public bool DayStart()
    {
        if (activeEnemy.Count == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void ClineDeadTarget(GameObject _target, HealthManager _hpTarget)
    {
        for (int i = 0; i <= activeEnemy.Count; i++)
        {
            Bot enemy = activeEnemy[i].GetComponent<Bot>();
            enemy.target.Remove(_target);
            enemy.targetHealth.Remove(_hpTarget);
            Debug.Log(_target);
        }
    }

}
