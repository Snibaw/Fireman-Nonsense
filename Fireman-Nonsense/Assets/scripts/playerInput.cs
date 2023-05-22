using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class playerInput : MonoBehaviour
{
    [SerializeField] private float directionMultiplier = 5;
    public UIBarScript ManaBarScript;
    private ParticleSystem Water_Steam;
    private Rigidbody rb;
    private bool isShooting = false;
    private CameraShake cameraShake;
    private float maxMana = 1000;
    private float currentMana;

    private float xBorderCoo = 4.5f;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Water_Steam = GameObject.Find("Water Steam").GetComponent<ParticleSystem>();
        cameraShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
        currentMana = maxMana;
    }
    // Update is called once per frame
    void Update()
    {
        //Make the player move only in the z axis every frame
        rb.velocity = new Vector3(rb.velocity.x,0,5);


        // The player can't get out of the map
        if(transform.position.x >= xBorderCoo)
        {
            transform.position = new Vector3(xBorderCoo,transform.position.y,transform.position.z);
            rb.velocity = new Vector3(0,0,rb.velocity.z);
        }
        else if(transform.position.x <= -xBorderCoo)
        {
            transform.position = new Vector3(-xBorderCoo,transform.position.y,transform.position.z);
            rb.velocity = new Vector3(0,0,rb.velocity.z);
        }

        // Shoot water if left mouse button is pressed
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if(currentMana > 0)
            {
                Water_Steam.Play();
                isShooting = true;
                // Make the player get knocked back when shooting water depending on its rotation
                Vector3 direction = new Vector3(-transform.forward.x,0,0);
                rb.AddForce(direction*directionMultiplier);

                //Lose mana
                currentMana -= 0.5f;
                ManaBarScript.UpdateValue((int)Mathf.Round(currentMana), (int)Mathf.Round(maxMana));
            }
        }
        else
        {
            Water_Steam.Stop();
            isShooting = false;
        }
        if(currentMana <= 0)
        {
            currentMana = 0;
            Water_Steam.Stop();
        }
        // Make player rotate depending on where the mouse is
        if(isShooting)
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
            mousePos.x = mousePos.x - objectPos.x;
            mousePos.y = mousePos.y - objectPos.y;
            float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, -(angle-90), 0));
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(new Vector3(0,0,0)),Time.deltaTime*5);
        }



        

    }
    public void GetBoucheIncendie()
    {
        currentMana = maxMana;
        ManaBarScript.UpdateValue((int)Mathf.Round(currentMana), (int)Mathf.Round(maxMana));
    }
    private IEnumerator EndOfBoucheIncendie()
    {
        // Wait 5 seconds before stopping the particle system
        yield return new WaitForSeconds(5);
        var emission = Water_Steam.emission;
        var main = Water_Steam.main;
        emission.rateOverTime = 400;
        main.startSpeed = 30;
        CameraShaker.Instance.ShakeOnce(5f,5f,.1f,1f);
    }
}
