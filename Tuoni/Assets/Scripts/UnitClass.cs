using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public abstract class UnitClass : MonoBehaviour, DamageableInterface
{

    [Header("Components")]
    [SerializeField] private Collider2D hitBox;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Collider2D unitCollider;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private UnitStateMachine stateMachine;
    [SerializeField] private UnitAnimationController animationController;

    [Header("General")]
    [SerializeField] private string unitName;
    public enum UnitAlignment
    {
        Player, Neutral, Enemy
    }
    [SerializeField] private UnitAlignment alignment;
    public enum UnitState
    {
        Idle, Moving, Attacking, TakeDamage, Invulnerable, Dead
    }

    [Header("Combat")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int attackDamage = 10;
    [SerializeField] private float attackDamageMultiplier = 1f;
    [SerializeField] private float criticalHitChance = 1f;
    [SerializeField] private float criticalHitMultiplier = 1.5f;
    [SerializeField] private bool invulnerable = false;
    [SerializeField] private float attackDuration = 1; //sekuntia
    [SerializeField] private float attackCooldown = 1; //sekuntia
    [SerializeField] private float attackRange = 1;
    [SerializeField] private Transform attackCircle;
    [SerializeField] private LayerMask enemyMask;

    private UnitState latestState = UnitState.Idle;
    private List<UnitState> currentStates;
    private int currentHealth;
    private int previousHealth;
    private float invulnerabilityDuration;
    private UnityEngine.Object lastDamagedBy;
    private MovementController movementController;
    private bool canAttack = true;
    private float attackDurationLeft;
    private float attackCooldownLeft;

//////////////////////////////////////////////////////////////////////////////////////////////////////////

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    protected void Update()
    {
        CalculateAttackTimer();

        // If there are no active states, switch state to idle
        if (currentStates.Count == 0)
        {
            latestState = UnitState.Idle;
        }
    }

    void Awake()
    {
        InitValues();
        CheckValues();
    }

    private void InitValues()
    {
         // Get components
        rigidBody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        unitCollider = GetComponent<Collider2D>();
        hitBox = GetComponent<Collider2D>();
        animationController = GetComponent<UnitAnimationController>();
        movementController = GetComponent<MovementController>();
        currentHealth = maxHealth;
        currentStates = new List<UnitState>();
    }

    private void CheckValues()
    {
        if (unitName == "")
        {
            unitName = gameObject.name;
        }

        // Check if components are assigned
        if (rigidBody == null)
        {
            Debug.LogError($": Rigid body not assigned.");
        }
        if (sprite == null)
        {
            // Debug.LogError($": Sprite not assigned.");
        }
        // if (unitCollider == null)
        // {
        //     Debug.LogError($": Unit collider not assigned.");
        // }
        // if (hitBox == null)
        // {
        //     Debug.LogError($": Hit box collider not assigned.");
        // }
        // if (stateMachine == null)
        // {
        //     Debug.LogError($": State machine not assigned.");
        // }
        // if (animationController == null)
        // {
        //     Debug.LogError($": Animation controller not assigned.");
        // }
    }

    private void CalculateAttackTimer()
    {
        // Calculate attack duration & cooldown
        if (canAttack == false)
        {
            if (attackDurationLeft > 0)
            {
                attackDurationLeft -= Time.deltaTime;
            }
            else if (attackCooldownLeft > 0)
            {
                attackCooldownLeft -= Time.deltaTime;
            }
            else
            {
                // FOR TESTING
                // Debug.Log("Attack enabled!");

                canAttack = true;
                currentStates.Remove(UnitState.Attacking);
            }
        }
    }

    virtual public void TakeDamage(int dmg, Object dmgDealtBy)
    {
        if (invulnerable == false && currentHealth > 0)
        {
            previousHealth = currentHealth;
            lastDamagedBy = dmgDealtBy;
            latestState = UnitState.TakeDamage;
            currentStates.Add(UnitState.TakeDamage);

            CalculateHealth(dmg);

            TakeDamageEffect();

            if (currentHealth <= 0)
            {
                OnDeath();
            }
            else
            {
                TakeDamageEffect();
                currentStates.Remove(UnitState.TakeDamage);
            }
        }

    }

    virtual public void DealDamage()
    {
        if (canAttack)
        {
            int damage = CalculateDealtDamage();
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackCircle.position, attackRange);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<EnemyClass>().TakeDamage(damage, gameObject);
            }

            // FOR TESTING
            // Debug.Log("Attack disabled!");

            attackDurationLeft = attackDuration;
            attackCooldownLeft = attackCooldown;
            canAttack = false;
            latestState = UnitState.Attacking;
            currentStates.Add(UnitState.Attacking);
        }
    }

    // FOR TESTING
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackCircle.position, attackRange);
    }

    virtual protected void CalculateHealth(int dmg)
    {
        currentHealth -= dmg;
    }

    virtual protected int CalculateDealtDamage()
    {
        int damage = (int)(attackDamage * attackDamageMultiplier);
        return damage;
    }

    virtual protected void OnDeath()
    {
        // Add animation and change state to dead
        latestState = UnitState.Dead;
        currentStates.Add(UnitState.Dead);
        rigidBody.isKinematic = true;
        movementController.DisableMovement();

    }

    // Make a cool damage effect
    virtual protected IEnumerator TakeDamageEffect()
    {
        float duration = 0.1f; // Total duration of the lerp
        float elapsedTime = 0f;

        Vector3 originalScale = sprite.transform.localScale;
        Vector3 targetScale = new Vector3(originalScale.x, originalScale.y * 1.2f, originalScale.z); // Scale up Y
        Color originalColor = sprite.color;
        Color targetColor = Color.white;


        // Lerp up
        while (elapsedTime < duration / 2)
        {
            elapsedTime += Time.deltaTime;
            sprite.transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / (duration / 2));
            yield return null;
            sprite.color = Color.white;
        }

        elapsedTime = 0f;


        // Lerp back down
        while (elapsedTime < duration / 2)
        {
            elapsedTime += Time.deltaTime;
            sprite.transform.localScale = Vector3.Lerp(targetScale, originalScale, elapsedTime / (duration / 2));
            yield return null;
            sprite.color = originalColor;
        }

        sprite.transform.localScale = originalScale; // Ensure exact original scale
    }

