using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Coin : MonoBehaviour{
    [SerializeField] PlayerStats stats;


    void OnTriggerEnter(Collider other){
        if(other.GetComponent<PlayerController>() != null){
            stats.coinsCollected += 1;
            Destroy(gameObject);
        }
    }

}
