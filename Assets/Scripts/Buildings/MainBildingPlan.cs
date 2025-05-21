using UnityEngine;

public class MainBildingPlan : MonoBehaviour
{
    [SerializeField] private int _buildingHp;

    public static MainBildingPlan mainBildingPlan;

    private void Start()
    {
        mainBildingPlan = this;
    }

    public void MinusHp(int count)
    {
        _buildingHp -= (_buildingHp - count) > 0 ? count : 0;
        Debug.Log(_buildingHp);
    }
    public void PlusHp(int count)
    {
        _buildingHp += (_buildingHp + count) <= 100 ? count : 100;

    }


}
