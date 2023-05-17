using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInput : MonoBehaviour
{
    private ParticleSystem Water_Steam;
    private Rigidbody rb;
    private float xBorderCoo = 4.5f;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Water_Steam = GameObject.Find("Water Steam").GetComponent<ParticleSystem>();
    }
 // Update is called once per frame
 void Update()
 {
    // The player can't get out of the map
    if(transform.position.x >= xBorderCoo)
    {
        transform.position = new Vector3(xBorderCoo,transform.position.y,transform.position.z);
        rb.velocity = Vector3.zero;
    }
    else if(transform.position.x <= -xBorderCoo)
    {
        transform.position = new Vector3(-xBorderCoo,transform.position.y,transform.position.z);
        rb.velocity = Vector3.zero;
    }

    // Shoot water if left mouse button is pressed
    if (Input.GetKey(KeyCode.Mouse0))
    {
         Water_Steam.Play();
         // Make the player get knocked back when shooting water depending on its rotation
            Vector3 direction = new Vector3(-transform.forward.x,0,0);
            rb.AddForce(direction*1.5f);

    }
    else
    {
        Water_Steam.Stop();
    }
    // Make player rotate depending on where the mouse is
    Vector3 mousePos = Input.mousePosition;
    Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
    mousePos.x = mousePos.x - objectPos.x;
    mousePos.y = mousePos.y - objectPos.y;
    float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
    transform.rotation = Quaternion.Euler(new Vector3(0, -(angle-90), 0));

    

 }
}
