using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjetToDestroyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private EndOfLevelManager endOfLevelManager;
    private int parentValue;
    private playerInput playerInput;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GameObject.Find("Player").GetComponent<playerInput>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        parentValue = int.Parse(transform.parent.name[5].ToString());
        maxHealth = endOfLevelManager.objectBasicHealth * (1 + parentValue*2*2);
        currentHealth = maxHealth;
        healthText.text = currentHealth.ToString();
    }
    private void OnParticleCollision(GameObject other) {
        if (other.name == "Water Steam")
        {
            currentHealth -= (0.01f + playerInput.GetDamageAddition())*playerInput.GetDamageMultiplier();
            healthText.text = Mathf.Round(currentHealth).ToString();
            if(currentHealth <= 0)
            {
                gameManager.EarnMoney((int)Mathf.Round(endOfLevelManager.objectBasicHealth * (1 + parentValue)*endOfLevelManager.moneyMultiplier));
                Destroy(gameObject);
            }
        }
    }  
}
