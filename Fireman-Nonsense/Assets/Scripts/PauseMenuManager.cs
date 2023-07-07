using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
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
    [SerializeField] private GameObject levelBonusText;

    [Header("Infinite")]
    [SerializeField] private GameObject EndOfLevelInfinite;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highScoreText;

    [Header("Settings")]
    [SerializeField] private GameObject tickVibration;
    [SerializeField] private GameObject tickLowQuality;
    [SerializeField] private GameObject tickHighQuality;
    [SerializeField] private GameObject tickMusic;
    [SerializeField] private GameObject tickSound;

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private AudioMixer audioMixer;

    [Header("Tuto")]
    [SerializeField] private GameObject[] tutoObjectsToHide;
    [SerializeField] private GameObject[] tutoObjectsToShow;
    private int level;
    

    private int mutedSound;
    private int mutedMusic;

    private GameManager gameManager;
    private int highScore = 0;
    private bool isInfinite = false;
    private bool isRandom = false;
    private bool isBossScene = false;

    private void Start()
    {
        isRandom= SceneManager.GetActiveScene().name == "LevelRandom";
        isBossScene = SceneManager.GetActiveScene().name == "BossScene";
        level = PlayerPrefs.GetInt("Level", 1);
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        isInfinite = PlayerPrefs.GetInt("Mode",0) == 1;
        audioSource = GetComponent<AudioSource>();
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        creditButton.SetActive(false);
        mainMenuButton.SetActive(true);
        creditButton.SetActive(true);
        if(!isBtwLevelScene) 
        {
            creditButton.SetActive(false);
            EndOfLevel.SetActive(false);
            if(isInfinite) gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
        if(isRandom)
        {
            levelBonusText.SetActive(false);
            if(level%5 == 0) SceneManager.LoadScene("BossScene");
        }
        // Settings
        UpdateVibration();
        UpdateGraphics();
        UpdateMusic();
        UpdateSound();

        //Tuto
        CheckIfTuto();

        if(isRandom)
        {
            CheckIfBonusLevel();
        }
        
    }

    public void OpenPauseMenu()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        settingsButton.SetActive(false);
        
        if(isBtwLevelScene) 
        {
            TapToStart.SetActive(false);
        }
    }
    public void ClosePauseMenu()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        settingsButton.SetActive(true);
        if(isBtwLevelScene) 
        {
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
        if(!isBtwLevelScene) pauseMenu.SetActive(true);
    }
    public void NextLevel()
    {
        Time.timeScale = 1;
        if(PlayerPrefs.GetInt("Mode",0) == 0) SceneManager.LoadScene("LevelRandom");
        else SceneManager.LoadScene("LevelInfini");
    }
    public void OpenEndOfLevel(bool doPause = false, bool isWin = true)
    {
        if(isInfinite && !isBossScene)
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

        EndOfLevelNumberOfLevel.GetComponent<TMP_Text>().text = "Level " + level.ToString();

        EndOfLevel.SetActive(true);
        if(isWin) 
        {
            PlayerPrefs.SetInt("Level", level + 1);
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
    // Settings
    public void UpdateVibration()
    {
        if(PlayerPrefs.GetInt("Vibration", 1) == 1)
        {
            tickVibration.SetActive(true);
        }
        else
        {
            tickVibration.SetActive(false);
        }
    }
    public void ChangeVibration()
    {
        PlayerPrefs.SetInt("Vibration", PlayerPrefs.GetInt("Vibration", 1) == 1 ? 0 : 1);
        UpdateVibration();
    }
    public void UpdateGraphics()
    {
        if(PlayerPrefs.GetInt("Quality", 0) == 1)
        {
            tickLowQuality.SetActive(false);
            tickHighQuality.SetActive(true);
        }
        else
        {
            tickLowQuality.SetActive(true);
            tickHighQuality.SetActive(false);
        }
    }
    public void ChangeGraphics(int graphicValue)
    {
        PlayerPrefs.SetInt("Quality", graphicValue);
        UpdateGraphics();
    }
    public void UpdateMusic()
    {
        mutedMusic = PlayerPrefs.GetInt("MutedMusic", 0);
        if(mutedMusic == 1)
        {
            tickMusic.SetActive(false);
            musicSlider.value = PlayerPrefs.GetFloat("Music", 0);
            audioMixer.SetFloat("Music", -80);
        }
        else
        {
            tickMusic.SetActive(true);
            musicSlider.value = PlayerPrefs.GetFloat("Music", 0);
            audioMixer.SetFloat("Music", musicSlider.value);
        }
    }
    public void UpdateSound()
    {
        mutedSound = PlayerPrefs.GetInt("MutedSound", 0);
        if(mutedSound == 1)
        {
            tickSound.SetActive(false);
            soundSlider.value = PlayerPrefs.GetFloat("Sound", 0);
            audioMixer.SetFloat("Sound", -80);
        }
        else
        {
            tickSound.SetActive(true);
            soundSlider.value = PlayerPrefs.GetFloat("Sound", 0);
            audioMixer.SetFloat("Sound", soundSlider.value);
        }
    }
    public void SetMusic()
    {
        if(mutedMusic == 0)
        {
            audioMixer.SetFloat("Music", musicSlider.value);   
        }
        PlayerPrefs.SetFloat("Music", musicSlider.value); 

    }
    public void SetSound()
    {
        if(mutedSound == 0)
        {
            audioMixer.SetFloat("Sound", soundSlider.value);
        }
        PlayerPrefs.SetFloat("Sound", soundSlider.value);
    }
    public void EnableMusic()
    {
        if(mutedMusic == 0)
        {
            audioMixer.SetFloat("Music", -80);
            mutedMusic = 1;
            PlayerPrefs.SetInt("MutedMusic", 1);
            tickMusic.SetActive(false);
        }
        else
        {
            audioMixer.SetFloat("Music", musicSlider.value);
            mutedMusic = 0;
            PlayerPrefs.SetInt("MutedMusic", 0);
            tickMusic.SetActive(true);
        }
    }
    public void EnableSound()
    {
        if(mutedSound == 0)
        {
            audioMixer.SetFloat("Sound", -80);
            mutedSound = 1;
            PlayerPrefs.SetInt("MutedSound", 1);
            tickSound.SetActive(false);
        }
        else
        {
            audioMixer.SetFloat("Sound", soundSlider.value);
            mutedSound = 0;
            PlayerPrefs.SetInt("MutedSound", 0);
            tickSound.SetActive(true);
        }
    }
    private void CheckIfTuto()
    {
        if(isBtwLevelScene) return;
        if(isInfinite) 
        {
            if(PlayerPrefs.GetInt("TutoInfinite", 0) == 0)
            {
                OpenTuto();
            }
        }
        else
        {
            if(PlayerPrefs.GetInt("TutoCampagne", 0) == 0)
            {
                OpenTuto();
            }
        }
    }
    private void OpenTuto()
    {
        Time.timeScale = 0;
        foreach(GameObject tuto in tutoObjectsToShow)
        {
            tuto.SetActive(true);
        }
        foreach(GameObject tuto in tutoObjectsToHide)
        {
            tuto.SetActive(false);
        }
    }
    public void CloseTuto()
    {
        Time.timeScale = 1;
        foreach(GameObject tuto in tutoObjectsToShow)
        {
            tuto.SetActive(false);
        }
        foreach(GameObject tuto in tutoObjectsToHide)
        {
            tuto.SetActive(true);
        }
        if(isInfinite) 
        {
            PlayerPrefs.SetInt("TutoInfinite", 1);
        }
        else
        {
            PlayerPrefs.SetInt("TutoCampagne", 1);
        }
    }
    private void CheckIfBonusLevel()
    {
        if(level % 4 == 0)
        {
            StartCoroutine(SpawnLevelBonusText());
        }
    }
    private IEnumerator SpawnLevelBonusText()
    {
        yield return new WaitForSeconds(1f);
        levelBonusText.SetActive(true);
        Destroy(levelBonusText, 2f);
        yield return new WaitForSeconds(0.2f);
        levelBonusText.GetComponent<AudioSource>().Play();
    }
    public void OpenQuestScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("QuestScene");
    }
}