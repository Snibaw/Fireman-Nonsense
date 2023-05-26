using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrickBehaviour : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private float attackSpeed = 2;
    [SerializeField] private GameObject SpongeBob;
    private float attackSpeedTimer;
    [SerializeField] private float maxhealth;
    private float currentHealth;
    private bool isRunning = false;

    private Rigidbody rb;
    private GameObject player;
    private Animator animator;
    private EnemyHealth enemyHealth;
    [SerializeField] private GameObject rightHand;

    private float distanceToPlayer;
    [SerializeField] private float minDistanceToPlayer = 10f;
    [SerializeField] private float maxDistanceToPlayer = 30f;
    [SerializeField] private int numberOfRunMax = 3;
    private int numberOfRun;
    [SerializeField] private float timeOfTheRun = 4f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
        player = GameObject.FindGameObjectWithTag("Player");

        currentHealth = maxhealth;
        attackSpeedTimer = attackSpeed;
        numberOfRun = numberOfRunMax;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isGrounded", enemyHealth.isGrounded);
        if(enemyHealth.isDead || !enemyHealth.isGrounded) return;
        attackSpeedTimer -= Time.deltaTime;
        if(isRunning)
        {
            rb.velocity = new Vector3((player.transform.position.x-transform.position.x)*0.05f,0,1) * speed;
        }
        else
        {
            distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            transform.LookAt(player.transform);


            if (distanceToPlayer < maxDistanceToPlayer && distanceToPlayer > minDistanceToPlayer)
            {
                isRunning = false;
                ShootThePlayer();
            }
            else if(distanceToPlayer < minDistanceToPlayer)
            {
                if(numberOfRun > 0)
                {
                    StartCoroutine(RunAway());
                }
                else
                {
                    isRunning = false;
                    ShootThePlayer();
                }
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        }
    }
    private void ShootThePlayer()
    {
        rb.velocity = Vector3.zero;
        animator.SetBool("isRunning", isRunning);
        if(attackSpeedTimer <= 0)
        {
            attackSpeedTimer = attackSpeed;
            animator.SetTrigger("Throw");
        }
    }
    private IEnumerator RunAway()
    {
        isRunning = true;
        animator.SetBool("isRunning", isRunning);
        numberOfRun--;
        yield return new WaitForSeconds(timeOfTheRun);
        isRunning = false;
        animator.SetBool("isRunning", isRunning);
    }
    public void Throw()
    {
        GameObject SpongeBobProjectile = Instantiate(SpongeBob, rightHand.transform.position + new Vector3(0.3f,-0.3f,-0.5f), Quaternion.identity);
        SpongeBobProjectile.GetComponent<SpongeProjectile>().Initialisation(8, damage, transform.forward, 10f);
    }
    
}
