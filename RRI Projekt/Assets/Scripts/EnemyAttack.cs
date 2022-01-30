using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {

    public float timeBetweenAttacks = 4f;     // The time in seconds between each attack.
    public int attackDamage = 10;               // The amount of health taken away per attack.


    GameObject player;                          // Reference to the player GameObject.
    PlayerHealth playerHealth;                  // Reference to the player's health.
    bool playerInRange;                         // Whether player is within the trigger collider and can be attacked.
    float timer;                                // Timer for counting up to the next attack.


    void Awake()
    {
        // Setting up the references.
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("I am triggered.");
        // If the entering collider is the player...
        if (other.gameObject == player)
        {
            // ... the player is in range.
            playerInRange = true;
        }
    }


    void OnTriggerExit(Collider other)
    {
        // If the exiting collider is the player...
        if (other.gameObject == player)
        {
            // ... the player is no longer in range.
            playerInRange = false;
        }
    }


    void Update()
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        // If the timer exceeds the time between attacks, and the player is in range...
        if (timer >= timeBetweenAttacks && playerInRange)
        {
            // ... attack.
            Attack();
        }

    }


    void Attack()
    {
        // Reset the timer.
        timer = 0f;

        // If the player has health to lose...
        if (playerHealth.currentHealth > 0)
        {
            Debug.Log("attacking");
            // ... damage the player.
            playerHealth.TakeDamage(attackDamage);
        }
    }
}
