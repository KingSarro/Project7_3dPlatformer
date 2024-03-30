using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Text : MonoBehaviour{
    [SerializeField] TMP_Text coinText;
    [SerializeField] PlayerStats stats;
    // Start is called before the first frame update

    void Start(){
        coinText.text = "Coins: " + stats.coinsCollected.ToString();
    }
    void Update(){
        coinText.text = "Coins: " + stats.coinsCollected.ToString();
    }
}
