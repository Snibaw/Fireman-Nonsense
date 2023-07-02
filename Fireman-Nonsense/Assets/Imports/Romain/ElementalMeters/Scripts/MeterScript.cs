using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeterScript : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    [SerializeField] private GameObject miniWaterImage;
    [SerializeField] private float timerMaxBtwWaterAnimation = 0.2f;
    private float timerBtwWaterAnimation = 0f;
    private float manaEarnedBtwWaterAnimation = 0f;

    private void Start()
    {
        timerBtwWaterAnimation = timerMaxBtwWaterAnimation;
    }
    private void Update()
    {
        timerBtwWaterAnimation -= Time.deltaTime;
        if(timerBtwWaterAnimation <= 0 && manaEarnedBtwWaterAnimation != 0)
        {
            SpawnWaterAnimation();
            timerBtwWaterAnimation = timerMaxBtwWaterAnimation;
            manaEarnedBtwWaterAnimation = 0;
        }
    }


    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate (1f) ;

    }

    public void SetHealth(float health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);

    }

    public void WaterAnimation(float manaEarned)
    {
        timerBtwWaterAnimation = timerMaxBtwWaterAnimation;
        manaEarnedBtwWaterAnimation += manaEarned;       
    }
    public void SpawnWaterAnimation()
    {
        GameObject miniWater = Instantiate(miniWaterImage, transform.position, Quaternion.identity);
        miniWater.transform.SetParent(transform);
        string manaEarnedString = manaEarnedBtwWaterAnimation > 0 ? "+" + manaEarnedBtwWaterAnimation.ToString("0") : manaEarnedBtwWaterAnimation.ToString("0");
        miniWater.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = manaEarnedString;
        Destroy(miniWater, 1.5f);
    }

}
