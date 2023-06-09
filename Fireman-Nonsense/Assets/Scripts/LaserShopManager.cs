using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LaserShopManager : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text laserCost;
    [SerializeField] private int[] laserCostList;
    [SerializeField] private Button buyButton;
    [SerializeField] private Button chooseButton;
    private Hovl_DemoLasers hovl_DemoLasers;
    private int numberOfPrefabs;
    private int index = 0;
    private float laserScale = 1;
    private int money = 0;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayerPrefs.SetInt("Laser0",1);
        hovl_DemoLasers = GameObject.Find("Sphere").GetComponent<Hovl_DemoLasers>();
        hovl_DemoLasers.StartShooting();
        numberOfPrefabs = hovl_DemoLasers.Prefabs.Length;
        
        money = PlayerPrefs.GetInt("Money",0);
        moneyText.text = money.ToString(); 

        ChangeLaser(0);
    }
    public void ChangeLaser(int i)
    {
        index += i;
        if(index < 0) index = numberOfPrefabs - 1;
        if(index >= numberOfPrefabs) index = 0;
        hovl_DemoLasers.Prefab = index;
        hovl_DemoLasers.StopShooting();
        hovl_DemoLasers.StartShooting();

        laserCost.text = laserCostList[index].ToString();
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
    public void ChangeLaserScale(int i)
    {
        laserScale = laserScale+i/2f;

        if(laserScale < 1)
        {
            laserScale = 1; 
            return;
        }
        else if(laserScale > 3)
        {
            laserScale = 3; 
            return;
        } 

        hovl_DemoLasers.laserScale = laserScale;
        hovl_DemoLasers.StopShooting();
        hovl_DemoLasers.StartShooting();

    }
    public void BuyLaser()
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
