using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plain : MonoBehaviour{
    [SerializeField] GameObject plane;
    [SerializeField] PlaneColorManager cManager;


    void Start(){
        plane.GetComponent<Renderer>().material = cManager.groundMaterial;
    }
}
