using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
// [SerializeField] private float yKnockbackMultiplier = 15;
    [SerializeField] private float globalKnockbackMultiplier = 1;
    [SerializeField] private float yknockBackEveryHit = 1;
    private Rigidbody rb;


    // Healthbar
    [SerializeField] private Healthbar healthBar;
    [SerializeField] private TMP_Text healthBarText;
    [SerializeField] private Image DMGSprite;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private GameObject DeathExplosion;
    private float currentHealth;
    private int combo = 0;
    public bool isDead = false;
    // Reset Rigidbody velocity after being hit
    private float timerBtwHitAndResetRb = 0;
    [SerializeField] private float timerBtwHitAndResetRbMax = 1f;
    private bool rbHasBeenReset = false;

    public bool isGrounded = true;


    private void Awake()
    {
        currentHealth = maxHealth;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
        healthBarText.text = "x" + combo.ToString();
        //Hide healthbar
        ShowHealthBar(false);
        rb = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        float distanceToGround = GetComponent<Collider>().bounds.extents.y;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, distanceToGround + 0.1f);

        if(isGrounded)
        {
            timerBtwHitAndResetRb -= Time.deltaTime;
        }
        if(timerBtwHitAndResetRb <= 0 && !rbHasBeenReset && !isDead)
        {
            rb.velocity = Vector3.zero;
            rbHasBeenReset = true;
            combo = 0;
        }
        // If ennemy is behind the camera
        if(transform.position.z < Camera.main.transform.parent.position.z && isGrounded)
        {
            //Give malus to player later
            Destroy(gameObject);
            
        }
        // If enemy is out of bounds
        if(transform.position.y < -10 && !isDead)
        {
            //Give a certain score of combo to the player
            Destroy(gameObject);
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
        if(!isDead)
        {
            isDead = true;
            // Play death animation
            var randomSigne = Random.Range(0,2) == 1 ? 1 : -1;
            rb.velocity = new Vector3(randomSigne*Random.Range(10,15),Random.Range(15,20),Random.Range(25,35));
            GameObject[] explosion = new GameObject[3];
            for(int i=0;i<3;i++)
            {
                explosion[i] = Instantiate(DeathExplosion, transform.position, Quaternion.identity);
                 yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(1f);
            foreach(GameObject explo in explosion)
            {
                Destroy(explo);
            }
            // Wait for the animation to finish
            yield return new WaitForSeconds(10f);
            // Destroy the enemy
            Destroy(gameObject);
        }
    }
}
