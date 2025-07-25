using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass : CharacterClass
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();
    }

    protected override int CalculateDealtDamage()
    {
        int damage = (int)(AttackDamage * AttackDamageMultiplier);
        bool isCrit = CheckAttackCrit();

        if (isCrit)
        {
            damage = (int)(damage * CriticalHitMultiplier);
        }

        return damage;
    }

    private bool CheckAttackCrit()
    {
        bool isCrit = false;
        int rand;

        rand = (int)(Random.value * 100);

        if (rand < CriticalHitChance)
        {
            isCrit = true;
        }
        else
        {
            isCrit = false;
        }

        return isCrit;
    }
}
