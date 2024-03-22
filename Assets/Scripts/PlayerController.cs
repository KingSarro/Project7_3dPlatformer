using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    Rigidbody rb;
    [SerializeField] float speed = 1.0f;
    float vMovement;
    float hMovement;
    // Start is called before the first frame update
    private void Start(){
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update(){
        //Vertical Movement
        vMovement = Input.GetAxis("Vertical");
        //Horizontal Movement
        hMovement = Input.GetAxis("Horizontal");
    }

    //Rigidbody stuff should go in fixed update
    private void FixedUpdate(){
        // !* Transform.up is local/instance up, and vector3.up is world up
        Vector3 movement = ((transform.forward * vMovement) + (transform.right * hMovement)).normalized * speed;//normalize so magnitudes stay the same 
        movement.y = rb.velocity.y; //Resets the y position to the current y position
        //Adjust the velocity of the player
        rb.velocity = movement;
    }

}
