using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    [SerializeField] private float jumpSpeed = 7f;
    [SerializeField] private float runSpeed = 7f;
    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f; // does this need to be initialized like this?

    // This enumerates all of our different animation states. Cast to int for the animator.
    private enum MovementState { Idle, Running, Jumping, Falling }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Makes the character move left to right. Using a float lets us support controllers
        // and avoids messy if statements. Consider Input.GetAxisRaw for more abrupt stops.
        dirX = Input.GetAxis("Horizontal");
        rigidBody.velocity = new Vector2(dirX * runSpeed, rigidBody.velocity.y);

        // Makes the character jump. Defaults to Space in unity.
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        // Checks for horizontal input and sets our movement state appropriately.
        if (dirX > 0f)
        {
            state = MovementState.Running;
            spriteRenderer.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.Running;
            spriteRenderer.flipX = true;
        }
        else
            state = MovementState.Idle;

        //Checks our y velocity and sets movement state. flipX is retained from above, but this overwrites
        //because it's higher priority. Checks against .1 instead of 0 to deal with inherent imprecision in engine.
        if (rigidBody.velocity.y > .1f)
        {
            state = MovementState.Jumping;
        }
        else if (rigidBody.velocity.y < -.1f)
        {
            state = MovementState.Falling;
        }

        //Cast state to Int to send to the animator.
        animator.SetInteger("State", (int)state);
    }

    private bool IsGrounded()
    {
        // Create a box around our player that is the same size as our normal box collider, but then
        // rotate it 0 degrees, and scooch it down a tiny bit. Check if that overlaps a layer.
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}