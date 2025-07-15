using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealthScript : MonoBehaviour
{
    public int maxHealth = 100;
    public Animator animator;
    public float destroyDelay = 5f;

    private int currentHealth;
    void Start()
    {
        currentHealth = maxHealth;

        // Finds the animator component from child at the start of the script
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        // Substracts damage from current health
        currentHealth -= damage;

        // Triggers the hurt animation
        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Debug
        Debug.Log(gameObject.ToString() + " died");

        // Triggers dying animation
        animator.SetBool("isDead", true);

        // Disables the enemy
        Disable();

        // Destroys the enemy from the world
        Invoke("DestroyEnemy", destroyDelay);
    }

    // Disable enemy
    public void Disable()
    {
        // Disables collision
        GetComponent<BoxCollider>().enabled = false;

        // Disable all other scripts here
        
        // This will disable this script, EXECUTE AFTER OTHERS!
        this.enabled = false;
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
