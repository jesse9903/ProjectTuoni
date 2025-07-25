using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    [Header("Variables")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float moveSpeedMultiplier = 1;
    [SerializeField, Range(0f, 100f)] private float Acceleration = 35f;
    [SerializeField] private InputController inputController;

    private bool movementEnabled = true;
    private UnityEngine.Vector2 moveDirection;
    private float acceleration = 15f;
    private UnityEngine.Vector2 inputDirection;
    private Rigidbody2D rb2d;
    private UnityEngine.Vector2 velocity;
    private UnitClass unitClass;

//////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void Awake()
    {
        // Initialize fields
        rb2d = GetComponent<Rigidbody2D>();
        unitClass = GetComponent<UnitClass>();
    }

    private void Update()
    {
        ChangeState();
    }

    private void FixedUpdate()
    {
        MoveCharacter();
    }

    public void MoveCharacter()
    {
        if (movementEnabled)
        {
            // Move the character
            velocity = rb2d.velocity;
            velocity = UnityEngine.Vector2.Lerp(velocity, (inputDirection * moveSpeed) * moveSpeedMultiplier, Acceleration * Time.fixedDeltaTime);
            rb2d.velocity = velocity;
        }
    }

    public void ChangeState()
    {
         if (movementEnabled)
        {
            inputDirection = inputController.GetMoveInput(gameObject);

            // Change current state
            if (inputDirection.magnitude > 0)
            {
                List<UnitClass.UnitState> states = unitClass.CurrentStates;

                // Add state to list only once
                if (!states.Contains(UnitClass.UnitState.Moving))
                {
                    unitClass.CurrentState = UnitClass.UnitState.Moving;
                    unitClass.AddToCurrentStates(UnitClass.UnitState.Moving);
                }
            }
            else
            {
                unitClass.RemoveFromCurrentStates(UnitClass.UnitState.Moving);
            }
        }
    }

    public void EnableMovement()
    {
        movementEnabled = true;
    }

    public void DisableMovement()
    {
        // Stop movement
        rb2d.velocity = UnityEngine.Vector2.zero;
        
        movementEnabled = false;
    }
    
//////////////////////////////////////////////////////////////////////////////////////////////////////////

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

    public UnityEngine.Vector2 MoveDirection
    {
        get => moveDirection;
        set => moveDirection = value;
    }

    public float AccelerationPrivate
    {
        get => acceleration;
        set => acceleration = value;
    }

    public UnityEngine.Vector2 InputDirection
    {
        get => inputDirection;
        set => inputDirection = value;
    }

    public Rigidbody2D Rb2d
    {
        get => rb2d;
        set => rb2d = value;
    }

    public UnityEngine.Vector2 Velocity
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
