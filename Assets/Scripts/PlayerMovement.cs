using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/*
This Class keeps track of the player's movenement inputs.
When the player used the WASD or ArrowKeys, the player will move in the
corresponding direction
*/

public class PlayerController : MonoBehaviour{
    //Makes a refernce to the InputManager Class
    public static InputManager input = null;
    //Makes a reference to the current object's rigidbody
    private Rigidbody rb = null;
    //Creates a Vector3 to be used in player movement and defaults it to zero
    private Vector3 movementVector3 = Vector3.zero;
    //Made a float to adjust player speed
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 3f;
    [SerializeField][Range(0f, 2f)] float rayLength = 1.2f;
    [SerializeField] Animator anim;

//Calls the Awake method to save objects/components to their refernce holder
    private void Awake(){
        //Sets a new instance of Inputmanager to input
        input = new InputManager();
        //Saves the rigidbody component's access to rb
        rb = GetComponent<Rigidbody>();
    }//Closes Awake
    
    private void OnEnable(){
        //This turns on the input manager 
        input.Enable();
        //the input is subscribing to the onMovementPerformed event.
        input.PlayerMovement.Move.performed += onMovementPerformed;
        //the input is subscribing to the onMovementCancelled event.
        input.PlayerMovement.Move.canceled += onMovementCancelled;
        //the input is subscribing to the onMovementPerformed event.
        input.PlayerMovement.Jump.performed += onJumpPerformed;
    }//closes onEnable

    private void OnDisable(){
        //This turns off the input manager 
        input.Disable();
        //the input is unsubscribing to the onMovementPerformed event.
        input.PlayerMovement.Move.performed -= onMovementPerformed;
        //the input is unsubscribing to the onMovementCancelled event.
        input.PlayerMovement.Move.canceled -= onMovementCancelled;
        //the input is subscribing to the onMovementPerformed event.
        input.PlayerMovement.Jump.performed += onJumpPerformed;
    }//Close onDisable

    private void Update(){
        //Draws a red line under the player's position
        Debug.DrawLine(transform.position, transform.position + (-transform.up) * rayLength , Color.red);
        //Sets the speed to vector
        anim.SetFloat("speed", movementVector3.sqrMagnitude);
    }
    //==Because we want to make sure rigid body calculations are going to be correct, were doing a fixed update
    private void FixedUpdate(){//Fixed Update already uses time.DeltaTime
        //Sets the movement of the objects
        rb.velocity = movementVector3 * moveSpeed;
        //rb.AddForce(movementVector3*moveSpeed, ForceMode.Force);
    }//closes fixed update

    //Checks if the assaigned input is being triggered and traking it's value
    public void onMovementPerformed(InputAction.CallbackContext value){
        //The value that was read gets saved to movementVector3
        movementVector3 = value.ReadValue<Vector3>();
    }//Closes the onMovePerformed

    //Checks if the assaigned input has been interrupted
    public void onMovementCancelled(InputAction.CallbackContext value){
        //movementVector3 gets set to zero
        movementVector3 = Vector3.zero;
    }//closes the onMovecancled

    public void onJumpPerformed(InputAction.CallbackContext value){
        //The value that was read gets saved to movementVector3
        movementVector3 += value.ReadValue<Vector3>().normalized * jumpForce * Time.deltaTime;
        //This is used to add a force to the player's rigidbody
        //rb.AddForce(Vector3.up*jumpForce, ForceMode.Impulse);
    }//Closes the onMovePerformed

}//closes the class