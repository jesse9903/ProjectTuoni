using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private InputController input = null;
    [SerializeField] private UnitStats unitStats = null;

    [SerializeField, Range(0f, 100f)] private float Acceleration = 35f;
    private Vector2 inputDirection;
    private Rigidbody2D rb2d;
    private Vector2 velocity;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        CharacterClass characterClass = GetComponent<CharacterClass>();
        unitStats = characterClass.GetUnitStats();
        input = characterClass.GetInputController();
    }

    private void Update()
    {
        inputDirection = input.GetMoveInput(gameObject);

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
        velocity = rb2d.velocity;
        velocity = Vector2.Lerp(velocity, inputDirection * unitStats.moveSpeed, Acceleration * Time.fixedDeltaTime);
        rb2d.velocity = velocity;

    }
}
