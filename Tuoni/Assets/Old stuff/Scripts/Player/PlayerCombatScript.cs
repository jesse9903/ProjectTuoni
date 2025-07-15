using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatScript : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float lightAttackRange;
    public float mediumAttackRange;
    public float heavyAttackRange;
    public int lightAttackDamage;
    public int mediumAttackDamage;
    public int heavyAttackDamage;
    public float lightAttackRate;
    public float mediumAttackRate;
    public float heavyAttackRate;
    public bool isAttacking = false;

    private bool canAttack = true;
    private float nextAttackTime = 0f;
    private ThirdPersonMovementScript movementScript;

    private void Start()
    {
        // Finds the animator component from child at the start of the script
        animator = GetComponentInChildren<Animator>();
        movementScript = GetComponentInChildren<ThirdPersonMovementScript>();
    }

    void Update()
    {
        // Delays the attack
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                switch (movementScript.weightClass)
                {
                    case ThirdPersonMovementScript.WeightClass.Light:
                        LightAttack();
                        nextAttackTime = Time.time + 1f / lightAttackRate;
                        break;

                    case ThirdPersonMovementScript.WeightClass.Medium:
                        MediumAttack();
                        nextAttackTime = Time.time + 1f / mediumAttackRate;
                        break;

                    case ThirdPersonMovementScript.WeightClass.Heavy:
                        HeavyAttack();
                        nextAttackTime = Time.time + 1f / heavyAttackRate;
                        break;
                }
                isAttacking = true;
            }
        }
    }

    void LightAttack()
    {
        if (canAttack)
        {
            // Sets the animation trigger to attack
            animator.SetTrigger("Attack");

            // Detects if the attack has overlapped with enemies
            Collider[] enemiesHit = Physics.OverlapSphere(attackPoint.position, lightAttackRange, enemyLayers);

            // Goes through the list of enemies that were hit, and deals damage to them
            foreach (Collider enemy in enemiesHit)
            {
                enemy.GetComponent<HealthScript>().TakeDamage(lightAttackDamage);
            }
        }
    }

    void MediumAttack()
    {
        if (canAttack)
        {
            // Sets the animation trigger to attack
            animator.SetTrigger("Attack");

            // Detects if the attack has overlapped with enemies
            Collider[] enemiesHit = Physics.OverlapSphere(attackPoint.position, mediumAttackRange, enemyLayers);

            // Goes through the list of enemies that were hit, and deals damage to them
            foreach (Collider enemy in enemiesHit)
            {
                enemy.GetComponent<HealthScript>().TakeDamage(mediumAttackDamage);
            }
        }
    }
    void HeavyAttack()
    {
        if (canAttack)
        {
            // Sets the animation trigger to attack
            animator.SetTrigger("Attack");

            // Detects if the attack has overlapped with enemies
            Collider[] enemiesHit = Physics.OverlapSphere(attackPoint.position, heavyAttackRange, enemyLayers);

            // Goes through the list of enemies that were hit, and deals damage to them
            foreach (Collider enemy in enemiesHit)
            {
                enemy.GetComponent<HealthScript>().TakeDamage(heavyAttackDamage);
            }
        }
    }
    public void EndAttack()
    {
        isAttacking = false;
    }

    // Debug
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, lightAttackRange);
    }

    public void SetCanAttackTrue()
    {
        canAttack = true;
    }
    public void SetCanAttackFalse()
    {
        canAttack = false;
    }
}
