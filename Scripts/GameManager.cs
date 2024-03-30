using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  
    public GameObject GoldenPlatform;
    public int coinsCollected = 0;
    public bool goldenPlatformSpawned = false;
    void Start()
{
    
    GoldenPlatform.SetActive(false);
}

    void Update()
    {
        
        if (coinsCollected >= 10 && !goldenPlatformSpawned )
        {
            ActivatePlatform();
            Debug.Log("Congrats!!!");
        }
    }

     void ActivatePlatform()
    {
        GoldenPlatform.SetActive(true); // Make the golden platform visible
        goldenPlatformSpawned = true;
    }

    public void CollectCoin()
    {
        coinsCollected++;
       
    }
}
