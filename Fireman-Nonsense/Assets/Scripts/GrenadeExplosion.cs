using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeExplosion : MonoBehaviour
{
    GameObject player;
    [SerializeField] GameObject BigExplosion;
    private float grenadeExplosionTime = 0.2f;
    private float grenadeExplosionTimer = 0f;
    bool readyToExplode = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player"); 
        grenadeExplosionTimer = grenadeExplosionTime;
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(player.transform.position, transform.position) < 3)
        {
            readyToExplode = true;
        }
        if (readyToExplode)
        {
            grenadeExplosionTimer -= Time.deltaTime;
            if (grenadeExplosionTimer <= 0)
            {
                Instantiate(BigExplosion, transform.position, Quaternion.identity);
                Debug.Log("EXPLOSION!!!");
                Destroy(gameObject);
                if (Vector3.Distance(player.transform.position, transform.position) < 2)
                {
                    player.GetComponent<PlayerHealth>().TakeDamage(10);
                }
            }
        }
    }
}
