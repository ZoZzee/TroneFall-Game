using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;

    private void Start()
    {
        settingsPanel.SetActive(false);
    }
    public void Play(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void SwitchSettings(bool state)
    {
        settingsPanel.SetActive(state);
    }
    public void Exid()
    {
        Application.Quit();
    }
}
