using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    //=----Camera Based Movement----=//
    Transform cam; //a ference for the camera game object
    //=-----Animations----=//
    [SerializeField] Animator anim;
    //=----SceneManagement----=//
    [SerializeField] int scentToTransitionTo = 0;
    //=----Scriptable Objects----=//
    [SerializeField] PlayerStats stats;



    //=====Starts and Updates=====//
    // Start is called before the first frame update
    private void Start(){
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
        rb = GetComponent<Rigidbody>(); //Saves the rigidbody attached to this object to this reference
        cam = Camera.main.transform;// Uses the camera keyword to get the transform of the camera object, the save to the camera reference
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
            anim.SetTrigger("jump");

            SceneManager.LoadScene(scentToTransitionTo);
        }

        //Drays a debug line to visualize the raycast position
        Debug.DrawLine(transform.position, (transform.position + (Vector3.up * -rayLength)), Color.red);//Draws a ray down
        onGround = (Physics.Raycast(transform.position, -Vector3.up, rayLength, enviornmentLayers)); //If Raycast returns true, the onGround is true. vise-versa

    }

    //Rigidbody stuff should go in fixed update
    private void FixedUpdate(){
        Vector3 camForward = cam.forward; //Get the values camera considers forward
        Vector3 camRight = cam.right; //Get the values camera considers right
        camForward.y  = 0;
        camForward.Normalize();
        camRight.y = 0;
        camRight.Normalize();

        //
        Vector3 forwardRelative = camForward * vMovement;
        Vector3 rightRelative = camRight * hMovement;


        // !* Transform.up is local/instance up, and vector3.up is world up
        Vector3 forwardMovement = ((forwardRelative) + (rightRelative)).normalized * speed;//normalize so magnitudes stay the same 
        forwardMovement.y = rb.velocity.y; //Resets the y position to the current y position
        //Adjust the velocity of the player
        rb.velocity = forwardMovement;
        //Sets the animators variable Speed to the magnitude value of forwardMovement
        anim.SetFloat("speed", forwardMovement.magnitude);//Magnitude takes into account how much you're moving in all directions
        //Sets the animations forward to the character's forward
        anim.transform.forward = forwardMovement;

        stats.curHealth -= 1.0f * 0.1f * Time.deltaTime;
    }

}
