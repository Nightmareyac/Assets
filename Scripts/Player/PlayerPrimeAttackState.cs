using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimeAttackState : PlayerState
{
    private int comboCounter;

    private float lastTimerAttacked;
    private float comboWindow = 2;

    public PlayerPrimeAttackState(Player _player, PlayerStatemachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if(comboCounter > 2 || Time.time >= lastTimerAttacked + comboWindow)
            comboCounter = 0;


        player.anim.SetInteger("ComboCounter", comboCounter);
        player.anim.speed = 1.2f;

        #region Choose attack direction

        float attackDir = player.facingDir;
        if (xInput != 0)
        {
            attackDir = xInput;
        }

        #endregion

        player.SetVelocity(player.attackMovment[comboCounter].x * attackDir, player.attackMovment[comboCounter].y);

        stateTimer = .1f;


    }

    public override void Exit()
    {
        base.Exit();
        player.StartCoroutine("BusyFor", .15f);
        player.anim.speed = 1;

        comboCounter++;
        lastTimerAttacked = Time.time;  
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            player.ZeroVelocity();

        if (triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
