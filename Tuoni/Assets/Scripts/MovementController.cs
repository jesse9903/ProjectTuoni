using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    [Header("Variables")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float moveSpeedMultiplier = 1;
    [SerializeField, Range(0f, 100f)] private float Acceleration = 35f;
    [SerializeField] private InputController inputController;

    private Vector2 moveDirection;
    private float acceleration = 15f;
    private Vector2 inputDirection;
    private Rigidbody2D rb2d;
    private Vector2 velocity;
    private UnitClass unitClass;

    private void Awake()
    {
        // Initialize fields
        rb2d = GetComponent<Rigidbody2D>();
        unitClass = GetComponent<UnitClass>();
    }

    private void Update()
    {
        inputDirection = inputController.GetMoveInput(gameObject);

        // Change current state
        if (inputDirection.magnitude > 0)
        {
            unitClass.CurrentState = UnitClass.UnitState.Moving;
        }
        else
        {
            unitClass.CurrentState = UnitClass.UnitState.Idle;
        }
    }

    private void FixedUpdate()
    {
        // Move the character
        velocity = rb2d.velocity;
        velocity = Vector2.Lerp(velocity, (inputDirection * moveSpeed) * moveSpeedMultiplier, Acceleration * Time.fixedDeltaTime);
        rb2d.velocity = velocity;

    }
    
    // Getters and Setters for all private variables
    public float MoveSpeedMultiplier
    {
        get => moveSpeedMultiplier;
        set => moveSpeedMultiplier = value;
    }

    public float AccelerationValue
    {
        get => Acceleration;
        set => Acceleration = value;
    }

    public InputController InputControllerRef
    {
        get => inputController;
        set => inputController = value;
    }

    public Vector2 MoveDirection
    {
        get => moveDirection;
        set => moveDirection = value;
    }

    public float AccelerationPrivate
    {
        get => acceleration;
        set => acceleration = value;
    }

    public Vector2 InputDirection
    {
        get => inputDirection;
        set => inputDirection = value;
    }

    public Rigidbody2D Rb2d
    {
        get => rb2d;
        set => rb2d = value;
    }

    public Vector2 Velocity
    {
        get => velocity;
        set => velocity = value;
    }

    public UnitClass UnitClassRef
    {
        get => unitClass;
        set => unitClass = value;
    }
}
