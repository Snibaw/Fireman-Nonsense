using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LaserShopManager : MonoBehaviour
{
    [SerializeField] private GameObject iconImage;
    [SerializeField] private GameObject moneyImage;
    [SerializeField] private GameObject crystalImage;
    [SerializeField] private TMP_Text crystalText;
    [SerializeField] private TMP_Text moneyText;

    [SerializeField] private TMP_Text laserCost;
    [SerializeField] private int[] laserCostList;
    [SerializeField] private int[] hatCostList;
    [SerializeField] private Sprite[] hatSpriteList;
    [SerializeField] private Button buyButton;
    [SerializeField] private Button chooseButton;

    [SerializeField] private Button changeToLaserButton;
    [SerializeField] private Button changeToHatButton;

    [SerializeField] private HatManager hatManager;
    private Hovl_DemoLasers hovl_DemoLasers;
    private int numberOfPrefabs;
    private int numberOfHats;
    private int index = 0;
    private int indexHat = 0;
    private float laserScale = 1;
    private int money = 0;
    private int crystal = 0;
    private AudioSource audioSource;
    private int shopType = 0; // 0: Laser, 1: Hat

    /*

    Need to rewrite the code to be cleaner

    */

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        PlayerPrefs.SetInt("Laser0",1);
        PlayerPrefs.SetInt("hat0",1);

        hovl_DemoLasers = GameObject.Find("Player").GetComponent<Hovl_DemoLasers>();
        hovl_DemoLasers.StartShooting();

        numberOfPrefabs = hovl_DemoLasers.Prefabs.Length;
        numberOfHats = hatManager.hats.Length;

        crystal = PlayerPrefs.GetInt("Crystal",0);
        crystalText.text = crystal.ToString();

        money = PlayerPrefs.GetInt("Money",0);
        moneyText.text = money.ToString(); 

        changeToLaserButton.interactable = false;
        changeToHatButton.interactable = true;

        ChangeLaser(0);
        hatManager.ChooseHat(0);
    }

    public void ChangeItem(int i)
    {
        if(shopType == 1) 
        {
            ChangeHat(i);
        }
        else
        {
            ChangeLaser(i);
        }
    }
    
    public void ChangeHat(int i)
    {
        indexHat += i;
        if(indexHat < 0) indexHat = numberOfHats - 1;
        if(indexHat >= numberOfHats) indexHat = 0;
        hatManager.ChooseHat(indexHat);      

        UpdateCostAndHatButton(); 
    }
    private void UpdateCostAndHatButton()
    {
        iconImage.SetActive(true);
        iconImage.GetComponent<Image>().sprite = hatSpriteList[indexHat];
        laserCost.text = hatCostList[indexHat].ToString();
        UpdateMoneyImage(hatCostList[indexHat]);
        if(PlayerPrefs.GetInt("Hat"+indexHat,0) == 1)
        {
            buyButton.gameObject.SetActive(false);
            chooseButton.gameObject.SetActive(true);
            if(PlayerPrefs.GetInt("Hat",0) == indexHat)
            {
                chooseButton.interactable = false;
            }
            else
            {
                chooseButton.interactable = true;
            }
        }
        else
        {
            buyButton.gameObject.SetActive(true);
            buyButton.interactable = true;
            chooseButton.gameObject.SetActive(false);
        }
    }
    private void UpdateMoneyImage(int value)
    {
        if(value < 1000 && value !=0)
        {
            crystalImage.SetActive(true);
            moneyImage.SetActive(false);
        }
        else
        {
            crystalImage.SetActive(false);
            moneyImage.SetActive(true);
        }

    }
    public void ChangeLaser(int i)
    {
        index += i;
        if(index < 0) index = numberOfPrefabs - 1;
        if(index >= numberOfPrefabs) index = 0;
        hovl_DemoLasers.Prefab = index;
        hovl_DemoLasers.StopShooting();
        hovl_DemoLasers.StartShooting();

        UpdateCostAndLaserButton();

    }
    private void UpdateCostAndLaserButton()
    {
        iconImage.SetActive(false);
        laserCost.text = laserCostList[index].ToString();
        UpdateMoneyImage(laserCostList[index]);
        if(PlayerPrefs.GetInt("Laser"+index,0) == 1)
        {
            buyButton.gameObject.SetActive(false);
            chooseButton.gameObject.SetActive(true);
            if(PlayerPrefs.GetInt("Laser",0) == index)
            {
                chooseButton.interactable = false;
            }
            else
            {
                chooseButton.interactable = true;
            }
        }
        else
        {
            buyButton.gameObject.SetActive(true);
            buyButton.interactable = true;
            chooseButton.gameObject.SetActive(false);
        }
    }

    public void changeToLaserShop()
    {
        shopType = 0;
        changeToLaserButton.interactable = false;
        changeToHatButton.interactable = true;
        UpdateCostAndLaserButton();
        
    }
    public void changeToHatShop()
    {
        shopType = 1;
        changeToLaserButton.interactable = true;
        changeToHatButton.interactable = false;
        UpdateCostAndHatButton();
    }
    public void BuyItem()
    {
        if(shopType == 1) 
        {
            BuyHat();
        }
        else
        {
            BuyLaser();
        }
    }
    public void BuyHat()
    {
        if(hatCostList[indexHat] < 1000)
        {
            if(crystal >= hatCostList[indexHat])
            {
                audioSource.Play();
                // Update Crystal
                crystal -= hatCostList[indexHat];
                PlayerPrefs.SetInt("Crystal",crystal);
                crystalText.text = crystal.ToString();

                // Update Hat
                PlayerPrefs.SetInt("Hat", indexHat);
                PlayerPrefs.SetInt("Hat"+indexHat,1);

                buyButton.gameObject.SetActive(false);
                chooseButton.gameObject.SetActive(true);
                chooseButton.interactable = false;
            }
        }
        else
        {
            if(money >= hatCostList[indexHat])
            {
                audioSource.Play();
                // Update Money
                money -= hatCostList[indexHat];
                PlayerPrefs.SetInt("Money",money);
                moneyText.text = money.ToString();

                // Update Hat
                PlayerPrefs.SetInt("Hat", indexHat);
                PlayerPrefs.SetInt("Hat"+indexHat,1);

                buyButton.gameObject.SetActive(false);
                chooseButton.gameObject.SetActive(true);
                chooseButton.interactable = false;
            }
        }
        
    }
    public void BuyLaser()
    {
        if(laserCostList[index] < 1000)
        {
            if(crystal >= laserCostList[index])
            {
                audioSource.Play();
                // Update Crystal
                crystal -= laserCostList[index];
                PlayerPrefs.SetInt("Crystal",crystal);
                crystalText.text = crystal.ToString();

                // Update Laser
                PlayerPrefs.SetInt("Laser", index);
                PlayerPrefs.SetInt("Laser"+index,1);

                buyButton.gameObject.SetActive(false);
                chooseButton.gameObject.SetActive(true);
                chooseButton.interactable = false;
            }
        }
        else
        {
            if(money >= laserCostList[index])
            {
                audioSource.Play();
                // Update Money
                money -= laserCostList[index];
                PlayerPrefs.SetInt("Money",money);
                moneyText.text = money.ToString();

                // Update Laser
                PlayerPrefs.SetInt("Laser", index);
                PlayerPrefs.SetInt("Laser"+index,1);

                buyButton.gameObject.SetActive(false);
                chooseButton.gameObject.SetActive(true);
                chooseButton.interactable = false;
            }
        }
            
    }
    public void ChooseItem()
    {
        if(shopType == 1) 
        {
            ChooseHat();
        }
        else
        {
            ChooseLaser();
        }
    }
    public void ChooseHat()
    {
        PlayerPrefs.SetInt("Hat", indexHat);
        chooseButton.interactable = false;
    }
    public void ChooseLaser()
    {
        PlayerPrefs.SetInt("Laser", index);
        chooseButton.interactable = false;
    }
    public void ExitShop()
    {
        SceneManager.LoadScene("BtwLevelScene");
    }
}
