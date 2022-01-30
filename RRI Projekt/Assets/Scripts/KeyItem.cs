using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour
{
    void OnTriggerEnter(Collider collider) {
        

            if (collider.gameObject.tag == "Player")
            {
                PlayerController.KeyCount += 1;
                Destroy(gameObject);
            }
        
        
    }
}
   
