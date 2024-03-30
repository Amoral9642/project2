using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public GameManager gameManager;
    void Update()
    {
      coinText.text = "Coins: " + gameManager.coinsCollected.ToString();  
    }
}
