using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [Header("Referenses")]
    [SerializeField]private GameObject _pauseUI;
    [SerializeField]private GameObject _gameUI;
    [SerializeField]private GameObject _gameOverUI;
    private bool _paused = false;

    public static UIController instance;
    private void Awake()
    {
        instance = this;
    }

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
    public void Play(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void gameOver()
    {

        _gameOverUI.SetActive(!_gameOverUI.active);
    }

}
