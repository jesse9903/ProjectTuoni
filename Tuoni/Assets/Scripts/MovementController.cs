using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    [SerializeField, Range(0f, 100f)] private float Acceleration = 35f;

    private Vector2 inputDirection;
    private Rigidbody2D rb2d;
    private Vector2 velocity;
    private InputController input = null;
    private UnitStats unitStats = null;

    private void Awake()
    {
        // Initialize fields
        rb2d = GetComponent<Rigidbody2D>();
        CharacterClass characterClass = GetComponent<CharacterClass>();
        unitStats = GetComponent<UnitStats>();
        input = characterClass.InputController;
    }

    private void Update()
    {
        inputDirection = input.GetMoveInput(gameObject);

        // Change current state
        if (inputDirection.magnitude > 0)
        {
            unitStats.currentState = UnitStats.UnitState.Moving;
        }
        else
        {
            unitStats.currentState = UnitStats.UnitState.Idle;
        }
    }

    private void FixedUpdate()
    {
        // Move the character
        velocity = rb2d.velocity;
        velocity = Vector2.Lerp(velocity, inputDirection * unitStats.moveSpeed, Acceleration * Time.fixedDeltaTime);
        rb2d.velocity = velocity;

    }
}
