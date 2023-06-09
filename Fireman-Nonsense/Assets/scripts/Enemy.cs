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
    [SerializeField] private float DistanceMaxToPlayer = 40f;
    private GameObject player;
    private Rigidbody rb;
    private AudioSource audioSource;
    private int quality;
    private bool firstTimeMoveTowardsPlayer = true;


    // Healthbar
    [SerializeField] private Healthbar healthBar;
    [SerializeField] private TMP_Text healthBarText;
    [SerializeField] private Image DMGSprite;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private GameObject DeathExplosion;
    [SerializeField] private float attackDamage = 200f;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float manaEarnWhenKilled = 150f;
    private bool isAttacking = false;
    private bool isMoving = false;
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
        audioSource = GetComponent<AudioSource>();
        quality = PlayerPrefs.GetInt("Quality", 0);
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
            else if(!isAttacking && Vector3.Distance(transform.position, player.transform.position) <= DistanceMaxToPlayer)
            {
                MoveTowardsPlayer();
            }
            else
            {
                isMoving = false;
                animator.SetBool("isMoving", isMoving);
            }
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
    private void HitByRay()
    {
        // Get knocked back his rigidbody if hit by water
        Vector3 direction = (transform.position - player.transform.position).normalized;
        direction.y = yknockBackEveryHit;
        rb.AddForce(direction*globalKnockbackMultiplier);
        timerBtwHitAndResetRb=timerBtwHitAndResetRbMax;
        rbHasBeenReset = false;

        // Take damage
        combo++;
        healthBarText.text = "x" + combo.ToString();
        currentHealth -= PlayerPrefs.GetFloat("UpgradeValue2",0.01f)+1.5f+Mathf.Min(3,player.GetComponent<playerInput>().GetDamageAddition()*player.GetComponent<playerInput>().GetDamageMultiplier());
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
        ShowHealthBar(true);
        if(currentHealth <= 0)
        {
            StartCoroutine(Death());
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
            PlayerPrefs.SetInt("EnemyKilled",PlayerPrefs.GetInt("EnemyKilled",0)+1);
            player.GetComponent<playerInput>().ChangeCurrentMana(manaEarnWhenKilled, Vibrator.vibrateTimeItem);
            // Play death animation
            var randomSigne = Random.Range(0,2) == 1 ? 1 : -1;
            rb.velocity = new Vector3(randomSigne*Random.Range(5,10),Random.Range(8,15),Random.Range(30,60));
            if(quality == 1)
            {
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
            }
            // Wait for the animation to finish
            yield return new WaitForSeconds(5f);
            // Destroy the enemy
            Destroy(gameObject);
        }
    }
    private void MoveTowardsPlayer()
    {
        if(firstTimeMoveTowardsPlayer) 
        {
            audioSource.Play();
            firstTimeMoveTowardsPlayer = false;
        }
        isMoving = true;
        animator.SetBool("isMoving", isMoving);
        Vector3 direction = (player.transform.position - transform.position).normalized;
        transform.Translate(direction * Time.deltaTime * speed);
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
            player.GetComponent<playerInput>().ChangeCurrentMana(-attackDamage, Vibrator.vibrateTimeDamage);
        }
    }
    private void StopAttacking()
    {
        isAttacking = false;
        animator.SetBool("isAttacking", isAttacking);
    }
}
