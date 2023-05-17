using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemie : MonoBehaviour
{
    // [SerializeField] private float yKnockbackMultiplier = 15;
    [SerializeField] private float globalKnockbackMultiplier = 1;
    [SerializeField] private float yknockBackEveryHit = 1;
    private GameObject player;
    private Rigidbody rb;

    // Reset Rigidbody velocity after being hit
    private float timerBtwHitAndResetRb = 0;
    [SerializeField] private float timerBtwHitAndResetRbMax = 1f;
    private bool rbHasBeenReset = false;

    // Start is called before the first frame update
    void Start()
    {
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

        if(timerBtwHitAndResetRb <= 0 && !rbHasBeenReset)
        {
            rb.velocity = Vector3.zero;
            rbHasBeenReset = true;
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
        }
    }
}
