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
    [SerializeField] private GameObject creditButton;
    [SerializeField] private bool isBtwLevelScene = false;
    private AudioSource audioSource;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip loseSound;
    [SerializeField] private GameObject TapToStart;

    [Header("Infinite")]
    [SerializeField] private GameObject EndOfLevelInfinite;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highScoreText;
    private GameManager gameManager;
    private int highScore = 0;
    private bool isInfinite = false;

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        isInfinite = PlayerPrefs.GetInt("Mode",0) == 1;
        audioSource = GetComponent<AudioSource>();
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        creditButton.SetActive(false);
        if(!isBtwLevelScene) EndOfLevel.SetActive(false);
        if(!isBtwLevelScene && isInfinite) gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void OpenPauseMenu()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        settingsButton.SetActive(false);
        mainMenuButton.SetActive(true);
        if(isBtwLevelScene) 
        {
            creditButton.SetActive(true);
            TapToStart.SetActive(false);
        }
    }
    public void ClosePauseMenu()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        mainMenuButton.SetActive(false);
        settingsButton.SetActive(true);
        if(isBtwLevelScene) 
        {
            creditButton.SetActive(false);
            TapToStart.SetActive(true);
        }
    }
    // public void OpenMainMenu()
    // {
    //     Time.timeScale = 1;
    //     SceneManager.LoadScene("MainMenu");
    // }
    public void RestartLevel()
    {
        Time.timeScale = 1;
        if(PlayerPrefs.GetInt("Mode",0) == 0) SceneManager.LoadScene("LevelRandom");
        else SceneManager.LoadScene("LevelInfini");
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
        if(PlayerPrefs.GetInt("Mode",0) == 0) SceneManager.LoadScene("LevelRandom");
        else SceneManager.LoadScene("LevelInfini");
    }
    public void OpenEndOfLevel(bool doPause = false, bool isWin = true)
    {
        if(isInfinite)
        {
            EndOfLevelInfinite.SetActive(true);
            EndOfLevelNumberOfLevel.GetComponent<TMP_Text>().text = "Score:" + scoreText.text;
            if(highScore < int.Parse(scoreText.text))
            {
                highScore = int.Parse(scoreText.text);
                PlayerPrefs.SetInt("HighScore", highScore);
            }
            highScoreText.text = "Best:" + highScore.ToString();
            Time.timeScale = 0;
            gameManager.UpdateTextTopLeftCorner();
            return;
        }

        EndOfLevelNumberOfLevel.GetComponent<TMP_Text>().text = "Level " + PlayerPrefs.GetInt("Level", 1).ToString();

        EndOfLevel.SetActive(true);
        if(isWin) 
        {
            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level", 1) + 1);
            nextLevelButton.interactable = true;
            audioSource.clip = winSound;
            audioSource.Play();
        }
        else 
        {
            nextLevelButton.interactable = false;
            audioSource.clip = loseSound;
            audioSource.Play();
        }
        
        if(doPause) Time.timeScale = 0;
        else Time.timeScale = 1;
    }
    public void ResetSave()
    {
        PlayerPrefs.SetInt("Level", 1);
    }
    public void OpenSkinShop()
    {
        SceneManager.LoadScene("SkinShop");
    }
    public void OpenUpgradeShop()
    {
        SceneManager.LoadScene("UpgradeShop");
    }
    public void OpenBtwLevelScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("BtwLevelScene");
    }
    public void Credits()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Credits");
    }
    public void MoreGames()
    {
        Application.OpenURL("https://itch.io/profile/snibaw");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void GiveFeedBack()
    {
        Application.OpenURL("https://forms.gle/RCGN2P7vvGjGbF9Q6");
    }
}