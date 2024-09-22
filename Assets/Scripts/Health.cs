using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public HealthBar healthBar;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        gameManager = GameObject.FindObjectOfType<GameManager>();
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " has died!");
        // For player: trigger game over
        if (gameObject.CompareTag("Player"))
        {
            gameManager.GameOver(false);
        }
        else if (gameObject.CompareTag("Enemy"))
        {
            gameManager.EnemyDefeated();
        }
        // For enemy: destroy the game object
        Destroy(gameObject); 
    }
}
