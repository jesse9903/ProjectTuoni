using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatScript : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public LayerMask playerLayer;
    public float attackRange = 0.5f;
    public int attackDamage = 40;
    public float attackRate = 2f;
    public bool isAttacking = false;

    private bool canAttack = true;

    private void Start()
    {
        // Finds the animator component from child at the start of the script
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        
    }

    void Attack(Collider collider)
    {
        if (canAttack)
        {
            // Sets the animation trigger to attack
            animator.SetTrigger("Attack");

            // Detects if the attack has overlapped with enemies
            
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

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
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
