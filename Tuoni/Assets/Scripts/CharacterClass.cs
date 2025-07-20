using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterClass : UnitClass
{

    [SerializeField] private InputController inputController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public InputController InputController
    {
        get { return inputController; }
        set { inputController = value; }
    }
}
