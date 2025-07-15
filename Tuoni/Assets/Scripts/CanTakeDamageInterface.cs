using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CanTakeDamageInterface
{
    abstract void TakeDamage(int dmg, Object dmgDealtBy);
    abstract void CalculateHealth(int dmg);
    abstract void OnDeath();
}