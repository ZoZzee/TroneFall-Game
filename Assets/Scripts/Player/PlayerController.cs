using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int _maxPlayerHp;

    private int _playerHp;
    public static PlayerController instance;

    private void Start()
    {
        instance = this;
        _playerHp = _maxPlayerHp;
    }

    public void MinusHp( int count)
    {
        _playerHp -= (_playerHp - count) > 0 ? count : 0;
        Debug.Log(_playerHp);
    }
    public void PlusHp(int count)
    {
        _playerHp += (_playerHp + count) <= 100 ? count : 100 ;

    }
}
