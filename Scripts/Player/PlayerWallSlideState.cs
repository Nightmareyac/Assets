using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player _player, PlayerStatemachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        { 
            stateMachine.ChangeState(player.walljump);
            return;
        
        }

        if (player.facingDir!=xInput && xInput !=0)
            stateMachine.ChangeState(player.idleState);

        if (yInput<0)
            rb.velocity = new Vector2 (0, rb.velocity.y);
        else
            rb.velocity = new Vector2(0,rb.velocity.y * .7f);


        if(player.isGroundDetected())
            stateMachine.ChangeState(player.idleState);

    }

   
}
