using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBossScript : MonoBehaviour
{
    [SerializeField] private GameObject[] flame;
    [SerializeField] private GolemBossBehaviour golemBossBehaviour;
    private int quality;

    private void Start()
    {
        quality = PlayerPrefs.GetInt("Quality", 0);
        if(quality == 0)
        {
            for(int i = 0; i < flame.Length; i++)
            {
                flame[i].SetActive(false);
            }
        }
    }
    public void HitByRay()
    {
        golemBossBehaviour.TakeDamage(3f+ 6*PlayerPrefs.GetFloat("UpgradeValue2",0.01f)*10);
    }
}
