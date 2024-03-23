using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    //======Initializations======//
    //=----Walking----=//
    //Initializes variables for movement 
    private Rigidbody rb; //A reference to the rigidbody attached to this object
    [SerializeField] float speed = 5.0f; //player speed
    private float vMovement; //vertical movement
    private float hMovement; //horizontal movement
    //=----Jumping----=//
    [SerializeField] float jumpForce = 10.0f; //player jump force
    [SerializeField][Range(0.1f, 2.0f)] float rayLength = 0.8f;
    private bool onGround = false;
    [SerializeField] LayerMask enviornmentLayers; // used to specify the layers allowed to jump off of


    // Start is called before the first frame update
    private void Start(){
        rb = GetComponent<Rigidbody>(); //Saves the rigidbody attached to this object to this reference
    }

    // Update is called once per frame
    private void Update(){
        //Vertical Movement
        vMovement = Input.GetAxis("Vertical"); //Get
        //Horizontal Movement
        hMovement = Input.GetAxis("Horizontal");

        //Checking for the jump input and if player is on ground
        if(Input.GetButtonDown("Jump") && onGround){
            rb.AddForce((Vector3.up * jumpForce), ForceMode.Impulse); //Add force to the rigidbody
        }

        //Drays a debug line to visualize the raycast position
        Debug.DrawLine(transform.position, (transform.position + (Vector3.up * -rayLength)), Color.red);//Draws a ray down
        onGround = (Physics.Raycast(transform.position, -Vector3.up, rayLength, enviornmentLayers)); //If Raycast returns true, the onGround is true. vise-versa

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
