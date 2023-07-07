using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest")]
public class Quest : ScriptableObject
{
    public string title;
    public string description;
    public int rewardAmount;
    public string rewardType;
    public Sprite rewardIcon;
    public int progress;
    public int goal;
    public bool completed;
    public int questRarity;
    public string playerPrefs;
}
