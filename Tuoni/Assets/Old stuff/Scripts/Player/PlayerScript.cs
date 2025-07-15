using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private HealthScript healthComponent;
    private Animator animator;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        healthComponent = gameObject.GetComponent<HealthScript>();
    }

    public void TakeDamage(int damage)
    {
        // Deals damage to player and updates the current health
        healthComponent.TakeDamage(damage);
    }
}