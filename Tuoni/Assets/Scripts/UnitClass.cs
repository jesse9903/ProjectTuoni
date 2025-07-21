using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
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
    [SerializeField] private UnitState currentState = UnitState.Idle;

    [Header("Combat")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int attackDamage = 10;
    [SerializeField] private float attackDamageMultiplier = 1f;
    [SerializeField] private float criticalHitChance = 1f;
    [SerializeField] private float criticalHitMultiplier = 1.5f;
    private int currentHealt;
    private int previousHealth;
    private float invulnerabilityDuration;
    private UnityEngine.Object lastDamagedBy;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Awake()
    {
        // Get components
        rigidBody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        unitCollider = GetComponent<Collider2D>();
        hitBox = GetComponent<Collider2D>();
        stateMachine = GetComponent<UnitStateMachine>();
        animationController = GetComponent<UnitAnimationController>();

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

    public void TakeDamage(int dmg, Object dmgDealtBy)
    {

        previousHealth = currentHealt;
        lastDamagedBy = dmgDealtBy;

        CalculateHealth(dmg);

        TakeDamageEffect();
        // TODO: Add damage effect
    }

    public void CalculateHealth(int dmg)
    {
        currentHealt = -dmg;
    }

    public void OnDeath()
    {
        // Add animation and change state to dead
    }

    // Make a cool damage effect
    private IEnumerator TakeDamageEffect()
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
        //currentState = UnitState.Idle; // Return to Idle state
    }

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

    public int CurrentHealt
    {
        get => currentHealt;
        set => currentHealt = value;
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
        get => currentState;
        set => currentState = (UnitState)value;
    }

}