using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatScript : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float attackRange = 0.5f;
    public int attackDamage = 40;
    public float attackRate = 2f;
    private bool canAttack = true;

    private float nextAttackTime = 0f;

    private void Start()
    {
        // Finds the animator component from child at the start of the script
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // Delays the attack
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack();
                nextAttackTime= Time.time + 1f / attackRate;
            }
        }
    }

    void Attack()
    {
        if (canAttack)
        {
            // Sets the animation trigger to attack
            animator.SetTrigger("Attack");

            // Detects if the attack has overlapped with enemies
            Collider[] enemiesHit = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

            // Goes through the list of enemies that were hit, and deals damage to them
            foreach (Collider enemy in enemiesHit)
            {
                enemy.GetComponent<EnemyHealthScript>().TakeDamage(attackDamage);
            }
        }
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
    
    }public void SetCanAttackFalse()
    {
        canAttack = false;
    }
}
