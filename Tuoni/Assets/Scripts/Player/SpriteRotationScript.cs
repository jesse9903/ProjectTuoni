using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRotationScript : MonoBehaviour
{
    public void RotateToEast()
    {
        gameObject.transform.eulerAngles = new Vector3(-45, 180, 0);
    }

    public void RotateToWest()
    {
        gameObject.transform.eulerAngles = new Vector3(45, 0, 0);
    }
}
