using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    public GameObject infoScreen;

    void Start()
    {
        infoScreen.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowInfo()
    {
        infoScreen.SetActive(true);
    }

    public void CloseInfo()
    {
        infoScreen.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
