using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    [SerializeField] float speed = 1.0f;
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        //Vertical Movement
        float vMovement = Input.GetAxis("Vertical") * speed;
        Vector3 movementV = vMovement * Vector3.up;
        transform.position += movementV * Time.deltaTime;
        //Horizontal Movement
        float hMovement = Input.GetAxis("Horizontal") * speed;
        Vector3 movementH = hMovement * Vector3.right;
        transform.position += movementH * Time.deltaTime;
    }
}
