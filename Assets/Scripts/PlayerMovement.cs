using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private LayerMask jumpableGround;
    
    private Rigidbody2D _rigidBody;
    private SpriteAnimator _animator;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider;
    
    // This enumerates all of our different animation states
    private enum MovementState { Idle, Running, Jumping, Falling }
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<SpriteAnimator>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }
    
    // Update is called once per frame
    void Update()
    {
        // The "Horizontal" input axis remaps the keyboard inputs of left and right to a floating point value from -1f to 1f
        // An axis result of 0f means no input
        float dirX = Input.GetAxis("Horizontal");
        _rigidBody.velocity = new Vector2(dirX * runSpeed, _rigidBody.velocity.y);
        
        // Makes the character jump. Defaults to Space in unity.
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, jumpSpeed);
        }
        
        UpdateAnimationState(dirX);
    }
    
    private void UpdateAnimationState(float dirX)
    {
        // By default, our character is idle
        MovementState state = MovementState.Idle;
        
        // Checks for horizontal input and sets our movement state appropriately. Checks against .1 and -.1 to avoid animating at the very end of deceleration
        if (dirX > 0)
        {
            state = MovementState.Running;
            _spriteRenderer.flipX = false;
        }
        else if (dirX < 0)
        {
            state = MovementState.Running;
            _spriteRenderer.flipX = true;
        }
        
        // Checks our y velocity and sets movement state. flipX is retained from above, but this overwrites
        // because it's higher priority. Checks against .001 instead of 0 to deal with inherent imprecision in engine.
        if (_rigidBody.velocity.y > .001f)
        {
            state = MovementState.Jumping;
        }
        else if (_rigidBody.velocity.y < -.001f)
        {
            state = MovementState.Falling;
        }
        
        // Play the appropriate animation (by string lookup)
        var animName = state switch
        {
            MovementState.Idle => "Idle",
            MovementState.Running => "Run",
            MovementState.Jumping => "Jump",
            MovementState.Falling => "Fall"
        };
        _animator.PlayAnimation(animName);
    }

    private bool IsGrounded()
    {
        // Create a box around our player that is the same size as our normal box collider, but then
        // rotate it 0 degrees, and scooch it down a tiny bit. Check if that overlaps a layer.
        return Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}