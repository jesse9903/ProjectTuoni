using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventScript : MonoBehaviour
{
    public PlayerCombatScript playerCombatScript;
    // Start is called before the first frame update
    void Start()
    {
        playerCombatScript = gameObject.GetComponentInParent<PlayerCombatScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallEndAttack()
    {
        playerCombatScript.EndAttack();
    }
}
