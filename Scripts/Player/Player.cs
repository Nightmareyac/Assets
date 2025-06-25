using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [Header("Attack details")]
    public Vector2[] attackMovment;
    public float counterAttackDuration=.2f;
  


    public bool isBusy { get; private set; }
    [Header("Move info")]
    public float moveSpeed = 8f;
    public float jumpForce;

    [Header("Dash info")]
    public float dashSpeed;
    public float dashDuration;
    public float dashDir { get; private set; }


    #region State
    public PlayerStatemachine stateMachine { get; private set; }//only read
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }

    public PlayerWallSlideState wallSlide { get; private set; }

    public PlayerWallJumpState walljump { get; private set; }

    public PlayerDashState dashState { get; private set; }


    public PlayerPrimeAttackState primaryAttack { get; private set; }

    public PlayerCounterAttackState counterAttack { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new PlayerStatemachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");//´´½¨×´Ì¬
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlide = new PlayerWallSlideState(this, stateMachine, "wallSlide");
        walljump = new PlayerWallJumpState(this, stateMachine, "Jump");
        primaryAttack = new PlayerPrimeAttackState(this, stateMachine, "Attack");
        counterAttack = new PlayerCounterAttackState(this,stateMachine,"CounterAttack");
    }

    protected override void Start()
    {
       base.Start();

        stateMachine.Initialize(idleState);

    }



    protected override void Update()
    {
        base.Update();

        stateMachine.currentState.Update();

        CheckforDashInput();
    }

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    private void CheckforDashInput()
    {
        if (IsWallDetected())
            return;



        if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.instance.dash.CanUseSkill())
        {

            dashDir = Input.GetAxisRaw("Horizontal");

            if (dashDir == 0)
                dashDir = facingDir;

            stateMachine.ChangeState(dashState);
        }


    }

    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;

        yield return new WaitForSeconds(_seconds);

        isBusy = false;
    }

    public void ZeroVelocity() => rb.velocity = new Vector2(0, 0);




}
