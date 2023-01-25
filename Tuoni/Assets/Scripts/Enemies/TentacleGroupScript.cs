// TODO: Do the battle

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleGroupScript : MonoBehaviour
{
    public float slamDelay;
    public float attackDelay;

    private GameObject tentacleL;
    private GameObject tentacleM;
    private GameObject tentacleR;
    private TentacleHitterMovementScript movementScriptL;
    private TentacleHitterMovementScript movementScriptM;
    private TentacleHitterMovementScript movementScriptR;
    private TentacleHitterScript hitterScriptL;
    private TentacleHitterScript hitterScriptM;
    private TentacleHitterScript hitterScriptR;

    private bool tentaclesRised = false;
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
            StartBattle();
        }
    }

    public void RaiseTentacles()
    {
        movementScriptL.RaiseTentacle();
        movementScriptM.RaiseTentacle();
        movementScriptR.RaiseTentacle();
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

    public void Attack1()
    {
        StartCoroutine(Timer(SlamTentacleL, slamDelay));
        StartCoroutine(Timer(SlamTentacleM, slamDelay));
        StartCoroutine(Timer(SlamTentacleR, slamDelay));
    }

    public void StartBattle()
    {
        RaiseTentacles();
        Attack1();
    }

    // TODO
    IEnumerator Timer(System.Action action, float time)
    {
        action();
        yield return new WaitForSeconds(time);
    }
}