using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour
{
    void OnTriggerEnter(Collider collider) {
            

        if (collider.gameObject.tag == "Player" && PlayerController.enemiesKilled == PlayerController.brojac)
        {
            PlayerController.KeyCount += 1;
            Destroy(gameObject);
            PlayerController.brojac += 1;
            Debug.Log("Key picked up! Number of keys: " + PlayerController.KeyCount);
        }
        
        
    }
}
   
