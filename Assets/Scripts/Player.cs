using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //public Transform checkGroundTransform;
    [SerializeField] private Transform checkGroundTransform; //proper way to do it instead of public, using serializeField, cause to expose it only to inspector class not other classes
    [SerializeField] private LayerMask playerMask;
    // Start is called before the first frame update
    bool spaceWasPressed;
    float horizontalInput;
    Rigidbody rb; //maybe name it rigidBodyComponent instead, better to do this than using GetComponent<Rigidbody>() because the unity compiler has to look every reference when it is called so defining it ahead, will save performance
    //float verticalInput; //checking if vertical input can be done this way too
    bool isGrounded;
    float offset;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //Check if space is pressed down
        if (Input.GetKeyDown(KeyCode.Space)){
            spaceWasPressed = true;
            //rb.AddForce(Vector3.up*5, ForceMode.VelocityChange); //predefined vector already for addForce
            
        }

        horizontalInput = Input.GetAxis("Horizontal");
        // verticalInput = Input.GetAxis("Vertical");
        
    }

    private void FixedUpdate()
    {
        /*if (Physics.OverlapSphere(checkGroundTransform,0.1f).Length==0) {*/ //doesn't work this overlapsphere way because the capsule collider is colliding with it self, so the length will be 1, not good practice cause imagine if the player had a weapon or armor, then there will be more than one colliders
        /*if (Physics.OverlapSphere(checkGroundTransform.position,0.1f).Length==1){*/ //overlapsphere returns an array of colliders within the given sphere radius so to check we use length
        rb.velocity = new Vector3(horizontalInput, rb.velocity.y, 0); // GetComponent<Rigidbody>().velocity.y means the velocity of y should be as it is
        if (Physics.OverlapSphere(checkGroundTransform.position, 0.1f, playerMask ).Length==0) { 
            return;
        } //the capsule collider will be collider with itself so 1 is put in there, so if more than 1 we know its either colliding with ground too
        //if (!isGrounded)
        //{
        //    return;
        //}

        if (spaceWasPressed) { 
            rb.AddForce(Vector3.up*6, ForceMode.VelocityChange);
            spaceWasPressed = false;
        }

       
        // GetComponent<Rigidbody>().velocity = new Vector3(0, verticalInput, 0);
        // GetComponent<Rigidbody>().velocity=new Vector3 (horizontalInput, verticalInput,0); //so it worked but space is cool tho
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    isGrounded = true;
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    isGrounded = false;
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer==7) {
            Destroy(other.gameObject);
        }
    }
}
