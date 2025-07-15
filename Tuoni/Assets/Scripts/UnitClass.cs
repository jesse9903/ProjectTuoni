using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public abstract class UnitClass : MonoBehaviour, CanTakeDamageInterface
{

    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Collider2D unitCollider;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private string unitName;
    [SerializeField] private UnitStateMachine stateMachine;
    [SerializeField] private UnitAnimationController animationController;
    [SerializeField] private InputController inputController;
    [SerializeField] private UnitStats stats;
    private Vector2 moveDirection;
    private float acceleration = 15f;
    private int currentHealt;
    private int previousHealth;
    private Object lastDmgdBy;


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
        // If name is not assigned, get object name
        if (unitName == "")
        {
            unitName = gameObject.name;
        }


        // Get components
        rigidBody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        unitCollider = GetComponent<Collider2D>();

        if (rigidBody == null)
        {
            Debug.LogError($": UnitStats body collider not assigned.");
        }
    }

    public void TakeDamage(int dmg, Object dmgDealtBy)
    {
        previousHealth = currentHealt;
        lastDmgdBy = dmgDealtBy;

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

    public UnitStats GetUnitStats()
    {
        return stats;
    }

    public InputController GetInputController()
    {
        return inputController;
    }

}