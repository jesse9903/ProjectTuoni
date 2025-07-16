using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "UnitStats")]
public class UnitStats : MonoBehaviour
{
    [Header("Unit Stats")]
    [SerializeField] private string unitName;
    public enum UnitAlignment
    {
        Player, Neutral, Enemy
    }
    public UnitAlignment alignment;
    public enum UnitState
    {
        Idle, Moving, Attacking, TakeDamage, Invulnerable, Dead
    }
    public UnitState currentState = UnitState.Idle;
    public float moveSpeed = 10f;
    [SerializeField] private float moveSpeedMultiplier = 1;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int attackDamage = 10;
    [SerializeField] private float attackDamageMultiplier = 1f;
    [SerializeField] private float criticalHitChance = 1f;
    [SerializeField] private float criticalHitMultiplier = 1.5f;
    private int currentHealth;
    private Vector2 moveDirection;
    private float acceleration = 15f;
    private int currentHealt;
    private int previousHealth;
    private float invulnerabilityDuration;
    private UnityEngine.Object lastDamagedBy;

    private void Awake()
    {
        // Initialize health
        currentHealth = maxHealth;

        // If name is not assigned, get object name
        if (unitName == "")
        {
            unitName = gameObject.name;
        }
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

    // Getters and Setters for all private variables
    public string UnitName
    {
        get { return unitName; }
        set { unitName = value; }
    }

    public float MoveSpeedMultiplier
    {
        get { return moveSpeedMultiplier; }
        set { moveSpeedMultiplier = value; }
    }

    public int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    public int AttackDamage
    {
        get { return attackDamage; }
        set { attackDamage = value; }
    }

    public float AttackDamageMultiplier
    {
        get { return attackDamageMultiplier; }
        set { attackDamageMultiplier = value; }
    }

    public float CriticalHitChance
    {
        get { return criticalHitChance; }
        set { criticalHitChance = value; }
    }

    public float CriticalHitMultiplier
    {
        get { return criticalHitMultiplier; }
        set { criticalHitMultiplier = value; }
    }

    public int CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }

    public Vector2 MoveDirection
    {
        get { return moveDirection; }
        set { moveDirection = value; }
    }

    public float Acceleration
    {
        get { return acceleration; }
        set { acceleration = value; }
    }

    public int CurrentHealt
    {
        get { return currentHealt; }
        set { currentHealt = value; }
    }

    public int PreviousHealth
    {
        get { return previousHealth; }
        set { previousHealth = value; }
    }

    public float InvulnerabilityDuration
    {
        get { return invulnerabilityDuration; }
        set { invulnerabilityDuration = value; }
    }

    public UnityEngine.Object LastDamagedBy
    {
        get { return lastDamagedBy; }
        set { lastDamagedBy = value; }
    }
}
