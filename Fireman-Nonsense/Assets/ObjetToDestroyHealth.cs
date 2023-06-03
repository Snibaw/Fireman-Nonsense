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
    private playerInput playerInput;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GameObject.Find("Player").GetComponent<playerInput>();
        // Get the 6th caracter of the parent name to int
        int parentValue = int.Parse(transform.parent.name[5].ToString());
        Debug.Log(parentValue);
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
                Destroy(gameObject);
            }
        }
    }  
}
