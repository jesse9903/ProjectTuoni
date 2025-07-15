using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "UnitStats")]
public class UnitStats : ScriptableObject
{
    [Header("Components")]
    public Collider2D hitBox;
    public enum UnitAlignment
    {
        Player, Neutral, Enemy
    }

    [Header("Unit Stats")]
    public UnitAlignment alignment;
    public enum UnitState
    {
        Idle, Moving, Attacking, TakeDamage, Invulnerable, Dead
    }
    public UnitState currentState = UnitState.Idle;
    private float invulnerabilityDuration;
    public float moveSpeed = 10f;
    [SerializeField] private float moveSpeedMultiplier = 1;
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;
    [SerializeField] private int attackDamage = 10;
    [SerializeField] private float attackDamageMultiplier = 1f;
    [SerializeField] private float criticalHitChance = 1f;
    [SerializeField] private float criticalHitMultiplier = 1.5f;

    private void Awake()
    {
        // Initialize health
        currentHealth = maxHealth;
    }

    private void Update()
    {
        //Invulnerability timer
        //     if (invulnerabilityDuration > 0)
        //     {
        //         Invulnerable();
        //         invulnerabilityDuration -= Time.deltaTime;
        //         if (invulnerabilityDuration < 0.001)
        //         {
        //             invulnerabilityDuration = 0;
        //         }
        //     }
        // }
    }
    
}
