using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class PlayerInputBossLevel : MonoBehaviour
{
    private Rigidbody rb;
    private CameraShake cameraShake;

    private float xBorderCoo = 5f;
    [SerializeField] private float[] zBorderCoo;
    [SerializeField] private float maxMovement = 40f;
    private Animator playerAnimator;
    public bool canMove = true;
    

    private Touch touch;
    [SerializeField ] private float speedModifier = 0.01f;
    void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        cameraShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();


    }
    private void FixedUpdate() 
    {      
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
        if(transform.position.z >= zBorderCoo[0])
        {
            transform.position = new Vector3(transform.position.x,transform.position.y,zBorderCoo[0]);
            rb.velocity = new Vector3(rb.velocity.x,0,0);
        }
        else if(transform.position.z <= zBorderCoo[1])
        {
            transform.position = new Vector3(transform.position.x,transform.position.y,zBorderCoo[1]);
            rb.velocity = new Vector3(rb.velocity.x,0,0);
        }
        
        // If he touches the sceen, he shoots and moves
        if(Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Moved)
            {
                float deltaPositionx = touch.deltaPosition.x;
                float deltaPositionz = touch.deltaPosition.y;

                if(deltaPositionx > maxMovement) deltaPositionx = maxMovement;
                else if(deltaPositionx < -maxMovement) deltaPositionx = -maxMovement;

                if(deltaPositionz > maxMovement) deltaPositionz = maxMovement;
                else if(deltaPositionz < -maxMovement) deltaPositionz = -maxMovement;

                //Smooth movement
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + deltaPositionx*speedModifier,transform.position.y,transform.position.z + deltaPositionz*speedModifier), 0.1f);

                if( -20 <deltaPositionx && deltaPositionx < 20 || -20 <deltaPositionz && deltaPositionz < 20)
                {
                    playerAnimator.SetFloat("moveX",deltaPositionx);
                    playerAnimator.SetFloat("moveY",deltaPositionz);
                }
                else
                {
                    playerAnimator.SetFloat("moveX",0);
                    playerAnimator.SetFloat("moveY",0);
                }
            }
            else
            {
                playerAnimator.SetFloat("moveX",0);
                playerAnimator.SetFloat("moveY",0);
            }
        }
        else
        {
            playerAnimator.SetFloat("moveX",0);
            playerAnimator.SetFloat("moveY",0);
        }
    }
}
