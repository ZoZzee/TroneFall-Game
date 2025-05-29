using UnityEngine;

public class HealthManager : MonoBehaviour
{

    [SerializeField] private int _maxHealth;
    private int _health;

    private void Start()
    {
        _health = _maxHealth;
    }

    public void MinusHp(int count)
    {
        _health -= (_health - count) >= 0 ? count : 0;
        Debug.Log(_health + " " + gameObject.name);
        if(EnoughHealth(count))
        {
            Destroy(gameObject);
        }
    }

    public bool EnoughHealth(int count)
    {
        if (_health >= count)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void PlusHp(int count)
    {
        _health += (_health + count) <= 100 ? count : 100;
    }
}
