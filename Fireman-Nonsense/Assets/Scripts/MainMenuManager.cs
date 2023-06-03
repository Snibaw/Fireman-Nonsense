using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenu;

    private void Start()
    {
        settingsMenu.SetActive(false);
    }

    public void LevelSelector()
    {
        SceneManager.LoadScene("BtwLevelScene");
    }
    public void MoreGames()
    {
        Application.OpenURL("https://itch.io/profile/snibaw");
    }
    public void OpenSettings()
    {
        settingsMenu.SetActive(true);
    }
    public void SaveSettings()
    {
        settingsMenu.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
}
