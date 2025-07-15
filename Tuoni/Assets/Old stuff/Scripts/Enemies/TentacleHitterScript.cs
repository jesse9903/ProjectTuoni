using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class TentacleHitterScript : MonoBehaviour
{
    public double raisedHeight = 0.17;
    public float raiseAmount = 0.1f;
    public int slamDamage;
    public float destroyDelay;

    private TentacleHitterMovementScript rotator;
    private BoxCollider tentacleCollider;
    private Vector3 currentPosition;
    private GameObject player;
    private Animator animator;
    private BoxCollider boxCollider;

    private void Start()
    {
        currentPosition = gameObject.transform.position;
        rotator = GetComponentInParent<TentacleHitterMovementScript>();
        tentacleCollider = GetComponent<BoxCollider>();
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
    }

    public void RaiseTentacle()
    {
        rotator.RaiseTentacle();
    }

    public void SlamTentacle()
    {
        // Enables the trigger (it's disabled in TentacleHitterMovementScript)
        SetColliderEnabledTrue();
        SetColliderIsTriggerTrue();

        // Trigger attack animation
        animator.SetTrigger("Slam");

        // Slam tentacle
        rotator.SlamTentacle();
    }

    // Checks for player hit and deals damage accordingly
    private void OnTriggerEnter(Collider other)
    {
        // Checks if the collided object was the player
        if (other.gameObject.layer == player.layer)
        {

            // Calls the Squish()-method from ThirdPersonMovementScript
            player.GetComponent<ThirdPersonMovementScript>().Squish();
            
            // Deal damage to player
            player.GetComponentInChildren<HealthScript>().TakeDamage(slamDamage);
            
            // Disables collision from collider
            SetColliderEnabledFalse();
        }
    }

    public void TentacleDied()
    {
        // Triggers the Dead-animation
        animator.SetBool("Dead", true);

        // Destroys the object after delay
        Invoke("Destroy", destroyDelay);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void SetColliderEnabledTrue()
    {
        boxCollider.enabled = true;
    }

    public void SetColliderEnabledFalse()
    {
        boxCollider.enabled = false;
    }

    public void SetColliderIsTriggerTrue()
    {
        boxCollider.isTrigger = true;
    }

    public void SetColliderIsTriggerFalse()
    {
        boxCollider.isTrigger = false;
    }
}
