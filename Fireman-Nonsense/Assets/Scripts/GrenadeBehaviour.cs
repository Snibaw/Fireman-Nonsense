using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GrenadeBehaviour : MonoBehaviour
{
    private int throwSpeed = 20;
     public GameObject Grenade;
    GameObject player;
    double randomDistance;
    bool doneThrowing = false;
    bool doneCalculating = false;
    private double throwAngle;
    private float grenadeThrowTime = 2f;
    private float grenadeThrowTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        grenadeThrowTimer = grenadeThrowTime;
    }

    // Update is called once per frame
    void Update()
    {
        grenadeThrowTimer -= Time.deltaTime;
        if (grenadeThrowTimer <= 0)
        {
            DoTheMath();
            ThrowTheGrenade();
        }
        if(doneThrowing)
        {
            doneThrowing = false;
            doneCalculating = false;
            grenadeThrowTimer = grenadeThrowTime;
        }
    }

    void DoTheMath()
    {
        if (doneCalculating)
            return;
        player = GameObject.Find("Player");
        float zDistance = Math.Abs(player.transform.position.z - transform.position.z);
        float playerVelocity = player.GetComponent<Rigidbody>().velocity.z;
        var rand = new System.Random();
        double asin;
        randomDistance =  zDistance + 1;
        while (randomDistance >= zDistance)
        {
            double throwDistance = rand.NextDouble() * zDistance/2 + zDistance/2;
            asin = throwDistance * 9.8 / (Math.Pow(throwSpeed,2));
            if (asin > 1 || asin < -1)
                continue;
            throwAngle = Math.Asin(asin) / 2;
            //randomDistance = throwDistance + playerVelocity * randomDistance / (throwSpeed * Mathf.Cos((float) throwAngle));
            randomDistance = throwDistance / (1 - playerVelocity / (throwSpeed * Mathf.Cos((float) throwAngle)));
        }
            doneCalculating = true;
    }
    void ThrowTheGrenade()
    {
        if(Math.Abs(player.transform.position.z - transform.position.z) <= randomDistance && !doneThrowing)
        {
            Vector3 target = transform.position;
            target.y -= 1;
            GameObject grenade = Instantiate(Grenade, target, Quaternion.identity);
            grenade.transform.LookAt(player.transform);
            grenade.transform.Rotate((float) (-throwAngle * 180/Math.PI), 0, 0);
            grenade.GetComponent<Rigidbody>().velocity = grenade.transform.forward * throwSpeed;
            doneThrowing = true;
        }
    }
}
