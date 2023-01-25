// TODO: Do the battle

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TentacleGroupScript : MonoBehaviour
{
    public float slamDelay;
    public float attackDelay;
    public float timeBetweenSlams;
    public float timeBetweenAttacks;
    public float tentacleRaiseTime;

    private GameObject tentacleL;
    private GameObject tentacleM;
    private GameObject tentacleR;
    private TentacleHitterMovementScript movementScriptL;
    private TentacleHitterMovementScript movementScriptM;
    private TentacleHitterMovementScript movementScriptR;
    private TentacleHitterScript hitterScriptL;
    private TentacleHitterScript hitterScriptM;
    private TentacleHitterScript hitterScriptR;

    private bool tentaclesIdle = true;
    private bool tentaclesRaised = false;
    private bool tentaclesRised = false;
    private int numberOfAttackPatterns = 3;
    private void Start()
    {
        tentacleL = GameObject.FindGameObjectWithTag("TentacleL");
        tentacleM = GameObject.FindGameObjectWithTag("TentacleM");
        tentacleR = GameObject.FindGameObjectWithTag("TentacleR");

        movementScriptL = tentacleL.GetComponentInChildren<TentacleHitterMovementScript>();
        movementScriptM = tentacleM.GetComponentInChildren<TentacleHitterMovementScript>();
        movementScriptR = tentacleR.GetComponentInChildren<TentacleHitterMovementScript>();

        hitterScriptL = tentacleL.GetComponentInChildren<TentacleHitterScript>();
        hitterScriptM = tentacleM.GetComponentInChildren<TentacleHitterScript>();
        hitterScriptR = tentacleR.GetComponentInChildren<TentacleHitterScript>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine("StartBattle");
        }
    }

    public void SetTentaclesRaisedTrue()
    {
        tentaclesRaised = true;
    }

    public void SetTentaclesRaisedFalse()
    {
        tentaclesRaised = false;
    }

    public void RaiseTentacles()
    {
        movementScriptL.RaiseTentacle();
        movementScriptM.RaiseTentacle();
        movementScriptR.RaiseTentacle();

        Invoke("SetTentaclesRaisedTrue", 3);
    }

    public void SlamTentacleL()
    {
        hitterScriptL.SlamTentacle();
    }

    public void SlamTentacleM()
    {
        hitterScriptM.SlamTentacle();
    }
    public void SlamTentacleR()
    {
        hitterScriptR.SlamTentacle();
    }

    public IEnumerator StartBattle()
    {
        if (!tentaclesRaised)
        {
            RaiseTentacles();
        }

        StartCoroutine("Attack");
        yield return new WaitForSeconds(tentacleRaiseTime);
        StartCoroutine("StartBattle");
    }

    // TODO: Make it so when a tentacle is destroyed, it cant attack anymore (or doesnt crash the game)
     public IEnumerator Attack()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);

        System.Random random = new System.Random();
        int number = random.Next(1, numberOfAttackPatterns + 1);

        switch (number)
        {
            case 1:
                SlamTentacleL();
                break;

            case 2:
                SlamTentacleM();
                break;

            case 3:
                SlamTentacleR();
                break;

            default:
                break;
        }
    }
}