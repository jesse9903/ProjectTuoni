using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public abstract class InputController : ScriptableObject
{

public abstract Vector2 GetMoveInput(GameObject gameObject);

}
