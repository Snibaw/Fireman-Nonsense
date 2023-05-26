using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject mainMenuButton;
    [SerializeField] private GameObject settingsButton;

    private void Start()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }

    public void OpenPauseMenu()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        settingsButton.SetActive(false);
        mainMenuButton.SetActive(true);
    }
    public void ClosePauseMenu()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        mainMenuButton.SetActive(false);
        settingsButton.SetActive(true);
    }
    public void OpenMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
    public void RestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void OpenSettingsMenu()
    {
        settingsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }
    public void SaveSettings()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }
}