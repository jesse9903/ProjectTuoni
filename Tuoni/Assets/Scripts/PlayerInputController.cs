using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[CreateAssetMenu (fileName = "PlayerInputController", menuName = "InputController/PlayerController")]

public class PlayerInputController : InputController
{
    private PlayerInputActions inputActions;

    private void OnEnable()
    {
        // Enable the input actions
        inputActions = new PlayerInputActions();
        inputActions.Enable();
    }

    public override Vector2 GetMoveInput(GameObject gameObject)
    {
        return inputActions.PlayerDefault.Movement.ReadValue<Vector2>();
    }
}