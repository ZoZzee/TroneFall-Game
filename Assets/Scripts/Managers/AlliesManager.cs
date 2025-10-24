using System.Collections.Generic;
using UnityEditor.Searcher;
using UnityEngine;

public class AlliesManager : MonoBehaviour
{
    public List<GameObject> activeAllies;
    public List<GameObject> bildingAllies;

    [SerializeField] private GameObject _archers;
    [SerializeField] private GameObject _targetPoint;
    [SerializeField] private GameObject _barbarians;
    [SerializeField] private byte _initialCount = 10;

    private List<GameObject> _poolArchers;
    private List<GameObject> _poolTargetP;
    private List<GameObject> _poolBarbarians;

    public static AlliesManager instance;

    private void Awake()
    {
        instance = this;


        _poolArchers = new List<GameObject>();
        _poolBarbarians = new List<GameObject>();
        _poolTargetP = new List<GameObject>();

        for (int i = 0; i < _initialCount; i++)
        {
            CreateBarbarians();
            CreateArchers();
            CreateTargetPoints();
        }
    }


    //Ñòâîðåííÿ âî¿íà
    private GameObject CreateBarbarians()
    {
        GameObject barbarian = Instantiate(_barbarians, transform);
        barbarian.SetActive(false);
        _poolBarbarians.Add(barbarian);
        return barbarian;
    }

    // Ñòâîðåííÿ ëó÷íèêà
    private GameObject CreateArchers()
    {
        GameObject archer = Instantiate(_archers, transform);
        archer.SetActive(false);
        _poolArchers.Add(archer);
        return archer;
    }
    //Ñòâîðåííÿ òî÷êè
    private GameObject CreateTargetPoints()
    {
        GameObject point = Instantiate(_targetPoint, transform);
        point.SetActive(false);

        _poolTargetP.Add(point);
        return point;
    }

    // Àêòèâàö³ÿ Âî¿íà
    public GameObject GetBarbarian(Vector3 position, Quaternion rotation)
    {
        foreach (GameObject barbarian in _poolBarbarians)
        {
            if (!barbarian.activeInHierarchy)
            {
                barbarian.transform.SetPositionAndRotation(position, rotation);
                barbarian.SetActive(true);

                return barbarian;
            }
        }

        GameObject newBarbarian = CreateBarbarians();
        newBarbarian.transform.SetPositionAndRotation(position, rotation);
        newBarbarian.SetActive(true);
        return newBarbarian;
    }

    // Àêòèâàö³ÿ Ëó÷íèêà
    public GameObject GetArchers(Vector3 position, Quaternion rotation)
    {
        foreach (GameObject archer in _poolArchers)
        {
            if (!archer.activeInHierarchy)
            {
                archer.transform.SetPositionAndRotation(position, rotation);
                archer.SetActive(true);
                return archer;
            }
        }

        GameObject newArcher = CreateArchers();
        newArcher.transform.SetPositionAndRotation(position, rotation);
        newArcher.SetActive(true);
        return newArcher;
    }
    // Àêòèâàö³ÿ òî÷êè äëÿ êîíòðîëþ ïåðñîíàæà
    public GameObject GetPoint(Vector3 position, Quaternion rotation)
    {
        foreach (GameObject point in _poolTargetP)
        {
            if (!point.activeInHierarchy)
            {
                point.transform.SetPositionAndRotation(position, rotation);
                point.SetActive(true);

                return point;
            }
        }
        GameObject newPoint = CreateTargetPoints();
        newPoint.transform.SetPositionAndRotation(position, rotation);
        newPoint.SetActive(true);
        return newPoint;
    }
    public void Disable(GameObject something)
    {
        something.SetActive(false);
    }

    public void ClineDeadTarget(GameObject _target , HealthManager _hpTarget)
    {
        Debug.Log("Çàéøîâ â âèäàëåííÿ");
        // Î÷èùàºìî òàðãåòà ç ñïèñêó ö³ëåé ñîþçíèê³â
        for (int i = 0;i < activeAllies.Count; i ++)
        {
            Bot allies = activeAllies[i].GetComponent<Bot>();
                allies.target.Remove(_target);
                allies.targetHealth.Remove(_hpTarget);
            allies.p_alliesTrigger._positionOnList--;
            allies.canAttack = false;                   // не правильно, але це потрібно буде потім доробити !!!
        }
        // Î÷èùàºìî òàðãåòà ç ñïèñêó ö³ëü áàøåíü
        for (int i = 0; i < bildingAllies.Count; i++)
        {
            TowerAttack allies = bildingAllies[i].GetComponent<TowerAttack>();
            allies.target.Remove(_target);
            allies.targetHealth.Remove(_hpTarget);
        }
    }

}
