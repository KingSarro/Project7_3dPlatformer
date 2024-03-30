using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Player Stats")]
public class PlayerStats : ScriptableObject{
    public float maxHealth = 10.0f;
    public float curHealth = 10.0f;
}
