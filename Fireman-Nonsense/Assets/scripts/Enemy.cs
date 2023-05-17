using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy : MonoBehaviour
{
    // [SerializeField] private float yKnockbackMultiplier = 15;
    [SerializeField] private float globalKnockbackMultiplier = 1;
    [SerializeField] private float yknockBackEveryHit = 1;
    private GameObject player;
    private Rigidbody rb;

    // Healthbar
    [SerializeField] private Healthbar healthBar;
    [SerializeField] private TMP_Text healthBarText;
    [SerializeField] private Image DMGSprite;
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;
    private int combo = 0;
    private bool isDead = false;

    // Reset Rigidbody velocity after being hit
    private float timerBtwHitAndResetRb = 0;
    [SerializeField] private float timerBtwHitAndResetRbMax = 1f;
    private bool rbHasBeenReset = false;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
        healthBarText.text = "x" + combo.ToString();
        //Hide healthbar
        ShowHealthBar(false);
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the enemy is on the ground
        if(transform.position.y <=1.5f)
        {
            timerBtwHitAndResetRb -= Time.deltaTime;
            //Move towards player
            if(Vector3.Distance(transform.position, player.transform.position) >= 1.5f)
            {
                Vector3 direction = (player.transform.position - transform.position).normalized;
                transform.Translate(direction * Time.deltaTime * 5);
            }

        }

        if(timerBtwHitAndResetRb <= 0 && !rbHasBeenReset && !isDead)
        {
            rb.velocity = Vector3.zero;
            rbHasBeenReset = true;
            combo = 0;
        }
    }
    private void OnParticleCollision(GameObject other) {
        // Get knocked back his rigidbody if hit by water
        if (other.name == "Water Steam")
        {
            Vector3 direction = (transform.position - other.transform.position).normalized;
            direction.y = yknockBackEveryHit;
            rb.AddForce(direction*globalKnockbackMultiplier);
            timerBtwHitAndResetRb=timerBtwHitAndResetRbMax;
            rbHasBeenReset = false;

            // Take damage
            combo++;
            healthBarText.text = "x" + combo.ToString();
            currentHealth -= 1f;
            healthBar.UpdateHealthBar(currentHealth, maxHealth);
            ShowHealthBar(true);
            if(currentHealth <= 0)
            {
                StartCoroutine(Death());
            }
        }
    }
    private void ShowHealthBar(bool show)
    {
        healthBar.gameObject.SetActive(show);
        DMGSprite.gameObject.SetActive(show);
        healthBarText.gameObject.SetActive(show);
    }
    private IEnumerator Death()
    {
        isDead = true;
        var randomSigne = Random.Range(0,2) == 1 ? 1 : -1;
        rb.velocity = new Vector3(randomSigne*Random.Range(10,15),Random.Range(15,20),Random.Range(25,35));
        // Wait for the animation to finish
        yield return new WaitForSeconds(10f);
        // Destroy the enemy
        Destroy(gameObject);
    }

}
