using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject EndOfLevelNumberOfLevel;
    [SerializeField] private GameObject EndOfLevel;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject mainMenuButton;
    [SerializeField] private GameObject settingsButton;
    [SerializeField] private Button nextLevelButton;


    private void Start()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        if(SceneManager.GetActiveScene().name != "BtwLevelScene")
        {
            EndOfLevel.SetActive(false);
        }
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
        SceneManager.LoadScene("BtwLevelScene");
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
    public void NextLevel()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetInt("LevelActuel", PlayerPrefs.GetInt("LevelActuel", 1) + 1);
        SceneManager.LoadScene("BtwLevelScene");
    }
    public void OpenEndOfLevel(bool doPause = false)
    {
        EndOfLevel.SetActive(true);
        EndOfLevelNumberOfLevel.GetComponent<TMP_Text>().text = "Level " + PlayerPrefs.GetInt("LevelActuel", 1).ToString();
        if(doPause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    public void ResetSave()
    {
        PlayerPrefs.SetInt("LevelActuel", 1);
    }
}