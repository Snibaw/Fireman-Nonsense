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
    [SerializeField] private float DistanceMinToPlayer = 1.5f;
    private GameObject player;
    private Rigidbody rb;


    // Healthbar
    [SerializeField] private Healthbar healthBar;
    [SerializeField] private TMP_Text healthBarText;
    [SerializeField] private Image DMGSprite;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private GameObject DeathExplosion;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackRange = 1f;
    private bool isAttacking = false;
    private float currentHealth;
    private int combo = 0;
    private bool isDead = false;

    // Reset Rigidbody velocity after being hit
    private float timerBtwHitAndResetRb = 0;
    [SerializeField] private float timerBtwHitAndResetRbMax = 1f;
    private bool rbHasBeenReset = false;

    // Animation
    private Animator animator;
    [SerializeField] private bool isGrounded = true;

    private void Awake()
    {
        currentHealth = maxHealth;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
        healthBarText.text = "x" + combo.ToString();
        //Hide healthbar
        ShowHealthBar(false);
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if ennemy is on the ground or in the air
        float distanceToGround = GetComponent<Collider>().bounds.extents.y;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, distanceToGround + 0.1f);
        animator.SetBool("isGrounded", isGrounded);

        if(isGrounded)
        {
            timerBtwHitAndResetRb -= Time.deltaTime;
            //Move towards player
            if(Vector3.Distance(transform.position, player.transform.position) <= DistanceMinToPlayer && !isAttacking)
            {
                AttackPlayer();
            }
            else if(!isAttacking)
            {
                MoveTowardsPlayer();
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
    private void MoveTowardsPlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        transform.Translate(direction * Time.deltaTime * 5);
    }
    private void AttackPlayer()
    {
        isAttacking = true;
        animator.SetBool("isAttacking", isAttacking);
        //player.GetComponent<PlayerHealth>().TakeDamage(10f);
    }
    private void DamagePlayer()
    {
        if(Vector3.Distance(transform.position, player.transform.position) <= DistanceMinToPlayer + attackRange)
        {
            player.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        }
    }
    private void StopAttacking()
    {
        isAttacking = false;
        animator.SetBool("isAttacking", isAttacking);
    }
}
