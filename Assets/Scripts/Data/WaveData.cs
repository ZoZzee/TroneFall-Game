using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "Data/WaveData", order = 0)]
public class WaveData : ScriptableObject
{
    public GameObject[] enemys;
    public int[] count;
}
