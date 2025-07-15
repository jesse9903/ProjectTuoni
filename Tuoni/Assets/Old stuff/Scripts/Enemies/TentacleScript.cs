using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleScript : MonoBehaviour
{
    private Animator animator;
    private TentacleHitterScript tentacleHitter;

    private void Start()
    {
        animator = GetComponent<Animator>();
        tentacleHitter = GameObject.FindGameObjectWithTag("TentacleHitter").GetComponent<TentacleHitterScript>();
    }
    private void Update()
    {
        if (animator.GetBool("Dead"))
        {
            Disable();
            tentacleHitter.TentacleDied();
        }
    }

    private void Disable()
    {
        // Disables collision
        GetComponent<BoxCollider>().enabled = false;

        // Disable all other scripts here
        GetComponent<HealthScript>().enabled = false;

        // This will disable this script, EXECUTE AFTER OTHERS!
        this.enabled = false;
    }
}