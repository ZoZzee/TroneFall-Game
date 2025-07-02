using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;

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
