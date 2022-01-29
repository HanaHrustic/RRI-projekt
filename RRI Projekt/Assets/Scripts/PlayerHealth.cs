using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Text gameOverText;

    bool isDead;

    void Awake()
    {
        gameOverText.enabled = false;

        currentHealth = startingHealth;
        healthSlider.value = startingHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        healthSlider.value = currentHealth;
        Debug.Log(currentHealth);
        if (currentHealth <= 0 && !isDead)
        {
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        isDead = true;
        gameOverText.enabled = true;

        this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX |
            RigidbodyConstraints.FreezeRotationY |
            RigidbodyConstraints.FreezeRotationZ |
            RigidbodyConstraints.FreezePositionY |
            RigidbodyConstraints.FreezePositionY |
            RigidbodyConstraints.FreezePositionZ;
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
