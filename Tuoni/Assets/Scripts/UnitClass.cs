using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public abstract class UnitClass : MonoBehaviour, CanTakeDamageInterface
{

     [Header("Components")]
    [SerializeField] private Collider2D hitBox;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Collider2D unitCollider;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private UnitStateMachine stateMachine;
    [SerializeField] private UnitAnimationController animationController;
    [SerializeField] private UnitStats stats;

        // Should this be moved to character class?
    [SerializeField] private InputController inputController;


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
        stats = GetComponent<UnitStats>();

        // Check if components are assigned
        if (rigidBody == null)
        {
            Debug.LogError($": Rigid body not assigned.");
        }
        if (sprite == null)
        {
            Debug.LogError($": Sprite not assigned.");
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
        if (stats == null)
        {
            Debug.LogError($": Unit stats not assigned.");
        }
    }

    public void TakeDamage(int dmg, Object dmgDealtBy)
    {

        stats.PreviousHealth = stats.CurrentHealt;
        stats.LastDamagedBy = dmgDealtBy;

        CalculateHealth(dmg);

        TakeDamageEffect();
        // TODO: Add damage effect
    }

    public void CalculateHealth(int dmg)
    {
        stats.CurrentHealt = -dmg;
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

    // Getters and Setters for all private fields
    public Collider2D HitBox
    {
        get { return hitBox; }
        set { hitBox = value; }
    }

       public InputController InputController
    {
        get { return inputController; }
        set { inputController = value; }
    }


       public UnitStats Stats
    {
        get { return stats; }
        set { stats = value; }
    }

    public Rigidbody2D RigidBody
    {
        get { return rigidBody; }
        set { rigidBody = value; }
    }

    public Collider2D UnitCollider
    {
        get { return unitCollider; }
        set { unitCollider = value; }
    }

    public SpriteRenderer Sprite
    {
        get { return sprite; }
        set { sprite = value; }
    }

    public UnitStateMachine StateMachine
    {
        get { return stateMachine; }
        set { stateMachine = value; }
    }

    public UnitAnimationController AnimationController
    {
        get { return animationController; }
        set { animationController = value; }
    }

}