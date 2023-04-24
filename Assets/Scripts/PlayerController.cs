using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;
*/

public class PlayerController : KinematicObject
{

    /// <summary>
    /// Max horizontal speed of the player.
    /// </summary>
    public float maxSpeed = 7;
    /// <summary>
    /// Initial jump velocity at the start of a jump.
    /// </summary>
    public float jumpTakeOffSpeed = 7;

    public JumpState jumpState = JumpState.Grounded;
    private bool stopJump;
    /*internal new*/
    public Collider2D collider2d;
    /*internal new*/
    public bool controlEnabled = true;

    bool jump;
    Vector2 move;
    SpriteRenderer spriteRenderer;
    //readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();

    public Bounds Bounds => collider2d.bounds;

    public float jumpModifier = 1.5f;
    public float jumpDeceleration = 0.5f;

    void Awake()
    {
        collider2d = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Update()
    {
        if (controlEnabled)
        {
            move.x = Input.GetAxis("Horizontal");
            if (jumpState == JumpState.Grounded && Input.GetButtonDown("Jump"))
                jumpState = JumpState.PrepareToJump;
            else if (Input.GetButtonUp("Jump"))
            {
                stopJump = true;
                //Schedule<PlayerStopJump>().player = this;
            }
        }
        else
        {
            move.x = 0;
        }
        UpdateJumpState();
        base.Update();
    }

    void UpdateJumpState()
    {
        jump = false;
        switch (jumpState)
        {
            case JumpState.PrepareToJump:
                jumpState = JumpState.Jumping;
                jump = true;
                stopJump = false;
                break;
            case JumpState.Jumping:
                if (!IsGrounded)
                {
                    //Schedule<PlayerJumped>().player = this;
                    jumpState = JumpState.InFlight;
                }
                break;
            case JumpState.InFlight:
                if (IsGrounded)
                {
                    //Schedule<PlayerLanded>().player = this;
                    jumpState = JumpState.Landed;
                }
                break;
            case JumpState.Landed:
                jumpState = JumpState.Grounded;
                break;
        }
    }

    protected override void ComputeVelocity()
    {
        if (jump && IsGrounded)
        {
            velocity.y = jumpTakeOffSpeed * jumpModifier;
            jump = false;
        }
        else if (stopJump)
        {
            stopJump = false;
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * jumpDeceleration;
            }
        }

        if (move.x > 0.01f)
            spriteRenderer.flipX = false;
        else if (move.x < -0.01f)
            spriteRenderer.flipX = true;

        targetVelocity = move * maxSpeed;
    }

    public enum JumpState
    {
        Grounded,
        PrepareToJump,
        Jumping,
        InFlight,
        Landed
    }
}
