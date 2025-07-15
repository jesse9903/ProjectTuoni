using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[CreateAssetMenu (fileName = "PlayerInputController", menuName = "InputController/PlayerController")]

public class PlayerInputController : InputController
{
    private PlayerInputActions inputActions;

    public override Vector2 GetMoveInput(GameObject gameObject)
    {
        inputActions = new PlayerInputActions();

        return inputActions.PlayerDefault.Movement.ReadValue<Vector2>();
    }
}