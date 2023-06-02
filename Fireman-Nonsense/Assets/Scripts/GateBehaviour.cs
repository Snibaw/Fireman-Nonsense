using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GateBehaviour : MonoBehaviour
{
    [SerializeField] private float value;
    [SerializeField] private string gateName;
    [SerializeField] private bool isValueMultiplier;
    [SerializeField] private TMP_Text bottomText;
    [SerializeField] private TMP_Text topText;
    private playerInput playerInput;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GameObject.Find("Player").GetComponent<playerInput>();
        UpdateBottomText();
        topText.text = gateName;
    }
    private void OnParticleCollision(GameObject other) {
        if (other.name == "Water Steam")
        {
            if(isValueMultiplier)
            {
                value += (0.005f + playerInput.GetDamageAddition())*playerInput.GetDamageMultiplier();
            }
            else
            {
                value += (0.01f + playerInput.GetDamageAddition())*playerInput.GetDamageMultiplier();
            }
            UpdateBottomText();
            ModifyValueExceptionCases();
        }
    }   

    private void UpdateBottomText()
    {
        if(value>=0)
        bottomText.text = isValueMultiplier ? "x" + (Mathf.Round(value*10)/10).ToString() : "+" + (Mathf.Round(value*10)/10).ToString();
        else
        bottomText.text = isValueMultiplier ? "/" + (Mathf.Round(-value*10)/10).ToString() : (Mathf.Round(value*10)/10).ToString();
    }
    private void ModifyValueExceptionCases()
    {
        if(isValueMultiplier && value > -1 && value < 0)
        {
            value = -value;
        }
    }
    public float Getvalue()
    {
        return this.value;
    }
    public string GetgateName()
    {
        return this.gateName;
    }
    public bool GetisValueMultiplier()
    {
        return this.isValueMultiplier;
    }
    
}
