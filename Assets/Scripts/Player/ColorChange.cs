using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{

    [SerializeField] private Material _blue;
    [SerializeField] private Material _white;
    [SerializeField] private List<SkinnedMeshRenderer> _gameObject;

    

    public void GameObjectColorChange(string color)
    {
        switch (color)
        {
            case "Blue":
                foreach(SkinnedMeshRenderer mesh in _gameObject)
                {
                    mesh.material = _blue;
                }
                break;
            case "White":
                foreach (SkinnedMeshRenderer mesh in _gameObject)
                {
                    mesh.material = _white;
                }
                break;
        }

    }
}
