using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpongeProjectile : MonoBehaviour
{
    private float speed;
    private float damage;
    private Vector3 Direction;
    private float lifeTime;
    // Update is called once per frame
    void Update()
    {
        // Move the projectile
        transform.position += Direction * speed * Time.deltaTime;
        // Rotate on X axis
        transform.Rotate(0, 6, 0);
    }

    public void Initialisation(float speed, float damage, Vector3 Direction, float lifeTime)
    {
        this.speed = speed;
        this.damage = damage;
        this.Direction = Direction;
        this.lifeTime = lifeTime;
        Destroy(gameObject, lifeTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player touched the projectile");
            GameObject player = other.gameObject;
            player.GetComponent<playerInput>().loseMana(damage);
            Destroy(gameObject);
        }
    }
}
