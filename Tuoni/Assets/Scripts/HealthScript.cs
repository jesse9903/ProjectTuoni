using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.InputSystem.Processors;

public class HealthScript : MonoBehaviour
{
    public int maxHealth;
    public float destroyDelay = 5f;

    private Animator animator;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;

        try
        {
            animator = GetComponent<Animator>();
        }
        catch 
        {
            Debug.Log("Can't find Animator-component from " + gameObject);
        }

        try
        {
            animator = GetComponentInChildren<Animator>();
        }
        catch
        {
            Debug.Log("Can't find Animator-component from " + gameObject + "s children");
        }

    }

    public int TakeDamage(int damage)
    {
        // Substracts damage from current health
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            // Triggers hurt-animation
            animator.SetTrigger("Hurt");
        }

        return currentHealth;
    }
    public void Die()
    {
        // Triggers death-animation
        animator.SetBool("Dead", true);

        // Debug
        Debug.Log(gameObject + " died");
    }
}
