using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[CreateAssetMenu (fileName = "PlayerInputController", menuName = "InputController/PlayerController")]

public class PlayerInputController : InputController
{
    private PlayerInputActions inputActions;

    // FOR TESTING
    private InputActionReference takeDamage;
    private PlayerClass playerClass;
    private GameObject obj;

    private void OnEnable()
    {
        // Enable the input actions
        inputActions = new PlayerInputActions();
        inputActions.Enable();

        // FOR TESTING
        takeDamage = InputActionReference.Create(inputActions.PlayerDefault.TakeDamage);
        takeDamage.action.started += TakeDamage;
    }

    public override Vector2 GetMoveInput(GameObject gameObject)
    {
        return inputActions.PlayerDefault.Movement.ReadValue<Vector2>();
    }

    private void TakeDamage(InputAction.CallbackContext context)
    {
        obj = GameObject.Find("UnitTest");
        playerClass = obj.GetComponent<PlayerClass>();
        playerClass.TakeDamage(10, playerClass);
    }
}