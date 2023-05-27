using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject mainMenuButton;
    [SerializeField] private GameObject settingsButton;
    [SerializeField] private GameObject EndOfLevelMenu;
    [SerializeField] private GameObject[] stars;
    [SerializeField] private TextMeshProUGUI winLoseText;
    [SerializeField] private TextMeshProUGUI levelText;
    private int levelNbr;
    [SerializeField] private Button nextLevelButton;


    private void Start()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        EndOfLevelMenu.SetActive(false);
        levelNbr = SceneManager.GetActiveScene().name[SceneManager.GetActiveScene().name.Length - 1] - '0';
        Debug.Log(levelNbr);
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
    public void EndOfLevel(int nbrOfStars)
    {
        Time.timeScale = 0;
        EndOfLevelMenu.SetActive(true);
        settingsButton.SetActive(false);
        mainMenuButton.SetActive(true);
        levelText.text = "Level " + levelNbr;
        for (int i = 0; i < nbrOfStars; i++)
        {
            stars[i].SetActive(true);
        }
        if(nbrOfStars == 0)
        {
            winLoseText.text = "You Lose!";
            nextLevelButton.interactable = false;
            return;
        }
        nextLevelButton.interactable = true;
        winLoseText.text = "You Win!";
    }
    public void NextLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level" + (levelNbr + 1));
    }
}