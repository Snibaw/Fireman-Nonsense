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
    [SerializeField] private int[] backpackCostList;
    [SerializeField] private Sprite[] hatSpriteList;
    [SerializeField] private Sprite[] backpackSpriteList;
    [SerializeField] private Button buyButton;
    [SerializeField] private Button chooseButton;

    [SerializeField] private Button[] ChangeShopTypeButtons;

    [SerializeField] private HatManager hatManager;
    [SerializeField] private BagManager backpackManager;

    private Hovl_DemoLasers hovl_DemoLasers;
    private int numberOfPrefabs;
    private int numberOfHats;
    private int numberOfBackpacks;
    private int index = 0;
    private int indexHat = 0;
    private int indexBP = 0;
    private float laserScale = 1;
    private long money = 0;
    private int crystal = 0;
    private AudioSource audioSource;
    private int shopType = 0; // 0: Laser, 1: Hat, 2: Backpack

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
        numberOfBackpacks = backpackManager.backpacks.Length;

        crystal = PlayerPrefs.GetInt("Crystal",0);
        crystalText.text = crystal.ToString();

        money = long.Parse(PlayerPrefs.GetString("Money","0"));
        moneyText.text = money.ToString(); 

        ChangeShopTypeButtons[0].interactable = false;
        ChangeShopTypeButtons[1].interactable = true;
        ChangeShopTypeButtons[2].interactable = true;

        ChangeItem(0);
        // hatManager.ChooseHat(0);
        // backpackManager.ChooseBackpack(0);
    }

    public void ChangeItem(int i)
    {
        if(shopType == 0) 
        {
            ChangeLaser(i);
        }
        else if(shopType == 1)
        {
            ChangeHat(i);
        }
        else if(shopType == 2)
        {
            ChangeBackpack(i);
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
    public void ChangeBackpack(int i)
    {
        indexBP += i;
        if(indexBP < 0) indexBP = numberOfBackpacks - 1;
        if(indexBP >= numberOfBackpacks) indexBP = 0;
        backpackManager.ChooseBackpack(indexBP);      

        UpdateCostAndBackpackButton(); 
    }
    public void UpdateCostAndBackpackButton()
    {
        if(indexBP == 0) iconImage.SetActive(false);
        else
        {
            iconImage.SetActive(true);
            iconImage.GetComponent<Image>().sprite = backpackSpriteList[indexBP-1];
        }
        laserCost.text = backpackCostList[indexBP].ToString();
        UpdateMoneyImage(backpackCostList[indexBP]);
        if(PlayerPrefs.GetInt("Backpack"+indexBP,0) == 1)
        {
            buyButton.gameObject.SetActive(false);
            chooseButton.gameObject.SetActive(true);
            if(PlayerPrefs.GetInt("Backpack",0) == indexBP)
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

    public void ChangeShopType(int i)
    {
        shopType = i;
        foreach(Button button in ChangeShopTypeButtons)
        {
            button.interactable = true;
        }
        ChangeShopTypeButtons[i].interactable = false;

        if(shopType == 0)
        {
            UpdateCostAndLaserButton();
        }
        else if(shopType == 1)
        {
            UpdateCostAndHatButton();
        }
        else if(shopType == 2)
        {
            UpdateCostAndBackpackButton();
        }
    }

    public void BuyItem()
    {
        if(shopType == 0) 
        {
            BuyLaser();
        }
        else if(shopType == 1)
        {
            BuyHat();
        }
        else
        {
            BuyBackpack();
        }
    }
    public void BuyBackpack()
    {
        if(backpackCostList[indexBP] < 1000)
        {
            if(crystal >= backpackCostList[indexBP])
            {
                audioSource.Play();
                // Update Crystal
                crystal -= backpackCostList[indexBP];
                PlayerPrefs.SetInt("Crystal",crystal);
                crystalText.text = crystal.ToString();

                // Update Backpack
                PlayerPrefs.SetInt("Backpack", indexBP);
                PlayerPrefs.SetInt("Backpack"+indexBP,1);

                buyButton.gameObject.SetActive(false);
                chooseButton.gameObject.SetActive(true);
                chooseButton.interactable = false;
            }
        }
        else
        {
            if(money >= backpackCostList[indexBP])
            {
                audioSource.Play();
                // Update Money
                money -= backpackCostList[indexBP];
                PlayerPrefs.SetString("Money",money.ToString());
                moneyText.text = money.ToString();

                // Update Backpack
                PlayerPrefs.SetInt("Backpack", indexBP);
                PlayerPrefs.SetInt("Backpack"+indexBP,1);

                buyButton.gameObject.SetActive(false);
                chooseButton.gameObject.SetActive(true);
                chooseButton.interactable = false;
            }
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
                PlayerPrefs.SetString("Money",money.ToString());
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
                PlayerPrefs.SetString("Money",money.ToString());
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
        if(shopType == 0) 
        {
            ChooseLaser();
        }
        else if(shopType == 1)
        {
            ChooseHat();
        }
        else
        {
            ChooseBackpack();
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
    public void ChooseBackpack()
    {
        PlayerPrefs.SetInt("Backpack", indexBP);
        chooseButton.interactable = false;
    }
    public void ExitShop()
    {
        SceneManager.LoadScene("BtwLevelScene");
    }
}
