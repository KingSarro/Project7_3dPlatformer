using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtBox : MonoBehaviour{
    [SerializeField] PlayerStats stats;
    [SerializeField] float damage;

    private void OnCollisionEnter(Collision other){
        if(other.gameObject.GetComponent<PlayerController>() != null){
            DamagePlayer(damage);
        }
    }

    private void DamagePlayer(float damageToTake){
        stats.curHealth -= damageToTake;
    }
}
