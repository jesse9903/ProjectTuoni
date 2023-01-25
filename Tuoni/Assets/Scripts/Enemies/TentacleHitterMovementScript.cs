using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleHitterMovementScript : MonoBehaviour
{
    public float slamSpeed;
    public float slamTick;
    public double raisedHeight;
    public float raiseAmount;
    public float raiseSpeed;
    public float slamDelay;

    private float idleRotation = 0f;
    private float currentRotation = 0f;
    private float slamRotation = 90f;
    private Vector3 currentPosition;
    private TentacleHitterScript hitterScript;

    // Takes the starting position of the tentacle
    private void Start()
    {
        currentPosition = gameObject.transform.position;
        hitterScript = GetComponentInChildren<TentacleHitterScript>();
    }

    public void SlamTentacle()
    {
        if (!(currentRotation >= slamRotation))
        {
            gameObject.transform.Rotate(slamTick, 0, 0);
            currentRotation = Mathf.Round(gameObject.transform.rotation.eulerAngles.x);

            Invoke("SlamTentacle", slamSpeed);
        }
        else
        {
            // Sets tentacle hitters collider to not being a trigger
            hitterScript.SetColliderIsTriggerFalse();

            // Starts tentacle reset after delay
            Invoke("ResetTentacleRotation", slamDelay);
        }
    }

    public void RaiseTentacle()
    {
        if (!(currentPosition.y >= raisedHeight))
        {
            gameObject.transform.Translate(new Vector3(0, raiseAmount, 0));
            currentPosition = gameObject.transform.position;
            Invoke("RaiseTentacle", raiseSpeed);
        }
    }

    public void ResetTentacleRotation()
    {
        // Disables the collider
        hitterScript.SetColliderEnabledFalse();

        if (!(currentRotation <= idleRotation))
        {
            gameObject.transform.Rotate(-slamTick, 0, 0);
            currentRotation = Mathf.Round(gameObject.transform.rotation.eulerAngles.x);

            Invoke("ResetTentacleRotation", raiseSpeed);
        }
        else
        {
            // Resets the collider
            hitterScript.SetColliderIsTriggerFalse();
            hitterScript.SetColliderEnabledFalse();
        }
    }
}
