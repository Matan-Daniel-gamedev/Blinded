using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicObject : MonoBehaviour
{

    public float minGroundNormalY = .65f;

    public float gravityModifier = 1f;

    public Vector2 velocity;

    public bool IsGrounded { get; private set; }

    protected Vector2 targetVelocity;
    protected Vector2 groundNormal;
    protected Rigidbody2D body;
    protected ContactFilter2D contactFilter;
    protected static int buffSize = 16;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[buffSize];

    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.01f;

    protected virtual void OnEnable()
    {
        body = GetComponent<Rigidbody2D>();
        body.isKinematic = true;
    }

    protected virtual void OnDisable()
    {
        body.isKinematic = false;
    }

    protected virtual void Start()
    {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
    }

    protected virtual void Update()
    {
        targetVelocity = Vector2.zero;
        ComputeVelocity();
    }

    protected virtual void ComputeVelocity()
    {

    }

    protected virtual void FixedUpdate()
    {
        bool isFalling = velocity.y < 0;
        if (isFalling)
            velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        else
            velocity += Physics2D.gravity * Time.deltaTime;

        velocity.x = targetVelocity.x;

        IsGrounded = false;

        var deltaPosition = velocity * Time.deltaTime;

        var moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);

        var move = moveAlongGround * deltaPosition.x;

        PerformMovement(move, false);

        move = Vector2.up * deltaPosition.y;

        PerformMovement(move, true);

    }

    void PerformMovement(Vector2 move, bool yMovement)
    {
        var distance = move.magnitude;

        if (distance > minMoveDistance)
        {
            //check for hits
            var count = body.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
            for (var i = 0; i < count; i++)
            {
                var currentNormal = hitBuffer[i].normal;

                bool isFlat = currentNormal.y > minGroundNormalY;
                if (isFlat)
                {
                    IsGrounded = true;
                    // if moving up, change the groundNormal to new surface normal.
                    if (yMovement)
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }
                if (IsGrounded)
                {
                    //does velocity allign with wall
                    var projection = Vector2.Dot(velocity, currentNormal);
                    if (projection < 0)
                    {
                        //if moving up a wall-slower velocity
                        velocity = velocity - projection * currentNormal;
                    }
                }
                else
                {
                    //we are in the air, and hit something
                    velocity.x *= 0;
                    velocity.y = Mathf.Min(velocity.y, 0);
                }
                //remove shellDistance from actual move distance.
                var modifiedDistance = hitBuffer[i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }
        body.position = body.position + move.normalized * distance;
    }

}