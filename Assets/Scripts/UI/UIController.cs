using UnityEngine;

public class UIController : MonoBehaviour
{
    [Header("Referenses")]
    [SerializeField]private GameObject _pauseUI;
    [SerializeField]private GameObject _gameUI;
    private bool _paused = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !_paused)
        {
            Pause();
        }
    }

    public void Pause()
    {
        _paused = !_paused;
        _pauseUI.SetActive(!_pauseUI.active);
        _gameUI.SetActive(!_gameUI.active);
    }
    public void Exid()
    {
        Application.Quit();
    }
}
