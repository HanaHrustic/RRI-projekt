using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHealthPoion : MonoBehaviour
{
    GameObject player;
    PlayerHealth playerHealth;


    void Awake()
    {
        // Setting up the references.
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.tag == "Player")
        {
            if (playerHealth.currentHealth < 80)
            {
                playerHealth.currentHealth += 20;
                playerHealth.healthSlider.value = playerHealth.currentHealth;
                Destroy(gameObject);
            }
        }


    }
}
