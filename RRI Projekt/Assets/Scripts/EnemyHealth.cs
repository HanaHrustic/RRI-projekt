using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 10;
    public int currentHealth;
    public int damageAmount;

    void Awake()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage()
    {
        currentHealth -= damageAmount;
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            Death();
            PlayerController.enemiesKilled++;
            Debug.Log("Enemies killed: " + PlayerController.enemiesKilled);
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