//////////////////////////////////////////////////////////////////////////////////////////////////////////

    // Getters and Setters for all private variables
    public Collider2D HitBox
    {
        get => hitBox;
        set => hitBox = value;
    }

    public Rigidbody2D RigidBody
    {
        get => rigidBody;
        set => rigidBody = value;
    }

    public Collider2D UnitCollider
    {
        get => unitCollider;
        set => unitCollider = value;
    }

    public SpriteRenderer Sprite
    {
        get => sprite;
        set => sprite = value;
    }

    public UnitStateMachine StateMachine
    {
        get => stateMachine;
        set => stateMachine = value;
    }

    public UnitAnimationController AnimationController
    {
        get => animationController;
        set => animationController = value;
    }

    public string UnitName
    {
        get => unitName;
        set => unitName = value;
    }

    public int MaxHealth
    {
        get => maxHealth;
        set => maxHealth = value;
    }

    public int AttackDamage
    {
        get => attackDamage;
        set => attackDamage = value;
    }

    public float AttackDamageMultiplier
    {
        get => attackDamageMultiplier;
        set => attackDamageMultiplier = value;
    }

    public float CriticalHitChance
    {
        get => criticalHitChance;
        set => criticalHitChance = value;
    }

    public float CriticalHitMultiplier
    {
        get => criticalHitMultiplier;
        set => criticalHitMultiplier = value;
    }

    public int CurrentHealth
    {
        get => currentHealth;
        set => currentHealth = value;
    }

    public int PreviousHealth
    {
        get => previousHealth;
        set => previousHealth = value;
    }

    public float InvulnerabilityDuration
    {
        get => invulnerabilityDuration;
        set => invulnerabilityDuration = value;
    }

    public Object LastDamagedBy
    {
        get => lastDamagedBy;
        set => lastDamagedBy = value;
    }

    public object Alignment
    {
        get => alignment;
        set => alignment = (UnitAlignment)value;
    }

    public object CurrentState
    {
        get => latestState;
        set => latestState = (UnitState)value;
    }

    public List<UnitState> CurrentStates
    {
        get => currentStates;
    }

    public void AddToCurrentStates(UnitState state)
    {
        currentStates.Add(state);
    }

    public void RemoveFromCurrentStates(UnitState state)
    {
        currentStates.Remove(state);
    }

}