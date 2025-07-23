using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClass : CharacterClass
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    new void Update()
    {

    }

    public override void TakeDamage(int dmg, Object dmgDealtBy)
    {
        base.TakeDamage(dmg, dmgDealtBy);

        // FOR TESTING
        Debug.Log($"{dmg} damage dealt by {dmgDealtBy}!");
    }
}